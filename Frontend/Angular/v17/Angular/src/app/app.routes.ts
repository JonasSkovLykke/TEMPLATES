import { Routes } from '@angular/router';

import { authenticationGuard } from './shared/utils/authentication/authentication.guard';
import { AuthenticationRoutes } from './authentication/utils/authentication-routes';
import { HomeRoutes } from './home/utils/home-routes';

export const routes: Routes = [
    {
        path: '', redirectTo: 'authentication/login', pathMatch: 'full'
    },
    {
        path: 'authentication',
        children: AuthenticationRoutes
    },
    {
        path: 'home',
        children: HomeRoutes,
        canActivate: [authenticationGuard]
    }
];
