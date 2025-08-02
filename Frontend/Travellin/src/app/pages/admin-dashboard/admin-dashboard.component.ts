import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { RouterModule } from '@angular/router';
import { BookingManagementService } from '../../services/booking-management.service';
import { CancellationService } from '../../services/cancellation.service';
import { ToastService } from '../../services/toast.service';
import { FormsModule } from '@angular/forms';
import { PropertyService } from '../../services/property.service';
import { UsersService } from '../../services/users.service';
import { IPropertyWithDistance } from '../../models/domain/iproperty-with-distance';
import { IpropertyRes } from '../../models/api/response/iproperty-res';
import { UserProfiles, User } from '../../models/api/response/iget-users';
import { IReview } from '../../models/domain/ireview';
import { Bookings } from '../../models/api/request/iget-bookings';
import { IPayment } from '../../models/domain/ipayment';
import { UsersAdminComponent } from '../../components/users-admin/users-admin.component';
import { AdminChatComponent } from '../../components/admin-chat/admin-chat.component';
export interface AnalyticsCard {
  title: string;
  value: string;
  change: string;
  changeType: 'positive' | 'negative';
  icon: string;
}

@Component({
  selector: 'app-admin-dashboard',
  imports: [CommonModule, FormsModule, RouterModule, UsersAdminComponent, AdminChatComponent],
  templateUrl: './admin-dashboard.component.html',
  styleUrl: './admin-dashboard.component.scss',
})
export class AdminDashboardComponent implements OnInit {
  activeTab = 'overview';
  sidebarOpen = true;
  adminPhoto: string = '';
  searchTerm = '';
  selectedFilter = 'all';
  showNotifications = false;
  analyticsCards: AnalyticsCard[] = [
    {
      title: 'Total Bookings',
      value: '2,456',
      change: '+12.5%',
      changeType: 'positive',
      icon: 'calendar'
    },
    {
      title: 'Active Properties',
      value: '1,234',
      change: '+8.2%',
      changeType: 'positive',
      icon: 'home'
    },
    {
      title: 'Total Revenue',
      value: '$486,250',
      change: '+15.3%',
      changeType: 'positive',
      icon: 'dollar-sign'
    },
    {
      title: 'Pending Payouts',
      value: '$12,450',
      change: '-2.1%',
      changeType: 'negative',
      icon: 'credit-card'
    }
  ];
    monthlyData = [
    { month: 'Jan', bookings: 245, revenue: 48500 },
    { month: 'Feb', bookings: 312, revenue: 62400 },
    { month: 'Mar', bookings: 389, revenue: 77800 },
    { month: 'Apr', bookings: 445, revenue: 89000 },
    { month: 'May', bookings: 523, revenue: 104600 },
    { month: 'Jun', bookings: 612, revenue: 122400 }
  ];
  bookings: any[] = [];
  loading = false;
  currentPage = 1;
  totalPages = 1;
  pageSize = 10;
  selectedBooking: any = null; // For modal display

  receiveAdminPhoto(photoUrl: string) {
    this.adminPhoto = photoUrl;
    console.log('Admin Photo URL received from child:', photoUrl);
  }
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

  // getStatusColor(status: string): string {
  //   switch (status.toLowerCase()) {
  //     case 'pending':
  //       return 'text-yellow-600 bg-yellow-100';
  //     case 'confirmed':
  //       return 'text-green-600 bg-green-100';
  //     case 'cancelled':
  //       return 'text-red-600 bg-red-100';
  //     case 'declined':
  //       return 'text-red-600 bg-red-100';
  //     case 'completed':
  //       return 'text-blue-600 bg-blue-100';
  //     default:
  //       return 'text-gray-600 bg-gray-100';
  //   }
  // }

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
    toggleSidebar(): void {
    this.sidebarOpen = !this.sidebarOpen;
  }

  setActiveTab(tab: string): void {
    this.activeTab = tab;
  }

  toggleNotifications(): void {
    this.showNotifications = !this.showNotifications;
  }



  // approveProperty(property: Property): void {
  //   property.status = 'Approved';
  // }

  // rejectProperty(property: Property): void {
  //   property.status = 'Rejected';
  // }

  // approveReview(review: Review): void {
  //   review.status = 'Approved';
  // }

  // hideReview(review: Review): void {
  //   review.status = 'Hidden';
  // }

  // deleteReview(reviewId: number): void {
  //   this.reviews = this.reviews.filter(review => review.id !== reviewId);
  // }

  // markNotificationAsRead(notification: Notification): void {
  //   notification.read = true;
  // }

  // getUnreadNotificationsCount(): number {
  //   return this.notifications.filter(n => !n.read).length;
  // }

  // getStatusColor(status: string): string {
  //   switch (status.toLowerCase()) {
  //     case 'active':
  //     case 'approved':
  //     case 'confirmed':
  //     case 'completed':
  //       return 'text-green-600 bg-green-100';
  //     case 'pending':
  //       return 'text-yellow-600 bg-yellow-100';
  //     case 'blocked':
  //     case 'rejected':
  //     case 'cancelled':
  //     case 'failed':
  //       return 'text-red-600 bg-red-100';
  //     default:
  //       return 'text-gray-600 bg-gray-100';
  //   }
  // }



  // getFilteredProperties(): Property[] {
  //   let filtered = this.properties;
    
  //   if (this.selectedFilter !== 'all') {
  //     filtered = filtered.filter(property => property.status.toLowerCase() === this.selectedFilter);
  //   }
    
  //   if (this.searchTerm) {
  //     filtered = filtered.filter(property => 
  //       property.title.toLowerCase().includes(this.searchTerm.toLowerCase()) ||
  //       property.location.toLowerCase().includes(this.searchTerm.toLowerCase())
  //     );
  //   }
    
  //   return filtered;
  // }
} 