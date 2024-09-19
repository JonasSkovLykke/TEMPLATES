import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

import { Authentication } from '../../data-access/models/authentication.model';
import { AuthenticationService } from '../../data-access/authentication.service';

@Component({
  selector: 'app-authentication-login',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './authentication-login.component.html',
  styleUrl: './authentication-login.component.scss'
})
export class AuthenticationLoginComponent {
  private _router = inject(Router);
  private _authenticationService: AuthenticationService = inject(AuthenticationService);

  public authentication: Authentication = new Authentication('', '');

  public onLogin(): void {
    this._authenticationService.login(this.authentication).subscribe((success: boolean) => {
      if (success) {
        this._router.navigateByUrl('/home');
      } else {
        alert('Login failed');
      }
    });
  }

  public onSignUp(): void { }

  public onForgotPassword(): void { }
}
