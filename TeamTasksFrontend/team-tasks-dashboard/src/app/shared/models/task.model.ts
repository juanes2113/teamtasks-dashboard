export interface TaskDto {
    taskid: number;
    projectid: number;
    title: string;
    description: string;
    assigneeid: number;
    status: TasksStatus;
    priority: TaskPriority;
    estimatedcomplexity: number;
    duedate: string;
    completiondate: string;
    createdat: string;
}

export enum TasksStatus {
    ToDo = 1,
    InProgress = 2,
    Blocked = 3,
    Completed = 4
}

export enum TaskPriority {
    Low = 1,
    Medium = 2,
    High = 3
}

export interface CreateTaskDto {
    projectid: number;
    title: string;
    description: string;
    assigneeid: number;
    status: TasksStatus;
    priority: TaskPriority;
    estimatedcomplexity: number;
    duedate: string;
}
