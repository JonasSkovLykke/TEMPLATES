import { Component, OnInit, inject } from '@angular/core';

import { UserService } from '../data-access/user.service';
import { User } from '../data-access/models/user.model';
import { HomeInformationCardComponent } from '../ui/home-information-card/home-information-card.component';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, HomeInformationCardComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent implements OnInit {
  private _userService = inject(UserService);

  public user?: User;

  public ngOnInit(): void {
    this._userService.me().subscribe((user: User) => {
      if (user) {
        this.user = user;
      }
    });
  }
}
