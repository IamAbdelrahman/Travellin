// src/app/services/notification.service.ts
import { Injectable, OnDestroy } from '@angular/core';
import { ChatService } from './chat.service';
import { ToastService } from './toast.service';
import { MessageDto } from '../models/chat/message.model';
import { takeWhile, filter } from 'rxjs/operators';
import { Subject, Subscription } from 'rxjs';

export interface NotificationData {
  id: string;
  type: 'message' | 'payment' | 'booking' | 'system' | 'review' | 'host' | 'guest';
  title: string;
  message: string;
  icon?: string;
  action?: () => void;
  priority: 'low' | 'medium' | 'high';
  timestamp: Date;
  isRead: boolean;
  metadata?: any;
}

// Booking Notifications
export interface BookingRequestNotification {
  bookingId: string;
  guestName: string;
  propertyTitle: string;
  checkIn: Date;
  checkOut: Date;
  totalAmount: number;
  guestMessage?: string;
  guestCount: number;
}

export interface BookingResponseNotification {
  bookingId: string;
  hostName: string;
  propertyTitle: string;
  status: 'accepted' | 'declined';
  checkIn: Date;
  checkOut: Date;
  hostMessage?: string;
}

export interface BookingReminderNotification {
  bookingId: string;
  propertyTitle: string;
  checkIn: Date;
  checkOut: Date;
  reminderType: 'checkin_tomorrow' | 'checkin_today' | 'checkout_tomorrow';
}

// Payment Notifications
export interface PaymentNotification {
  bookingId: string;
  amount: number;
  currency: string;
  status: 'success' | 'failed' | 'pending' | 'cancelled';
  propertyTitle: string;
  checkIn: Date;
  checkOut: Date;
  transactionId?: string;
}

// Review Notifications
export interface ReviewNotification {
  reviewId: string;
  bookingId: string;
  propertyTitle: string;
  reviewerName: string;
  rating: number;
  reviewText?: string;
  reviewDate: Date;
}

// Host Notifications
export interface HostUpgradeNotification {
  requestId: string;
  userName: string;
  status: 'pending' | 'approved' | 'rejected';
  requestDate: Date;
  adminMessage?: string;
}

export interface CoHostInvitationNotification {
  propertyId: string;
  hostName: string;
  propertyTitle: string;
  invitationDate: Date;
}

// Guest Notifications
export interface GuestArrivalNotification {
  bookingId: string;
  guestName: string;
  propertyTitle: string;
  checkIn: Date;
  guestMessage?: string;
}

// System Notifications
export interface SystemNotification {
  title: string;
  message: string;
  type: 'promotion' | 'maintenance' | 'security';
  expiresAt: Date;
  actionUrl?: string;
}

@Injectable({
  providedIn: 'root'
})
export class NotificationService implements OnDestroy {
  private isActive = true;
  private notificationPermission: NotificationPermission = 'default';
  private notifications: NotificationData[] = [];
  private notificationsSubject = new Subject<NotificationData>();
  private subscriptions = new Subscription();

  public notifications$ = this.notificationsSubject.asObservable();

  constructor(
    private chatService: ChatService,
    private toastService: ToastService
  ) {
    this.initializeNotifications();
  }

  private async initializeNotifications(): Promise<void> {
    await this.requestNotificationPermission();
    this.setupMessageNotifications();
    this.setupPaymentNotifications();
    this.setupBookingNotifications();
    this.setupReviewNotifications();
    this.setupHostNotifications();
    this.setupGuestNotifications();
    this.setupSystemNotifications();
  }

  private async requestNotificationPermission(): Promise<void> {
    if ('Notification' in window) {
      this.notificationPermission = await Notification.requestPermission();
    }
  }

  // Message Notifications
  private setupMessageNotifications(): void {
    const messageSubscription = this.chatService.messageReceived$
      .pipe(
        takeWhile(() => this.isActive),
        filter(message => this.shouldShowMessageNotification(message))
      )
      .subscribe(message => {
        this.handleNewMessage(message);
      });

    this.subscriptions.add(messageSubscription);
  }

  private shouldShowMessageNotification(message: MessageDto): boolean {
    // Don't show notification if page is visible and user is in the active conversation
    if (!document.hidden) {
      const activeConversation = this.chatService.getActiveConversation();
      if (activeConversation && activeConversation.id === message.conversationId) {
        return false;
      }
    }
    return true;
  }

  private handleNewMessage(message: MessageDto): void {
    const notification: NotificationData = {
      id: `message-${message.id}`,
      type: 'message',
      title: 'New Message',
      message: `You have a new message: ${message.content.substring(0, 50)}${message.content.length > 50 ? '...' : ''}`,
      icon: '💬',
      priority: 'medium',
      timestamp: new Date(),
      isRead: false,
      action: () => {
        // Navigate to chat or specific conversation
      }
    };

    this.addNotification(notification);
    this.showBrowserNotification(notification);
    this.showToastNotification(notification);
  }

  // Payment Notifications
  public handlePaymentNotification(paymentData: PaymentNotification): void {
    const statusMessages = {
      success: 'Payment successful',
      failed: 'Payment failed',
      pending: 'Payment pending',
      cancelled: 'Payment cancelled'
    };

    const notification: NotificationData = {
      id: `payment-${paymentData.bookingId}`,
      type: 'payment',
      title: statusMessages[paymentData.status],
      message: `${paymentData.status === 'success' ? 'Your payment of' : 'Payment for'} ${paymentData.currency} ${paymentData.amount} for ${paymentData.propertyTitle} has been ${paymentData.status}`,
      icon: paymentData.status === 'success' ? '✅' : paymentData.status === 'failed' ? '❌' : '⏳',
      priority: paymentData.status === 'success' ? 'high' : 'medium',
      timestamp: new Date(),
      isRead: false,
      action: () => {
        // Navigate to booking details
      }
    };

    this.addNotification(notification);
    this.showBrowserNotification(notification);
    this.showToastNotification(notification);
  }

  private setupPaymentNotifications(): void {
    // Listen for payment status changes from the backend
    // This would typically be through SignalR or WebSocket
    // For now, we'll rely on the service being called directly
  }

  // Booking Notifications
  public handleBookingRequestNotification(bookingData: BookingRequestNotification): void {
    const notification: NotificationData = {
      id: `booking-request-${bookingData.bookingId}`,
      type: 'booking',
      title: 'New Booking Request',
      message: `${bookingData.guestName} wants to book ${bookingData.propertyTitle} for ${bookingData.checkIn.toLocaleDateString()} - ${bookingData.checkOut.toLocaleDateString()}`,
      icon: '📅',
      priority: 'high',
      timestamp: new Date(),
      isRead: false,
      action: () => {
      }
    };

    this.addNotification(notification);
    this.showBrowserNotification(notification);
    this.showToastNotification(notification);
  }

  public handleBookingResponseNotification(bookingData: BookingResponseNotification): void {
    const notification: NotificationData = {
      id: `booking-response-${bookingData.bookingId}`,
      type: 'booking',
      title: `Booking ${bookingData.status}`,
      message: `${bookingData.hostName} has ${bookingData.status} your booking for ${bookingData.propertyTitle}`,
      icon: bookingData.status === 'accepted' ? '✅' : '❌',
      priority: 'high',
      timestamp: new Date(),
      isRead: false,
      action: () => {
      }
    };

    this.addNotification(notification);
    this.showBrowserNotification(notification);
    this.showToastNotification(notification);
  }

  private setupBookingNotifications(): void {
    // Listen for booking status changes from the backend
    // This would typically be through SignalR or WebSocket
  }

  // Review Notifications
  public handleReviewNotification(reviewData: ReviewNotification): void {
    const notification: NotificationData = {
      id: `review-${reviewData.reviewId}`,
      type: 'review',
      title: 'New Review',
      message: `${reviewData.reviewerName} left a ${reviewData.rating}-star review for ${reviewData.propertyTitle}`,
      icon: '⭐',
      priority: 'medium',
      timestamp: new Date(),
      isRead: false,
      action: () => {
      }
    };

    this.addNotification(notification);
    this.showBrowserNotification(notification);
    this.showToastNotification(notification);
  }

  private setupReviewNotifications(): void {
    // Listen for review notifications from the backend
  }

  // Host Notifications
  public handleHostUpgradeNotification(upgradeData: HostUpgradeNotification): void {
    const notification: NotificationData = {
      id: `host-upgrade-${upgradeData.requestId}`,
      type: 'host',
      title: 'Host Upgrade Request',
      message: `Your host upgrade request has been ${upgradeData.status}`,
      icon: '🏠',
      priority: 'medium',
      timestamp: new Date(),
      isRead: false,
      action: () => {
      }
    };

    this.addNotification(notification);
    this.showBrowserNotification(notification);
    this.showToastNotification(notification);
  }

  private setupHostNotifications(): void {
    // Listen for host-related notifications
  }

  // Guest Notifications
  public handleGuestArrivalNotification(arrivalData: GuestArrivalNotification): void {
    const notification: NotificationData = {
      id: `guest-arrival-${arrivalData.bookingId}`,
      type: 'guest',
      title: 'Guest Arrival',
      message: `${arrivalData.guestName} has arrived at ${arrivalData.propertyTitle}`,
      icon: '👋',
      priority: 'medium',
      timestamp: new Date(),
      isRead: false,
      action: () => {
      }
    };

    this.addNotification(notification);
    this.showBrowserNotification(notification);
    this.showToastNotification(notification);
  }

  private setupGuestNotifications(): void {
    // Listen for guest-related notifications
  }

  // System Notifications
  public handleSystemNotification(systemData: SystemNotification): void {
    const notification: NotificationData = {
      id: `system-${Date.now()}`,
      type: 'system',
      title: systemData.title,
      message: systemData.message,
      icon: '🔔',
      priority: 'low',
      timestamp: new Date(),
      isRead: false,
      action: () => {
        if (systemData.actionUrl) {
        }
      }
    };

    this.addNotification(notification);
    this.showBrowserNotification(notification);
    this.showToastNotification(notification);
  }

  private setupSystemNotifications(): void {
    // Listen for system notifications
  }

  // System Notifications
  public showSystemNotification(title: string, message: string, priority: 'low' | 'medium' | 'high' = 'medium'): void {
    const notification: NotificationData = {
      id: `system-${Date.now()}`,
      type: 'system',
      title,
      message,
      icon: '🔔',
      priority,
      timestamp: new Date(),
      isRead: false
    };

    this.addNotification(notification);
    this.showBrowserNotification(notification);
    this.showToastNotification(notification);
  }

  private addNotification(notification: NotificationData): void {
    this.notifications.unshift(notification);
    this.notificationsSubject.next(notification);
    
    // Keep only last 100 notifications
    if (this.notifications.length > 100) {
      this.notifications = this.notifications.slice(0, 100);
    }
  }

  private showBrowserNotification(notification: NotificationData): void {
    if (this.notificationPermission === 'granted' && document.hidden) {
      new Notification(notification.title, {
        body: notification.message,
        icon: '/assets/logo.png',
        tag: notification.id
      });
    }
  }

  private showToastNotification(notification: NotificationData): void {
    if (notification.priority === 'high') {
      this.toastService.showError(notification.message);
    } else if (notification.priority === 'medium') {
      this.toastService.showWarning(notification.message);
    } else {
      this.toastService.showInfo(notification.message);
    }
  }

  public getNotifications(): NotificationData[] {
    return this.notifications;
  }

  public getUnreadCount(): number {
    return this.notifications.filter(n => !n.isRead).length;
  }

  public markAsRead(notificationId: string): void {
    const notification = this.notifications.find(n => n.id === notificationId);
    if (notification) {
      notification.isRead = true;
    }
  }

  public markAllAsRead(): void {
    this.notifications.forEach(n => n.isRead = true);
  }

  public clearNotifications(): void {
    this.notifications = [];
  }

  public removeNotification(notificationId: string): void {
    this.notifications = this.notifications.filter(n => n.id !== notificationId);
  }

  // Test methods
  public testMessageNotification(): void {
    const testMessage: MessageDto = {
      id: 1,
      senderId: 'test-sender',
      receiverId: 'test-receiver',
      content: 'This is a test message notification',
      sentAt: new Date(),
      isRead: false,
      conversationId: 1
    };
    this.handleNewMessage(testMessage);
  }

  public testPaymentNotification(): void {
    const testPayment: PaymentNotification = {
      bookingId: 'test-booking-123',
      amount: 150.00,
      currency: 'USD',
      status: 'success',
      propertyTitle: 'Beautiful Beach House',
      checkIn: new Date('2025-08-01'),
      checkOut: new Date('2025-08-05')
    };
    this.handlePaymentNotification(testPayment);
  }

  public testBookingNotification(): void {
    const testBooking: BookingRequestNotification = {
      bookingId: 'test-booking-456',
      guestName: 'John Doe',
      propertyTitle: 'Cozy Mountain Cabin',
      checkIn: new Date('2025-08-10'),
      checkOut: new Date('2025-08-15'),
      totalAmount: 500.00,
      guestCount: 2
    };
    this.handleBookingRequestNotification(testBooking);
  }

  ngOnDestroy(): void {
    this.isActive = false;
    this.subscriptions.unsubscribe();
  }
} 