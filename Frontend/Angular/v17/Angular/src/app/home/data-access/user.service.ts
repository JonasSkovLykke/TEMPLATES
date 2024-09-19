import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';

import { environment } from '../../../environments/environment';
import { User } from './models/user.model';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  // Initialize the HttpClient using Angular's dependency injection mechanism
  private _httpClient: HttpClient = inject(HttpClient);

  /**
   * Retrieves the currently authenticated user's information.
   * @returns An Observable emitting the User object.
   */
  public me(): Observable<User> {
    return new Observable<User>((observer) => {
      // Send a GET request to the getMe endpoint to retrieve user information
      this._httpClient.get<User>(environment.apiEndpoints.getMe).subscribe({
        next: (user: User) => {
          // If the user information is successfully retrieved, emit it and complete the observable
          observer.next(user);
          observer.complete();
        },
        error: (error) => {
          // If an error occurs during the request, log the error and complete the observable
          console.error(`An error occurred within ${UserService.name}/${this.me.name}, with message: ${error}`);

          observer.next(); // Emit undefined to indicate an error
          observer.complete(); // Complete the observable
        }
      });
    });
  }
}
