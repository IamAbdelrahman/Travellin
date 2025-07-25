import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import {
  AbstractControl,
  FormArray,
  FormBuilder,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  ValidationErrors,
  Validators,
} from '@angular/forms';

import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';

import { catchError, finalize, map, Observable, of, retry, tap } from 'rxjs';
import { AddPropertyService } from '../../services/add-property.service';
import { IPropertyAmenity } from '../../models/domain/iproperty-amenity';
import { IPropertyType } from '../../models/domain/iproperty-type';
import { IPropertyInfo } from '../../models/domain/iproperty-info';
import { PropertyService } from '../../services/property-service.service';
import { ILocation } from '../../models/domain/ilocation';
import { ToastService } from '../../services/toast.service';
import { IPropertyResponse } from '../../models/api/response/iproperty-response';
import { IPropertyRequest } from '../../models/api/request/iproperty-request';
import { IAminityCategory } from '../../models/domain/IAminity-category';
import { IPropertyAmenityReq } from '../../models/api/request/iproperty-amenity-req';
import { IGuestTypeResponse } from '../../models/api/response/iguest-type-response';
import { IGuestType } from '../../models/domain/iguest-type';
import { HttpEventType } from '@angular/common/http';

@Component({
  selector: 'app-add-property',
  imports: [CommonModule, RouterModule, ReactiveFormsModule, FormsModule],
  templateUrl: './add-property.component.html',
  styleUrl: './add-property.component.css',
})
export class AddPropertyComponent implements OnInit {
  propertyForm: FormGroup;
  propertyTypes: IPropertyType[] = [];
  locations: ILocation[] = [];
  isLoading = false;
  loadError: string | null = null;
  errorMessage: string | null = null;
  propertyResponse!: IPropertyResponse;

  constructor(
    private fb: FormBuilder,
    private propertyService: PropertyService,
    private addPropertyService: AddPropertyService,
    private router: Router,
    private toastr: ToastService
  ) {
    this.propertyForm = this.fb.group({
      title: [
        '',
        [
          Validators.required,
          Validators.minLength(5),
          Validators.maxLength(100),
          Validators.pattern(/^[\p{L}\p{N}\s.,!?'&-]{5,100}$/u),
        ],
      ],
      description: [
        '',
        [
          Validators.required,
          Validators.minLength(10),
          Validators.maxLength(1000),
          Validators.pattern(/^[\p{L}\p{N}\s.,!?'&()\-:;\n\r]{10,1000}$/u),
        ],
      ],
      propertyTypeId: ['', Validators.required],
      locationId: ['', Validators.required],
      pricePerNight: ['', [Validators.required, Validators.min(0.01)]],
      latitude: [
        '',
        [Validators.required, Validators.min(-90), Validators.max(90)],
      ],
      longitude: [
        '',
        [Validators.required, Validators.min(-180), Validators.max(180)],
      ],
      safetyInfo: [
        '',
        [
          Validators.required,
          Validators.minLength(10),
          Validators.maxLength(500),
          Validators.pattern(/^[\p{L}\p{N}\s.,!?'()"-]{10,500}$/u), // 'u' flag for Unicode
        ],
      ],
      houseRules: [
        '',
        [
          Validators.required,
          Validators.minLength(10),
          Validators.maxLength(500),
          Validators.pattern(/^[\p{L}\p{N}\s.,!?'()"-]{10,500}$/u),
        ],
      ],
      cancellationPolicy: [
        '',
        [
          Validators.required,
          Validators.minLength(10),
          Validators.maxLength(500),
          Validators.pattern(/^[\p{L}\p{N}\s.,!?'()"-]{10,500}$/u),
        ],
      ],
    });
  }

  ngOnInit(): void {
    this.loadFormData();
    this.loadAllAmenities();
    this.initAvailabilityForm();
    this.initGuestForm();
    this.loadGuestTypes();
    this.initSpaceForms();
    this.loadSpaceData();
  }

  loadFormData(): void {
    this.isLoading = true;
    this.loadError = null;

    this.propertyService
      .getPropertyTypes()
      .pipe(map(response => response.items))
      .subscribe({
        next: (types: IPropertyType[]) => {
          this.propertyTypes = types;
          this.checkLoadingComplete();
          console.log('Property Types:', this.propertyTypes);
        },
        error: err => {
          this.handleLoadError('Failed to load property types');
        },
      });

    this.propertyService
      .getAllLocations()
      .pipe(
        map(response => {
          if (!Array.isArray(response)) {
            return Object.values(response);
          }
          return response;
        })
      )
      .subscribe({
        next: locations => {
          this.locations = locations as ILocation[];
          console.log('Locations:', this.locations);
          this.checkLoadingComplete();
        },
        error: err => {
          this.handleLoadError('Failed to load locations');
        },
      });
  }

  private checkLoadingComplete(): void {
    if (this.propertyTypes.length > 0 && this.locations.length > 0) {
      this.isLoading = false;
    }
  }

  private handleLoadError(message: string): void {
    this.loadError = message;
    this.isLoading = false;
  }

  onSubmit(): void {
    if (this.propertyForm.valid) {
      this.addPropertyService.addProperty(this.propertyForm.value).subscribe({
        next: response => {
          console.log('Property added successfully:', response);
          this.toastr.showSuccess('Property added successfully!', 5000);
          this.propertyResponse = response;
          console.log('Property Response:', this.propertyResponse.id);
          // this.router.navigate(['/properties', response.id]);
        },
        error: error => {
          this.errorMessage = this.getErrorMessage(error);
          this.toastr.showError(this.errorMessage, 5000);
        },
      });
    }
  }

  private getErrorMessage(error: any): string {
    if (error.error && error.error.message) {
      return error.error.message;
    } else if (error.status === 0) {
      return 'Cannot connect to server. Please try again later.';
    } else if (error.status === 401) {
      return 'You are not authorized. Please log in to continue.';
    } else if (error.status === 403) {
      return 'Access denied. You do not have permission.';
    } else if (error.status === 404) {
      return 'Requested resource not found.';
    } else if (error.status === 500) {
      return 'Server error occurred. Please contact support.';
    } else {
      return 'An unknown error occurred. Please try again.';
    }
  }

  onCancel(): void {
    // this.toastr.showSuccess('Property added successfully!', 5000);
    // this.router.navigate(['/properties']);
  }

  //=================================add property amenity===================================

  allAmenities: IPropertyAmenity[] = [];
  filteredAmenities: IPropertyAmenity[] = [];
  amenityCategories: IAminityCategory[] = [];
  activeCategoryId: string | null = null;
  selectedAmenities: IPropertyAmenity[] = [];
  isLoadingAmenities: boolean = false;
  // Pagination
  currentPage = 1;
  pageSize = 9; // Show 3x3 grid
  totalPages = 1;
  totalItems = 0;

  // Filtering
  searchTerm = '';
  selectedCategory = '';
  categories: string[] = [];

  tempSelectedAmenities: IPropertyAmenity[] = [];

  selectCategory(categoryId: string): void {
    this.activeCategoryId = categoryId;
    this.currentPage = 1;
    this.filterAmenities();
  }

  filterAmenities(): void {
    let filtered = this.allAmenities;

    if (this.activeCategoryId) {
      filtered = filtered.filter(a => a.categoryId === this.activeCategoryId);
    }

    this.totalItems = filtered.length;
    this.totalPages = Math.ceil(this.totalItems / this.pageSize);

    const startIndex = (this.currentPage - 1) * this.pageSize;
    this.filteredAmenities = filtered.slice(
      startIndex,
      startIndex + this.pageSize
    );
  }

  isSelected(amenityId: number): boolean {
    return this.selectedAmenities.some(a => a.id === amenityId);
  }

  // Pagination methods
  prevPage(): void {
    if (this.currentPage > 1) {
      this.currentPage--;
      this.filterAmenities();
    }
  }

  nextPage(): void {
    if (this.currentPage < this.totalPages) {
      this.currentPage++;
      this.filterAmenities();
    }
  }

  goToPage(page: number): void {
    this.currentPage = page;
    this.filterAmenities();
  }

  getPageNumbers(): number[] {
    const pages = [];
    const maxVisiblePages = 5;

    let startPage = Math.max(
      1,
      this.currentPage - Math.floor(maxVisiblePages / 2)
    );
    let endPage = startPage + maxVisiblePages - 1;

    if (endPage > this.totalPages) {
      endPage = this.totalPages;
      startPage = Math.max(1, endPage - maxVisiblePages + 1);
    }

    for (let i = startPage; i <= endPage; i++) {
      pages.push(i);
    }

    return pages;
  }

  // New method to check temp selection
  isTempSelected(amenityId: number): boolean {
    return (
      this.tempSelectedAmenities.some(a => a.id === amenityId) ||
      this.selectedAmenities.some(a => a.id === amenityId)
    );
  }

  isLoadingCategories: boolean = false;
  // New property to track if amenities need to be saved
  unsavedChanges: boolean = false;

  loadAllAmenities(): void {
    this.isLoadingAmenities = true;
    this.propertyService.getAllAmenities().subscribe({
      next: amenities => {
        this.allAmenities = amenities;
        this.totalItems = amenities.length;
        this.totalPages = Math.ceil(this.totalItems / this.pageSize);
        this.filterAmenities();
        this.isLoadingAmenities = false;
      },
      error: err => {
        console.error('Failed to load amenities', err);
        this.isLoadingAmenities = false;
      },
    });
  }

  toggleAmenity(amenity: IPropertyAmenity): void {
    if (this.isSelected(amenity.id)) {
      this.selectedAmenities = this.selectedAmenities.filter(
        a => a.id !== amenity.id
      );
    } else {
      this.selectedAmenities.push(amenity);
    }
    this.unsavedChanges = true;
  }

  async saveAmenities(): Promise<void> {
    if (!this.propertyResponse?.id) {
      this.toastr.showError('Property ID is missing');
      return;
    }

    if (this.selectedAmenities.length === 0) {
      this.toastr.showInfo('No amenities selected');
      return;
    }

    this.isLoadingAmenities = true;
    const errors: string[] = [];
    let savedCount = 0;

    // Process amenities sequentially
    for (const [index, amenity] of this.selectedAmenities.entries()) {
      try {
        const request: IPropertyAmenityReq = {
          propertyId: this.propertyResponse.id,
          amenityId: amenity.id,
        };

        // Add delay between requests (100ms)
        if (index > 0) await new Promise(resolve => setTimeout(resolve, 100));

        await this.addPropertyService.addPropertyAmenity(request).toPromise();
        savedCount++;
      } catch (err: any) {
        errors.push(
          `Failed to save ${amenity.name}: ${err.error?.message || 'Server error'}`
        );
        console.error(`Error saving ${amenity.name}:`, err);
        this.toastr.showError(err.error);
      }
    }

    // Show result summary
    if (errors.length === 0) {
      this.toastr.showSuccess(`All ${savedCount} amenities saved!`, 5000);
      this.unsavedChanges = false;
    }

    this.isLoadingAmenities = false;
  }

  // ================================availability=================================

  availabilityForm!: FormGroup;
  minDate: Date = new Date();
  availabilityPeriods: any[] = [];

  initAvailabilityForm(): void {
    this.availabilityForm = this.fb.group(
      {
        startDate: ['', Validators.required],
        endDate: ['', Validators.required],
      },
      { validator: this.dateRangeValidator }
    );
  }

  private dateRangeValidator(
    control: AbstractControl
  ): ValidationErrors | null {
    const startDate = control.get('startDate')?.value;
    const endDate = control.get('endDate')?.value;

    if (startDate && endDate) {
      const start = new Date(startDate);
      const end = new Date(endDate);

      if (start > end) {
        return { dateRange: true };
      }
    }
    return null;
  }

  availabilitySubmit(): void {
    if (this.availabilityForm.valid && this.propertyResponse?.id) {
      const formValue = this.availabilityForm.value;
      const requestBody = {
        propertyId: this.propertyResponse.id,
        startDate: new Date(formValue.startDate),
        endDate: new Date(formValue.endDate),
      };

      this.isLoading = true;
      this.addPropertyService.addPropertyAvailability(requestBody).subscribe({
        next: response => {
          this.toastr.showSuccess('Availability period added successfully!');
          this.availabilityPeriods.push({
            startDate: formValue.startDate,
            endDate: formValue.endDate,
          });
          this.availabilityForm.reset();
        },
        error: error => {
          this.toastr.showError(error.error);
          console.error('Error adding availability:', error);
        },
        complete: () => {
          this.isLoading = false;
        },
      });
    }
  }

  // ===========================add property Guests==============================
  guestTypes: IGuestType[] = [];
  guestForm!: FormGroup;
  maxGuests = 10;

  private initGuestForm(): void {
    this.guestForm = this.fb.group({
      guestTypeId: ['', Validators.required],
      guestCount: [
        1,
        [
          Validators.required,
          Validators.min(1),
          Validators.max(this.maxGuests),
        ],
      ],
    });
    this.guestForm.get('guestTypeId')?.disable();
  }

  loadGuestTypes(): void {
    this.isLoading = true;
    this.guestForm.get('guestTypeId')?.disable();
    this.propertyService.getAllGuestTypes().subscribe({
      next: (types: IGuestTypeResponse) => {
        this.guestTypes = types.items;

        if (this.guestTypes.length > 0) {
          this.guestForm.get('guestTypeId')?.enable();
        }
        this.isLoading = false;
      },
      error: (err: any) => {
        console.error('Failed to load guest types', err);
        this.guestForm.get('guestTypeId')?.disable();
        this.isLoading = false;
        this.toastr.showError('Failed to load guest types');
      },
    });
  }

  onGuestSubmit(): void {
    if (this.guestForm.valid && this.propertyResponse?.id) {
      const formValue = this.guestForm.value;
      const requestBody = {
        propertyId: this.propertyResponse.id,
        guestTypeId: formValue.guestTypeId,
        guestCount: formValue.guestCount,
      };

      this.isLoading = true;
      this.addPropertyService.addPropertyGuests(requestBody).subscribe({
        next: response => {
          this.toastr.showSuccess('Guest policy added successfully!');
          this.guestForm.reset({ guestCount: 1 });
        },
        error: error => {
          this.toastr.showError(error.error);
          console.error('Error adding guest policy:', error);
        },
        complete: () => {
          this.isLoading = false;
        },
      });
    }
  }

  incrementGuestCount(): void {
    const current = this.guestForm.get('guestCount')?.value || 0;
    if (current < this.maxGuests) {
      this.guestForm.get('guestCount')?.setValue(current + 1);
    }
  }

  decrementGuestCount(): void {
    const current = this.guestForm.get('guestCount')?.value || 0;
    if (current > 1) {
      this.guestForm.get('guestCount')?.setValue(current - 1);
    }
  }

  getGuestTypeName(guestTypeId: number): string {
    const type = this.guestTypes.find(t => t.id === guestTypeId);
    return type ? type.name : 'Unknown';
  }

  // ==================================== add property photo ==========================================

  uploadedPhotos: { id: string; url: string; file?: File }[] = [];
  isUploading = false;
  uploadProgress = 0;
  isDragOver = false;

  onFileSelected(event: any): void {
    const files: FileList = event.target.files;
    this.previewFiles(files);
  }

  previewFiles(files: FileList): void {
    console.log('Number of files selected:', files.length);
    for (let i = 0; i < files.length; i++) {
      console.log(`File ${i}:`, files[i].name, files[i].size, files[i].type);
    }

    for (let i = 0; i < files.length; i++) {
      const file = files[i];
      if (file.type.match('image.*')) {
        const reader = new FileReader();
        reader.onload = (e: any) => {
          this.uploadedPhotos.push({
            id: 'temp-' + Date.now() + i,
            url: e.target.result,
            file: file,
          });
        };
        reader.readAsDataURL(file);
      }
    }
  }

  uploadPhotos(): void {
    if (!this.propertyResponse?.id || this.uploadedPhotos.length === 0) return;

    this.isUploading = true;
    this.uploadProgress = 0;

    const formData = new FormData();
    formData.append('PropertyId', this.propertyResponse.id);

    const filesToUpload = this.uploadedPhotos
      .filter(photo => photo.file)
      .map(photo => photo.file as File);

    // Append all files at once
    filesToUpload.forEach((file, index) => {
      formData.append('Photos', file, file.name);
    });

    this.addPropertyService
      .uploadPropertyPhotos(formData)
      .pipe(
        tap(event => {
          if (event.type === HttpEventType.UploadProgress) {
            this.uploadProgress = Math.round(
              (100 * event.loaded) / (event.total || 1)
            );
          }
        }),
        finalize(() => (this.isUploading = false))
      )
      .subscribe({
        next: (response: any) => {
          this.toastr.showSuccess('Photos uploaded successfully!');
          if (response.id) {
            this.uploadedPhotos = this.uploadedPhotos.map((photo, index) => ({
              ...photo,
              id: response.photoIds[index] || photo.id,
              file: undefined, // Clear file after upload
            }));
          }
        },
        error: err => {
          this.toastr.showError(err.error);
          console.error('Photo upload error:', err);
        },
      });
  }

  removePhoto(index: number): void {
    this.uploadedPhotos.splice(index, 1);
  }

  onFileDropped(event: DragEvent): void {
    event.preventDefault();
    event.stopPropagation();
    this.isDragOver = false;

    if (event.dataTransfer?.files) {
      this.previewFiles(event.dataTransfer.files);
    }
  }

  onDragOver(event: DragEvent): void {
    event.preventDefault();
    event.stopPropagation();
    this.isDragOver = true;
  }

  onDragLeave(event: DragEvent): void {
    event.preventDefault();
    event.stopPropagation();
    this.isDragOver = false;
  }

  //----------------------------------add property spaces and space items------------------------------

  spaceTypes: any[] = [];
  spaceItems: any[] = [];
  spaceItemTypes: any[] = [];
  newSpaceForm!: FormGroup;
  addSpaceItemForm!: FormGroup;
  selectedSpaceId: string | null = null;

  private initSpaceForms(): void {
    this.newSpaceForm = this.fb.group({
      name: ['', [Validators.required, Validators.maxLength(100)]],
      propertySpaceTypeId: ['', Validators.required],
      // propertySpaceItemId: ['', Validators.required],
      isShared: [false],
    });

    this.addSpaceItemForm = this.fb.group({
      propertySpaceItemTypeId: ['', Validators.required],
      quantity: [1, [Validators.required, Validators.min(1)]],
    });
  }

  loadSpaceData(): void {
    // Load space types
    this.propertyService
      .getPropertySpaces()
      .pipe(
        tap(response => console.log('Raw API response:', response)),
        map(response => (Array.isArray(response) ? response : [])),
        catchError(() => of([]))
      )
      .subscribe(types => (this.spaceTypes = types));

    // Load space item types
    this.propertyService
      .getPropertySpaceItem()
      .pipe(
        tap(response => console.log('Raw API response:', response)),
        map(response => (Array.isArray(response) ? response : [])),
        catchError(() => of([]))
      )
      .subscribe(items => (this.spaceItemTypes = items));
  }

  addSpace(): void {
    if (this.newSpaceForm.valid && this.propertyResponse?.id) {
      const formData = {
        ...this.newSpaceForm.value,
        propertyId: this.propertyResponse.id,
      };

      this.addPropertyService.addPropertySpace(formData).subscribe({
        next: response => {
          this.toastr.showSuccess('Space added successfully!');
          this.spaceTypes.push(response);
          this.newSpaceForm.reset();
          this.selectedSpaceId = response.propertySpaceTypeId;
          console.log('space id ', this.selectedSpaceId);
        },
        error: (err: any) => this.toastr.showError(err.error),
      });
    }
  }

  addSpaceItem(): void {
    if (this.addSpaceItemForm.valid && this.selectedSpaceId) {
      const formData = {
        ...this.addSpaceItemForm.value,
        propertySpaceId: this.selectedSpaceId.toString(),
      };

      this.addPropertyService.addSpaceItem(formData).subscribe({
        next: response => {
          this.toastr.showSuccess('Item added to space!');
          const space = this.spaceTypes.find(
            s => s.id === this.selectedSpaceId
          );
          if (space) {
            space.items = space.items || [];
            space.items.push(response);
          }
          this.addSpaceItemForm.reset({ quantity: 1 });
        },
        error: (err: any) => this.toastr.showError(err.error),
      });
    }
  }

  selectSpace(spaceId: string): void {
    this.selectedSpaceId = spaceId;
  }

  getSelectedSpace(): any {
    return this.spaceTypes.find(s => s.id === this.selectedSpaceId);
  }
}
