import { CommonModule } from '@angular/common';
import { Component, OnInit, EventEmitter, Output, Input, OnDestroy } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { StickyNavDirective } from '../../directive/sticky-nav.directive';
import { AuthService } from '../../core/services/auth.service';
import { AccountService } from '../../services/account.service';
import { ChatService } from '../../services/chat.service';
import { BookingManagementService } from '../../services/booking-management.service';
import { Subscription } from 'rxjs';
import {
  LucideAngularModule,
  Home,
  Heart,
  User,
  LogIn,
  LogOut,
  Menu,
  MessageCircle,
} from 'lucide-angular';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule,
    StickyNavDirective,
    LucideAngularModule,
  ],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss',
})
export class HeaderComponent implements OnInit, OnDestroy {
  readonly icons = {
    home: Home,
    heart: Heart,
    user: User,
    login: LogIn,
    logout: LogOut,
    menu: Menu,
    chat: MessageCircle,
  };
  @Input() showSearchButtons = false;

@Output() onSimpleClick = new EventEmitter<void>();
@Output() onAiClick = new EventEmitter<void>();
  unreadCount: number = 0;
  pendingBookingsCount: number = 0;
  adminPendingBookingsCount: number = 0;
  private subscription = new Subscription();

  constructor(
    private authService: AuthService,
    private accountService: AccountService,
    private chatService: ChatService,
    private bookingManagementService: BookingManagementService,
    private router: Router
  ) {}

  ngOnInit(): void {
    if (this.isAuthenticated) {
      this.loadUnreadCount();
      this.setupUnreadCountSubscription();
      this.loadPendingBookingsCount();
    }
    
    // Make component available globally for debugging
    (window as any).headerComponent = this;
  }

  // Manual refresh method for debugging
  manualRefreshUnreadCount(): void {
    console.log('Header: Manual unread count refresh triggered');
    this.loadUnreadCount();
  }

  // Debug method to check header state
  debugHeaderState(): void {
    console.log('=== HEADER STATE DEBUG ===');
    console.log('Is authenticated:', this.isAuthenticated);
    console.log('Current unread count:', this.unreadCount);
    console.log('User ID:', this.authService.getUserId());
    console.log('Active subscriptions:', this.subscription.closed ? 'None' : 'Active');
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  get isAuthenticated(): boolean {
    return this.authService.isAuthenticated();
  }

  private loadUnreadCount(): void {
    const userId = this.authService.getUserId();
    if (!userId) {
      console.log('Header: No user ID available for unread count');
      this.unreadCount = 0;
      return;
    }

    console.log('Header: Loading unread count for user:', userId);
    
    this.chatService.getUnreadCount().subscribe({
      next: (response) => {
        console.log('Header: Unread count updated:', response.unreadCount);
        this.unreadCount = response.unreadCount;
      },
      error: (error) => {
        console.error('Header: Error loading unread count:', error);
        this.unreadCount = 0;
      }
    });
  }

  private setupUnreadCountSubscription(): void {
    console.log('Setting up unread count subscriptions in header...');
    
    // Subscribe to new messages to update unread count
    const messageSubscription = this.chatService.messageReceived$.subscribe(() => {
      console.log('Header: New message received, updating unread count');
      this.loadUnreadCount();
    });

    // Subscribe to new message observable (alternative)
    const newMessageSubscription = this.chatService.newMessage$.subscribe(() => {
      console.log('Header: New message via newMessage$, updating unread count');
      this.loadUnreadCount();
    });

    // Subscribe to conversation marked as read to update unread count
    const conversationReadSubscription = this.chatService.conversationMarkedAsRead$.subscribe(() => {
      console.log('Header: Conversation marked as read, updating unread count');
      this.loadUnreadCount();
    });

    // Subscribe to chat state changes
    const chatStateSubscription = this.chatService.chatState$.subscribe((state) => {
      console.log('Header: Chat state updated, unread count:', state.unreadCount);
      this.unreadCount = state.unreadCount;
    });

    // Periodic refresh as fallback (every 10 seconds)
    const periodicRefresh = setInterval(() => {
      if (this.isAuthenticated) {
        console.log('Header: Periodic unread count refresh');
        this.loadUnreadCount();
      }
    }, 10000);

    this.subscription.add(messageSubscription);
    this.subscription.add(newMessageSubscription);
    this.subscription.add(conversationReadSubscription);
    this.subscription.add(chatStateSubscription);
    
    // Clean up interval on destroy
    this.subscription.add({
      unsubscribe: () => clearInterval(periodicRefresh)
    });
  }

  logout() {
    this.accountService.logout().subscribe({
      next: () => {
        this.authService.unsetAuthData();
        this.router.navigate(['/home']);
      },
      error: err => {
        console.error('Logout failed:', err);
        this.router.navigate(['/home']);
      },
    });
  }

  // Role checking methods
  isHost(): boolean {
    return this.authService.isHost();
  }

  isAdmin(): boolean {
    return this.authService.isAdmin();
  }

  isGuest(): boolean {
    return this.authService.isGuest();
  }
  // Load pending bookings count for hosts
  private loadPendingBookingsCount(): void {
    if (this.isHost()) {
      this.bookingManagementService.getHostPendingCount().subscribe({
        next: (count) => {
          this.pendingBookingsCount = count;
        },
        error: (error) => {
          console.error('Error loading host pending count:', error);
          this.pendingBookingsCount = 0;
        },
      });
    }

    if (this.isAdmin()) {
      this.bookingManagementService.getAdminPendingCount().subscribe({
        next: (count) => {
          this.adminPendingBookingsCount = count;
        },
        error: (error) => {
          console.error('Error loading admin pending count:', error);
          this.adminPendingBookingsCount = 0;
        },
      });
    }
  }
}
