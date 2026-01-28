import { Developers } from './features/developers/developers';
import { Projects } from './features/projects/projects';
import { Dashboard } from './features/dashboard/dashboard';
import { Routes } from '@angular/router';

export const routes: Routes = [
    { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
    { path: 'developers', component: Developers },
    { path: 'projects', component: Projects },
    { path: 'dashboard', component: Dashboard }
];
