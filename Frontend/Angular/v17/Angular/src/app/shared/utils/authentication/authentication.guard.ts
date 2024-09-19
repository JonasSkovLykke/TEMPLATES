import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';

import { AuthenticationService } from '../../../authentication/data-access/authentication.service';

// Define a guard function to check authentication status.
export const authenticationGuard: CanActivateFn = (route, state) => {
  // Inject the Router to enable navigation.
  const router = inject(Router);
  // Inject the AuthenticationService to access authentication data.
  const authenticationService: AuthenticationService = inject(AuthenticationService);

  // Get the authentication token string from the authentication service.
  const authenticationTokenString = authenticationService.authenticationToken;

  // Check if there is an authentication token.
  if (authenticationTokenString) {
    // If there is a authentication token, allow navigation.
    return true;
  } else {
    // If there is an authentication token, redirect to the login page.
    router.navigateByUrl('/authentication/login');

    // Deny navigation.
    return false;
  }
};
