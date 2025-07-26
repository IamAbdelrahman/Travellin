import { Component } from '@angular/core';
import { ProfileHeaderComponent } from '../../../components/Profile/profile-header/profile-header.component';
import { Router, RouterModule } from '@angular/router';
import { ProfilePromptComponent } from '../../../components/Profile/profile-prompt/profile-prompt.component';
import { ProfileUpdateModalComponent } from '../../../components/Profile/profile-update-modal/profile-update-modal.component';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-profile',
  imports: [
    ProfileHeaderComponent,
    RouterModule,
    ProfilePromptComponent,
    ProfileUpdateModalComponent,
    CommonModule,
    RouterModule,
  ],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css',
})
export class ProfileComponent {
  constructor(private router: Router) {}
}
