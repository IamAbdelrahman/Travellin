import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { UsersService } from '../../services/users.service';
import { UserProfiles, User } from '../../models/api/response/iget-users';
import { HttpResponse } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { ToastService } from '../../services/toast.service';
import { Bookings, GetBookingsResponse } from '../../models/api/request/iget-bookings';
import { BookingService } from '../../services/booking.service';
import { UserProfileService } from '../../services/user-profile.service';
@Component({
  selector: 'app-booking-admin',
  imports: [CommonModule, RouterModule, FormsModule],
  templateUrl: './booking-admin.component.html',
  styleUrl: './booking-admin.component.scss'
})
export class BookingAdminComponet {
  bookings: Bookings[] = [];
  filteredBookings: Bookings[] = [];
  Guest: string = '';
  loading = false;
  currentPage = 1;
  totalPages = 1;
  pageSize = 10;
  searchTerm = '';
    constructor(
      private bookingService: BookingService,
      private userProfileService: UserProfileService,
      private toastService: ToastService
    ) { }
    ngOnInit(): void {
    this.loadBookings();
  }

  loadBookings(): void {
    this.loading = true;
    this.bookingService.getBookingsForAdmin().subscribe({
      next: (response: any) => {
        this.bookings = response.body?.items || [];
        this.totalPages = response.body?.metaData?.total ? Math.ceil(response.body.metaData.total / this.pageSize) : 1;
        this.filteredBookings = [...this.bookings];
        this.loading = false;
        console.log(this.bookings);
      },
      error: (error: any) => {
        console.error('Error loading bookings:', error);
        this.toastService.showError('Failed to load bookings');
        this.loading = false;
      },
    });
  }
  getStatusColor(status: string): string {
      switch (status.toLowerCase()) {
        case 'active':
        case 'approved':
        case 'confirmed':
        case 'completed':
          return 'text-green-600 bg-green-100';
        case 'pending':
          return 'text-yellow-600 bg-yellow-100';
        case 'blocked':
        case 'rejected':
        case 'cancelled':
        case 'failed':
          return 'text-red-600 bg-red-100';
        default:
          return 'text-gray-600 bg-gray-100';
      }
  }
  getFilteredUsers(): Bookings[] {    
    if (this.searchTerm) {
      this.filteredBookings = this.filteredBookings.filter(bookings => 
        bookings.status.toLowerCase().includes(this.searchTerm.toLowerCase()) ||
        bookings.createdAt.toLowerCase().includes(this.searchTerm.toLowerCase()) ||
        bookings.createdAt.toLowerCase().includes(this.searchTerm.toLowerCase()) ||
        bookings.createdAt.toLowerCase().includes(this.searchTerm.toLowerCase()) ||
        bookings.createdAt.toLowerCase().includes(this.searchTerm.toLowerCase()) ||
        bookings.createdAt.toLowerCase().includes(this.searchTerm.toLowerCase())
      );
    }
    
    return this.filteredBookings;
  }
  calculateNumberOfNights(checkIn: string, checkOut: string): number {
    const checkInDate = new Date(checkIn);
    const checkOutDate = new Date(checkOut);
    const timeDiff = checkOutDate.getTime() - checkInDate.getTime();
    return Math.ceil(timeDiff / (1000 * 3600 * 24));
  }
}
