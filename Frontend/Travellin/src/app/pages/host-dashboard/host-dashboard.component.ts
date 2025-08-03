import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { BookingManagementService } from '../../services/booking-management.service';
import { CancellationService } from '../../services/cancellation.service';
import { ToastService } from '../../services/toast.service';
import { AnalyticsCard } from '../admin-dashboard/admin-dashboard.component';
import { AddPropertyComponent } from '../add-property/add-property.component';
import { HostBookingComponent } from '../host-booking/host-booking.component';

@Component({
  selector: 'app-host-dashboard',
  imports: [CommonModule, FormsModule, RouterModule, AddPropertyComponent, HostBookingComponent],
  templateUrl: './host-dashboard.component.html',
  styleUrl: './host-dashboard.component.scss',
})
export class HostDashboardComponent implements OnInit {
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

  // Computed properties for template


  constructor(
    private router: Router
  ) { }

  ngOnInit(): void {
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