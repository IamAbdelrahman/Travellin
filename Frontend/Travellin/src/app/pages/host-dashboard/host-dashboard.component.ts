import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { BookingManagementService } from '../../services/booking-management.service';
import { CancellationService } from '../../services/cancellation.service';
import { ToastService } from '../../services/toast.service';
import { ReviewService } from '../../services/review.service';
import { Review, ReviewType, ReviewStatus } from '../../models/api/request/review.model';
import { AnalyticsCard } from '../admin-dashboard/admin-dashboard.component';
import { AddPropertyComponent } from '../add-property/add-property.component';
import { HostBookingComponent } from '../host-booking/host-booking.component';
import { HostPropertyComponent } from "../host-property/host-property.component";
import { ChatPageComponent } from '../chat-page/chat-page.component';

@Component({
  selector: 'app-host-dashboard',
  imports: [CommonModule, FormsModule, RouterModule, HostBookingComponent, HostPropertyComponent, 
    AddPropertyComponent, ChatPageComponent],
  templateUrl: './host-dashboard.component.html',
  styleUrl: './host-dashboard.component.scss',
})
export class HostDashboardComponent implements OnInit {
  activeTab = 'overview';
  showAddProperty: boolean = false;

  toggleAddProperty() {
    this.showAddProperty = !this.showAddProperty;
  }

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
  averageRating = 0;
  totalReviews = 0;
  pendingReviews = 0;
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
    private toastService: ToastService
  ) { }

  ngOnInit(): void {
    this.loadReviews();
  }

  setActiveTab(tab: string) {
    this.activeTab = tab;
    if (tab === 'reviews') {
      this.loadReviews();
    }
  }

  toggleSidebar() {
    this.sidebarOpen = !this.sidebarOpen;
  }

  async loadReviews() {
    try {
      this.loading = true;
      // Get current user ID from localStorage or service
      const userId = localStorage.getItem('userId') || '';
      
      // Load reviews for the current host
      const reviews = await this.reviewService.getUserReviews(userId, ReviewType.Host).toPromise();
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
    
    if (this.reviews.length > 0) {
      const totalRating = this.reviews.reduce((sum, review) => sum + (review.avg || 0), 0);
      this.averageRating = totalRating / this.reviews.length;
    }
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
} 