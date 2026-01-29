import { Developers } from './features/developers/developers';
import { Dashboard } from './features/dashboard/dashboard';
import { Projects } from './features/projects/projects';
import { Task } from './features/task/task';
import { Routes } from '@angular/router';

export const routes: Routes = [
    { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
    { path: 'developers', component: Developers },
    { path: 'projects', component: Projects },
    { path: 'dashboard', component: Dashboard },
    { path: 'projects/:id/tasks', component: Task },
];