import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { catchError, throwError } from 'rxjs';

import { environment } from '../../../../environments/environment';

// Define an HTTP interceptor function to add API key to outgoing requests
export const apiKeyInterceptor: HttpInterceptorFn = (req, next) => {
  // Get API key name and value from environment variables
  const apiKeyName = environment.apiKeyName;
  const apiKey = environment.apiKey;

  // If API key is not provided in environment, proceed with the original request
  if (!apiKey) {
    return next(req);
  }

  // Clone the request and add API key to the headers
  const clonedRequest = req.clone({
    setHeaders: { [apiKeyName]: apiKey } // Use square brackets to use the variable apiKeyName as a key
  });

  // Proceed with the cloned request and handle any errors that may occur
  return next(clonedRequest).pipe(
    catchError((error: HttpErrorResponse) => {
      // Log the error to the console
      console.error('Failed to add API key to request:', error);

      // Handle errors by re-throwing the error to be caught by the calling code
      return throwError(() => error);
    })
  );
};
