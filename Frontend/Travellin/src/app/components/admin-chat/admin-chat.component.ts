import { Component, OnInit, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ChatService } from '../../services/chat.service';
import { TokenStorageService } from '../../services/token-storage.service';
import { UserProfileService } from '../../services/user-profile.service';
import { ToastService } from '../../services/toast.service';
import { AuthService } from '../../core/services/auth.service';
import { ConversationDto, InboxDto } from '../../models/chat/conversation.model';
import { MessageDto, CreateMessageDto } from '../../models/chat/message.model';
import { Subject, takeUntil } from 'rxjs';

@Component({
  selector: 'app-admin-chat',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './admin-chat.component.html',
  styleUrls: ['./admin-chat.component.scss']
})
export class AdminChatComponent implements OnInit, OnDestroy {
  private destroy$ = new Subject<void>();

  // Chat state
  conversations: ConversationDto[] = [];
  activeConversation: ConversationDto | null = null;
  messages: MessageDto[] = [];
  isConnected = false;
  isLoading = false;
  searchQuery = '';

  // Message input
  messageText = '';
  isSending = false;

  // Error state
  errorMessage = '';
  hasError = false;

  // User profile cache for profile images
  private userProfilesCache: Map<string, any> = new Map();

  constructor(
    private chatService: ChatService,
    private tokenStorage: TokenStorageService,
    private userProfileService: UserProfileService,
    private toastService: ToastService,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    // Check if user is admin
    const currentUserId = this.tokenStorage.getUserId();
    if (!currentUserId) {
      console.error('No user ID found - user not logged in');
      return;
    }
    
    // Start SignalR connection
    this.chatService.startConnection().then(() => {
      this.isConnected = true;
      this.loadAllConversations();
    }).catch(error => {
      console.error('Failed to connect to chat:', error);
      // Still try to load conversations even if SignalR fails
      this.loadAllConversations();
    });

    // Subscribe to real-time updates
    this.chatService.newMessage$.pipe(takeUntil(this.destroy$)).subscribe(message => {
      this.handleNewMessage(message);
    });

    this.chatService.connectionStatus$.pipe(takeUntil(this.destroy$)).subscribe(status => {
      this.isConnected = status;
    });
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  async loadAllConversations(): Promise<void> {
    this.isLoading = true;
    this.hasError = false;
    this.errorMessage = '';
    this.conversations = []; // Clear previous conversations
    
    try {
      this.chatService.getAllConversations().subscribe({
        next: (conversations) => {
          this.conversations = conversations || [];
          this.loadUserProfiles(); // Load user profiles after getting conversations
          this.isLoading = false;
          this.hasError = false;
        },
        error: (error) => {
          console.error('Error loading conversations:', error);
          this.conversations = [];
          this.isLoading = false;
          this.hasError = true;
          this.errorMessage = error.error?.message || error.message || 'Failed to load conversations';
        }
      });
    } catch (error) {
      console.error('Error in loadAllConversations:', error);
      this.isLoading = false;
      this.hasError = true;
      this.errorMessage = 'Failed to load conversations';
    }
  }

  selectConversation(conversation: ConversationDto): void {
    if (!conversation || !conversation.id) {
      console.error('Invalid conversation selected');
      return;
    }
    
    this.activeConversation = conversation;
    this.messages = []; // Clear previous messages
    this.loadMessages(conversation.id);
  }

  async loadMessages(conversationId: number): Promise<void> {
    this.messages = []; // Clear previous messages
    
    try {
      this.chatService.getMessagesByConversationIdForAdmin(conversationId).subscribe({
        next: (messages) => {
          this.messages = messages || [];
          this.markConversationAsRead(conversationId);
        },
        error: (error) => {
          console.error('Error loading messages:', error);
          // Show user-friendly error message
          this.messages = [];
        }
      });
    } catch (error) {
      console.error('Error loading messages:', error);
      this.messages = [];
    }
  }

  async sendMessage(): Promise<void> {
    if (!this.messageText.trim() || !this.activeConversation || this.isSending) {
      return;
    }

    this.isSending = true;
    const currentUserId = this.tokenStorage.getUserId();

    if (!currentUserId) {
      console.error('No current user ID found');
      this.isSending = false;
      return;
    }

    // For admin, we need to determine which user to send the message to
    // The conversation is between host and guest, so admin can send to either
    // We'll send to the user who is not the current admin
    let receiverId = '';
    if (this.activeConversation.user1Id === currentUserId) {
      // Admin is user1, send to user2
      receiverId = this.activeConversation.user2Id;
    } else if (this.activeConversation.user2Id === currentUserId) {
      // Admin is user2, send to user1
      receiverId = this.activeConversation.user1Id;
    } else {
      // Admin is not in the conversation, send to user1 (host)
      receiverId = this.activeConversation.user1Id;
    }

    const createMessageDto: CreateMessageDto = {
      senderId: currentUserId,
      receiverId: receiverId,
      content: this.messageText.trim(),
      conversationId: this.activeConversation.id
    };

    try {
      this.chatService.sendMessageAsAdmin(createMessageDto).subscribe({
        next: (message) => {
          this.messages.push(message);
          this.messageText = '';
          this.isSending = false;
        },
        error: (error) => {
          console.error('Error sending message:', error);
          this.isSending = false;
        }
      });
    } catch (error) {
      console.error('Error sending message:', error);
      this.isSending = false;
    }
  }

  private handleNewMessage(message: MessageDto): void {
    // Add message to current conversation if it matches
    if (this.activeConversation && message.conversationId === this.activeConversation.id) {
      this.messages.push(message);
    }

    // Update conversation in the list
    const conversationIndex = this.conversations.findIndex(c => c.id === message.conversationId);
    if (conversationIndex !== -1) {
      const conversation = this.conversations[conversationIndex];
      if (!conversation.messages) {
        conversation.messages = [];
      }
      conversation.messages.push(message);
    }
  }

  private markConversationAsRead(conversationId: number): void {
    this.chatService.markConversationAsReadForAdmin(conversationId).subscribe({
      next: () => {
        // Conversation marked as read successfully
      },
      error: (error) => {
        console.error('Error marking conversation as read:', error);
      }
    });
  }

  // User profile display methods
  getUserInitial(userId: string): string {
    // First try to get the user name from the conversation data
    const conversation = this.conversations.find(c => 
      c.user1Id === userId || c.user2Id === userId
    );
    
    if (conversation) {
      let userName = '';
      if (conversation.user1Id === userId) {
        userName = conversation.user1Name || '';
      } else if (conversation.user2Id === userId) {
        userName = conversation.user2Name || '';
      }
      
      if (userName) {
        return userName.charAt(0).toUpperCase();
      }
    }
    
    // Fallback to user ID
    return userId.charAt(0).toUpperCase();
  }

  shouldShowProfileImage(userId: string): boolean {
    const profile = this.userProfilesCache.get(userId);
    const hasImage = !!(profile && profile.photo && profile.photo.photoUrl && profile.photo.photoUrl.trim() !== '');
    return hasImage;
  }

  getUserProfileImage(userId: string): string {
    const profile = this.userProfilesCache.get(userId);
    const imageUrl = profile?.photo?.photoUrl || '';
    return imageUrl;
  }

  getUserName(userId: string): string {
    const currentUserId = this.authService.getUserId();
    
    // Check if this is the current user (admin)
    if (userId === currentUserId) {
      const adminProfile = this.userProfilesCache.get(userId);
      if (adminProfile) {
        const fullName = `${adminProfile.firstName} ${adminProfile.lastName}`.trim();
        if (fullName) {
          return fullName;
        }
        if (adminProfile.firstName) {
          return adminProfile.firstName;
        }
      }
      return 'Admin';
    }

    // Check active conversation participants
    if (this.activeConversation) {
      if (this.activeConversation.user1Id === userId) {
        const name = this.activeConversation.user1Name;
        if (name) return name;
      }
      if (this.activeConversation.user2Id === userId) {
        const name = this.activeConversation.user2Name;
        if (name) return name;
      }
    }

    // Check user profile cache
    const userProfile = this.userProfilesCache.get(userId);
    if (userProfile) {
      const fullName = `${userProfile.firstName} ${userProfile.lastName}`.trim();
      if (fullName) {
        return fullName;
      }
      if (userProfile.firstName) {
        return userProfile.firstName;
      }
    }

    // Fallback
    const fallbackName = this.getDisplayName(userId);
    return fallbackName || 'Unknown User';
  }

  getDisplayName(userId: string): string {
    return `User ${userId.substring(0, 8)}`;
  }

  // Get the other user in a conversation (not the current user)
  getOtherUserName(conversation: ConversationDto): string {
    const currentUserId = this.tokenStorage.getUserId();
    
    if (conversation.user1Id === currentUserId) {
      return conversation.user2Name || `User ${conversation.user2Id.substring(0, 8)}`;
    } else {
      return conversation.user1Name || `User ${conversation.user1Id.substring(0, 8)}`;
    }
  }

  // Get the other user's initial in a conversation
  getOtherUserInitial(conversation: ConversationDto): string {
    const currentUserId = this.tokenStorage.getUserId();
    
    if (conversation.user1Id === currentUserId) {
      const userName = conversation.user2Name || '';
      return userName ? userName.charAt(0).toUpperCase() : conversation.user2Id.charAt(0).toUpperCase();
    } else {
      const userName = conversation.user1Name || '';
      return userName ? userName.charAt(0).toUpperCase() : conversation.user1Id.charAt(0).toUpperCase();
    }
  }

  // Message display methods
  formatMessageTime(date: Date): string {
    const messageDate = new Date(date);
    const now = new Date();
    const diffInHours = (now.getTime() - messageDate.getTime()) / (1000 * 60 * 60);

    if (diffInHours < 1) {
      const diffInMinutes = Math.floor(diffInHours * 60);
      return `${diffInMinutes}m ago`;
    } else if (diffInHours < 24) {
      return `${Math.floor(diffInHours)}h ago`;
    } else {
      return messageDate.toLocaleDateString();
    }
  }

  formatMessageTimeForTooltip(date: Date): string {
    return new Date(date).toLocaleString();
  }

  getMessageStatusFromMessage(message: MessageDto): string {
    if (message.isRead) {
      return '✓✓';
    }
    return '✓';
  }

  isOwnMessage(message: MessageDto): boolean {
    return message.senderId === this.tokenStorage.getUserId();
  }

  // Check if message is from admin
  isAdminMessage(message: MessageDto): boolean {
    const adminUserId = this.tokenStorage.getUserId();
    return message.senderId === adminUserId;
  }

  // Get the conversation participants (host and guest)
  getConversationParticipants(conversation: ConversationDto): { host: string; guest: string } {
    // For now, we'll assume user1 is host and user2 is guest
    // In a real implementation, you'd need to determine this based on user roles
    return {
      host: conversation.user1Name || `User ${conversation.user1Id.substring(0, 8)}`,
      guest: conversation.user2Name || `User ${conversation.user2Id.substring(0, 8)}`
    };
  }

  // Get the role of a user in the conversation (host, guest, or admin)
  getUserRole(userId: string, conversation: ConversationDto): string {
    const currentUserId = this.tokenStorage.getUserId();
    
    if (userId === currentUserId) {
      return 'Admin';
    }
    
    // For now, assume user1 is host and user2 is guest
    // In a real implementation, you'd need to check user roles
    if (userId === conversation.user1Id) {
      return 'Host';
    } else if (userId === conversation.user2Id) {
      return 'Guest';
    }
    
    return 'User';
  }

  // Handle Enter key in textarea
  onKeyDown(event: KeyboardEvent): void {
    if (event.key === 'Enter' && !event.shiftKey) {
      event.preventDefault();
      this.sendMessage();
    }
  }

  // Search functionality
  filterConversations(): ConversationDto[] {
    if (!this.searchQuery.trim()) {
      return this.conversations;
    }

    const query = this.searchQuery.toLowerCase();
    return this.conversations.filter(conversation => {
      const user1Name = this.getUserName(conversation.user1Id).toLowerCase();
      const user2Name = this.getUserName(conversation.user2Id).toLowerCase();
      const propertyTitle = conversation.propertyTitle?.toLowerCase() || '';
      
      return user1Name.includes(query) || 
             user2Name.includes(query) || 
             propertyTitle.includes(query);
    });
  }

  // Utility methods
  getUnreadCount(conversation: ConversationDto): number {
    if (!conversation.messages) return 0;
    const currentUserId = this.tokenStorage.getUserId();
    return conversation.messages.filter(m => 
      !m.isRead && m.senderId !== currentUserId
    ).length;
  }

  getLastMessage(conversation: ConversationDto): string {
    if (!conversation.messages || conversation.messages.length === 0) {
      return 'No messages yet';
    }
    
    const lastMessage = conversation.messages[conversation.messages.length - 1];
    return lastMessage.content.length > 50 
      ? lastMessage.content.substring(0, 50) + '...'
      : lastMessage.content;
  }

  getLastMessageTime(conversation: ConversationDto): string {
    if (!conversation.messages || conversation.messages.length === 0) {
      return '';
    }
    
    const lastMessage = conversation.messages[conversation.messages.length - 1];
    return this.formatMessageTime(lastMessage.sentAt);
  }

  // Delete conversation functionality
  async deleteConversation(conversationId: number): Promise<void> {
    if (!confirm('Are you sure you want to delete this conversation? This action cannot be undone.')) {
      return;
    }

    try {
      await this.chatService.deleteConversationAsAdmin(conversationId).toPromise();
      
      // Remove from local list
      this.conversations = this.conversations.filter(c => c.id !== conversationId);
      
      // If this was the active conversation, clear it
      if (this.activeConversation?.id === conversationId) {
        this.activeConversation = null;
        this.messages = [];
      }
      
      this.toastService.showSuccess('Conversation deleted successfully');
    } catch (error) {
      console.error('Error deleting conversation:', error);
      this.toastService.showError('Failed to delete conversation');
    }
  }

  // Check if user can delete conversation (admin only)
  canDeleteConversation(): boolean {
    return this.authService.isAdmin();
  }

  private loadUserProfiles(): void {
    // Get unique user IDs from conversations
    const userIds = new Set<string>();
    
    // Add current user
    const currentUserId = this.tokenStorage.getUserId();
    if (currentUserId) {
      userIds.add(currentUserId);
    }
    
    // Add users from conversations
    this.conversations.forEach(conversation => {
      userIds.add(conversation.user1Id);
      userIds.add(conversation.user2Id);
    });
    
    // Convert to array
    const usersToLoad = Array.from(userIds);
    
    if (usersToLoad.length === 0) {
      return; // No users to load
    }
    
    // Load profiles for users
    this.userProfileService.getUserProfilesByUserIds(usersToLoad).subscribe({
      next: (response) => {
        if (response && response.body && response.body.items) {
          // Store profiles in cache
          response.body.items.forEach((profile: any) => {
            this.userProfilesCache.set(profile.userId, profile);
          });
        }
      },
      error: (error) => {
        console.error('admin-chat loadUserProfiles - Error loading user profiles:', error);
      }
    });
  }
} 