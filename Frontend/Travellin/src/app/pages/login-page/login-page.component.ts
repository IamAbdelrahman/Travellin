import { Component } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { AccountService } from '../../services/account.service';
import { HttpErrorResponse, HttpResponse } from '@angular/common/http';
import { ILoginRes } from '../../models/api/response/ilogin-res';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../core/services/auth.service';
import { Eye, EyeOff, LucideAngularModule } from 'lucide-angular';

@Component({
  selector: 'app-login-page',
  imports: [
    RouterModule,
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule,
    LucideAngularModule,
  ],
  templateUrl: './login-page.component.html',
  styleUrl: './login-page.component.scss',
})
export class LoginPageComponent {
  loginForm: FormGroup;
  errorMessage: string = '';
  showPassword = false;
  icons = {
    eye: Eye,
    eyeOff: EyeOff,
  };
  isLoggingIn: boolean = false;

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private accountService: AccountService,
    private authService: AuthService
  ) {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required],
    });
  }

  get passwordFieldType(): string {
    return this.showPassword ? 'text' : 'password';
  }

  togglePasswordVisibility() {
    this.showPassword = !this.showPassword;
  }

  onSubmit(): void {
    if (this.loginForm.valid) {
      const { email, password } = this.loginForm.value;
      this.isLoggingIn = true;
      this.errorMessage = '';

      this.accountService.login({ email, password }).subscribe({
        next: (response: HttpResponse<ILoginRes>) => {
          const body = response.body;
          if (response.status === 200 && body && body.token) {
            this.authService.setAuthData(body.id, body.userName, body.token);
            this.router.navigate(['/home']);
          } else {
            this.handleLoginError('Invalid credentials');
          }
        },
        error: (error: HttpErrorResponse) => {
          if (error.status === 401 || error.status === 403) {
            this.handleLoginError('Invalid email or password');
          } else if (error.status === 0) {
            this.handleLoginError(
              'Network error - please check your connection'
            );
          } else if (error.status >= 500) {
            this.handleLoginError('Server error - please try again later');
          } else if (error.status === 429) {
            this.handleLoginError(
              'Too many Requests Try Again after 30 minutes.'
            );
          } else {
            this.handleLoginError('Login failed - please try again');
          }
        },
        complete: () => {
          this.isLoggingIn = false;
        },
      });
    } else {
      this.errorMessage = 'Please enter valid email and password.';
    }
  }

  private handleLoginError(message: string): void {
    this.errorMessage = message;
    this.loginForm.get('password')?.reset();
    this.isLoggingIn = false;
  }
}
