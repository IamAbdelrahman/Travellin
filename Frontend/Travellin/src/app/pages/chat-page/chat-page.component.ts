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

  constructor(
    private chatService: ChatService,
    private toastService: ToastService,
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnInit(): void {
    const userId = this.authService.getUserId();
    if (!userId) {
      console.error('No user ID available');
      return;
    }
    
    this.currentUserId = userId;
    this.isAdmin = this.authService.isAdmin();
    
    console.log('Chat component initialized with user ID:', this.currentUserId);
    console.log('Is admin:', this.isAdmin);

    this.initializeChat();
    this.setupSubscriptions();
    this.loadInitialData();
    this.startSilentRefresh();
    
    // Make component available globally for debugging
    (window as any).chatComponent = this;
  }

  // Test method for debugging unread counts
  testUnreadCount(): void {
    console.log('=== UNREAD COUNT DEBUG ===');
    console.log('Total unread count:', this.unreadCount);
    console.log('Inbox items:', this.inbox);
    console.log('Filtered inbox:', this.filteredInbox);
    
    this.inbox.forEach((item, index) => {
      console.log(`Inbox item ${index}:`, {
        conversationId: item.conversationId,
        participant: item.participant,
        unreadCount: item.unreadCount,
        isUnread: item.isUnread,
        lastMessage: item.lastMessage
      });
    });
  }

  // Test method to simulate new message
  testNewMessage(conversationId: number, senderId: string, content: string): void {
    const testMessage: MessageDto = {
      id: Date.now(),
      conversationId: conversationId,
      senderId: senderId,
      receiverId: this.currentUserId,
      content: content,
      sentAt: new Date(),
      isRead: false
    };
    
    console.log('Testing new message:', testMessage);
    this.handleNewMessage(testMessage);
  }

  // Test method to test SignalR connection
  async testSignalRConnection(): Promise<void> {
    try {
      console.log('Testing SignalR connection...');
      await this.chatService.testConnection();
      console.log('SignalR connection test completed');
    } catch (error) {
      console.error('SignalR connection test failed:', error);
    }
  }

  // Debug method to check authentication state
  debugAuthState(): void {
    console.log('=== AUTH STATE DEBUG ===');
    console.log('AuthService.getUserId():', this.authService.getUserId());
    console.log('AuthService.getUserName():', this.authService.getUserName());
    console.log('AuthService.getAccessToken():', this.authService.getAccessToken() ? 'Present' : 'Missing');
    console.log('AuthService.isAuthenticated():', this.authService.isAuthenticated());
    console.log('AuthService.getUserRole():', this.authService.getUserRole());
    console.log('AuthService.isAdmin():', this.authService.isAdmin());
    console.log('Component currentUserId:', this.currentUserId);
    console.log('Component isAdmin:', this.isAdmin);
  }

  // Debug method to check SignalR connection and real-time status
  debugRealTimeStatus(): void {
    console.log('=== REAL-TIME STATUS DEBUG ===');
    console.log('ChatService connection state:', this.chatService.getConnectionState());
    console.log('ChatService is connected:', this.chatService.isConnected());
    console.log('Component isConnected:', this.isConnected);
    console.log('Active conversation:', this.activeConversation);
    console.log('Current user ID:', this.currentUserId);
    
    // Test SignalR connection
    this.chatService.testConnection().then(() => {
      console.log('✅ SignalR connection test successful');
    }).catch((error) => {
      console.error('❌ SignalR connection test failed:', error);
    });

    // Get connected users
    this.chatService.getConnectedUsers().then(() => {
      console.log('✅ Connected users request sent');
    }).catch((error) => {
      console.error('❌ Connected users request failed:', error);
    });
  }

  // Test real-time message delivery
  testRealTimeMessage(): void {
    if (!this.activeConversation) {
      console.error('No active conversation to test');
      return;
    }

    console.log('=== TESTING REAL-TIME MESSAGE ===');
    const testMessage: MessageDto = {
      id: Date.now(),
      conversationId: this.activeConversation.id,
      senderId: this.currentUserId,
      receiverId: this.activeConversation.user1Id === this.currentUserId ? 
          this.activeConversation.user2Id : this.activeConversation.user1Id,
      content: `Test real-time message at ${new Date().toLocaleTimeString()}`,
      sentAt: new Date(),
      isRead: false
    };

    console.log('Simulating new message:', testMessage);
    this.handleNewMessage(testMessage);
  }

  // Manual refresh method (can be called by user or automatically)
  async manualRefresh(): Promise<void> {
    console.log('Manual refresh triggered...');
    try {
      await this.silentRefreshData();
      console.log('Manual refresh completed');
    } catch (error) {
      console.error('Manual refresh failed:', error);
    }
  }

  // Debug method to check refresh status
  debugRefreshStatus(): void {
    console.log('=== REFRESH STATUS DEBUG ===');
    console.log('Refresh interval active:', !!this.refreshInterval);
    console.log('Last refresh time:', this.lastRefreshTime);
    console.log('Current time:', new Date());
    console.log('Time since last refresh:', new Date().getTime() - this.lastRefreshTime.getTime(), 'ms');
    console.log('Active conversation messages count:', this.activeConversation?.messages?.length || 0);
    console.log('Total conversations count:', this.conversations.length);
    console.log('Inbox items count:', this.inbox.length);
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
    this.stopSilentRefresh();
    this.chatService.stopConnection();
  }

  // Silent refresh methods
  private startSilentRefresh(): void {
    console.log('Starting silent refresh mechanism...');
    // Refresh every 5 seconds silently
    this.refreshInterval = setInterval(() => {
      this.silentRefreshData();
    }, 5000);
  }

  private stopSilentRefresh(): void {
    if (this.refreshInterval) {
      clearInterval(this.refreshInterval);
      this.refreshInterval = null;
      console.log('Stopped silent refresh mechanism');
    }
  }

  private async silentRefreshData(): Promise<void> {
    try {
      const userId = this.authService.getUserId();
      if (!userId) return;

      console.log('Silent refresh: Loading updated data...');
      
      // Refresh conversations silently
      this.chatService.getUserConversations(userId).subscribe({
        next: (conversations) => {
          this.updateConversationsSilently(conversations);
        },
        error: (error) => {
          console.error('Silent refresh conversations error:', error);
        }
      });

      // Refresh inbox silently
      this.chatService.getInboxPreview(userId).subscribe({
        next: (inbox) => {
          this.updateInboxSilently(inbox);
        },
        error: (error) => {
          console.error('Silent refresh inbox error:', error);
        }
      });

      // Refresh active conversation messages if exists
      if (this.activeConversation) {
        this.refreshActiveConversationMessages();
      }

      this.lastRefreshTime = new Date();
    } catch (error) {
      console.error('Silent refresh error:', error);
    }
  }

  private updateConversationsSilently(newConversations: ConversationDto[]): void {
    // Only update if there are new messages
    const hasNewMessages = this.hasNewMessagesInConversations(newConversations);
    if (hasNewMessages) {
      console.log('Silent refresh: New messages detected, updating conversations');
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
      console.log('Silent refresh: Inbox changes detected, updating inbox');
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
          console.log('Silent refresh: Updating active conversation messages');
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
    const userId = this.authService.getUserId();
    if (!userId) {
      console.error('No user ID available');
      return;
    }

    // Load conversations
    this.chatService.getUserConversations(userId).subscribe({
      next: (conversations) => {
        this.conversations = conversations;
        console.log('Loaded conversations:', conversations);
      },
      error: (error) => {
        console.error('Error loading conversations:', error);
        this.toastService.showError('Failed to load conversations');
      }
    });

    // Load inbox preview
    this.chatService.getInboxPreview(userId).subscribe({
      next: (inbox) => {
        this.inbox = inbox;
        this.filteredInbox = inbox;
        console.log('Loaded inbox:', inbox);
        
        // Initialize unread counts after loading inbox
        this.updateTotalUnreadCount();
      },
      error: (error) => {
        console.error('Error loading inbox:', error);
        this.toastService.showError('Failed to load inbox');
      }
    });
  }

  async sendMessage(): Promise<void> {
    if (!this.newMessageContent.trim() || !this.activeConversation || this.isSendingMessage) {
      console.log('SendMessage validation failed:', {
        hasContent: !!this.newMessageContent.trim(),
        hasActiveConversation: !!this.activeConversation,
        isSending: this.isSendingMessage
      });
      return;
    }

    if (!this.currentUserId) {
      console.error('Cannot send message: currentUserId is not set');
      this.toastService.showError('User not authenticated');
      return;
    }

    this.isSendingMessage = true;
    const receiverId = this.activeConversation.user1Id === this.currentUserId ? 
        this.activeConversation.user2Id : this.activeConversation.user1Id;
    
    console.log('Sending message:', {
      currentUserId: this.currentUserId,
      receiverId: receiverId,
      activeConversation: this.activeConversation,
      content: this.newMessageContent.trim()
    });
    
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
      const message = await this.chatService.sendMessage(createMessageDto).toPromise();
      if (message) {
        // Add temporary message with 'sending' status
        const tempMessage: MessageDto = {
          ...message,
          id: Date.now(), // Temporary ID
          sentAt: new Date(),
          isRead: false
        };
        
        // Add to active conversation messages
        if (this.activeConversation) {
          this.activeConversation.messages = this.activeConversation.messages || [];
          this.activeConversation.messages.push(tempMessage);
          this.messageStatus.set(tempMessage.id, 'sending');
        }
        
        this.newMessageContent = '';
        this.shouldScrollToBottom = true;
        
        // Update status to 'sent' after a short delay
        setTimeout(() => {
          this.messageStatus.set(tempMessage.id, 'sent');
        }, 500);
        
        // Update status to 'delivered' after another delay
        setTimeout(() => {
          this.messageStatus.set(tempMessage.id, 'delivered');
        }, 1000);
      }
    } catch (error) {
      console.error('Error sending message:', error);
      this.toastService.showError('Failed to send message');
    } finally {
      this.isSendingMessage = false;
    }
  }

  selectConversation(conversation: ConversationDto): void {
    console.log('Selecting conversation:', conversation);
    console.log('Current active conversation:', this.activeConversation);
    
    if (this.activeConversation?.id === conversation.id) {
      console.log('Conversation already active, returning');
      return;
    }

    // Leave current conversation group
    if (this.activeConversation) {
      console.log('Leaving conversation group:', this.activeConversation.id);
      this.leaveConversationGroup(this.activeConversation.id);
    }

    this.activeConversation = conversation;
    console.log('Set active conversation:', this.activeConversation);
    this.shouldScrollToBottom = true;

    // Join new conversation group
    console.log('Joining conversation group:', conversation.id);
    this.joinConversationGroup(conversation.id);

    // Mark conversation as read
    console.log('Marking conversation as read:', conversation.id);
    this.markConversationAsRead(conversation.id);
  }

  selectConversationByInbox(inboxItem: InboxDto): void {
    console.log('Selecting conversation by inbox item:', inboxItem);
    console.log('Available conversations:', this.conversations);
    
    // First try to find the conversation in the existing array
    let conversation = this.conversations.find(c => c.id === inboxItem.conversationId);
    console.log('Found conversation in array:', conversation);
    
    if (conversation) {
      console.log('Using existing conversation');
      this.selectConversation(conversation);
    } else {
      // If conversation doesn't exist in the array, load it from the API
      console.log('Loading conversation from API:', inboxItem.conversationId);
      this.chatService.getConversationById(inboxItem.conversationId).subscribe({
        next: (loadedConversation) => {
          console.log('Loaded conversation from API:', loadedConversation);
          // Add to conversations array if not already there
          if (!this.conversations.find(c => c.id === loadedConversation.id)) {
            this.conversations.push(loadedConversation);
          }
          this.selectConversation(loadedConversation);
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
    console.log('Handling new message:', message);
    console.log('Current user ID:', this.currentUserId);
    console.log('Message sender ID:', message.senderId);
    console.log('Is message from current user:', message.senderId === this.currentUserId);
    
    // Update conversation messages
    const conversation = this.conversations.find(c => c.id === message.conversationId);
    if (conversation) {
      conversation.messages = conversation.messages || [];
      conversation.messages.push(message);
      console.log('Updated conversation messages for ID:', message.conversationId);
      
      // If this is the active conversation, scroll to bottom
      if (this.activeConversation?.id === conversation.id) {
        this.shouldScrollToBottom = true;
        
        // Mark message as read if it's from another user
        if (message.senderId !== this.currentUserId) {
          this.chatService.markMessageAsRead(message.id).subscribe();
        }
      }
    }

    // Update inbox with proper unread count
    const inboxItem = this.inbox.find(item => item.conversationId === message.conversationId);
    console.log('Found inbox item:', inboxItem);
    
    if (inboxItem) {
      inboxItem.lastMessage = message.content;
      inboxItem.lastMessageTime = message.sentAt;
      
      // Only increment unread count if message is from another user
      if (message.senderId !== this.currentUserId) {
        const previousCount = inboxItem.unreadCount || 0;
        inboxItem.unreadCount = previousCount + 1;
        inboxItem.isUnread = true;
        console.log('Updated unread count for conversation:', message.conversationId, 'Previous:', previousCount, 'New count:', inboxItem.unreadCount);
      } else {
        console.log('Message from current user, not incrementing unread count');
      }
    } else {
      console.log('No inbox item found for conversation ID:', message.conversationId);
    }

    // Update total unread count
    this.updateTotalUnreadCount();
    
    // Force change detection
    this.filteredInbox = [...this.inbox];
  }

  private updateTotalUnreadCount(): void {
    this.unreadCount = this.inbox.reduce((total, item) => total + (item.unreadCount || 0), 0);
    console.log('Total unread count updated:', this.unreadCount);
  }

  private handleMessageSent(message: MessageDto): void {
    // Update message status to 'read' if it's from current user
    if (message.senderId === this.currentUserId) {
      this.messageStatus.set(message.id, 'read');
    }
  }

  private handleNewConversation(conversation: ConversationDto): void {
    this.conversations.push(conversation);
  }

  private handleMessageMarkedAsRead(messageId: number): void {
    // Update message read status in all conversations
    this.conversations.forEach(conversation => {
      const message = conversation.messages?.find(m => m.id === messageId);
      if (message) {
        message.isRead = true;
        this.messageStatus.set(messageId, 'read');
      }
    });
  }

  private handleConversationMarkedAsRead(conversationId: number): void {
    console.log('Handling conversation marked as read:', conversationId);
    
    // Update conversation unread count
    const inboxItem = this.inbox.find(item => item.conversationId === conversationId);
    if (inboxItem) {
      inboxItem.unreadCount = 0;
      inboxItem.isUnread = false;
      console.log('Reset unread count for conversation:', conversationId);
    }

    // Update total unread count
    this.updateTotalUnreadCount();
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

  getOtherUserName(conversation: ConversationDto): string {
    console.log('Getting other user name for conversation:', conversation);
    console.log('Current user ID:', this.currentUserId);
    console.log('User1 ID:', conversation.user1Id);
    console.log('User2 ID:', conversation.user2Id);
    console.log('User1 Name:', conversation.user1Name);
    console.log('User2 Name:', conversation.user2Name);
    
    if (!this.currentUserId) {
      console.error('Current user ID is not set');
      return 'Unknown User';
    }
    
    if (conversation.user1Id === this.currentUserId) {
      // Current user is user1, return user2's name
      if (conversation.user2Name) {
        return conversation.user2Name;
      } else {
        // Fallback to user ID if name is not available
        return `User ${conversation.user2Id.substring(0, 8)}`;
      }
    } else {
      // Current user is user2, return user1's name
      if (conversation.user1Name) {
        return conversation.user1Name;
      } else {
        // Fallback to user ID if name is not available
        return `User ${conversation.user1Id.substring(0, 8)}`;
      }
    }
  }

  formatMessageTime(sentAt: Date | undefined): string {
    if (!sentAt) return '';
    
    const now = new Date();
    const messageTime = new Date(sentAt);
    const diffInHours = (now.getTime() - messageTime.getTime()) / (1000 * 60 * 60);

    if (diffInHours < 1) {
      const diffInMinutes = Math.floor((now.getTime() - messageTime.getTime()) / (1000 * 60));
      return diffInMinutes < 1 ? 'Just now' : `${diffInMinutes}m ago`;
    } else if (diffInHours < 24) {
      return `${Math.floor(diffInHours)}h ago`;
    } else if (diffInHours < 168) { // 7 days
      return messageTime.toLocaleDateString('en-US', { weekday: 'short' });
    } else {
      return messageTime.toLocaleDateString('en-US', { 
        month: 'short', 
        day: 'numeric' 
      });
    }
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
    return this.isConnected ? 'text-success' : 'text-danger';
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
    // First try to parse as "FirstName LastName"
    const nameParts = participant.split(' ');
    if (nameParts.length >= 2) {
      return `${nameParts[0]} ${nameParts[1]}`;
    }
    
    // If it's just one word, return as is
    return participant;
  }

  // New methods for message status
  getMessageStatus(messageId: number): 'sending' | 'sent' | 'delivered' | 'read' | 'error' {
    return this.messageStatus.get(messageId) || 'sent';
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
}