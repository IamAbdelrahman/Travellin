import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { Component } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { RouterModule } from '@angular/router';
import { UpgradeServiceService } from '../../../services/upgrade-service.service';
import { ToastService } from '../../../services/toast.service';

@Component({
  selector: 'app-upgrade-tohost',
  imports: [RouterModule, ReactiveFormsModule, CommonModule, HttpClientModule],
  templateUrl: './upgrade-tohost.component.html',
  styleUrl: './upgrade-tohost.component.css',
})
export class UpgradeTohostComponent {
  upgradeForm!: FormGroup;
  selectedDocumentType: string = 'NationalId';
  uploadedFiles: { front?: File; back?: File } = {};
  uploadProgress: number = 0;
  isUploading: boolean = false;
  submissionError: string | null = null;
  submissionSuccess: boolean = false;

  constructor(
    private fb: FormBuilder,
    private upgradeService: UpgradeServiceService,
    private toaster: ToastService
  ) {}

  ngOnInit(): void {
    this.upgradeForm = this.fb.group({
      documentType: ['NationalId', Validators.required],
      firstName: ['', [Validators.required, Validators.pattern('[a-zA-Z ]*')]],
      lastName: ['', [Validators.required, Validators.pattern('[a-zA-Z ]*')]],
      documentNumber: [
        '',
        [
          Validators.required,
          Validators.pattern('^(?!.*(\\d)\\1{3,})\\d{6,14}$'),
        ],
      ],
      expiryDate: ['', Validators.required],
    });
  }

  onDocumentTypeChange(type: 'Passport' | 'NationalId'): void {
    this.selectedDocumentType = type;
    this.upgradeForm.patchValue({ documentType: type });
  }

  onFileSelected(event: Event, side: 'front' | 'back'): void {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files[0]) {
      this.uploadedFiles[side] = input.files[0];
    }
  }

  // submitForm(): void {
  //   if (this.upgradeForm.valid && this.uploadedFiles.front) {
  //     this.isUploading = true;

  //     const interval = setInterval(() => {
  //       this.uploadProgress += 10;
  //       if (this.uploadProgress >= 100) {
  //         clearInterval(interval);
  //         this.isUploading = false;

  //         console.log('Form submitted', {
  //           formData: this.upgradeForm.value,
  //           files: this.uploadedFiles
  //         });
  //       }
  //     }, 300);
  //   }
  // }

  get firstName() {
    return this.upgradeForm.get('firstName');
  }
  get lastName() {
    return this.upgradeForm.get('lastName');
  }
  get documentNumber() {
    return this.upgradeForm.get('documentNumber');
  }
  get expiryDate() {
    return this.upgradeForm.get('expiryDate');
  }

  //==============================================api management=================================================
  async submitForm(): Promise<void> {
    // if (!this.upgradeForm.valid || !this.uploadedFiles.front || !this.uploadedFiles.back) {
    //   this.submissionError = 'Please fill all required fields and upload both document sides';
    //   return;
    // }

    this.resetSubmissionState();
    this.isUploading = true;

    try {
      const formData = this.prepareFormData();
      await this.submitFormData(formData);
      this.handleSuccess();
    } catch (error) {
      this.handleError(error);
    } finally {
      this.isUploading = false;
    }
  }

  private resetSubmissionState(): void {
    this.submissionError = null;
    this.submissionSuccess = false;
    this.uploadProgress = 0;
  }

  private prepareFormData(): FormData {
    const formData = new FormData();
    const formValues = this.upgradeForm.value;

    // Append all fields with exact names expected by backend
    formData.append('DocumentType', formValues.documentType);
    formData.append('DocumentNumber', formValues.documentNumber);
    formData.append('FirstName', formValues.firstName);
    formData.append('LastName', formValues.lastName);
    formData.append('ExpiryDate', formValues.expiryDate);

    // Append files with exact field names expected by backend
    formData.append('FrontPhoto', this.uploadedFiles.front!);
    formData.append('BackPhoto', this.uploadedFiles.back!);
    console.log('Front File:', this.uploadedFiles.front);
    console.log('Back File:', this.uploadedFiles.back);

    return formData;
  }

  private async submitFormData(formData: FormData): Promise<void> {
    const interval = setInterval(() => {
      this.uploadProgress = Math.min(this.uploadProgress + 10, 90);
    }, 300);

    try {
      await this.upgradeService.submitHostUpgrade(formData).toPromise();
      this.uploadProgress = 100;
    } finally {
      clearInterval(interval);
    }
  }

  private handleSuccess(): void {
    this.submissionSuccess = true;
    this.upgradeForm.reset();
    this.uploadedFiles = {};
    this.toaster.showSuccess('Form submitted successfully!');
  }

  private handleError(error: any): void {
    this.submissionError =
      error.status === 400
        ? 'Please check your information and try again.'
        : 'An error occurred. Please try again later.';
    console.error('Submission error:', error);
    this.toaster.showError(error.error);
  }
}
