import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { Authentication } from './models/authentication.model';
import { environment } from '../../../environments/environment';
import { AuthenticationToken } from './models/authentication-token.model';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  // Initialize the HttpClient using Angular's dependency injection mechanism
  private _httpClient: HttpClient = inject(HttpClient);

  // Getter method to retrieve the authentication token from local storage
  public get authenticationToken(): string {
    return localStorage.getItem('authenticationToken')!;
  }

  // Private method to set the authentication token in local storage
  private setAuthenticationToken(authenticationToken: AuthenticationToken): void {
    const authenticationTokenString = JSON.stringify(authenticationToken);
    localStorage.setItem('authenticationToken', authenticationTokenString);
  }

  // Method to perform user login
  public login(authentication: Authentication): Observable<boolean> {
    return new Observable<boolean>((observer) => {
      // Send a POST request to the login endpoint
      this._httpClient.post<AuthenticationToken>(environment.apiEndpoints.login, authentication).subscribe({
        next: (authenticationToken: AuthenticationToken) => {
          // If login is successful, set the authentication token
          this.setAuthenticationToken(authenticationToken);
          // Notify observers that login was successful
          observer.next(true);
          observer.complete();
        },
        error: (error) => {
          // Log and handle errors that occur during login
          console.error(`An error occurred within ${AuthenticationService.name}/${this.login.name}, with message: ${error}`);
          // Notify observers that login failed
          observer.next(false);
          observer.complete();
        }
      });
    });
  }

  // Method to refresh the authentication token
  public refresh(refreshToken: string): Observable<boolean> {
    return new Observable<boolean>((observer) => {
      // Send a POST request to the refresh token endpoint
      this._httpClient.post<AuthenticationToken>(environment.apiEndpoints.refresh, { refreshToken: refreshToken }).subscribe({
        next: (authenticationToken: AuthenticationToken) => {
          // If token refresh is successful, set the new authentication token
          this.setAuthenticationToken(authenticationToken);
          // Notify observers that token refresh was successful
          observer.next(true);
          observer.complete();
        },
        error: (error) => {
          // Log and handle errors that occur during token refresh
          console.error(`An error occurred within ${AuthenticationService.name}/${this.refresh.name}, with message: ${error}`);
          // Notify observers that token refresh failed
          observer.next(false);
          observer.complete();
        }
      });
    });
  }

  // Method to log out the user by clearing the authentication token from local storage
  public logOut(): void {
    localStorage.clear();
  }
}
