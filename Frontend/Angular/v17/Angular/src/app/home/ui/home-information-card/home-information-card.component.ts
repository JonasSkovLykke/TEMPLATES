import { Component, Input } from '@angular/core';

import { User } from '../../data-access/models/user.model';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-home-information-card',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './home-information-card.component.html',
  styleUrl: './home-information-card.component.scss'
})
export class HomeInformationCardComponent {
  @Input({ required: true }) public user!: User;
}
