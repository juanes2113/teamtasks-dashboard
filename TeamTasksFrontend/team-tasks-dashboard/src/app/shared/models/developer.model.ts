export interface Developer {
    developerid: number;
    firstname: string;
    lastname: string;
    email: string;
    isactive: boolean;
    createdat: Date;
    projectTasks: any[];
}

export interface CreateDeveloperDto {
    firstname: string;
    lastname: string;
    email: string;
    isactive: boolean;
}

export interface UpdateDeveloperDto {
    developerid: number;
    firstname: string;
    lastname: string;
    email: string;
    isactive: boolean;
}

export interface DeveloperWorkload {
    developerId: number;
    developerName: string;
    openTasksCount: number;
    averageEstimatedComplexity: number;
}

export interface DeveloperRisk {
    developerId: number;
    developerName: string;
    openTasksCount: number;
    avgDelayDays: number;
    nearestDueDate: Date;
    latestDueDate: Date;
    predictedCompletionDate: Date;
    highRiskFlag: boolean;
}


