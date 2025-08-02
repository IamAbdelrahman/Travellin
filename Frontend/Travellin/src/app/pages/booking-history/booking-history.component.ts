import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { CheckOutBookingService } from '../../services/check-out-booking.service';
import { CancellationService, CancellationRequest } from '../../services/cancellation.service';
import { ToastService } from '../../services/toast.service';
import { Bookings, GetBookingsResponse } from '../../models/api/request/iget-bookings';

@Component({
  selector: 'app-booking-history',
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './booking-history.component.html',
  styleUrl: './booking-history.component.scss',
})
export class BookingHistoryComponent implements OnInit {
  bookings: Bookings[] = [];
  filteredBookings: Bookings[] = [];
  loading = false;
  currentPage = 1;
  totalPages = 1;
  pageSize = 10;

  // Search and filter properties
  searchQuery = '';
  selectedStatus = 'all';
  selectedFilter = 'all';

  constructor(
    private checkOutService: CheckOutBookingService,
    private cancellationService: CancellationService,
    private toastService: ToastService
  ) { }

  ngOnInit(): void {
    this.loadBookings();
  }

  loadBookings(): void {
    this.loading = true;
    this.checkOutService.getAllBookings().subscribe({
      next: (response: any) => {
        this.bookings = response.body?.items || [];
        this.totalPages = response.body?.metaData?.total ? Math.ceil(response.body.metaData.total / this.pageSize) : 1;
        this.filteredBookings = [...this.bookings];
        this.loading = false;

      },
      error: (error: any) => {
        console.error('Error loading bookings:', error);
        this.toastService.showError('Failed to load bookings');
        this.loading = false;
      },
    });
  }

  // Search functionality
  searchBookings(): void {
    if (!this.searchQuery.trim()) {
      this.filteredBookings = [...this.bookings];
      return;
    }

    this.filteredBookings = this.bookings.filter(booking =>
      booking.property?.title?.toLowerCase().includes(this.searchQuery.toLowerCase()) ||
      booking.property?.location?.name?.toLowerCase().includes(this.searchQuery.toLowerCase())
    );
  }

  // Filter functionality
  applyFilters(): void {
    let filtered = [...this.bookings];

    // Apply status filter
    if (this.selectedStatus !== 'all') {
      filtered = filtered.filter(booking => booking.status === this.selectedStatus);
    }

    // Apply search filter
    if (this.searchQuery.trim()) {
      filtered = filtered.filter(booking =>
        booking.property?.title?.toLowerCase().includes(this.searchQuery.toLowerCase()) ||
        booking.property?.location?.name?.toLowerCase().includes(this.searchQuery.toLowerCase())

      );
    }

    this.filteredBookings = filtered;
  }

  // Enhanced cancellation with refund support
  async cancelBookingEnhanced(booking: Bookings) {
    try {
      // First check if booking can be cancelled
      const canCancelResponse = await this.cancellationService.canCancelBooking(booking.id).toPromise();

      if (!canCancelResponse?.canCancel) {
        this.toastService.showWarning('This booking cannot be cancelled at this time.');
        return;
      }

      // Show refund information
      const refundInfo = canCancelResponse.refundAmount > 0
        ? `You will receive a refund of $${canCancelResponse.refundAmount.toFixed(2)}.`
        : 'No refund will be issued.';

      if (!confirm(`Are you sure you want to cancel this booking? ${refundInfo}`)) {
        return;
      }

      const cancellationRequest: CancellationRequest = {
        bookingId: booking.id,
        cancelledByUserId: localStorage.getItem('userId') || '',
        isHostCancellation: false,
        cancellationReason: 'Cancelled by guest'
      };

      this.cancellationService.cancelBookingEnhanced(cancellationRequest).subscribe({
        next: (result) => {
          if (result.isSuccessful) {
            this.toastService.showSuccess(result.message);
            this.loadBookings(); // Refresh the list
          } else {
            this.toastService.showError(result.message);
          }
        },
        error: (error) => {
          console.error('Cancellation failed:', error);
          this.toastService.showError('Failed to cancel booking');
        },
      });
    } catch (error) {
      console.error('Error checking cancellation status:', error);
      this.toastService.showError('Failed to check cancellation status');
    }
  }

  // Legacy cancellation (existing functionality)
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

  // New method to show cancellation options
  showCancellationOptions(booking: Bookings) {
    const status = booking.status.toLowerCase();

    if (status === 'pending') {
      // Use legacy cancellation for pending bookings
      this.cancelBooking(booking);
    } else if (status === 'confirmed') {
      // Use enhanced cancellation for confirmed bookings
      this.cancelBookingEnhanced(booking);
    } else {
      this.toastService.showWarning('This booking cannot be cancelled.');
    }
  }

  // Get guest count for display
  getGuestCount(booking: Bookings): number {
    if (!booking.bookingGuests || booking.bookingGuests.length === 0) {
      return 0;
    }
    return booking.bookingGuests.reduce((total, guest) => total + guest.guestCount, 0);
  }

  calculateNumberOfNights(checkIn: string, checkOut: string): number {
    const checkInDate = new Date(checkIn);
    const checkOutDate = new Date(checkOut);
    const timeDiff = checkOutDate.getTime() - checkInDate.getTime();
    return Math.ceil(timeDiff / (1000 * 3600 * 24));
  }

  calculateTotalWithFees(booking: Bookings): number {
    const nights = this.calculateNumberOfNights(booking.checkIn, booking.checkOut);
    const basePrice = booking.property?.pricePerNight || 0;
    const fees = booking.totalFees || 0;
    return (basePrice * nights) + fees;
  }

  getStatusColor(status: string): string {
    switch (status.toLowerCase()) {
      case 'pending':
        return 'text-yellow-600 bg-yellow-100';
      case 'confirmed':
        return 'text-green-600 bg-green-100';
      case 'cancelled':
        return 'text-red-600 bg-red-100';
      case 'declined':
        return 'text-red-600 bg-red-100';
      case 'completed':
        return 'text-blue-600 bg-blue-100';
      default:
        return 'text-gray-600 bg-gray-100';
    }
  }

}
