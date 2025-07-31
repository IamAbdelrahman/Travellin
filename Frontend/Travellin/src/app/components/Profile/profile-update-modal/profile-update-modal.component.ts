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
  maxBirthDate = new Date(new Date().getFullYear() - 18, new Date().getMonth(), new Date().getDate());

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
  
  // Track form changes
  private originalFormValues: any = {};
  formHasChanges = false;

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
      bio: ['', [Validators.maxLength(500)]],
      country: [''],
      phoneNumber: [null],
      birthday: [null, [this.validateBirthDate.bind(this)]],
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
    console.log('ProfileUpdateModalComponent initialized');
    this.loadUserData();
  }



  loadUserData(): void {
    console.log('Loading user data...');
    this.isLoading = true;
    this.errorMessage = '';

    this.userProfileService.getUserProfile().subscribe({
      next: (response: any) => {
        console.log('User profile response:', response);
        if (response.body) {
          this.user = response.body;
                                // Handle birthdate properly - convert to YYYY-MM-DD format for HTML date input
                      let birthDate = null;
                      if (this.user?.birthDate) {
                        let dateObj: Date;
                        // If it's already a Date object, use it directly
                        if (typeof this.user.birthDate === 'object' && this.user.birthDate && (this.user.birthDate as any) instanceof Date) {
                          dateObj = this.user.birthDate;
                        } else {
                          // If it's a string, convert to Date
                          dateObj = new Date(this.user.birthDate as string);
                        }
                        // Format as YYYY-MM-DD for HTML date input
                        birthDate = dateObj.toISOString().split('T')[0];
                      }

                      this.userForm.patchValue({
                        firstName: this.user?.firstName,
                        lastName: this.user?.lastName,
                        email: this.user?.email,
                        username: this.user?.userName,
                        bio: this.user?.bio,
                        country: this.user?.country,
                        phoneNumber: this.user?.phoneNumber,
                        birthday: birthDate,
                      });
          
          // Store original values for change detection
          this.originalFormValues = {
            firstName: this.user?.firstName || '',
            lastName: this.user?.lastName || '',
            email: this.user?.email || '',
            username: this.user?.userName || '',
            bio: this.user?.bio || '',
            country: this.user?.country || '',
            phoneNumber: this.user?.phoneNumber || null,
            birthday: birthDate
          };
          

          
          // Set up form change detection
          this.userForm.valueChanges.subscribe(() => {
            this.checkFormChanges();
          });
          
          // Initial change detection check
          this.checkFormChanges();
        }
        this.isLoading = false;
      },
      error: err => {
        console.error('Error loading user data:', err);
        this.errorMessage = err.error || 'Failed to load profile data';
        this.isLoading = false;

        if (err.status === 401) {
          this.authService.unsetAuthData();
          this.router.navigate(['/login']);
        }
      },
    });
  }



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
      birthDate: this.userForm.value.birthday || null,
      country: this.userForm.value.country,
      photo: this.user.photo,
    };

    // Always send FormData since the API expects multipart/form-data
    const payload = this.createFormData(requestData);

    this.userProfileService.UpdateUserProfile(payload).subscribe({
      next: updatedProfile => {
        this.user = updatedProfile;
        this.saveSuccess = true;
        this.selectedFile = null;
        this.previewUrl = updatedProfile.photo.photoUrl || null;

        // Update form with normalized data
        let birthDate = null;
        if (updatedProfile.birthDate) {
          let dateObj: Date;
          if (typeof updatedProfile.birthDate === 'object' && updatedProfile.birthDate && (updatedProfile.birthDate as any) instanceof Date) {
            dateObj = updatedProfile.birthDate;
          } else {
            dateObj = new Date(updatedProfile.birthDate as string);
          }
          // Format as YYYY-MM-DD for HTML date input
          birthDate = dateObj.toISOString().split('T')[0];
        }

        this.userForm.patchValue({
          firstName: updatedProfile.firstName,
          lastName: updatedProfile.lastName,
          username: updatedProfile.userName,
          bio: updatedProfile.bio,
          country: updatedProfile.country,
          phoneNumber: updatedProfile.phoneNumber,
          birthday: birthDate,
        });

        // Reset change detection
        this.originalFormValues = {
          firstName: updatedProfile.firstName || '',
          lastName: updatedProfile.lastName || '',
          email: updatedProfile.email || '',
          username: updatedProfile.userName || '',
          bio: updatedProfile.bio || '',
          country: updatedProfile.country || '',
          phoneNumber: updatedProfile.phoneNumber || null,
          birthday: birthDate
        };
        this.formHasChanges = false;
        
        console.log('Form reset after save:', this.originalFormValues);

        setTimeout(() => this.saveSuccess = false, 3000);
        this.isSaving = false;
      },
      error: err => {
        this.errorMessage = err.error || 'Failed to save changes';
        this.isSaving = false;

        if (err.status === 401) {
          this.authService.unsetAuthData();
          this.router.navigate(['/login']);
        }
      },
    });
  }

  private checkFormChanges(): void {
    const currentValues = this.userForm.value;
    const hasFileChanges = this.selectedFile !== null;
    
    // Simple comparison - check if any field has changed
    let formChanged = false;
    
    if (this.originalFormValues) {
      // Compare phone number properly - handle both string and object formats
      let phoneChanged = false;
      if (currentValues.phoneNumber && this.originalFormValues.phoneNumber) {
        const currentPhone = typeof currentValues.phoneNumber === 'string' 
          ? currentValues.phoneNumber 
          : currentValues.phoneNumber.e164Number;
        const originalPhone = typeof this.originalFormValues.phoneNumber === 'string'
          ? this.originalFormValues.phoneNumber
          : this.originalFormValues.phoneNumber?.e164Number;
        phoneChanged = currentPhone !== originalPhone;
      } else {
        phoneChanged = currentValues.phoneNumber !== this.originalFormValues.phoneNumber;
      }
      
      formChanged = 
        currentValues.firstName !== this.originalFormValues.firstName ||
        currentValues.lastName !== this.originalFormValues.lastName ||
        currentValues.username !== this.originalFormValues.username ||
        currentValues.bio !== this.originalFormValues.bio ||
        currentValues.country !== this.originalFormValues.country ||
        phoneChanged ||
        currentValues.birthday !== this.originalFormValues.birthday;
    }
    
    this.formHasChanges = formChanged || hasFileChanges;
  }

  private createFormData(requestData: ApiUserProfileRequest): FormData {
    const formData = new FormData();

    // Append all fields
    formData.append('userId', requestData.userId ?? '');
    formData.append('userName', requestData.userName ?? '');
    formData.append('email', requestData.email ?? '');
    formData.append('firstName', requestData.firstName ?? '');
    formData.append('lastName', requestData.lastName ?? '');
    formData.append('phoneNumber', requestData.phoneNumber ?? '');
    formData.append('bio', requestData.bio ?? '');
    formData.append('birthDate', requestData.birthDate ?? '');
    
    // Handle country object
    if (requestData.country) {
      formData.append('country[id]', requestData.country.id.toString());
      formData.append('country[name]', requestData.country.name);
      formData.append('country[regionId]', requestData.country.regionId.toString());
    }
    
    // Handle photo file
    if (this.selectedFile) {
      formData.append('photo', this.selectedFile, this.selectedFile.name);
    }

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
      
      // Trigger change detection for file changes
      this.checkFormChanges();
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

  validateBirthDate(control: AbstractControl): { [key: string]: any } | null {
    if (!control.value) return null;
    
    const selectedDate = new Date(control.value);
    const today = new Date();
    const minAgeDate = new Date(today.getFullYear() - 18, today.getMonth(), today.getDate());
    
    if (selectedDate > minAgeDate) {
      return { underAge: true };
    }
    
    return null;
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
    if (control.errors['underAge']) {
      errors.push('You must be at least 18 years old');
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
