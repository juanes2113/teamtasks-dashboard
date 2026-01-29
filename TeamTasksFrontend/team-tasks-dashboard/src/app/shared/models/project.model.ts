export interface ProjectSummary {
    projectId: number;
    name: string;
    clientName: string;
    status: 'Planned' | 'InProgress' | 'Completed' | string;
    totalTasks: number;
    openTasks: number;
    completedTasks: number;
}

export interface CreateProjectDto {
    name: string;
    clientname: string;
    startdate: Date;
    enddate: Date;
}
