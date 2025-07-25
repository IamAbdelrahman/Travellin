// src/app/services/chat-notification.service.ts
import { Injectable } from '@angular/core';
import { ChatService } from './chat.service';
import { MessageDto } from '../models/chat/message.model';
import { takeWhile } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ChatNotificationService {
  private isActive = true;
  private notificationPermission: NotificationPermission = 'default';

  constructor(private chatService: ChatService) {
    this.requestNotificationPermission();
    this.setupMessageListener();
  }

  private async requestNotificationPermission(): Promise<void> {
    if ('Notification' in window) {
      this.notificationPermission = await Notification.requestPermission();
    }
  }

  private setupMessageListener(): void {
    this.chatService.messageReceived$
      .pipe(takeWhile(() => this.isActive))
      .subscribe(message => {
        this.showNotification(message);
      });
  }

  private showNotification(message: MessageDto): void {
    // Only show notification if page is not visible and user is not in the active conversation
    if (document.hidden && this.notificationPermission === 'granted') {
      const activeConversation = this.chatService.getActiveConversation();
      
      // Don't show notification for active conversation
      if (activeConversation && activeConversation.id === message.conversationId) {
        return;
      }

      const notification = new Notification(`New message from ${message.senderId}`, {
        body: message.content,
        icon: '/assets/icons/message-icon.png', // Add your icon
        badge: '/assets/icons/message-badge.png', // Add your badge icon
        tag: `message-${message.id}`,
        requireInteraction: false,
        silent: false
      });

      notification.onclick = () => {
        window.focus();
        notification.close();
        // You could navigate to the specific conversation here
      };

      // Auto-close after 5 seconds
      setTimeout(() => {
        notification.close();
      }, 5000);
    }
  }

  public destroy(): void {
    this.isActive = false;
  }
}