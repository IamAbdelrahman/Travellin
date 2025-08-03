import { Component, OnInit, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BookingManagementService } from '../../services/booking-management.service';
import { Subscription } from 'rxjs';

export interface Notification {
  id: string;
  type: 'booking_request' | 'booking_response' | 'booking_cancellation' | 'payment_success' | 'payment_refund';
  bookingId: string;
  propertyTitle: string;
  message: string;
  timestamp: Date;
  isRead: boolean;
}

@Component({
  selector: 'app-notification-bell',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './notification-bell.component.html',
  styleUrls: ['./notification-bell.component.scss']
})
export class NotificationBellComponent implements OnInit, OnDestroy {
  notifications: Notification[] = [];
  unreadCount = 0;
  showNotifications = false;
  private subscription: Subscription = new Subscription();

  constructor(private bookingService: BookingManagementService) {}

  ngOnInit(): void {
    // Subscribe to booking status changes
    this.subscription.add(
      this.bookingService.bookingStatus$.subscribe((status: any) => {
        if (status) {
          this.handleBookingStatusChange(status);
        }
      })
    );

    // Subscribe to notifications
    this.subscription.add(
      this.bookingService.notifications$.subscribe((notifications: any) => {
        this.notifications = notifications;
        this.updateUnreadCount();
      })
    );
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  toggleNotifications(): void {
    this.showNotifications = !this.showNotifications;
  }

  markAsRead(notificationId: string): void {
    this.bookingService.markNotificationAsRead(notificationId);
  }

  markAllAsRead(): void {
    this.notifications.forEach(notification => {
      if (!notification.isRead) {
        this.bookingService.markNotificationAsRead(notification.id);
      }
    });
  }

  clearAllNotifications(): void {
    this.bookingService.clearNotifications();
  }

  getNotificationIcon(type: string): string {
    switch (type) {
      case 'booking_request': return '📋';
      case 'booking_response': return '✅';
      case 'booking_cancellation': return '❌';
      case 'payment_success': return '💰';
      case 'payment_refund': return '💸';
      default: return '🔔';
    }
  }

  getNotificationClass(type: string): string {
    switch (type) {
      case 'booking_request': return 'badge-info';
      case 'booking_response': return 'badge-success';
      case 'booking_cancellation': return 'badge-warning';
      case 'payment_success': return 'badge-success';
      case 'payment_refund': return 'badge-info';
      default: return 'badge-default';
    }
  }

  formatTimestamp(timestamp: Date): string {
    const now = new Date();
    const diff = now.getTime() - new Date(timestamp).getTime();
    const minutes = Math.floor(diff / 60000);
    const hours = Math.floor(diff / 3600000);
    const days = Math.floor(diff / 86400000);

    if (minutes < 1) return 'Just now';
    if (minutes < 60) return `${minutes}m ago`;
    if (hours < 24) return `${hours}h ago`;
    return `${days}d ago`;
  }

  private handleBookingStatusChange(status: any): void {
    // Handle booking status changes and create notifications
    console.log('Booking status changed:', status);
  }

  private updateUnreadCount(): void {
    this.unreadCount = this.notifications.filter(n => !n.isRead).length;
  }

  onNotificationClick(notification: Notification): void {
    if (!notification.isRead) {
      this.markAsRead(notification.id);
    }
    // Handle notification click - could navigate to booking details
    console.log('Notification clicked:', notification);
  }

  trackByNotificationId(index: number, notification: Notification): string {
    return notification.id;
  }
} 