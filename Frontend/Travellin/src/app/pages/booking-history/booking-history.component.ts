import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import {
  Bookings,
  GetBookingsResponse,
} from '../../models/api/request/iget-bookings';
import { CheckOutBookingService } from '../../services/check-out-booking.service';
import { HttpErrorResponse, HttpResponse } from '@angular/common/http';
import { ToastService } from '../../services/toast.service';
import { IRole } from '../../models/domain/iuser-role';
import { Router, RouterModule } from '@angular/router';

@Component({
  selector: 'app-booking-history',
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './booking-history.component.html',
  styleUrl: './booking-history.component.scss',
})
export class BookingHistoryComponent implements OnInit {
  allBookings: Bookings[] = []; // Store original bookings
  filteredBookings: Bookings[] = []; // Store filtered bookings
  selectedStatus: string = 'all';
  selectedFilter: string = 'checkIn-desc';
  searchQuery: string = '';
  userRole: string | undefined;

  constructor(
    private checkOutService: CheckOutBookingService,
    private toastService: ToastService,
    private route: Router
  ) {}

  ngOnInit(): void {
    this.loadBookings();
    this.loadUserProfile();
  }

  loadBookings(): void {
    this.checkOutService.getAllBookings().subscribe({
      next: (response: HttpResponse<GetBookingsResponse>) => {
        this.allBookings = response?.body?.items || [];
        this.applyFilters(); // Apply filters when data loads
      },
      error: (err: HttpErrorResponse) => {
        console.error('Booking load error:', err);
        this.allBookings = [];
        this.filteredBookings = [];
        this.toastService.showError('Failed to load bookings');
      },
    });
  }

  loadUserProfile(): void {
    this.checkOutService.getUserRole().subscribe({
      next: (response: HttpResponse<IRole>) => {
        this.userRole = response?.body?.name || undefined;
      },
      error: (err: HttpErrorResponse) => {
        console.error('Role load error:', err);
      },
    });
  }

  // Get guest count for specific booking
  getGuestCount(booking: Bookings): number {
    return booking?.bookingGuests?.[0]?.guestCount || 0;
  }

  // Apply all filters (status, search, sort)
  applyFilters(): void {
    // Start with all bookings
    let filtered = [...this.allBookings];

    // Apply status filter
    if (this.selectedStatus !== 'all') {
      filtered = filtered.filter(
        booking => booking?.status === this.selectedStatus
      );
    }

    // Apply search filter
    if (this.searchQuery) {
      const query = this.searchQuery.toLowerCase();
      filtered = filtered.filter(
        booking =>
          booking?.property?.title?.toLowerCase().includes(query) ||
          booking?.property?.location?.name?.toLowerCase().includes(query) ||
          booking?.property?.owner?.userName?.toLowerCase().includes(query)
      );
    }

    // Apply sorting
    this.applySort(filtered);

    this.filteredBookings = filtered;
  }

  // Apply sorting to the provided array
  applySort(bookings: Bookings[]): void {
    switch (this.selectedFilter) {
      case 'checkIn-desc':
        bookings.sort(
          (a, b) =>
            new Date(b.checkIn).getTime() - new Date(a.checkIn).getTime()
        );
        break;
      case 'checkIn-asc':
        bookings.sort(
          (a, b) =>
            new Date(a.checkIn).getTime() - new Date(b.checkIn).getTime()
        );
        break;
      case 'price-asc':
        bookings.sort(
          (a, b) =>
            (a.property?.pricePerNight || 0) - (b.property?.pricePerNight || 0)
        );
        break;
      case 'price-desc':
        bookings.sort(
          (a, b) =>
            (b.property?.pricePerNight || 0) - (a.property?.pricePerNight || 0)
        );
        break;
    }
  }

  searchBookings(): void {
    this.applyFilters(); // Now uses the unified filter approach
  }

  calculateNumberOfNights(checkIn: string, checkOut: string): number {
    if (!checkIn || !checkOut) return 0;

    const checkInDate = new Date(checkIn);
    const checkOutDate = new Date(checkOut);

    // Ensure valid dates and check-out is after check-in
    if (
      isNaN(checkInDate.getTime()) ||
      isNaN(checkOutDate.getTime()) ||
      checkOutDate <= checkInDate
    ) {
      return 0;
    }

    const timeDiff = checkOutDate.getTime() - checkInDate.getTime();
    return Math.max(1, Math.floor(timeDiff / (1000 * 3600 * 24))); // At least 1 night
  }

  calculateTotalPrice(booking: Bookings): number {
    if (!booking?.property?.pricePerNight) return 0;

    const nights = this.calculateNumberOfNights(
      booking.checkIn,
      booking.checkOut
    );
    return booking.property.pricePerNight * nights;
  }

  calculateTotalWithFees(booking: Bookings): number {
    const basePrice = this.calculateTotalPrice(booking);
    return basePrice + (booking.totalFees || 0);
  }

  cancelBooking(booking: Bookings) {
    if (booking.status.toLowerCase() === 'pending') {
      this.checkOutService.cancelBookings(booking.id).subscribe({
        next: () => {
          this.toastService.showSuccess('Booking cancelled successfully!');
          this.loadBookings(); // Refresh the list
        },
        error: () => {
          this.toastService.showError('Failed to cancel booking!');
        },
      });
    } else {
      this.toastService.showWarning('Only pending bookings can be cancelled.');
    }
  }
}
