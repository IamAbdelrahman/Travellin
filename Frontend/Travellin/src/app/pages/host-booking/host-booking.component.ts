import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { BookingManagementService } from '../../services/booking-management.service';
import { CancellationService } from '../../services/cancellation.service';
import { ToastService } from '../../services/toast.service';

@Component({
  selector: 'app-host-booking',
  imports: [CommonModule, FormsModule],
  templateUrl: './host-booking.component.html',
  styleUrl: './host-booking.component.scss',
})
export class HostBookingComponent implements OnInit {
  bookings: any[] = [];
  pendingBookings: any[] = [];
  loading = false;
  currentPage = 1;
  totalPages = 1;
  pageSize = 10;
  pendingCount = 0;
  selectedFilter = 'all'; // 'all', 'pending', 'confirmed', 'cancelled'
  selectedBooking: any = null; // For modal display

  constructor(
    private bookingManagementService: BookingManagementService,
    private cancellationService: CancellationService,
    private toastService: ToastService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.loadBookings();
    this.loadPendingCount();
  }

  loadBookings(): void {
    this.loading = true;

    if (this.selectedFilter === 'pending') {
      console.log('Loading pending bookings for host...');
      this.bookingManagementService.getHostPendingBookings(this.currentPage, this.pageSize).subscribe({
        next: (response) => {
          console.log('Pending bookings response:', response);
          this.bookings = response.items;
          this.totalPages = Math.ceil(response.metaData.total / this.pageSize);
          this.loading = false;
        },
        error: (error) => {
          console.error('Error loading pending bookings:', error);
          console.error('Error details:', error.error);
          this.toastService.showError(`Failed to load bookings: ${error.error?.message || error.message}`);
          this.loading = false;
        },
      });
    } else {
      console.log('Loading all bookings for host with filter:', this.selectedFilter);
      // Only pass status parameter if it's not 'all'
      const status = this.selectedFilter === 'all' ? undefined : this.selectedFilter;
      this.bookingManagementService.getHostBookings(this.currentPage, this.pageSize, status).subscribe({
        next: (response) => {
          console.log('All bookings response:', response);
          this.bookings = response.items;
          this.totalPages = Math.ceil(response.metaData.total / this.pageSize);
          this.loading = false;
        },
        error: (error) => {
          console.error('Error loading bookings:', error);
          console.error('Error details:', error.error);
          this.toastService.showError(`Failed to load bookings: ${error.error?.message || error.message}`);
          this.loading = false;
        },
      });
    }
  }

  loadPendingCount(): void {
    console.log('Loading pending count for host...');
    this.bookingManagementService.getHostPendingCount().subscribe({
      next: (count) => {
        console.log('Pending count response:', count);
        this.pendingCount = count;
      },
      error: (error) => {
        console.error('Error loading pending count:', error);
        console.error('Error details:', error.error);
        this.pendingCount = 0;
      },
    });
  }

  onFilterChange(): void {
    this.currentPage = 1;
    this.loadBookings();
  }

  acceptBooking(booking: any): void {
    this.bookingManagementService.acceptBooking(booking.id).subscribe({
      next: () => {
        this.toastService.showSuccess('Booking accepted successfully!');
        this.loadBookings();
        this.loadPendingCount();
      },
      error: (error) => {
        console.error('Error accepting booking:', error);
        this.toastService.showError('Failed to accept booking');
      },
    });
  }

  declineBooking(booking: any): void {
    if (confirm('Are you sure you want to decline this booking?')) {
      this.bookingManagementService.declineBooking(booking.id).subscribe({
        next: () => {
          this.toastService.showSuccess('Booking declined successfully!');
          this.loadBookings();
          this.loadPendingCount();
        },
        error: (error) => {
          console.error('Error declining booking:', error);
          this.toastService.showError('Failed to decline booking');
        },
      });
    }
  }

  async cancelBookingAsHost(booking: any): Promise<void> {
    try {
      const cancellationRequest = {
        bookingId: booking.id,
        cancelledByUserId: localStorage.getItem('userId') || '',
        isHostCancellation: true,
        cancellationReason: 'Cancelled by host'
      };

      this.cancellationService.cancelBookingEnhanced(cancellationRequest).subscribe({
        next: (result) => {
          if (result.isSuccessful) {
            this.toastService.showSuccess(result.message);
            this.loadBookings();
            this.loadPendingCount();
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
      console.error('Error cancelling booking:', error);
      this.toastService.showError('Failed to cancel booking');
    }
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

  calculateNumberOfNights(checkIn: string, checkOut: string): number {
    const checkInDate = new Date(checkIn);
    const checkOutDate = new Date(checkOut);
    const timeDiff = checkOutDate.getTime() - checkInDate.getTime();
    return Math.ceil(timeDiff / (1000 * 3600 * 24));
  }

  calculateTotalWithFees(booking: any): number {
    const nights = this.calculateNumberOfNights(booking.checkIn, booking.checkOut);
    const basePrice = booking.property?.pricePerNight || 0;
    const fees = booking.totalFees || 0;
    return (basePrice * nights) + fees;
  }

  previousPage(): void {
    if (this.currentPage > 1) {
      this.currentPage--;
      this.loadBookings();
    }
  }

  nextPage(): void {
    if (this.currentPage < this.totalPages) {
      this.currentPage++;
      this.loadBookings();
    }
  }

  get confirmedBookingsCount(): number {
    return this.bookings?.filter(b => b.status === 'Confirmed').length || 0;
  }

  viewBooking(booking: any): void {
    // Show booking details in a detailed toast
    const nights = this.calculateNumberOfNights(booking.checkIn, booking.checkOut);
    const totalAmount = this.calculateTotalWithFees(booking);

    const details = `
Booking Details:
- ID: ${booking.id || 'N/A'}
- Guest: ${booking.user?.firstName || 'N/A'} ${booking.user?.lastName || 'N/A'}
- Property: ${booking.property?.title || 'N/A'}
- Location: ${booking.property?.location?.name || 'N/A'}
- Check-in: ${booking.checkIn ? new Date(booking.checkIn).toLocaleDateString() : 'N/A'}
- Check-out: ${booking.checkOut ? new Date(booking.checkOut).toLocaleDateString() : 'N/A'}
- Nights: ${nights}
- Price per night: $${booking.pricePerNight || 0}
- Total fees: $${booking.totalFees || 0}
- Total amount: $${totalAmount.toFixed(2)}
- Status: ${booking.status || 'N/A'}
- Created: ${booking.createdAt ? new Date(booking.createdAt).toLocaleDateString() : 'N/A'}
    `.trim();

    // Show as info toast with longer duration for detailed info
    this.toastService.showInfo(details, 8000);

    // Also log to console for debugging
    console.log('Viewing booking:', booking);
  }
} 