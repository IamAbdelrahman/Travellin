import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { BookingManagementService } from '../../services/booking-management.service';
import { CancellationService } from '../../services/cancellation.service';
import { ToastService } from '../../services/toast.service';

@Component({
  selector: 'app-admin-dashboard',
  imports: [CommonModule, FormsModule],
  templateUrl: './admin-dashboard.component.html',
  styleUrl: './admin-dashboard.component.scss',
})
export class AdminDashboardComponent implements OnInit {
  bookings: any[] = [];
  loading = false;
  currentPage = 1;
  totalPages = 1;
  pageSize = 10;
  selectedFilter = 'all'; // 'all', 'pending', 'confirmed', 'cancelled'
  selectedBooking: any = null; // For modal display

  // Computed properties for template
  get pendingBookingsCount(): number {
    return this.bookings?.filter(b => b.status === 'Pending').length || 0;
  }

  get confirmedBookingsCount(): number {
    return this.bookings?.filter(b => b.status === 'Confirmed').length || 0;
  }

  get totalBookingsCount(): number {
    return this.bookings?.length || 0;
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
- Host: ${booking.property?.owner?.firstName || 'N/A'} ${booking.property?.owner?.lastName || 'N/A'}
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

  constructor(
    private bookingManagementService: BookingManagementService,
    private cancellationService: CancellationService,
    private toastService: ToastService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.loadBookings();
  }

  loadBookings(): void {
    this.loading = true;
    
    if (this.selectedFilter === 'pending') {
      this.bookingManagementService.getAdminPendingBookings(this.currentPage, this.pageSize).subscribe({
        next: (response) => {
          this.bookings = response.items;
          this.totalPages = Math.ceil(response.metaData.total / this.pageSize);
          this.loading = false;
        },
        error: (error) => {
          console.error('Error loading pending bookings:', error);
          this.toastService.showError('Failed to load bookings');
          this.loading = false;
        },
      });
    } else {
      this.bookingManagementService.getAllBookingsForAdmin(this.currentPage, this.pageSize, this.selectedFilter).subscribe({
        next: (response) => {
          this.bookings = response.items;
          this.totalPages = Math.ceil(response.metaData.total / this.pageSize);
          this.loading = false;
        },
        error: (error) => {
          console.error('Error loading bookings:', error);
          this.toastService.showError('Failed to load bookings');
          this.loading = false;
        },
      });
    }
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
        },
        error: (error) => {
          console.error('Error declining booking:', error);
          this.toastService.showError('Failed to decline booking');
        },
      });
    }
  }

  async cancelBookingAsAdmin(booking: any): Promise<void> {
    try {
      const cancellationRequest = {
        bookingId: booking.id,
        cancelledByUserId: localStorage.getItem('userId') || '',
        isHostCancellation: false,
        cancellationReason: 'Cancelled by admin'
      };

      this.cancellationService.cancelBookingEnhanced(cancellationRequest).subscribe({
        next: (result) => {
          if (result.isSuccessful) {
            this.toastService.showSuccess(result.message);
            this.loadBookings();
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
} 