// src/app/pages/chat-page/chat-page.component.ts
import { Component, OnInit, OnDestroy, ViewChild, ElementRef, AfterViewChecked } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { Subject, takeUntil } from 'rxjs';
import { ChatService } from '../../services/chat.service';
import { ToastService } from '../../services/toast.service';
import { AuthService } from '../../core/services/auth.service';
import { 
  MessageDto, 
  CreateMessageDto, 
} from '../../models/chat/message.model';
import { ConversationDto, InboxDto , ConversationSearchResultDto  } from '../../models/chat/conversation.model';

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

  // State
  currentUserId: string = '';
  isConnected: boolean = false;
  conversations: ConversationDto[] = [];
  activeConversation?: ConversationDto;
  inbox: InboxDto[] = [];
  unreadCount: number = 0;
  isAdmin: boolean = false;

  // UI State
  newMessageContent: string = '';
  searchQuery: string = '';
  isLoading: boolean = false;
  isSendingMessage: boolean = false;

  // Filters
  filteredInbox: InboxDto[] = [];

  constructor(
    private chatService: ChatService,
    private toastService: ToastService,
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.currentUserId = this.authService.getUserId() || '';
    this.isAdmin = this.authService.isAdmin();
    
    // Update the chat service with the current user ID
    this.chatService.updateCurrentUserId();
    
    this.initializeChat();
    this.setupSubscriptions();
    this.loadInitialData();
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
    this.chatService.stopConnection();
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
        this.inbox = state.inbox;
        this.unreadCount = state.unreadCount;
        this.isConnected = state.isConnected;
        this.filterInbox();
      });

    // Subscribe to real-time message events
    this.chatService.messageReceived$
      .pipe(takeUntil(this.destroy$))
      .subscribe(message => {
        this.handleNewMessage(message);
      });

    this.chatService.messageSent$
      .pipe(takeUntil(this.destroy$))
      .subscribe(message => {
        this.handleMessageSent(message);
      });

    this.chatService.conversationStarted$
      .pipe(takeUntil(this.destroy$))
      .subscribe(conversation => {
        this.handleNewConversation(conversation);
      });

    this.chatService.messageMarkedAsRead$
      .pipe(takeUntil(this.destroy$))
      .subscribe(messageId => {
        this.handleMessageMarkedAsRead(messageId);
      });

    this.chatService.conversationMarkedAsRead$
      .pipe(takeUntil(this.destroy$))
      .subscribe(conversationId => {
        this.handleConversationMarkedAsRead(conversationId);
      });

    this.chatService.joinedConversation$
      .pipe(takeUntil(this.destroy$))
      .subscribe(conversationId => {
        console.log(`Joined conversation: ${conversationId}`);
      });

    this.chatService.leftConversation$
      .pipe(takeUntil(this.destroy$))
      .subscribe(conversationId => {
        console.log(`Left conversation: ${conversationId}`);
      });

    this.chatService.errorReceived$
      .pipe(takeUntil(this.destroy$))
      .subscribe(error => {
        console.error('Chat error:', error);
        this.toastService.showError(error);
      });
  }

  private loadInitialData(): void {
    const userId = this.authService.getUserId();
    if (userId) {
      this.chatService.loadUserConversations();
      this.chatService.loadInboxPreview();
    } else {
      console.error('No user ID found, cannot load conversations');
      this.toastService.showError('Authentication error. Please log in again.');
    }
  }

  // Message handling
  async sendMessage(): Promise<void> {
    if (!this.newMessageContent.trim() || !this.activeConversation || this.isSendingMessage) {
      return;
    }

    this.isSendingMessage = true;
    const receiverId = this.activeConversation.user1Id === this.currentUserId ? 
      this.activeConversation.user2Id : this.activeConversation.user1Id;
    
    // Add admin prefix if user is admin
    let messageContent = this.newMessageContent.trim();
    if (this.isAdmin) {
      messageContent = `[ADMIN] ${messageContent}`;
    }
    
    const createMessageDto: CreateMessageDto = {
      senderId: this.currentUserId,
      receiverId: receiverId,
      content: messageContent
    };

    try {
      // Send via SignalR for real-time delivery
      await this.chatService.sendMessageViaHub(createMessageDto);
      this.newMessageContent = '';
      this.shouldScrollToBottom = true;
    } catch (error) {
      console.error('Error sending message via SignalR:', error);
      // Fallback to REST API
      this.chatService.sendMessage(createMessageDto).subscribe({
        next: (message) => {
          this.newMessageContent = '';
          this.shouldScrollToBottom = true;
        },
        error: (error) => {
          console.error('Error sending message via REST:', error);
          this.toastService.showError('Failed to send message. Please try again.');
        },
        complete: () => {
          this.isSendingMessage = false;
        }
      });
    } finally {
      this.isSendingMessage = false;
    }
  }

  // Conversation management
  selectConversation(conversation: ConversationDto): void {
    this.chatService.setActiveConversation(conversation);
    this.shouldScrollToBottom = true;
    
    // Join conversation group for real-time updates
    this.joinConversationGroup(conversation.id);
    
    // Mark messages as read
    this.markConversationAsRead(conversation.id);
  }

  // New method to handle inbox item selection
  selectConversationByInbox(inboxItem: InboxDto): void {
    const conversation = this.conversations.find(c => c.id === inboxItem.conversationId);
    if (conversation) {
      this.selectConversation(conversation);
    } else {
      // If conversation not found in local state, fetch it
      this.chatService.getConversationById(inboxItem.conversationId).subscribe({
        next: (conv) => {
          this.selectConversation(conv);
        },
        error: (error) => {
          console.error('Error loading conversation:', error);
          this.toastService.showError('Failed to load conversation');
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
    // Try SignalR first
    this.chatService.markConversationAsReadViaHub(conversationId).catch(error => {
      console.error('Error marking conversation as read via SignalR:', error);
      // Fallback to REST API
      this.chatService.markAllMessagesAsRead(conversationId).subscribe({
        next: () => {
          // Update local state
          if (this.activeConversation && this.activeConversation.id === conversationId) {
            this.activeConversation.messages = this.activeConversation.messages.map(m => ({
              ...m,
              isRead: m.receiverId === this.currentUserId ? true : m.isRead
            }));
          }
        },
        error: (error) => {
          console.error('Error marking messages as read:', error);
        }
      });
    });
  }

  // Event handlers
  private handleNewMessage(message: MessageDto): void {
    this.shouldScrollToBottom = true;
    
    // Update inbox
    this.chatService.loadInboxPreview();
    
    // If this message is for the active conversation, mark it as read
    if (this.activeConversation && message.conversationId === this.activeConversation.id) {
      setTimeout(() => {
        this.markConversationAsRead(message.conversationId);
      }, 500);
    }
  }

  private handleMessageSent(message: MessageDto): void {
    this.shouldScrollToBottom = true;
    this.chatService.loadInboxPreview();
  }

  private handleNewConversation(conversation: ConversationDto): void {
    this.chatService.setActiveConversation(conversation);
    this.chatService.loadInboxPreview();
  }

  private handleMessageMarkedAsRead(messageId: number): void {
    console.log(`Message ${messageId} marked as read`);
  }

  private handleConversationMarkedAsRead(conversationId: number): void {
    console.log(`Conversation ${conversationId} marked as read`);
  }

  // Search and filter
  onSearchChange(): void {
    this.filterInbox();
  }

  private filterInbox(): void {
    if (!this.searchQuery.trim()) {
      this.filteredInbox = this.inbox;
    } else {
      this.filteredInbox = this.inbox.filter(item =>
        item.participant.toLowerCase().includes(this.searchQuery.toLowerCase()) ||
        (item.lastMessage && item.lastMessage.toLowerCase().includes(this.searchQuery.toLowerCase()))
      );
    }
  }

  getOtherUserName(conversation: ConversationDto): string {
    const otherUserId = conversation.user1Id === this.currentUserId ? conversation.user2Id : conversation.user1Id;
    // For now, return a formatted user ID, but this should be replaced with actual username
    return `User ${otherUserId.substring(0, 8)}`;
  }

  formatMessageTime(sentAt: Date): string {
    const date = new Date(sentAt);
    const now = new Date();
    const diffInHours = (now.getTime() - date.getTime()) / (1000 * 60 * 60);

    if (diffInHours < 24) {
      return date.toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' });
    } else if (diffInHours < 168) { // 7 days
      return date.toLocaleDateString([], { weekday: 'short', hour: '2-digit', minute: '2-digit' });
    } else {
      return date.toLocaleDateString([], { month: 'short', day: 'numeric', hour: '2-digit', minute: '2-digit' });
    }
  }

  private scrollToBottom(): void {
    if (this.messagesContainer) {
      try {
        this.messagesContainer.nativeElement.scrollTop = this.messagesContainer.nativeElement.scrollHeight;
      } catch (err) {
        console.error('Error scrolling to bottom:', err);
      }
    }
  }

  onKeyPress(event: KeyboardEvent): void {
    if (event.key === 'Enter' && !event.shiftKey) {
      event.preventDefault();
      this.sendMessage();
    }
  }

  // Connection status methods
  get connectionStatusText(): string {
    return this.isConnected ? 'Connected' : 'Disconnected';
  }

  get connectionStatusClass(): string {
    return this.isConnected ? 'text-success' : 'text-danger';
  }

  // Message status methods
  isMessageFromCurrentUser(message: MessageDto): boolean {
    return message.senderId === this.currentUserId;
  }

  isMessageUnread(message: MessageDto): boolean {
    return !message.isRead && message.receiverId === this.currentUserId;
  }

  // Conversation status methods
  getConversationUnreadCount(conversation: ConversationDto): number {
    return conversation.messages.filter(m => 
      !m.isRead && m.receiverId === this.currentUserId
    ).length;
  }

  hasUnreadMessages(conversation: ConversationDto): boolean {
    return conversation.messages.some(message => 
      message.receiverId === this.currentUserId && !message.isRead
    );
  }

  // TrackBy functions for better performance
  trackByConversationId(index: number, item: InboxDto): number {
    return item.conversationId;
  }

  trackByMessageId(index: number, item: MessageDto): number {
    return item.id;
  }

  // Display name helpers
  getParticipantInitial(participant: string): string {
    return participant.charAt(0).toUpperCase();
  }

  getDisplayName(participant: string): string {
    // If it's a user ID (GUID format), try to get a display name
    if (participant.includes('-') && participant.length > 20) {
      // This is likely a user ID, we should get the actual username
      // For now, return a formatted version
      return `User ${participant.substring(0, 8)}`;
    }
    return participant;
  }
}