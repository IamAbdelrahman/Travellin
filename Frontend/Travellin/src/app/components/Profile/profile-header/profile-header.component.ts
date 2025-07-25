import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../../core/services/auth.service';
import { IUserProfile } from '../../../models/domain/iuser-profile';
import { AccountService } from '../../../services/account.service';
import { catchError, finalize, of } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { UserProfileService } from '../../../services/user-profile.service';

@Component({
  selector: 'app-profile-header',
  templateUrl: './profile-header.component.html',
  styleUrls: ['./profile-header.component.css'],
})
export class ProfileHeaderComponent implements OnInit {
  user: IUserProfile | null = null;
  isLoading = true;
  isErrored = false;
  errorMessage = '';

  constructor(
    private authService: AuthService,
    private accountService: AccountService,
    private userProfileService: UserProfileService
  ) {}

  ngOnInit(): void {
    this.loadUserData();
  }

  private loadUserData(): void {
    if (!this.authService.isAuthenticated()) {
      this.handleUnauthenticated();
      return;
    }

    this.fetchUserProfile();
  }

  public isInRole(role: string): boolean {
    return Boolean(this.user?.roles?.includes(role));
  }

  private fetchUserProfile(): void {
    this.isLoading = true;
    this.isErrored = false;

    this.userProfileService
      .getUserProfile()
      .pipe(
        catchError((error: HttpErrorResponse) => {
          this.handleError(error);
          return of(null);
        }),
        finalize(() => (this.isLoading = false))
      )
      .subscribe(response => {
        if (response && response.body) {
          this.user = response.body;
          // Update auth data if needed
          if (response.body.id && response.body.username) {
            this.authService.setAuthData(
              response.body.id,
              response.body.username,
              localStorage.getItem('accessToken') || ''
            );
          }
        } else {
          this.user = null;
        }
      });
  }

  private handleError(error: HttpErrorResponse): void {
    this.isErrored = true;
    this.errorMessage = this.getErrorMessage(error);

    if (error.status === 401) {
      this.authService.unsetAuthData();
    }

    console.error('Error loading user profile:', error);
  }

  private handleUnauthenticated(): void {
    this.isLoading = false;
    this.isErrored = true;
    this.errorMessage = 'Please log in to view your profile';
    this.user = null;
  }

  private getErrorMessage(error: HttpErrorResponse): string {
    if (error.status === 401) return 'Session expired. Please log in again.';
    if (error.status === 404) return 'User profile not found.';
    return 'Failed to load profile data. Please try again later.';
  }

  get profileInitial(): string {
    return this.user?.firstName?.charAt(0) || 'U';
  }

  // If you need to implement role functionality later, you can add:
  /*
  getUserRole(): string {
    // Implement role logic using accountService if needed
    return 'User'; // Default role
  }
  */
}
