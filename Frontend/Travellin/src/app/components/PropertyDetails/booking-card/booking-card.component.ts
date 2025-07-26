import { Component, Input, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { IPropertyInfo } from '../../../models/domain/iproperty-info';
import { PropertyService } from '../../../services/property.service';
import { IBookingResponse } from '../../../models/api/response/ibooking-response';
import { IBookingRequest } from '../../../models/api/request/ibooking-req';
import { IPropertyFees } from '../../../models/domain/iproperty-fees';
import { BookingService } from '../../../services/booking.service';
import { IBookingSendingRequest } from '../../../models/api/request/ibooking-sending-req';
import { IGuestType } from '../../../models/domain/iguest-type';
import { IGuestTypeResponse } from '../../../models/api/response/iguest-type-response';
import { ToastContainerComponent } from '../../toast-container/toast-container.component';
import { ToastService } from '../../../services/toast.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-booking-card',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './booking-card.component.html',
  styleUrls: ['./booking-card.component.scss'],
})
export class BookingCardComponent implements OnInit {
  @Input({ required: true }) property!: IPropertyInfo;

  constructor(
    private propertyService: PropertyService,
    private bookingService: BookingService,
    private toaster: ToastService,
    private router: Router
  ) {}
  ngOnInit(): void {
    this.loadFees(this.property.id);
    this.loadGuestTypes();
  }

  checkInDate: string = '';
  checkOutDate: string = '';
  guests: number = 1;
  selectedGuestType: number = 1;
  bookingData: IBookingSendingRequest = {
    propertyId: '',
    checkIn: '',
    checkOut: '',
    guests: [],
  };
  get today(): string {
    return new Date().toISOString().split('T')[0];
  }

  get guestOptions(): number[] {
    const max = Math.min(this.property.maxGuestCount || 10, 16);
    return Array.from({ length: max }, (_, i) => i + 1);
  }

  get nights(): number {
    if (!this.checkInDate || !this.checkOutDate) return 0;
    const diff =
      new Date(this.checkOutDate).getTime() -
      new Date(this.checkInDate).getTime();
    return Math.ceil(diff / (1000 * 60 * 60 * 24));
  }

  get subtotal(): number {
    return this.nights * (this.property.pricePerNight || 0);
  }

  get total(): number {
    return this.subtotal + this.cleaningFee + this.serviceFee;
  }

  // ======================================Guest selection state=====================================
  showGuestModal = false;
  guestCounts: Record<number, number> = {};
  guestTypes: IGuestType[] = [];
  loadingGuestTypes = false;

  // Initialize guest counts to 0 when guest types are loaded
  initializeGuestCounts(guestTypes: IGuestType[]): void {
    guestTypes.forEach(type => {
      this.guestCounts[type.id] = 0;
    });
  }

  // Fetch guest types from API
  loadGuestTypes(): void {
    this.loadingGuestTypes = true;
    this.propertyService.getAllGuestTypes().subscribe({
      next: response => {
        if (response.body) {
          this.guestTypes = response.body.items;
          this.initializeGuestCounts(this.guestTypes);
        }
        this.loadingGuestTypes = false;
      },
      error: error => {
        console.error('Failed to load guest types:', error);
        this.loadingGuestTypes = false;
      },
    });
  }

  get totalGuests(): number {
    // Sum all guest counts except infants and pets if they shouldn't count
    return Object.entries(this.guestCounts).reduce((total, [id, count]) => {
      const numericId = Number(id);
      // Only count adults and children (assuming IDs 1 and 2)
      return numericId === 1 || numericId === 2 ? total + count : total;
    }, 0);
  }

  get guestSummary(): string {
    if (this.loadingGuestTypes) return 'Loading...';
    if (this.guestTypes.length === 0) return 'Add guests';

    const parts = this.guestTypes
      .filter(type => this.guestCounts[type.id] > 0)
      .map(type => {
        const count = this.guestCounts[type.id];
        return `${count} ${type.name.toLowerCase()}${count !== 1 ? 's' : ''}`;
      });

    return parts.join(', ') || 'Add guests';
  }

  decrementGuestCount(typeId: number, min: number = 0): void {
    this.guestCounts[typeId] = Math.max(min, this.guestCounts[typeId] - 1);
  }

  incrementGuestCount(typeId: number, max?: number): void {
    if (max !== undefined && this.guestCounts[typeId] >= max) return;
    this.guestCounts[typeId] = (this.guestCounts[typeId] || 0) + 1;
  }

  //==========================fees======================================
  cleaningFee: number = 0;
  serviceFee: number = 0;
  extraGuestFee: number = 0;
  petFee: number = 0;

  loadFees(propertyId: string) {
    this.propertyService.getPropertyFeesById(propertyId).subscribe({
      next: response => {
        (response.body.items as IPropertyFees[]).forEach(fee => {
          switch (fee.name.toLowerCase()) {
            case 'cleaning fee':
              this.cleaningFee = fee.amount;
              break;
            case 'service fee':
              this.serviceFee = fee.amount;
              break;
            case 'extra guest fee':
              this.extraGuestFee = fee.amount;
              break;
            case 'pet fee':
              this.petFee = fee.amount;
              break;
          }

          console.log(
            `fees${(this.cleaningFee, this.serviceFee, this.extraGuestFee, this.petFee)}`
          );
        });
      },
      error: err => {
        console.error('Error loading fees:', err);
      },
    });
  }
  // =============================booking reservation=================================
  isBooking: boolean = false;
  bookingError: string | null = null;

  handleBookNow(): void {
    if (!this.checkInDate || !this.checkOutDate) return;

    this.isBooking = true;
    this.bookingError = null;

    this.bookingData = {
      propertyId: this.property.id,
      checkIn: this.checkInDate,
      checkOut: this.checkOutDate,
      guests: Object.entries(this.guestCounts)
        .filter(([_, count]) => count > 0)
        .map(([typeId, count]) => ({
          guestTypeId: Number(typeId),
          guestCount: count,
        })),
    };

    console.log('Booking data:', this.bookingData);

    this.bookingService.createBooking(this.bookingData).subscribe({
      next: (response: any) => {
        this.isBooking = false;

        if (response.status === 201) {
          this.toaster.showSuccess('Booking created successfully!');
          setTimeout(() => {
            this.router.navigateByUrl(`/booking/${response.body.id}`);
          }, 1000);
        }
      },
      error: error => {
        this.isBooking = false;
        this.toaster.showError('Booking failed!');

        if (error.status === 409) {
          this.toaster.showError(error.error);
        } else if (error.status === 401) {
          this.toaster.showError('Unauthorized. Please login.');
        } else {
          this.toaster.showError('Booking failed. Please try again.');
        }
      },
    });
  }
}
