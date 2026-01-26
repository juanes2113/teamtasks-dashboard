-- Create the database

CREATE DATABASE "TeamTasks";

-- Create enums

CREATE TYPE project_status AS ENUM ('Planned', 'InProgress', 'Completed');
CREATE TYPE task_status AS ENUM ('ToDo', 'InProgress', 'Blocked', 'Completed');
CREATE TYPE task_priority AS ENUM ('Low', 'Medium', 'High');

-- Create tables

CREATE TABLE Developers (
    DeveloperId SERIAL PRIMARY KEY,
    FirstName VARCHAR(100) NOT NULL,
    LastName VARCHAR(100) NOT NULL,
    Email VARCHAR(150) NOT NULL UNIQUE,
    IsActive BOOLEAN NOT NULL DEFAULT TRUE,
    CreatedAt TIMESTAMP NOT NULL DEFAULT NOW()
);

CREATE TABLE Projects (
    ProjectId SERIAL PRIMARY KEY,
    Name VARCHAR(150) NOT NULL,
    ClientName VARCHAR(150) NOT NULL,
    StartDate DATE NOT NULL,
    EndDate DATE,
    Status project_status NOT NULL
);


CREATE TABLE Tasks (
    TaskId SERIAL PRIMARY KEY,
    ProjectId INT NOT NULL,
    Title VARCHAR(200) NOT NULL,
    Description TEXT,
    AssigneeId INT NOT NULL,
    Status task_status NOT NULL,
    Priority task_priority NOT NULL,
    EstimatedComplexity INT CHECK (EstimatedComplexity BETWEEN 1 AND 5),
    DueDate DATE NOT NULL,
    CompletionDate DATE,
    CreatedAt TIMESTAMP NOT NULL DEFAULT NOW(),

    CONSTRAINT fk_task_project FOREIGN KEY (ProjectId) REFERENCES Projects(ProjectId),
    CONSTRAINT fk_task_assignee FOREIGN KEY (AssigneeId) REFERENCES Developers(DeveloperId)
);

-- Enter minimum data

INSERT INTO Developers (FirstName, LastName, Email)
VALUES
('Juan', 'Perez', 'juan@company.com'),
('Ana', 'Gomez', 'ana@company.com'),
('Carlos', 'Ruiz', 'carlos@company.com'),
('Laura', 'Martinez', 'laura@company.com'),
('Sofia', 'Lopez', 'sofia@company.com');

INSERT INTO Projects (Name, ClientName, StartDate, EndDate, Status)
VALUES
('Website Redesign', 'ACME Corp', '2025-01-01', '2025-03-31', 'InProgress'),
('Mobile App', 'Globex', '2025-02-01', '2025-05-30', 'Planned'),
('Internal Tool', 'InHouse', '2024-12-01', '2025-02-28', 'Completed');

INSERT INTO Tasks
(ProjectId, Title, Description, AssigneeId, Status, Priority, EstimatedComplexity, DueDate, CompletionDate)
SELECT
    (random()*2 + 1)::int,
    'Task #' || gs,
    'Sample task description',
    (random()*4 + 1)::int,
    (ARRAY['ToDo','InProgress','Blocked','Completed'])[floor(random()*4)+1]::task_status,
    (ARRAY['Low','Medium','High'])[floor(random()*3)+1]::task_priority,
    (random()*4 + 1)::int,
    CURRENT_DATE + (random()*15)::int,
    CASE WHEN random() > 0.5 THEN CURRENT_DATE ELSE NULL END
FROM generate_series(1,20) gs;

-- Required queries (DML)

-- Developer workload summary

SELECT
    d.FirstName || ' ' || d.LastName AS DeveloperName,
    COUNT(t.TaskId) FILTER (WHERE t.Status <> 'Completed') AS OpenTasksCount,
    AVG(t.EstimatedComplexity) FILTER (WHERE t.Status <> 'Completed') AS AverageEstimatedComplexity
FROM Developers d
LEFT JOIN Tasks t ON d.DeveloperId = t.AssigneeId
WHERE d.IsActive = TRUE
GROUP BY d.DeveloperId;

-- Status by project

SELECT
    p.Name AS ProjectName,
    COUNT(t.TaskId) AS TotalTasks,
    COUNT(t.TaskId) FILTER (WHERE t.Status <> 'Completed') AS OpenTasks,
    COUNT(t.TaskId) FILTER (WHERE t.Status = 'Completed') AS CompletedTasks
FROM Projects p
LEFT JOIN Tasks t ON p.ProjectId = t.ProjectId
GROUP BY p.ProjectId;

-- Upcoming tasks due

SELECT *
FROM Tasks
WHERE Status <> 'Completed'
AND DueDate BETWEEN CURRENT_DATE AND CURRENT_DATE + INTERVAL '7 days';

-- Insert new task

CREATE OR REPLACE FUNCTION InsertTask(
    p_projectId INT,
    p_title VARCHAR,
    p_description TEXT,
    p_assigneeId INT,
    p_status task_status,
    p_priority task_priority,
    p_complexity INT,
    p_dueDate DATE
)
RETURNS VOID AS $$
BEGIN
    IF NOT EXISTS (SELECT 1 FROM Projects WHERE ProjectId = p_projectId) THEN
        RAISE EXCEPTION 'ProjectId % does not exist', p_projectId;
    END IF;

    IF NOT EXISTS (SELECT 1 FROM Developers WHERE DeveloperId = p_assigneeId) THEN
        RAISE EXCEPTION 'AssigneeId % does not exist', p_assigneeId;
    END IF;

    INSERT INTO Tasks
    (ProjectId, Title, Description, AssigneeId, Status, Priority, EstimatedComplexity, DueDate)
    VALUES
    (p_projectId, p_title, p_description, p_assigneeId, p_status, p_priority, p_complexity, p_dueDate);
END;
$$ LANGUAGE plpgsql;

-- Developer Delay Risk Prediction

SELECT
    d.FirstName || ' ' || d.LastName AS DeveloperName,
    COUNT(t.TaskId) FILTER (WHERE t.Status <> 'Completed') AS OpenTasksCount,

    COALESCE(
        AVG(GREATEST((CompletionDate - DueDate), 0))
        FILTER (WHERE t.Status = 'Completed'),
        0
    ) AS AvgDelayDays,

    MIN(t.DueDate) FILTER (WHERE t.Status <> 'Completed') AS NearestDueDate,
    MAX(t.DueDate) FILTER (WHERE t.Status <> 'Completed') AS LatestDueDate,

    MAX(t.DueDate) FILTER (WHERE t.Status <> 'Completed')
        + COALESCE(
            AVG(GREATEST((CompletionDate - DueDate), 0))
            FILTER (WHERE t.Status = 'Completed'),
            0
        ) * INTERVAL '1 day'
        AS PredictedCompletionDate,

    CASE
        WHEN
            (
                MAX(t.DueDate) FILTER (WHERE t.Status <> 'Completed')
                + COALESCE(
                    AVG(GREATEST((CompletionDate - DueDate), 0))
                    FILTER (WHERE t.Status = 'Completed'),
                    0
                ) * INTERVAL '1 day'
            ) >
            MAX(t.DueDate) FILTER (WHERE t.Status <> 'Completed')
        THEN 1
        ELSE 0
    END AS HighRiskFlag
FROM Developers d
LEFT JOIN Tasks t ON d.DeveloperId = t.AssigneeId
WHERE d.IsActive = TRUE
GROUP BY d.DeveloperId;

