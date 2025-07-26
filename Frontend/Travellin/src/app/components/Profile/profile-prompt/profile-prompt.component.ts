import { Component, OnInit } from '@angular/core';
import { finalize } from 'rxjs/operators';
import { ProfileUpdateModalComponent } from '../profile-update-modal/profile-update-modal.component';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { HttpErrorResponse, HttpResponse } from '@angular/common/http';
import { IUserProfile } from '../../../models/domain/iuser-profile';
import { UserProfileService } from '../../../services/user-profile.service';

@Component({
  selector: 'app-profile-prompt',
  standalone: true,
  imports: [ProfileUpdateModalComponent, CommonModule],
  templateUrl: './profile-prompt.component.html',
  styleUrls: ['./profile-prompt.component.css'],
})
export class ProfilePromptComponent implements OnInit {
  showUpdateModal = false;
  currentUser: IUserProfile | null = null;
  isLoading = true;
  errorMessage: string | null = null;
  isErrored = false;

  constructor(
    private userService: UserProfileService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.loadUserData();
  }

  loadUserData(): void {
    this.isLoading = true;
    this.errorMessage = null;

    this.userService
      .getUserProfile()
      .pipe(finalize(() => (this.isLoading = false)))
      .subscribe({
        next: (response: HttpResponse<any>) => {
          // Extract the body of the response, which should contain the user data
          const user = response.body;

          console.log('User loaded:', user);
          this.currentUser = user;
          console.log('Current User:', this.currentUser);
          console.log('Roles:', this.currentUser?.roles);
        },
        error: (error: HttpErrorResponse) => {
          this.handleError(error);
        },
      });
  }

  updateProfile(updatedData: IUserProfile): void {
    if (!this.currentUser) return;

    this.isLoading = true;
    this.errorMessage = null;

    this.userService
      .UpdateUserProfile(updatedData)
      .pipe(finalize(() => (this.isLoading = false)))
      .subscribe({
        next: updatedUser => {
          this.currentUser = { ...this.currentUser, ...updatedUser };
          this.showUpdateModal = false;
        },
        error: (error: HttpErrorResponse) => {
          this.handleError(error, 'Failed to update profile');
        },
      });
  }

  public isInRole(role: string): boolean {
    return Boolean(this.currentUser?.roles?.includes(role));
  }

  private handleError(error: HttpErrorResponse, customMessage?: string): void {
    console.error('Error:', error);
    this.errorMessage = customMessage || this.getErrorMessage(error);
    this.isErrored = true;

    if (error.status === 401) {
      this.router.navigate(['/login']);
    }
  }

  private getErrorMessage(error: HttpErrorResponse): string {
    switch (error.status) {
      case 401:
        return 'Session expired. Please log in again.';
      case 404:
        return 'User profile not found.';
      case 500:
        return 'Server error. Please try again later.';
      case 400:
        return 'you ha';
      default:
        return 'An unexpected error occurred.';
    }
  }

  navigateToProfile(): void {
    this.router.navigate(['/user']);
  }

  toggleUpdateModal(): void {
    this.showUpdateModal = !this.showUpdateModal;
  }

  // =====================upgrade button===============================

  showButtonForGuest(): boolean {
    if (!this.currentUser?.roles) return false;
    return this.currentUser.roles.some(role => role.toLowerCase() === 'guest');
  }

  handleGuestAction(): void {
    this.router.navigate(['/upgrade']);
  }

  showButtonForAdmin(): boolean {
    if (!this.currentUser?.roles) return false;
    return this.currentUser.roles.some(role => role.toLowerCase() === 'admin');
  }

  handleAdmintAction(): void {
    this.router.navigate(['/admin']);
  }

  showButtonForHost(): boolean {
    if (!this.currentUser?.roles) return false;
    return this.currentUser.roles.some(role => role.toLowerCase() === 'host');
  }

  handleHostAction(): void {
    this.router.navigate(['/addproperty']);
  }
}
