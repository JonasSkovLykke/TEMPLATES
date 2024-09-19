import { Routes } from '@angular/router';

import { AuthenticationLoginComponent } from '../feature/authentication-login/authentication-login.component';

export const AuthenticationRoutes: Routes = [
    {
        path: 'login', component: AuthenticationLoginComponent
    }
];
