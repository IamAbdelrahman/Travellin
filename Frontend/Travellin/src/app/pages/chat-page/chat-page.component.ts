// src/app/pages/chat-page/chat-page.component.ts
import { Component, OnInit, OnDestroy, ViewChild, ElementRef, AfterViewChecked } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { Subject, takeUntil, Observable } from 'rxjs';
import { ChatService } from '../../services/chat.service';
import { ToastService } from '../../services/toast.service';
import { AuthService } from '../../core/services/auth.service';
import { UserProfileService } from '../../services/user-profile.service';
import { TokenStorageService } from '../../services/token-storage.service';
import { 
  MessageDto, 
  CreateMessageDto, 
} from '../../models/chat/message.model';
import { ConversationDto, InboxDto , ConversationSearchResultDto  } from '../../models/chat/conversation.model';
import { IUserProfile } from '../../models/domain/iuser-profile';

@Component({
  selector: 'app-chat-page',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './chat-page.component.html',
  styleUrls: ['./chat-page.component.scss']
})
export class ChatPageComponent implements OnInit, OnDestroy, AfterViewChecked {
  @ViewChild('messagesContainer') private messagesContainer!: ElementRef;
  @ViewChild('messageInput') private messageInput!: ElementRef;

  private destroy$ = new Subject<void>();
  private shouldScrollToBottom = false;
  private refreshInterval: any = null;
  private lastRefreshTime: Date = new Date();

  // State
  currentUserId: string = '';
  isConnected: boolean = false;
  conversations: ConversationDto[] = [];
  activeConversation?: ConversationDto;
  inbox: InboxDto[] = [];
  unreadCount: number = 0;
  isAdmin: boolean = false;
  isLoading: boolean = false;
  isSendingMessage: boolean = false;
  newMessageContent: string = '';
  searchQuery: string = '';
  messageStatus: Map<number, 'sending' | 'sent' | 'delivered' | 'read' | 'error'> = new Map();
  filteredInbox: InboxDto[] = [];
  showMobileChat: boolean = false;

  // Enhanced error handling and loading states
  hasError: boolean = false;
  errorMessage: string = '';
  isLoadingConversations: boolean = false;
  isLoadingMessages: boolean = false;
  
  // User profile cache for profile images
  private userProfilesCache: Map<string, IUserProfile> = new Map();

  constructor(
    private chatService: ChatService,
    private toastService: ToastService,
    private authService: AuthService,
    private userProfileService: UserProfileService,
    private tokenStorage: TokenStorageService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.setupSubscriptions();
    this.loadInitialData();
    this.startSilentRefresh();
    
    // Check for conversationId in URL query params
    const conversationId = this.route.snapshot.queryParams['conversationId'];
    if (conversationId) {
      this.selectConversationById(conversationId);
    }
  }













  // Manual refresh method (can be called by user or automatically)
  async manualRefresh(): Promise<void> {
    try {
      await this.silentRefreshData();
    } catch (error) {
      console.error('Manual refresh failed:', error);
    }
  }

  private logRefreshStatus(): void {
    // Removed console.log statements
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
    this.stopSilentRefresh();
    this.chatService.stopConnection();
  }

  // Silent refresh methods
  private startSilentRefresh(): void {
    this.refreshInterval = setInterval(() => {
      this.silentRefreshData();
    }, 30000); // Refresh every 30 seconds
  }

  private stopSilentRefresh(): void {
    if (this.refreshInterval) {
      clearInterval(this.refreshInterval);
      this.refreshInterval = null;
    }
  }

  private async silentRefreshData(): Promise<void> {
    try {
      this.lastRefreshTime = new Date();
      
      // Refresh conversations and inbox
      const userId = this.authService.getUserId();
      if (!userId) return;

      const [conversations, inbox] = await Promise.all([
        this.chatService.getAllConversations().toPromise(),
        this.chatService.getInboxPreview(userId).toPromise()
      ]);

      if (conversations) {
        this.updateConversationsSilently(conversations);
      }

      if (inbox) {
        this.updateInboxSilently(inbox);
      }

      // Refresh active conversation messages if needed
      if (this.activeConversation) {
        this.refreshActiveConversationMessages();
      }

    } catch (error) {
      // Silent refresh failed, but don't show error to user
    }
  }

  private updateConversationsSilently(newConversations: ConversationDto[]): void {
    // Only update if there are new messages
    const hasNewMessages = this.hasNewMessagesInConversations(newConversations);
    if (hasNewMessages) {
      this.conversations = newConversations;
      
      // Update active conversation if it exists
      if (this.activeConversation) {
        const updatedActiveConversation = newConversations.find(c => c.id === this.activeConversation?.id);
        if (updatedActiveConversation) {
          this.activeConversation = updatedActiveConversation;
          this.shouldScrollToBottom = true;
        }
      }
    }
  }

  private updateInboxSilently(newInbox: InboxDto[]): void {
    // Only update if there are changes
    const hasChanges = this.hasInboxChanges(newInbox);
    if (hasChanges) {
      this.inbox = newInbox;
      this.filteredInbox = this.searchQuery.trim() ? this.inbox.filter(item => 
        item.participant.toLowerCase().includes(this.searchQuery.toLowerCase()) ||
        item.lastMessage?.toLowerCase().includes(this.searchQuery.toLowerCase())
      ) : this.inbox;
      this.updateTotalUnreadCount();
    }
  }

  private hasNewMessagesInConversations(newConversations: ConversationDto[]): boolean {
    for (const newConv of newConversations) {
      const existingConv = this.conversations.find(c => c.id === newConv.id);
      if (!existingConv) return true; // New conversation
      
      if (newConv.messages.length !== existingConv.messages.length) {
        return true; // Different number of messages
      }
      
      // Check if any messages are different
      for (let i = 0; i < newConv.messages.length; i++) {
        if (newConv.messages[i].id !== existingConv.messages[i]?.id) {
          return true; // Different message
        }
      }
    }
    return false;
  }

  private hasInboxChanges(newInbox: InboxDto[]): boolean {
    if (newInbox.length !== this.inbox.length) return true;
    
    for (let i = 0; i < newInbox.length; i++) {
      const newItem = newInbox[i];
      const existingItem = this.inbox[i];
      
      if (newItem.conversationId !== existingItem.conversationId) return true;
      if (newItem.lastMessage !== existingItem.lastMessage) return true;
      if (newItem.unreadCount !== existingItem.unreadCount) return true;
    }
    return false;
  }

  private refreshActiveConversationMessages(): void {
    if (!this.activeConversation) return;
    
    this.chatService.getMessagesByConversationId(this.activeConversation.id).subscribe({
      next: (messages) => {
        if (messages.length !== this.activeConversation?.messages.length) {
          this.activeConversation!.messages = messages;
          this.shouldScrollToBottom = true;
        }
      },
      error: (error) => {
        console.error('Silent refresh active conversation error:', error);
      }
    });
  }

  ngAfterViewChecked(): void {
    if (this.shouldScrollToBottom) {
      this.scrollToBottom();
      this.shouldScrollToBottom = false;
    }
  }

  private async initializeChat(): Promise<void> {
    this.isLoading = true;
    try {
      await this.chatService.startConnection();
      this.toastService.showSuccess('Chat connected successfully');
    } catch (error) {
      console.error('Failed to establish chat connection:', error);
      this.toastService.showError('Failed to connect to chat. Please refresh the page.');
    } finally {
      this.isLoading = false;
    }
  }

  private setupSubscriptions(): void {
    // Subscribe to chat state
    this.chatService.chatState$
      .pipe(takeUntil(this.destroy$))
      .subscribe(state => {
        this.currentUserId = state.currentUserId;
        this.conversations = state.conversations;
        this.activeConversation = state.activeConversation;
        this.isConnected = state.isConnected;
      });

    // Subscribe to new messages
    this.chatService.newMessage$
      .pipe(takeUntil(this.destroy$))
      .subscribe((message: MessageDto) => {
        this.handleNewMessage(message);
      });

    // Subscribe to message sent events
    this.chatService.messageSent$
      .pipe(takeUntil(this.destroy$))
      .subscribe((message: MessageDto) => {
        this.handleMessageSent(message);
      });

    // Subscribe to message read status
    this.chatService.messageMarkedAsRead$
      .pipe(takeUntil(this.destroy$))
      .subscribe((messageId: number) => {
        this.handleMessageMarkedAsRead(messageId);
      });

    // Subscribe to conversation read status
    this.chatService.conversationMarkedAsRead$
      .pipe(takeUntil(this.destroy$))
      .subscribe((conversationId: number) => {
        this.handleConversationMarkedAsRead(conversationId);
      });

    // Subscribe to new conversations
    this.chatService.newConversation$
      .pipe(takeUntil(this.destroy$))
      .subscribe((conversation: ConversationDto) => {
        this.handleNewConversation(conversation);
      });
  }

  private loadInitialData(): void {
    this.isLoading = true;
    this.hasError = false;

    // Load user role and conversations based on role
    this.isAdmin = this.authService.isAdmin();

    if (this.isAdmin) {
      // Admin loads all conversations
      this.loadAllConversations();
    } else {
      // Regular user loads their conversations
      const userId = this.authService.getUserId();
      if (userId) {
        this.chatService.getUserConversations(userId)
          .pipe(takeUntil(this.destroy$))
          .subscribe({
            next: (conversations) => {
              this.conversations = conversations;
              this.isLoading = false;
            },
            error: (error) => {
              this.handleError('Failed to load conversations', error);
            }
          });
      }
    }

    // Load inbox for all users
    const userId = this.authService.getUserId();
    if (userId) {
      this.chatService.getInboxPreview(userId)
        .pipe(takeUntil(this.destroy$))
        .subscribe({
          next: (inbox) => {
            this.inbox = inbox;
            this.filteredInbox = inbox;
            this.updateTotalUnreadCount();
          },
          error: (error) => {
            this.handleError('Failed to load inbox', error);
          }
        });
    }
  }

  private handleError(message: string, error: any): void {
    this.hasError = true;
    this.errorMessage = error.error?.message || error.message || message;
    this.isLoading = false;
    this.toastService.showError(this.errorMessage);
  }

  async sendMessage(): Promise<void> {
    if (!this.activeConversation || !this.newMessageContent.trim()) {
      return;
    }

    const messageContent = this.newMessageContent.trim();
    this.newMessageContent = '';
    this.isSendingMessage = true;

    const receiverId = this.activeConversation.user1Id === this.currentUserId ? 
        this.activeConversation.user2Id : this.activeConversation.user1Id;

    const createMessageDto: CreateMessageDto = {
      conversationId: this.activeConversation.id,
      content: messageContent,
      senderId: this.currentUserId,
      receiverId: receiverId
    };

    try {
      const message = await this.chatService.sendMessage(createMessageDto).toPromise();
      
      if (message) {
        // Add message to active conversation
        this.activeConversation.messages.push(message);
        this.shouldScrollToBottom = true;
        
        // Update message status
        this.messageStatus.set(message.id, 'sent');
        
        // Update unread count for other participants
        this.handleMessageSent(message);
      }
    } catch (error) {
      this.toastService.showError('Failed to send message');
      this.newMessageContent = messageContent; // Restore the message content
    } finally {
      this.isSendingMessage = false;
    }
  }

  selectConversation(conversation: ConversationDto): void {
    if (this.activeConversation?.id === conversation.id) {
      return;
    }

    // Leave current conversation group
    if (this.activeConversation) {
      this.leaveConversationGroup(this.activeConversation.id);
    }

    // Set new active conversation
    this.activeConversation = conversation;
    this.loadUserProfiles();

    // Join new conversation group
    this.joinConversationGroup(conversation.id);

    // Mark conversation as read
    this.markConversationAsRead(conversation.id);
  }

  selectConversationByInbox(inboxItem: InboxDto): void {
    // Find conversation in loaded conversations
    let conversation = this.conversations.find(c => c.id === inboxItem.conversationId);
    
    if (conversation) {
      this.selectConversation(conversation);
    } else {
      // Load conversation from API if not in memory
      this.chatService.getConversationById(inboxItem.conversationId)
        .pipe(takeUntil(this.destroy$))
        .subscribe({
          next: (loadedConversation) => {
            this.selectConversation(loadedConversation);
          },
          error: (error) => {
            this.handleError('Failed to load conversation', error);
          }
        });
    }
  }

  selectConversationById(conversationId: string | number): void {
    const id = typeof conversationId === 'string' ? parseInt(conversationId, 10) : conversationId;
    
    // Find conversation in loaded conversations
    let conversation = this.conversations.find(c => c.id === id);
    
    if (conversation) {
      this.selectConversation(conversation);
    } else {
      // Load conversation from API if not in memory
      this.chatService.getConversationById(id)
        .pipe(takeUntil(this.destroy$))
        .subscribe({
          next: (conversation) => {
            this.selectConversation(conversation);
          },
          error: (error) => {
            this.handleError('Failed to load conversation', error);
          }
        });
    }
  }

  private async joinConversationGroup(conversationId: number): Promise<void> {
    try {
      await this.chatService.joinConversationViaHub(conversationId);
    } catch (error) {
      console.error('Error joining conversation group:', error);
    }
  }

  private async leaveConversationGroup(conversationId: number): Promise<void> {
    try {
      await this.chatService.leaveConversationViaHub(conversationId);
    } catch (error) {
      console.error('Error leaving conversation group:', error);
    }
  }

  private markConversationAsRead(conversationId: number): void {
    this.chatService.markConversationAsRead(conversationId).subscribe({
      next: () => {
        // Update local state
        if (this.activeConversation) {
          this.activeConversation.messages?.forEach(message => {
            if (!message.isRead && message.senderId !== this.currentUserId) {
              message.isRead = true;
            }
          });
        }
      },
      error: (error: any) => {
        console.error('Error marking conversation as read:', error);
      }
    });
  }

  private handleNewMessage(message: MessageDto): void {
    // Update conversation messages
    const conversation = this.conversations.find(c => c.id === message.conversationId);
    if (conversation) {
      conversation.messages.push(message);
      
      // Update active conversation if it's the same
      if (this.activeConversation?.id === message.conversationId) {
        this.activeConversation = conversation;
        this.shouldScrollToBottom = true;
      }
    }

    // Update inbox unread count
    const inboxItem = this.inbox.find(item => item.conversationId === message.conversationId);
    if (inboxItem) {
      const previousCount = inboxItem.unreadCount || 0;
      
      // Only increment unread count if message is not from current user
      if (message.senderId !== this.currentUserId) {
        inboxItem.unreadCount = previousCount + 1;
        this.updateTotalUnreadCount();
      }
    }
  }

  private updateTotalUnreadCount(): void {
    this.unreadCount = this.inbox.reduce((total, item) => total + (item.unreadCount || 0), 0);
  }

  private handleMessageSent(message: MessageDto): void {
    // Update message status
    this.messageStatus.set(message.id, 'sent');
  }

  private handleNewConversation(conversation: ConversationDto): void {
    this.conversations.push(conversation);
  }

  private handleMessageMarkedAsRead(messageId: number): void {
    // Update message status in active conversation
    if (this.activeConversation) {
      const message = this.activeConversation.messages.find(m => m.id === messageId);
      if (message) {
        message.isRead = true;
      }
    }
  }

  private handleConversationMarkedAsRead(conversationId: number): void {
    // Reset unread count for this conversation in inbox
    const inboxItem = this.inbox.find(item => item.conversationId === conversationId);
    if (inboxItem) {
      inboxItem.unreadCount = 0;
      this.updateTotalUnreadCount();
    }
  }

  onSearchChange(): void {
    this.filterInbox();
  }

  private filterInbox(): void {
    if (!this.searchQuery.trim()) {
      this.filteredInbox = this.inbox;
    } else {
      const query = this.searchQuery.toLowerCase();
      this.filteredInbox = this.inbox.filter(item => 
        item.participant.toLowerCase().includes(query) ||
        item.lastMessage?.toLowerCase().includes(query)
      );
    }
  }

  formatMessageTime(sentAt: Date | undefined): string {
    if (!sentAt) return '';
    
    const now = new Date();
    const messageDate = new Date(sentAt);
    const diffInHours = (now.getTime() - messageDate.getTime()) / (1000 * 60 * 60);
    
    // If message is from today, show only time
    if (messageDate.toDateString() === now.toDateString()) {
      return messageDate.toLocaleTimeString('en-US', { 
        hour: 'numeric', 
        minute: '2-digit',
        hour12: true 
      });
    }
    
    // If message is from yesterday, show "Yesterday" and time
    const yesterday = new Date(now);
    yesterday.setDate(yesterday.getDate() - 1);
    if (messageDate.toDateString() === yesterday.toDateString()) {
      return `Yesterday ${messageDate.toLocaleTimeString('en-US', { 
        hour: 'numeric', 
        minute: '2-digit',
        hour12: true 
      })}`;
    }
    
    // If message is from this week, show day name and time
    if (diffInHours < 168) { // 7 days
      return messageDate.toLocaleDateString('en-US', { 
        weekday: 'short',
        hour: 'numeric', 
        minute: '2-digit',
        hour12: true 
      });
    }
    
    // If message is older, show full date and time
    return messageDate.toLocaleDateString('en-US', { 
      month: 'short',
      day: 'numeric',
      year: 'numeric',
      hour: 'numeric', 
      minute: '2-digit',
      hour12: true 
    });
  }

  formatMessageTimeForTooltip(sentAt: Date | undefined): string {
    if (!sentAt) return '';
    
    const messageDate = new Date(sentAt);
    return messageDate.toLocaleDateString('en-US', { 
      weekday: 'long',
      year: 'numeric',
      month: 'long',
      day: 'numeric',
      hour: 'numeric', 
      minute: '2-digit',
      hour12: true 
    });
  }

  private scrollToBottom(): void {
    if (this.messagesContainer) {
      const element = this.messagesContainer.nativeElement;
      element.scrollTop = element.scrollHeight;
    }
  }

  onKeyPress(event: KeyboardEvent): void {
    if (event.key === 'Enter' && !event.shiftKey) {
      event.preventDefault();
      this.sendMessage();
    }
  }

  get connectionStatusText(): string {
    return this.isConnected ? 'Connected' : 'Disconnected';
  }

  get connectionStatusClass(): string {
    return this.isConnected ? 'bg-success' : 'bg-danger';
  }

  // Admin methods
  loadAllConversations(): void {
    if (!this.isAdmin) return;
    
    this.chatService.getAllConversations().subscribe({
      next: (conversations) => {
        this.conversations = conversations;
      },
      error: (error) => {
        this.toastService.showError('Failed to load all conversations');
      }
    });
  }

  sendAsAdmin(): void {
    if (!this.activeConversation || !this.newMessageContent.trim() || !this.isAdmin) {
      return;
    }

    const messageContent = this.newMessageContent.trim();
    this.newMessageContent = '';
    this.isSendingMessage = true;

    const receiverId = this.activeConversation.user1Id === this.currentUserId ? 
        this.activeConversation.user2Id : this.activeConversation.user1Id;

    const createMessageDto: CreateMessageDto = {
      conversationId: this.activeConversation.id,
      content: messageContent,
      senderId: this.currentUserId,
      receiverId: receiverId
    };

    try {
      const message = this.chatService.sendMessageAsAdmin(createMessageDto);
      message.subscribe({
        next: (sentMessage) => {
          // Add message to active conversation
          this.activeConversation!.messages.push(sentMessage);
          this.shouldScrollToBottom = true;
          
          // Update message status
          this.messageStatus.set(sentMessage.id, 'sent');
          
          // Update unread count for other participants
          this.handleMessageSent(sentMessage);
          
          this.isSendingMessage = false;
        },
        error: (error) => {
          this.toastService.showError('Failed to send admin message');
          this.newMessageContent = messageContent; // Restore the message content
          this.isSendingMessage = false;
        }
      });
    } catch (error) {
      this.toastService.showError('Failed to send admin message');
      this.newMessageContent = messageContent; // Restore the message content
      this.isSendingMessage = false;
    }
  }

  isMessageFromCurrentUser(message: MessageDto): boolean {
    if (!this.currentUserId) {
      console.error('Current user ID is not set for message comparison');
      return false;
    }
    return message.senderId === this.currentUserId;
  }

  isMessageUnread(message: MessageDto): boolean {
    return !message.isRead && message.senderId !== this.currentUserId;
  }

  getConversationUnreadCount(conversation: ConversationDto): number {
    return conversation.messages?.filter(m => this.isMessageUnread(m)).length || 0;
  }

  hasUnreadMessages(conversation: ConversationDto): boolean {
    return this.getConversationUnreadCount(conversation) > 0;
  }

  trackByConversationId(index: number, item: InboxDto): number {
    return item.conversationId;
  }

  trackByMessageId(index: number, item: MessageDto): number {
    return item.id;
  }

  getParticipantInitial(participant: string): string {
    return participant.charAt(0).toUpperCase();
  }

  getDisplayName(participant: string): string {
    // If participant is a user ID (starts with a letter and contains numbers/letters)
    if (participant && participant.length > 8 && /^[a-zA-Z0-9-]+$/.test(participant)) {
      // It's likely a user ID, show a shortened version
      return `User ${participant.substring(0, 8)}`;
    }
    
    // First try to parse as "FirstName LastName"
    const nameParts = participant.split(' ');
    if (nameParts.length >= 2) {
      return `${nameParts[0]} ${nameParts[1]}`;
    }
    
    // If it's just one word, return as is
    return participant || 'Unknown User';
  }

  // New methods for message status
  getMessageStatus(messageId: number): 'sending' | 'sent' | 'delivered' | 'read' | 'error' {
    return this.messageStatus.get(messageId) || 'sent';
  }

  getMessageStatusFromMessage(message: MessageDto): string {
    if (this.isMessageFromCurrentUser(message)) {
      if (message.isRead) {
        return 'Read';
      } else {
        return 'Delivered';
      }
    } else {
      return 'Received';
    }
  }

  getMessageStatusIcon(status: 'sending' | 'sent' | 'delivered' | 'read' | 'error'): string {
    switch (status) {
      case 'sending': return '⏳';
      case 'sent': return '✓';
      case 'delivered': return '✓✓';
      case 'read': return '✓✓';
      case 'error': return '❌';
      default: return '✓';
    }
  }

  getMessageStatusClass(status: 'sending' | 'sent' | 'delivered' | 'read' | 'error'): string {
    switch (status) {
      case 'sending': return 'text-muted';
      case 'sent': return 'text-muted';
      case 'delivered': return 'text-info';
      case 'read': return 'text-success';
      case 'error': return 'text-danger';
      default: return 'text-muted';
    }
  }

  isMessageStatusVisible(message: MessageDto): boolean {
    return this.isMessageFromCurrentUser(message) && 
           (this.getMessageStatus(message.id) === 'sent' || 
            this.getMessageStatus(message.id) === 'delivered' || 
            this.getMessageStatus(message.id) === 'read');
  }

  // Admin-specific methods for enhanced UI/UX
  isAdminMessage(message: MessageDto): boolean {
    const isFromAdminUser = message.senderId === '2dacdb51-fee9-4479-904c-cafe7dca22a6';
    const hasAdminIndicator = message.content.includes('[ADMIN]') || message.content.includes('Admin:');
    const isCurrentUserAdmin = this.currentUserId === '2dacdb51-fee9-4479-904c-cafe7dca22a6';
    const isCurrentUserAdminMessage = message.senderId === this.currentUserId && isCurrentUserAdmin;
    
    return isFromAdminUser || hasAdminIndicator || isCurrentUserAdminMessage;
  }
  
  private isUserAdminOnly(userId: string): boolean {
    // Only check for actual admin users, not host users
    const adminUserIds = [
      '2dacdb51-fee9-4479-904c-cafe7dca22a6', // Admin user
    ];
    
    return adminUserIds.includes(userId);
  }

  private isUserHost(userId: string): boolean {
    // Check if the user is a host
    const hostUserIds = [
      '3dacdb51-fee9-4479-904c-cafe7dca22a7', // Host user
    ];
    
    return hostUserIds.includes(userId);
  }

  private isUserGuest(userId: string): boolean {
    // Check if the user is a guest
    const guestUserIds = [
      '4dacdb51-fee9-4479-904c-cafe7dca22a8', // Guest user
    ];
    
    return guestUserIds.includes(userId);
  }

  getConversationParticipants(conversation: ConversationDto): { host: string; guest: string } {
    // For now, we'll assume user1 is host and user2 is guest
    // In a real implementation, you'd need to determine this based on user roles
    return {
      host: conversation.user1Name || `User ${conversation.user1Id.substring(0, 8)}`,
      guest: conversation.user2Name || `User ${conversation.user2Id.substring(0, 8)}`
    };
  }

  getUserRole(userId: string, conversation: ConversationDto): string {
    const currentUserId = this.currentUserId;
    
    // Check if current user is admin
    if (userId === currentUserId && this.isAdmin) {
      return 'Admin';
    }
    
    // Check specific user roles based on known IDs
    if (this.isUserAdminOnly(userId)) {
      return 'Admin';
    } else if (this.isUserHost(userId)) {
      return 'Host';
    } else if (this.isUserGuest(userId)) {
      return 'Guest';
    }
    
    // Fallback: assume user1 is host and user2 is guest
    if (userId === conversation.user1Id) {
      return 'Host';
    } else if (userId === conversation.user2Id) {
      return 'Guest';
    }
    
    return 'User';
  }

  getUserInitial(userId: string): string {
    // Get user name from conversation participants
    if (this.activeConversation) {
      if (userId === this.activeConversation.user1Id) {
        const userName = this.activeConversation.user1Name || '';
        return userName ? userName.charAt(0).toUpperCase() : userId.charAt(0).toUpperCase();
      } else if (userId === this.activeConversation.user2Id) {
        const userName = this.activeConversation.user2Name || '';
        return userName ? userName.charAt(0).toUpperCase() : userId.charAt(0).toUpperCase();
      }
    }
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
    // Check if this is the current user (admin)
    if (userId === this.currentUserId) {
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

    // Check if this is the specific admin user ID
    if (userId === '2dacdb51-fee9-4479-904c-cafe7dca22a6') {
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

  getOtherUserName(conversation: ConversationDto): string {
    const currentUserId = this.currentUserId;
    
    if (conversation.user1Id === currentUserId) {
      return conversation.user2Name || `User ${conversation.user2Id.substring(0, 8)}`;
    } else {
      return conversation.user1Name || `User ${conversation.user1Id.substring(0, 8)}`;
    }
  }

  getOtherUserInitial(conversation: ConversationDto): string {
    const currentUserId = this.currentUserId;
    
    if (conversation.user1Id === currentUserId) {
      const userName = conversation.user2Name || '';
      return userName ? userName.charAt(0).toUpperCase() : conversation.user2Id.charAt(0).toUpperCase();
    } else {
      const userName = conversation.user1Name || '';
      return userName ? userName.charAt(0).toUpperCase() : conversation.user1Id.charAt(0).toUpperCase();
    }
  }

  // Load user profiles for profile images
  private loadUserProfiles(): void {
    // Get unique user IDs from conversations
    const userIds = new Set<string>();
    
    // Add current user
    if (this.currentUserId) {
      userIds.add(this.currentUserId);
    }
    
    // Add users from conversations
    this.conversations.forEach(conversation => {
      userIds.add(conversation.user1Id);
      userIds.add(conversation.user2Id);
    });
    
    // Add users from active conversation messages
    if (this.activeConversation && this.activeConversation.messages) {
      this.activeConversation.messages.forEach(message => {
        userIds.add(message.senderId);
        userIds.add(message.receiverId);
      });
    }
    
    // Explicitly add admin user ID to ensure admin profile is loaded
    userIds.add('2dacdb51-fee9-4479-904c-cafe7dca22a6');
    
    // Convert to array and filter out users already in cache
    const usersToLoad = Array.from(userIds).filter(userId => !this.userProfilesCache.has(userId));
    
    if (usersToLoad.length === 0) {
      return; // All profiles already loaded
    }
    
    // Load profiles for users not in cache
    this.userProfileService.getUserProfilesByUserIds(usersToLoad).subscribe({
      next: (response) => {
        if (response && response.body && response.body.items) {
          response.body.items.forEach((profile: IUserProfile) => {
            this.userProfilesCache.set(profile.userId, profile);
          });
        }
      },
      error: (error) => {
        console.error('Error loading user profiles:', error);
      }
    });
  }



  // Enhanced error recovery method
  retryLoadData(): void {
    this.hasError = false;
    this.errorMessage = '';
    this.loadInitialData();
  }
}