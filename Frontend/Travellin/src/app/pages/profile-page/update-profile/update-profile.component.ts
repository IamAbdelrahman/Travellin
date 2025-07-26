import { Component } from '@angular/core';
import { ProfileUpdateModalComponent } from '../../../components/Profile/profile-update-modal/profile-update-modal.component';

@Component({
  selector: 'app-update-profile',
  imports: [ProfileUpdateModalComponent],
  templateUrl: './update-profile.component.html',
  styleUrl: './update-profile.component.css',
})
export class UpdateProfileComponent {}
