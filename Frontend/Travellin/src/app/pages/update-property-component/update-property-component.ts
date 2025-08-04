import { ToastMessage, ToastService } from './../../services/toast.service';
import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { PropertyService } from '../../services/property.service';
import { ReactiveFormsModule } from '@angular/forms';
import { Property } from './../../models/api/request/iget-bookings';

@Component({
  selector: 'app-update-property-component',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './update-property-component.html',
  styleUrl: './update-property-component.sass'
})
export class UpdatePropertyComponent implements OnInit {
  @Input() propertyId!: string;
  @Input() currentTitle: string = '';
  @Input() currentDescription: string = '';
  @Input() currentPricePerNight!: number;
  @Input() currentLatitude!: number;
  @Input() currentLongitude!: number;


  @Output() closed = new EventEmitter<void>();

  form!: FormGroup;
  isUpdating = false;
  successMessage = '';
  errorMessage = '';

  constructor(private fb: FormBuilder, private propertyService: PropertyService, private toaster: ToastService   // ✅ Add this
  ) { }

  ngOnInit(): void {
    this.form = this.fb.group({
      title: [this.currentTitle, [
        Validators.required,
        Validators.minLength(5),
        Validators.maxLength(100)
      ]],
      pricePerNight: [this.currentPricePerNight, [Validators.required, Validators.min(0.01)]],
      latitude: [this.currentLatitude, [Validators.required, Validators.min(-90), Validators.max(90)]],
      longitude: [this.currentLongitude, [Validators.required, Validators.min(-180), Validators.max(180)]],
    });
  }

  closePopup() {
    this.closed.emit();
  }

  onSubmit(): void {
    if (this.form.invalid || !this.propertyId) return;

    this.isUpdating = true;
    this.successMessage = '';
    this.errorMessage = '';

    const dto = this.form.value;

    this.propertyService.updateProperty(this.propertyId, dto).subscribe({
      next: () => {
        this.toaster.showSuccess('Property updated successfully!');
        window.location.reload();
        this.isUpdating = false;

      },
      error: (err) => {
        this.errorMessage = 'Update failed.';
        this.isUpdating = false;
        console.error(err);
      }
    });
  }
}
