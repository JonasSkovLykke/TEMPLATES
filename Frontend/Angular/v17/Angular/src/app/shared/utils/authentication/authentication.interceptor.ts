import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { catchError, switchMap, throwError } from 'rxjs';

import { AuthenticationService } from '../../../authentication/data-access/authentication.service';

// Define an HTTP interceptor function.
export const authenticationInterceptor: HttpInterceptorFn = (req, next) => {
  // Inject the AuthenticationService to get access to authentication data.
  const authenticationService: AuthenticationService = inject(AuthenticationService);

  // Get the authentication token string from the authentication service.
  const authenticationTokenString = authenticationService.authenticationToken;

  // If the authentication token string is null or undefined, handle this case.
  if (!authenticationTokenString) {
    // Handle the case where authenticationTokenString is null or undefined.
    // For example, you might want to handle unauthorized requests here
    // or redirect the user to the login page.
    // For now, let's return the request without setting the Authorization header.
    return next(req);
  }

  // Parse the authentication token string into an AuthenticationToken object.
  const authenticationToken = JSON.parse(authenticationTokenString);

  // Clone the request and set the Authorization header with the token.
  const clonedRequest = req.clone({
    setHeaders: { Authorization: `${authenticationToken.tokenType} ${authenticationToken.accessToken}` }
  });

  // Proceed with the cloned request.
  return next(clonedRequest).pipe(
    catchError((error: HttpErrorResponse) => {
      if (error.status === 401) {
        // If the response status is 401 (Unauthorized), refresh the token.
        return authenticationService.refresh(authenticationToken.refreshToken).pipe(
          switchMap((success: boolean) => {
            if (success) {
              // If token refresh is successful, retry the original request.
              return next(clonedRequest);
            } else {
              // If token refresh fails, handle the error accordingly.
              // For example, you might want to redirect the user to the login page.
              // For now, let's just pass the error downstream.
              return throwError(() => error); // Pass error as a factory function
            }
          }),
          catchError((refreshError: any) => {
            // Handle errors that occur during token refresh.
            // For example, you might want to log the error or perform other actions.
            console.error('Error occurred during token refresh:', refreshError);
            // Return a new observable to propagate the error downstream.
            return throwError(() => error); // Pass error as a factory function
          })
        );
      }

      // For other errors, pass the error downstream.
      return throwError(() => error); // Pass error as a factory function
    })
  );
};
