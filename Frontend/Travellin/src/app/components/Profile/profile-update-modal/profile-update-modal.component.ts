import { Component, OnInit } from '@angular/core';
import {
  AbstractControl,
  FormBuilder,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { CommonModule } from '@angular/common';
import {
  CountryISO,
  NgxIntlTelInputModule,
  SearchCountryField,
} from 'ngx-intl-tel-input';
import { Router } from '@angular/router';
import { UserProfileService } from '../../../services/user-profile.service';
import { AccountService } from '../../../services/account.service';
import { AuthService } from '../../../core/services/auth.service';
import { IUserProfile } from '../../../models/domain/iuser-profile';
import { ApiUserProfileRequest } from '../../../models/api/request/api-user-profile-request';
import { ToastService } from '../../../services/toast.service';

@Component({
  selector: 'app-profile-update-modal',
  standalone: true,
  imports: [
    FormsModule,
    ReactiveFormsModule,
    CommonModule,
    NgxIntlTelInputModule,
  ],
  templateUrl: './profile-update-modal.component.html',
  styleUrls: ['./profile-update-modal.component.css'],
})
export class ProfileUpdateModalComponent implements OnInit {
  userForm: FormGroup;
  passwordForm: FormGroup;
  user: IUserProfile | null = null;
  isLoading = false;
  isSaving = false;
  saveSuccess = false;
  errorMessage = '';
  showPasswordModal = false;
  maxBirthDate = new Date();

  // Validation patterns
  readonly namePattern = /^[a-zA-ZÀ-ÿ '-]{3,}$/;
  readonly passwordPattern =
    '^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[^A-Za-z\\d])[A-Za-z\\d\\W]{8,}$';

  // Phone input configuration
  CountryISO = CountryISO;
  SearchCountryField = SearchCountryField;
  phoneValidation = true;
  autoCountrySelect = true;
  separateDialCode = true;
  searchCountryFlag = true;

  searchCountryFields: SearchCountryField[] = [
    SearchCountryField.Iso2,
    SearchCountryField.Name,
    SearchCountryField.DialCode,
  ];

  // File upload
  selectedFile: File | null = null;
  previewUrl: string | null = null;

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private userProfileService: UserProfileService,
    private accountService: AccountService,
    private authService: AuthService,
    private toaster: ToastService
  ) {
    this.userForm = this.fb.group({
      firstName: [
        '',
        [
          Validators.required,
          Validators.maxLength(50),
          this.validateName.bind(this),
        ],
      ],
      lastName: [
        '',
        [
          Validators.required,
          Validators.maxLength(50),
          this.validateName.bind(this),
        ],
      ],
      email: [{ value: '', disabled: true }],
      username: [
        '',
        [
          Validators.required,
          Validators.minLength(3),
          Validators.maxLength(20),
          Validators.pattern(/^[a-zA-Z0-9_]+$/),
        ],
      ],
      bio: ['', [Validators.maxLength(200)]],
      country: [''],
      phoneNumber: [null],
      birthday: [''],
    });

    this.passwordForm = this.fb.group(
      {
        currentPassword: ['', Validators.required],
        newPassword: [
          '',
          [
            Validators.required,
            Validators.minLength(8),
            Validators.pattern(this.passwordPattern),
          ],
        ],
        confirmPassword: ['', Validators.required],
      },
      { validator: this.passwordMatchValidator }
    );
  }

  ngOnInit(): void {
    this.loadUserData();
  }

  loadUserData(): void {
    this.isLoading = true;
    this.errorMessage = '';

    this.userProfileService.getUserProfile().subscribe({
      next: (response: any) => {
        if (response.body) {
          this.user = response.body;
          this.userForm.patchValue({
            firstName: this.user?.firstName,
            lastName: this.user?.lastName,
            email: this.user?.email,
            username: this.user?.userName,
            bio: this.user?.bio,
            country: this.user?.country,
            phoneNumber: this.user?.phoneNumber,
            birthday: this.user?.birthDate
              ? new Date(this.user.birthDate)
              : null,
          });
        }
        this.isLoading = false;
      },
      error: err => {
        this.errorMessage = err.error || 'Failed to load profile data';
        this.isLoading = false;
        console.error(err);

        if (err.status === 401) {
          this.authService.unsetAuthData();
          this.router.navigate(['/login']);
        }
      },
    });
  }

  // onSubmitProfile(): void {
  //   if (this.userForm.invalid || !this.user) return;

  //   this.isSaving = true;
  //   this.saveSuccess = false;
  //   this.errorMessage = '';

  //   const updatedUser: IUserProfile = {
  //     ...this.user,
  //     firstName: this.userForm.value.firstName,
  //     lastName: this.userForm.value.lastName,
  //     userName: this.userForm.value.username,
  //     bio: this.userForm.value.bio,
  //     country: this.userForm.value.country,
  //     phoneNumber: this.userForm.value.phoneNumber,
  //     birthDate: this.userForm.value.birthday
  //   };

  //   // If we have a file, convert to FormData
  //   const payload = this.selectedFile ? this.createFormData(updatedUser) : updatedUser;

  //   this.userProfileService.UpdateUserProfile(payload).subscribe({
  //     next: (response: any) => {
  //       if (response.body) {
  //         this.user = response.body;
  //         this.saveSuccess = true;
  //         this.selectedFile = null;
  //         this.previewUrl = null;
  //         setTimeout(() => this.saveSuccess = false, 3000);
  //       }
  //       this.isSaving = false;
  //     },
  //     error: (err) => {
  //       this.errorMessage = 'Failed to save changes';
  //       this.isSaving = false;
  //       console.error(err);

  //       if (err.status === 401) {
  //         this.authService.unsetAuthData();
  //         this.router.navigate(['/login']);
  //       }
  //     }
  //   });
  // }

  // private createFormData(userData: IUserProfile): FormData {
  //   const formData = new FormData();
  //   formData.append('firstName', userData.firstName);
  //   formData.append('lastName', userData.lastName);
  //   formData.append('username', userData.userName);
  //   if (userData.bio) formData.append('bio', userData.bio);
  //   if (userData.country) {
  //     formData.append('country', JSON.stringify(userData.country));

  //   }
  //   if (userData.phoneNumber) formData.append('phoneNumber', userData.phoneNumber);
  //   if (userData.birthDate) formData.append('birthday', userData.birthDate.toString());
  //   if (this.selectedFile) {
  //     formData.append('photo', this.selectedFile, this.selectedFile.name);
  //   }
  //   return formData;
  // }

  // In ProfileUpdateModalComponent

  onSubmitProfile(): void {
    if (this.userForm.invalid || !this.user) return;

    this.isSaving = true;
    this.saveSuccess = false;
    this.errorMessage = '';

    // Create request payload with required fields
    const requestData: ApiUserProfileRequest = {
      userId: this.user.userId,
      userName: this.userForm.value.username,
      email: this.user.email,
      firstName: this.userForm.value.firstName,
      lastName: this.userForm.value.lastName,
      phoneNumber:
        this.userForm.value.phoneNumber?.e164Number ||
        this.userForm.value.phoneNumber,
      bio: this.userForm.value.bio,
      // birthDate: this.userForm.value.birthday ? new Date(this.userForm.value.birthday): new Date(),
      birthDate: '2025-04-26',
      country: this.userForm.value.country,
      photo: this.user.photo,
    };

    // Create payload (FormData if file selected, otherwise plain object)
    const payload = this.selectedFile
      ? this.createFormData(requestData)
      : requestData;

    this.userProfileService.UpdateUserProfile(payload).subscribe({
      next: updatedProfile => {
        this.user = updatedProfile;
        this.saveSuccess = true;
        this.selectedFile = null;
        this.previewUrl = updatedProfile.photo.photoUrl || null;

        // Update form with normalized data
        this.userForm.patchValue({
          firstName: updatedProfile.firstName,
          lastName: updatedProfile.lastName,
          username: updatedProfile.userName,
          bio: updatedProfile.bio,
          country: updatedProfile.country,
          phoneNumber: updatedProfile.phoneNumber,
          // birthday: updatedProfile.birthDate ? new Date(updatedProfile.birthDate) : null
          birthday: '2025-04-26',
        });

        // setTimeout(() => this.saveSuccess = false, 3000);
        this.isSaving = false;
      },
      error: err => {
        this.errorMessage = err.error || 'Failed to save changes';
        this.isSaving = false;
        // this.toaster.showError(err.error);

        if (err.status === 401) {
          this.authService.unsetAuthData();
          this.router.navigate(['/login']);
        }
      },
    });
  }

  private createFormData(requestData: ApiUserProfileRequest): FormData {
    const formData = new FormData();

    if (requestData.country) {
      // Match the server's expected structure
      formData.append('country[id]', requestData.country.id.toString());
      formData.append('country[name]', requestData.country.name);
      formData.append(
        'country[regionId]',
        requestData.country.regionId.toString()
      );
    }
    // Append all fields including undefined ones (API might handle them)
    formData.append('userId', requestData.userId ?? '');
    formData.append('userName', requestData.userName ?? '');
    formData.append('email', requestData.email ?? '');
    if (requestData.firstName)
      formData.append('firstName', requestData.firstName);
    if (requestData.lastName) formData.append('lastName', requestData.lastName);
    if (requestData.phoneNumber)
      formData.append('phoneNumber', requestData.phoneNumber);
    if (requestData.bio) formData.append('bio', requestData.bio);
    // if (requestData.birthDate) formData.append('birthDate', requestData.birthDate);
    if (requestData.birthDate) formData.append('birthDate', '2025-04-26');
    if (requestData.country)
      formData.append('country', JSON.stringify(requestData.country));
    if (this.selectedFile)
      formData.append('photo', this.selectedFile, this.selectedFile.name);

    return formData;
  }

  onFileSelected(event: any): void {
    const file = event.target.files[0];
    if (file && file.type.match('image.*')) {
      this.selectedFile = file;

      const reader = new FileReader();
      reader.onload = () => {
        this.previewUrl = reader.result as string;
      };
      reader.readAsDataURL(file);
    }
  }

  openPasswordModal(): void {
    this.passwordForm.reset();
    this.showPasswordModal = true;
  }

  closePasswordModal(): void {
    this.showPasswordModal = false;
  }

  onSubmitPassword(): void {
    if (this.passwordForm.invalid) return;

    this.isSaving = true;
    this.errorMessage = '';

    const passwordData = {
      oldPassword: this.passwordForm.value.currentPassword,
      newPassword: this.passwordForm.value.newPassword,
    };

    this.accountService.changePassword(passwordData).subscribe({
      next: (response: any) => {
        this.saveSuccess = true;
        this.isSaving = false;
        this.showPasswordModal = false;
        setTimeout(() => (this.saveSuccess = false), 3000);
      },
      error: err => {
        this.errorMessage = err.error?.message || 'Failed to change password';
        this.isSaving = false;
        console.error(err);
        // this.toaster.showError(err.error);

        if (err.status === 401) {
          this.authService.unsetAuthData();
          this.router.navigate(['/login']);
        }
      },
    });
  }

  // Validation helpers
  validateName(control: AbstractControl): { [key: string]: any } | null {
    const valid = this.namePattern.test(control.value);
    return valid ? null : { invalidName: true };
  }

  passwordMatchValidator(form: FormGroup) {
    return form.get('newPassword')?.value === form.get('confirmPassword')?.value
      ? null
      : { mismatch: true };
  }

  getFormErrors(
    controlName: string,
    form: FormGroup = this.userForm
  ): string[] {
    const control = form.get(controlName);
    const errors: string[] = [];

    if (!control || !control.errors || !control.touched) return errors;

    if (control.errors['required']) {
      errors.push('This field is required');
    }
    if (control.errors['maxlength']) {
      errors.push(
        `Maximum length is ${control.errors['maxlength'].requiredLength} characters`
      );
    }
    if (control.errors['invalidName']) {
      errors.push('Only letters, spaces, hyphens and apostrophes are allowed');
    }
    if (control.errors['pattern']) {
      if (controlName.includes('password')) {
        errors.push(
          'Password must contain at least one uppercase letter, one lowercase letter, one number and one special character'
        );
      }
      if (controlName === 'username') {
        errors.push('Only letters, numbers and underscores are allowed');
      }
    }
    if (control.errors['mismatch']) {
      errors.push('Passwords do not match');
    }
    if (control.errors['minlength']) {
      errors.push(
        `Minimum length is ${control.errors['minlength'].requiredLength}`
      );
    }

    return errors;
  }

  // Password strength indicators
  isPasswordLengthValid(): boolean {
    return this.passwordForm.get('newPassword')?.value?.length >= 8;
  }

  isPasswordLowerValid(): boolean {
    return /[a-z]/.test(this.passwordForm.get('newPassword')?.value);
  }

  isPasswordUpperValid(): boolean {
    return /[A-Z]/.test(this.passwordForm.get('newPassword')?.value);
  }

  isPasswordNumberValid(): boolean {
    return /\d/.test(this.passwordForm.get('newPassword')?.value);
  }

  isPasswordSpecialCharValid(): boolean {
    return /[^A-Za-z\d]/.test(this.passwordForm.get('newPassword')?.value);
  }

  // Country handling
  getAvailableCountries(): string[] {
    return Object.values(CountryISO).filter(
      value => typeof value === 'string'
    ) as string[];
  }

  // Photo handling
  get photoUrl(): string | null {
    return (
      this.previewUrl ||
      (this.user?.photo.photoUrl ? this.user.photo.photoUrl : 'favicon.png')
    );
  }
}
