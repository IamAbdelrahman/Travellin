// src/app/services/notification.service.ts
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import * as signalR from '@microsoft/signalr';
import { environment } from '../../environments/environment';
import { TokenStorageService } from './token-storage.service';

export interface NotificationData {
  type: string;
  title: string;
  message: string;
  bookingId?: string;
  propertyTitle?: string;
  isHostNotification?: boolean;
  timestamp: Date;
}

@Injectable({
  providedIn: 'root'
})
export class NotificationService {
  private hubConnection: signalR.HubConnection | null = null;
  private notificationsSubject = new BehaviorSubject<NotificationData[]>([]);
  private isConnectedSubject = new BehaviorSubject<boolean>(false);

  public notifications$ = this.notificationsSubject.asObservable();
  public isConnected$ = this.isConnectedSubject.asObservable();

  constructor(private tokenStorage: TokenStorageService) {
    this.startConnection();
  }

  private async startConnection(): Promise<void> {
    try {
      // Prevent multiple connections
      if (this.hubConnection && this.hubConnection.state === signalR.HubConnectionState.Connected) {
        return;
      }

      const token = this.tokenStorage.getAccessToken();
      if (!token) {
        return;
      }

      this.hubConnection = new signalR.HubConnectionBuilder()
        .withUrl(`${environment.apiUrl}/hubs/notification`, {
          accessTokenFactory: () => token,
          transport: signalR.HttpTransportType.WebSockets
        })
        .withAutomaticReconnect([0, 2000, 5000, 10000, 30000])
        .configureLogging(signalR.LogLevel.Information)
        .build();

      this.setupSignalRHandlers();

      await this.hubConnection.start();
      this.isConnectedSubject.next(true);
      
      // Test the connection
      try {
        await this.hubConnection.invoke('TestConnection');
      } catch (testError) {
        // Connection test failed, but don't break the flow
      }
    } catch (error) {
      this.isConnectedSubject.next(false);
    }
  }

  private setupSignalRHandlers(): void {
    if (!this.hubConnection) return;

    this.hubConnection.on('ReceiveNotification', (notification: NotificationData) => {
      // Check if we already have this notification to prevent duplicates
      const currentNotifications = this.notificationsSubject.value;
      const isDuplicate = currentNotifications.some(n => 
        n.bookingId === notification.bookingId && 
        n.type === notification.type && 
        n.timestamp === notification.timestamp
      );
      
      if (isDuplicate) {
        return;
      }
      
      this.addNotification(notification);
      this.showToastNotification(notification);
    });

    this.hubConnection.on('TestResponse', (message: string) => {
      // Test response received
    });

    this.hubConnection.on('ReceiveError', (error: string) => {
      // Handle error silently
    });

    this.hubConnection.onreconnecting(() => {
      this.isConnectedSubject.next(false);
    });

    this.hubConnection.onreconnected(() => {
      this.isConnectedSubject.next(true);
    });

    this.hubConnection.onclose(() => {
      this.isConnectedSubject.next(false);
    });
  }

  private addNotification(notification: NotificationData): void {
    const currentNotifications = this.notificationsSubject.value;
    this.notificationsSubject.next([notification, ...currentNotifications]);
  }

  private showToastNotification(notification: NotificationData): void {
    // Count existing notifications to position this one
    const existingNotifications = document.querySelectorAll('.notification-toast');
    const notificationIndex = existingNotifications.length;
    const topOffset = 20 + (notificationIndex * 120); // Stack notifications with 120px spacing

    // Create a toast notification
    const toast = document.createElement('div');
    toast.className = 'notification-toast';
    
    // Determine notification type and styling
    const isCancellation = notification.type === 'booking_cancellation';
    const isHostNotification = notification.isHostNotification;
    
    // Choose colors based on notification type (matching existing toast styles)
    let toastClass = 'toast-info';
    let icon = '🔔';
    
    if (isCancellation) {
      if (isHostNotification) {
        // Guest cancelled - warning theme for host
        toastClass = 'toast-warning';
        icon = '❌';
      } else {
        // Host cancelled - danger theme for guest
        toastClass = 'toast-danger';
        icon = '⚠️';
      }
    } else if (notification.type === 'booking_request') {
      // Booking request - success theme
      toastClass = 'toast-success';
      icon = '📋';
    } else if (notification.type === 'booking_response') {
      // Booking response - info theme
      toastClass = 'toast-info';
      icon = '✅';
    }

    toast.innerHTML = `
      <div class="toast-header">
        <div class="toast-icon">${icon}</div>
        <div class="toast-title">
          <strong>${notification.title}</strong>
        </div>
        <button onclick="this.parentElement.parentElement.remove()" class="btn-close btn-close-white">&times;</button>
      </div>
      <div class="toast-body">
        ${notification.message}
      </div>
      <div class="toast-footer">
        <small>${new Date(notification.timestamp).toLocaleTimeString()}</small>
      </div>
    `;

    // Add styles matching existing toast container
    toast.style.cssText = `
      position: fixed;
      top: ${topOffset}px;
      right: 20px;
      min-width: 350px;
      max-width: 450px;
      z-index: 9999;
      animation: fadeIn 0.3s ease-out;
      font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, sans-serif;
      border: none;
      opacity: 98%;
      font-weight: 500;
    `;

    // Add animation styles matching existing toast
    const style = document.createElement('style');
    style.textContent = `
      @keyframes fadeIn {
        from {
          opacity: 0;
          transform: translateY(-20px);
        }
        to {
          opacity: 1;
          transform: translateY(0);
        }
      }
      
      @keyframes fadeOut {
        from {
          opacity: 1;
          transform: translateY(0);
        }
        to {
          opacity: 0;
          transform: translateY(-20px);
        }
      }
      
      .notification-toast {
        border-radius: 12px;
        box-shadow: 0 4px 15px rgba(0,0,0,0.3);
        padding: 16px;
        margin-bottom: 8px;
      }
      
      .notification-toast.toast-success {
        background: linear-gradient(135deg, #28a745 0%, #1e7e34 100%);
        border-left: 4px solid #155724;
        color: white !important;
        box-shadow: 0 4px 15px rgba(40, 167, 69, 0.3);
      }
      
      .notification-toast.toast-danger {
        background: linear-gradient(135deg, #dc3545 0%, #c82333 100%);
        border-left: 4px solid #721c24;
        color: white !important;
        box-shadow: 0 4px 15px rgba(220, 53, 69, 0.3);
      }
      
      .notification-toast.toast-warning {
        background: linear-gradient(135deg, #fd7e14 0%, #e55a00 100%);
        border-left: 4px solid #856404;
        color: white !important;
        box-shadow: 0 4px 15px rgba(253, 126, 20, 0.3);
      }
      
      .notification-toast.toast-info {
        background: linear-gradient(135deg, #17a2b8 0%, #138496 100%);
        border-left: 4px solid #0c5460;
        color: white !important;
        box-shadow: 0 4px 15px rgba(23, 162, 184, 0.3);
      }
      
      .toast-header {
        display: flex;
        align-items: center;
        margin-bottom: 12px;
        gap: 12px;
      }
      
      .toast-icon {
        font-size: 20px;
        flex-shrink: 0;
      }
      
      .toast-title {
        flex: 1;
        font-size: 14px;
        font-weight: 600;
        color: white;
      }
      
      .btn-close {
        background: none;
        border: none;
        font-size: 20px;
        cursor: pointer;
        color: rgba(255,255,255,0.8);
        padding: 4px;
        border-radius: 50%;
        width: 28px;
        height: 28px;
        display: flex;
        align-items: center;
        justify-content: center;
        transition: all 0.2s ease;
        flex-shrink: 0;
      }
      
      .btn-close:hover {
        background: rgba(255,255,255,0.2);
        color: white;
      }
      
      .toast-body {
        color: white;
        line-height: 1.5;
        font-size: 13px;
        margin-bottom: 8px;
      }
      
      .toast-footer {
        text-align: right;
        font-size: 11px;
        color: rgba(255,255,255,0.8);
        border-top: 1px solid rgba(255,255,255,0.2);
        padding-top: 8px;
        margin-top: 8px;
      }
      
      .notification-toast {
        transition: all 0.3s ease;
      }
      
      .notification-toast:hover {
        transform: translateY(-2px);
        box-shadow: 0 8px 25px rgba(0,0,0,0.4);
      }
    `;
    
    // Remove existing style if it exists
    const existingStyle = document.getElementById('notification-styles');
    if (existingStyle) {
      existingStyle.remove();
    }
    style.id = 'notification-styles';
    document.head.appendChild(style);

    // Add the appropriate toast class
    toast.classList.add(toastClass);

    // Add to page
    document.body.appendChild(toast);

    // Auto-remove after 6 seconds with smooth animation
    setTimeout(() => {
      if (toast.parentElement) {
        toast.style.animation = 'fadeOut 0.3s ease forwards';
        setTimeout(() => {
          if (toast.parentElement) {
            toast.remove();
          }
        }, 300);
      }
    }, 6000);
  }

  public getNotifications(): Observable<NotificationData[]> {
    return this.notifications$;
  }

  public clearNotifications(): void {
    this.notificationsSubject.next([]);
  }

  public async testConnection(): Promise<void> {
    if (this.hubConnection && this.hubConnection.state === signalR.HubConnectionState.Connected) {
      await this.hubConnection.invoke('TestConnection');
    }
  }
} 