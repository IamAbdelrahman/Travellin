import { Component } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { AccountService } from '../../services/account.service';
import { IRegisterRes } from '../../models/api/response/iregister-res';
import { AuthService } from '../../core/services/auth.service';
import { Eye, EyeOff, LucideAngularModule } from 'lucide-angular';

@Component({
  standalone: true,
  imports: [
    RouterModule,
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule,
    LucideAngularModule,
  ],
  templateUrl: './register-page.component.html',
  styleUrl: './register-page.component.scss',
})
export class RegisterPageComponent {
  signupForm: FormGroup;
  phoneForm: FormGroup;
  showPassword = false;
  isModalOpen = true;
  isEmailOption = false;
  isPhoneOption = true;
  icons = {
    eye: Eye,
    eyeOff: EyeOff,
  };
  countries = [
    { name: 'Egypt', code: '+20' },
    { name: 'United States', code: '+1' },
    { name: 'United Kingdom', code: '+44' },
    { name: 'France', code: '+33' },
    { name: 'Germany', code: '+49' },
    { name: 'Italy', code: '+39' },
    { name: 'Spain', code: '+34' },
    { name: 'Netherlands', code: '+31' },
    { name: 'Belgium', code: '+32' },
    { name: 'Switzerland', code: '+41' },
    { name: 'Austria', code: '+43' },
    { name: 'Denmark', code: '+45' },
    { name: 'Sweden', code: '+46' },
    { name: 'Norway', code: '+47' },
    { name: 'Finland', code: '+358' },
    { name: 'Portugal', code: '+351' },
    { name: 'Greece', code: '+30' },
    { name: 'Turkey', code: '+90' },
    { name: 'Saudi Arabia', code: '+966' },
    { name: 'UAE', code: '+971' },
    { name: 'Kuwait', code: '+965' },
    { name: 'Bahrain', code: '+973' },
    { name: 'Qatar', code: '+974' },
    { name: 'Oman', code: '+968' },
    { name: 'Jordan', code: '+962' },
    { name: 'Lebanon', code: '+961' },
    { name: 'Morocco', code: '+212' },
    { name: 'Algeria', code: '+213' },
    { name: 'Tunisia', code: '+216' },
  ];
  constructor(
    private fb: FormBuilder,
    private accountService: AccountService,
    private authService: AuthService,
    private router: Router
  ) {
    this.signupForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: [
        '',
        [
          Validators.required,
          Validators.minLength(12),
          Validators.pattern(
            /^(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()_+])[A-Za-z\d!@#$%^&*()_+]{12,}$/
          ),
        ],
      ],
    });
    this.phoneForm = this.fb.group({
      countryCode: ['+20', Validators.required],
      phoneNumber: ['', [Validators.required, Validators.pattern('^[0-9]{6,15}$')]]
    });
  }

  get formControls() {
    return this.signupForm.controls;
  }
  get phoneControls() {
    return this.phoneForm.controls;
  }
  get passwordFieldType(): string {
    return this.showPassword ? 'text' : 'password';
  }

  togglePasswordVisibility() {
    this.showPassword = !this.showPassword;
  }

  onSubmit(): void {
    if (this.signupForm.valid) {
      const formData = this.signupForm.value;
      this.accountService
        .register({
          email: formData.email,
          password: formData.password,
          firstName: formData.firstName,
          lastName: formData.lastName,
          birthDate: formData.birthDate,
        })
        .subscribe({
          next: (res: { body: IRegisterRes }) => {
            const body = res.body;
            if (body && body.token) {
              this.authService.setAuthData(body.id, body.userName, body.token);
              this.router.navigate(['/home']);
            }
          },
          error: err => {
            console.error('Registration error:', err);
          },
        });

  minimumAgeValidator(minAge: number) {
    return (control: any) => {
      const birthDate = new Date(control.value);
      const today = new Date();

      if (isNaN(birthDate.getTime())) {
        return null; // Ignore if not a valid date yet
      }

      const age = today.getFullYear() - birthDate.getFullYear();
      const monthDiff = today.getMonth() - birthDate.getMonth();
      const dayDiff = today.getDate() - birthDate.getDate();

      const isOldEnough =
        age > minAge ||
        (age === minAge &&
          (monthDiff > 0 || (monthDiff === 0 && dayDiff >= 0)));

      return isOldEnough ? null : { tooYoung: true };
    };
  }
    continueWithGoogle() {
    console.log('Continue with Google');
    // TODO: Call Firebase Google auth
  }

  continueWithEmail() {
    console.log('Continue with Email');
    this.isEmailOption = true;
    this.isPhoneOption = false;
  }
  continueWithPhoneNumber() {
    console.log('Continue with Phone Number');
    this.isPhoneOption = true;
    this.isEmailOption = false;
  }
  continueWithFacebook() {
    console.log('Continue with Facebook');
    // TODO: Call Firebase Facebook auth
  }

  openPrivacyPolicy() {
    console.log('Open privacy policy');
  }
  closeModal() {
    this.isModalOpen = false;
    this.router.navigate(['/home']);
  }
}
