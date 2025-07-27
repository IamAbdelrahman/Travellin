import { Component, OnInit, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NotificationService, NotificationData } from '../../services/notification.service';
import { Subscription } from 'rxjs';
import { LucideAngularModule, Bell } from 'lucide-angular';

@Component({
  selector: 'app-notification-bell',
  standalone: true,
  imports: [CommonModule, LucideAngularModule],
  template: `
    <div class="notification-bell position-relative">
      <button 
        class="btn btn-link position-relative p-0" 
        (click)="toggleDropdown()"
        title="Notifications">
        <lucide-icon [name]="bellIcon" size="20"></lucide-icon>
        
        <!-- Notification Badge -->
        <span 
          *ngIf="unreadCount > 0" 
          class="position-absolute top-0 start-100 translate-middle badge rounded-pill bg-danger"
          style="font-size: 0.6rem; transform: translate(-50%, -50%);">
          {{ unreadCount > 99 ? '99+' : unreadCount }}
        </span>
      </button>

      <!-- Notification Dropdown -->
      <div 
        *ngIf="showDropdown" 
        class="notification-dropdown position-absolute top-100 end-0 mt-2 bg-white border rounded shadow-lg"
        style="width: 350px; max-height: 400px; z-index: 1050;">
        
        <!-- Header -->
        <div class="p-3 border-bottom d-flex justify-content-between align-items-center">
          <h6 class="mb-0 fw-bold">Notifications</h6>
          <div class="d-flex gap-2">
            <button 
              *ngIf="unreadCount > 0"
              class="btn btn-sm btn-outline-primary" 
              (click)="markAllAsRead()">
              Mark all read
            </button>
            <button 
              class="btn btn-sm btn-outline-secondary" 
              (click)="clearAll()">
              Clear all
            </button>
          </div>
        </div>

        <!-- Notification List -->
        <div class="notification-list" style="max-height: 300px; overflow-y: auto;">
          <div 
            *ngFor="let notification of notifications; trackBy: trackByNotificationId" 
            class="notification-item p-3 border-bottom"
            [class.unread]="!notification.isRead"
            (click)="handleNotificationClick(notification)">
            
            <div class="d-flex align-items-start">
              <div class="notification-icon me-3" style="font-size: 1.2rem;">
                {{ notification.icon }}
              </div>
              
              <div class="flex-grow-1">
                <div class="d-flex justify-content-between align-items-start">
                  <h6 class="mb-1 fw-semibold" [class.text-primary]="!notification.isRead">
                    {{ notification.title }}
                  </h6>
                  <small class="text-muted">
                    {{ formatTime(notification.timestamp) }}
                  </small>
                </div>
                <p class="mb-1 small text-muted">{{ notification.message }}</p>
                <div class="d-flex align-items-center gap-2">
                  <span 
                    class="badge badge-sm"
                    [class]="getPriorityClass(notification.priority)">
                    {{ notification.priority }}
                  </span>
                  <span class="badge badge-sm bg-secondary">{{ notification.type }}</span>
                </div>
              </div>
            </div>
          </div>

          <!-- Empty State -->
          <div 
            *ngIf="notifications.length === 0" 
            class="text-center p-4 text-muted">
            <div class="mb-2">🔔</div>
            <p class="mb-0 small">No notifications yet</p>
          </div>
        </div>

        <!-- Footer -->
        <div class="p-2 border-top text-center">
          <button 
            class="btn btn-sm btn-link text-decoration-none"
            (click)="viewAllNotifications()">
            View all notifications
          </button>
        </div>
      </div>
    </div>
  `,
  styles: [`
    .notification-bell {
      display: inline-block;
    }

    .notification-dropdown {
      min-width: 350px;
    }

    .notification-item {
      cursor: pointer;
      transition: background-color 0.2s;
    }

    .notification-item:hover {
      background-color: #f8f9fa;
    }

    .notification-item.unread {
      background-color: #e3f2fd;
    }

    .notification-item.unread:hover {
      background-color: #bbdefb;
    }

    .notification-icon {
      flex-shrink: 0;
    }

    .badge-sm {
      font-size: 0.6rem;
    }

    .priority-high {
      background-color: #dc3545;
      color: white;
    }

    .priority-medium {
      background-color: #fd7e14;
      color: white;
    }

    .priority-low {
      background-color: #6c757d;
      color: white;
    }
  `]
})
export class NotificationBellComponent implements OnInit, OnDestroy {
  bellIcon = Bell;
  notifications: NotificationData[] = [];
  unreadCount: number = 0;
  showDropdown: boolean = false;
  private subscription = new Subscription();

  constructor(private notificationService: NotificationService) {}

  ngOnInit(): void {
    // Subscribe to new notifications
    this.subscription.add(
      this.notificationService.notifications$.subscribe(notification => {
        this.updateNotifications();
      })
    );

    this.updateNotifications();
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  private updateNotifications(): void {
    this.notifications = this.notificationService.getNotifications();
    this.unreadCount = this.notificationService.getUnreadCount();
  }

  toggleDropdown(): void {
    this.showDropdown = !this.showDropdown;
  }

  handleNotificationClick(notification: NotificationData): void {
    // Mark as read
    this.notificationService.markAsRead(notification.id);
    this.updateNotifications();

    // Execute action if available
    if (notification.action) {
      notification.action();
    }

    // Close dropdown
    this.showDropdown = false;
  }

  markAllAsRead(): void {
    this.notificationService.markAllAsRead();
    this.updateNotifications();
  }

  clearAll(): void {
    this.notificationService.clearNotifications();
    this.updateNotifications();
  }

  viewAllNotifications(): void {
    // Navigate to notifications page or show full list
    console.log('Navigate to notifications page');
    this.showDropdown = false;
  }

  formatTime(timestamp: Date): string {
    const now = new Date();
    const diff = now.getTime() - timestamp.getTime();
    const minutes = Math.floor(diff / (1000 * 60));
    const hours = Math.floor(diff / (1000 * 60 * 60));
    const days = Math.floor(diff / (1000 * 60 * 60 * 24));

    if (minutes < 1) return 'Just now';
    if (minutes < 60) return `${minutes}m ago`;
    if (hours < 24) return `${hours}h ago`;
    if (days < 7) return `${days}d ago`;
    return timestamp.toLocaleDateString();
  }

  getPriorityClass(priority: string): string {
    switch (priority) {
      case 'high': return 'priority-high';
      case 'medium': return 'priority-medium';
      case 'low': return 'priority-low';
      default: return 'priority-medium';
    }
  }

  trackByNotificationId(index: number, notification: NotificationData): string {
    return notification.id;
  }
} 