import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { UsersAdminComponent } from '../../components/users-admin/users-admin.component';
import { AdminChatComponent } from '../../components/admin-chat/admin-chat.component';
import { PropertyAdminComponent } from '../../components/property-admin/property-admin.component';
import { BookingAdminComponet } from '../../components/booking-admin/booking-admin.component';
import { RequestsAdmin } from '../../components/requests-admin/requests-admin';
import { ReviewService } from '../../services/review.service';
import { Review, ReviewType, ReviewStatus } from '../../models/api/request/review.model';
import { ToastService } from '../../services/toast.service';
import { BookingManagementService } from '../../services/booking-management.service';
import { CancellationService } from '../../services/cancellation.service';

export interface AnalyticsCard {
  title: string;
  value: string;
  change: string;
  changeType: 'positive' | 'negative';
  icon: string;
}

@Component({
  selector: 'app-admin-dashboard',
  imports: [CommonModule, FormsModule, RouterModule, UsersAdminComponent, 
    AdminChatComponent, PropertyAdminComponent, BookingAdminComponet, RequestsAdmin],
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
  
  // Review properties
  reviews: Review[] = [];
  filteredReviews: Review[] = [];
  selectedReviewFilter = 'all';
  reviewSearchTerm = '';
  totalReviews = 0;
  pendingReviews = 0;
  publishedReviews = 0;
  hiddenReviews = 0;
  loading = false;
  currentPage = 1;
  totalPages = 1;
  pageSize = 10;

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
  selectedBooking: any = null; // For modal display

  constructor(
    private router: Router,
    private reviewService: ReviewService,
    private toastService: ToastService,
    private bookingManagementService: BookingManagementService,
    private cancellationService: CancellationService
  ) { }

  ngOnInit(): void {
    this.loadReviews();
    this.loadBookings();
  }

  setActiveTab(tab: string) {
    this.activeTab = tab;
    if (tab === 'reviews') {
      this.loadReviews();
    }
  }

  async loadReviews() {
    try {
      this.loading = true;
      // Load all reviews for admin
      const reviews = await this.reviewService.getAllReviews().toPromise();
      this.reviews = reviews || [];
      this.filteredReviews = [...this.reviews];
      
      this.calculateReviewStats();
    } catch (error) {
      console.error('Error loading reviews:', error);
      this.toastService.showError('Failed to load reviews');
    } finally {
      this.loading = false;
    }
  }

  calculateReviewStats() {
    this.totalReviews = this.reviews.length;
    this.pendingReviews = this.reviews.filter(r => r.status === ReviewStatus.Submitted).length;
    this.publishedReviews = this.reviews.filter(r => r.status === ReviewStatus.Published).length;
    this.hiddenReviews = this.reviews.filter(r => r.status === ReviewStatus.Hidden).length;
  }

  filterReviews() {
    let filtered = [...this.reviews];
    
    if (this.selectedReviewFilter !== 'all') {
      filtered = filtered.filter(review => review.status.toLowerCase() === this.selectedReviewFilter);
    }
    
    if (this.reviewSearchTerm) {
      const searchTerm = this.reviewSearchTerm.toLowerCase();
      filtered = filtered.filter(review => 
        review.comment.toLowerCase().includes(searchTerm) ||
        review.reviewer?.firstName?.toLowerCase().includes(searchTerm) ||
        review.reviewer?.lastName?.toLowerCase().includes(searchTerm)
      );
    }
    
    this.filteredReviews = filtered;
  }

  searchReviews() {
    this.filterReviews();
  }

  async approveReview(reviewId: string) {
    try {
      await this.reviewService.publishReview(reviewId).toPromise();
      this.toastService.showSuccess('Review approved successfully');
      this.loadReviews();
    } catch (error) {
      console.error('Error approving review:', error);
      this.toastService.showError('Failed to approve review');
    }
  }

  async hideReview(reviewId: string) {
    try {
      await this.reviewService.hideReview(reviewId).toPromise();
      this.toastService.showSuccess('Review hidden successfully');
      this.loadReviews();
    } catch (error) {
      console.error('Error hiding review:', error);
      this.toastService.showError('Failed to hide review');
    }
  }

  async publishReview(reviewId: string) {
    try {
      await this.reviewService.publishReview(reviewId).toPromise();
      this.toastService.showSuccess('Review published successfully');
      this.loadReviews();
    } catch (error) {
      console.error('Error publishing review:', error);
      this.toastService.showError('Failed to publish review');
    }
  }

  async deleteReview(reviewId: string) {
    try {
      await this.reviewService.deleteReview(reviewId).toPromise();
      this.toastService.showSuccess('Review deleted successfully');
      this.loadReviews();
    } catch (error) {
      console.error('Error deleting review:', error);
      this.toastService.showError('Failed to delete review');
    }
  }

  getPropertyTitle(bookingId: string): string {
    // This would need to be implemented to get property title from booking
    return 'Property Title'; // Placeholder
  }

  getReviewStatusClass(status: ReviewStatus): string {
    switch (status) {
      case ReviewStatus.Published:
        return 'status-published';
      case ReviewStatus.Submitted:
        return 'status-pending';
      case ReviewStatus.Hidden:
        return 'status-hidden';
      default:
        return 'status-default';
    }
  }

  receiveAdminPhoto(photoUrl: string) {
    this.adminPhoto = photoUrl;
    console.log('Admin Photo URL received from child:', photoUrl);
  }

  toggleSidebar() {
    this.sidebarOpen = !this.sidebarOpen;
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

  loadBookings(): void {
    this.loading = true;
    
    if (this.selectedFilter === 'pending') {
      this.bookingManagementService.getAdminPendingBookings(this.currentPage, this.pageSize).subscribe({
        next: (response: any) => {
          this.bookings = response.items;
          this.totalPages = Math.ceil(response.metaData.total / this.pageSize);
          this.loading = false;
        },
        error: (error: any) => {
          console.error('Error loading pending bookings:', error);
          this.toastService.showError('Failed to load bookings');
          this.loading = false;
        },
      });
    } else {
      this.bookingManagementService.getAllBookingsForAdmin(this.currentPage, this.pageSize, this.selectedFilter).subscribe({
        next: (response: any) => {
          this.bookings = response.items;
          this.totalPages = Math.ceil(response.metaData.total / this.pageSize);
          this.loading = false;
        },
        error: (error: any) => {
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
      error: (error: any) => {
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
        error: (error: any) => {
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
        next: (result: any) => {
          if (result.isSuccessful) {
            this.toastService.showSuccess(result.message);
            this.loadBookings();
          } else {
            this.toastService.showError(result.message);
          }
        },
        error: (error: any) => {
          console.error('Cancellation failed:', error);
          this.toastService.showError('Failed to cancel booking');
        },
      });
    } catch (error) {
      console.error('Error cancelling booking:', error);
      this.toastService.showError('Failed to cancel booking');
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
    }
  }

  nextPage(): void {
    if (this.currentPage < this.totalPages) {
      this.currentPage++;
    }
  }

  toggleNotifications(): void {
    this.showNotifications = !this.showNotifications;
  }
} 