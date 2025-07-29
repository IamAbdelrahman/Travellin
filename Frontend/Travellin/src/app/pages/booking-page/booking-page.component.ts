import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { CheckOutBookingService } from '../../services/check-out-booking.service';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpHeaders, HttpResponse } from '@angular/common/http';
import {
  Bookings,
  GetBookingsResponse,
} from '../../models/api/request/iget-bookings';
import { PropertyDetails } from '../../models/api/request/iget-propertiesDetails';
import { ICheckoutBookingRequest } from '../../models/api/request/ICheckoutBookingRequest';
import { ToastService } from '../../services/toast.service';

@Component({
  selector: 'app-booking-page',
  imports: [CommonModule, FormsModule],
  templateUrl: './booking-page.component.html',
  styleUrl: './booking-page.component.scss',
})
export class BookingPageComponent implements OnInit {
  //overlay
  isOverlayOpen = false;
  overlayType: 'guest' | 'date' | null = null;
  checkInDate: string = '';
  checkOutDate: string = '';
  savedCheckIn: string = '';
  savedCheckOut: string = '';
  adults = 1;
  children = 0;
  infants = 0;
  pets = 0;
  savedAdults = 1;
  savedChildren = 0;
  savedInfants = 0;
  savedPets = 0;
  isCheckingOut = false;
  isBooking = false;

  openOverlay(type: 'guest' | 'date'): void {
    // this.isOverlayOpen = true;
    this.overlayType = type;
    //dates
    this.checkInDate = this.savedCheckIn;
    this.checkOutDate = this.savedCheckOut;
    //guests
    this.adults = this.savedAdults;
    this.children = this.savedChildren;
    this.infants = this.savedInfants;
    this.pets = this.savedPets;
  }
  //add guests
  increment(type: string): void {
    if (type === 'adults') {
      this.adults++;
    } else if (type === 'children') {
      this.children++;
    } else if (type === 'infants') {
      this.infants++;
    } else if (type === 'pets') {
      this.pets++;
    }
  }
  // decrease guests number
  decrement(type: string): void {
    if (type === 'adults' && this.adults > 1) {
      this.adults--;
    } else if (type === 'children' && this.children > 0) {
      this.children--;
    } else if (type === 'infants' && this.infants > 0) {
      this.infants--;
    } else if (type === 'pets' && this.pets > 0) {
      this.pets--;
    }
  }
  closeOverlay(): void {
    // this.isOverlayOpen = false;7
    this.overlayType = null;
  }
  saveChanges(): void {
    //dates
    this.savedCheckIn = this.checkInDate;
    this.savedCheckOut = this.checkOutDate;
    //guests
    this.savedAdults = this.adults;
    this.savedChildren = this.children;
    this.savedInfants = this.infants;
    this.savedPets = this.pets;

    this.closeOverlay();
  }
  get totalGuests(): string {
    const total =
      this.savedAdults + this.savedChildren + this.savedInfants + this.pets;
    if (total === 0) return 'Add guests';
    return `${total} guest${total > 1 ? 's' : ''}`;
  }

  //----------------------------------Api Integration----------------------------------------------------------

  constructor(
    private checkOutService: CheckOutBookingService,
    private route: ActivatedRoute,
    private router: Router,
    private toaster: ToastService
  ) {
    this.selectedBookingId = this.route.snapshot.paramMap.get('id')!;
  }

  bookings: Bookings[] = [];
  propertyDetails: PropertyDetails | null = null;
  propertyId: string = '';
  selectedBookingId: string = '';
  ngOnInit(): void {
    this.loadBookingDetails(this.selectedBookingId);
  }

  loadBookingDetails(bookingId: string): void {
    this.checkOutService.getBookingById(bookingId).subscribe({
      next: response => {
        this.bookings = [response];
        this.loadPropertyDetails(this.bookings[0].property.id);
      },
      error: err => {
        console.error('Error loading booking details', err);
      },
    });
  }

  loadPropertyDetails(propertyId: string) {
    this.checkOutService.getPropertyDetails(propertyId).subscribe({
      next: response => {
        this.propertyDetails = response;
        console.log('Property Details:', this.propertyDetails);
      },
      error: err => {
        console.error('Error loading property details', err);
      },
    });
  }


  get hasBookingWithPhotos(): boolean {
    return (
      this.bookings &&
      this.bookings.length > 0 &&
      this.bookings[0] &&
      this.bookings[0].property &&
      this.bookings[0].property.photos &&
      this.bookings[0].property.photos.length > 0
    );
  }
  get firstBookingPhotoUrl(): string {
    return this.bookings?.[0]?.property?.photos?.[0]?.photoUrl;
  }
  get firstGuestCount(): number | null {
    return this.bookings?.[0]?.bookingGuests?.[0]?.guestCount ?? null;
  }

  // calculate nights function

  calculateNumberOfNights(
    checkIn: string | undefined,
    checkOut: string | undefined
  ): number | null {
    if (!checkIn || !checkOut) {
      return null;
    }

    const checkInDate = new Date(checkIn);
    const checkOutDate = new Date(checkOut);

    const differenceInTime = checkOutDate.getTime() - checkInDate.getTime();

    const numberOfNights = differenceInTime / (1000 * 3600 * 24); // 1000ms * 3600s * 24h
    return numberOfNights;
  }
  // to calculate total price (price per night * number pf nights)
  calculateTotalPrice(
    checkIn: string | undefined,
    checkOut: string | undefined
  ): number | null {
    if (!this.bookings.length || !this.bookings[0].property?.pricePerNight) {
      return null;
    }

    const pricePerNight = this.bookings[0].property.pricePerNight;
    const numberOfNights = this.calculateNumberOfNights(checkIn, checkOut);

    if (numberOfNights === null) {
      return null;
    }

    return pricePerNight * numberOfNights;
  }
  // to calculate total price
  calculateTotal(
    checkIn: string | undefined,
    checkOut: string | undefined
  ): number | null {
    if (!this.bookings.length) {
      return null;
    }

    const pricePerNight = this.bookings[0].property?.pricePerNight;
    const totalFees = this.bookings[0].totalFees;

    if (pricePerNight === undefined || totalFees === undefined) {
      return null;
    }

    const numberOfNights = this.calculateNumberOfNights(checkIn, checkOut);
    if (numberOfNights === null) {
      return null;
    }

    return pricePerNight * numberOfNights + totalFees;
  }
  createBooking() {
    if (!this.checkInDate || !this.checkOutDate) {
      alert('Please select both check-in and check-out dates.');
      return;
    }

    const bookingData = {
      checkIn: this.checkInDate,
      checkOut: this.checkOutDate,
    };

    this.checkOutService.createBooking(bookingData).subscribe({
      next: response => {
        console.log('Booking created successfully:', response);
        alert('Booking created successfully!');
      },
      error: err => {
        console.error('Error creating booking:', err);
        alert('Failed to create booking.');
      },
    });
  }

onCheckOut(): void {
  this.isCheckingOut = true;

  const booking = this.bookings[0]; // Assume you have a bookings array
  if (!booking || !booking.property) {
    alert('Booking data is incomplete.');
    this.isCheckingOut = false;
    return;
  }

  const email = localStorage.getItem('email') || 'guest@example.com';
  const userId = localStorage.getItem('userId') || 'default-id';

  const requestBody: ICheckoutBookingRequest = {
    bookingId: booking.id,
    guest: {
      id: userId,
      email: email,
    },
    property: {
      id: booking.property.id,
      title: booking.property.title,
      description:  '',
      mainPhotoUrl: booking.property.photos?.[0]?.photoUrl || '',
      locationName: booking.property.location?.name || '',
    },
    bookingPeriod: {
      checkInDate: booking.checkIn,
      checkOutDate: booking.checkOut,
      nights: this.calculateNumberOfNights(booking.checkIn, booking.checkOut) || 1,
    },
    pricing: {
      pricePerNight: booking.property.pricePerNight,
      totalFees: booking.totalFees,
      totalAmount: this.calculateTotal(booking.checkIn, booking.checkOut)??0,
    },
    metadata: {
      additionalProp1: 'test1',
      additionalProp2: 'test2',
      additionalProp3: 'test3',
    },
    totalAmount: this.calculateTotal(booking.checkIn, booking.checkOut)??0,
  };

  this.checkOutService.checkOut(requestBody).subscribe({
    next: (res) => {
      if (res?.sessionUrl) {
        window.location.href = res.sessionUrl;
      } else {
        alert('Stripe session URL not received.');
        this.isCheckingOut = false;
      }
    },
    error: (err) => {
      console.error('Checkout failed:', err);
      alert('Checkout failed.');
      this.isCheckingOut = false;
    }
  });
}


}