// src/app/pages/chat-page/chat-page.component.ts
import { Component, OnInit, OnDestroy, ViewChild, ElementRef, AfterViewChecked } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { Subject, takeUntil } from 'rxjs';
import { ChatService } from '../../services/chat.service';
import { UsersService } from '../../services/users.service';
import { 
  MessageDto, 
  CreateMessageDto, 
} from '../../models/chat/message.model';
import { ConversationDto, StartConversationDto , InboxDto , ConversationSearchResultDto  } from '../../models/chat/conversation.model';
import { ChatUser } from '../../models/chat/user.model';

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

  // UI State
  newMessageContent: string = '';
  searchQuery: string = '';
  isLoading: boolean = false;
  showNewChatModal: boolean = false;
  selectedUserId: string = '';
  users: ChatUser[] = [];

  // Filters
  filteredInbox: InboxDto[] = [];

  constructor(
    private chatService: ChatService,
    private usersService: UsersService,
    private router: Router
  ) {}

  ngOnInit(): void {
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
    } catch (error) {
      console.error('Failed to establish chat connection:', error);
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

    this.chatService.errorReceived$
      .pipe(takeUntil(this.destroy$))
      .subscribe(error => {
        console.error('Chat error:', error);
        // You can show a toast notification here
      });
  }

  private loadInitialData(): void {
    this.chatService.loadUserConversations();
    this.chatService.loadInboxPreview();
    this.loadUsers();
  }

  private loadUsers(): void {
    // Assuming you have a method to get all users
    // You might want to modify this based on your users service
    this.usersService.getAllUsers().subscribe({
      next: (users) => {
        this.users = users.filter(u => u.id !== this.currentUserId);
      },
      error: (error) => {
        console.error('Error loading users:', error);
      }
    });
  }

  // Message handling
  async sendMessage(): Promise<void> {
    if (!this.newMessageContent.trim() || !this.activeConversation) {
      return;
    }

    const receiverId = this.getOtherUserId(this.activeConversation);
    const createMessageDto: CreateMessageDto = {
      senderId: this.currentUserId,
      receiverId: receiverId,
      content: this.newMessageContent.trim()
    };

    try {
      // Send via SignalR for real-time delivery
      await this.chatService.sendMessageViaHub(createMessageDto);
      this.newMessageContent = '';
      this.shouldScrollToBottom = true;
    } catch (error) {
      console.error('Error sending message:', error);
      // Fallback to REST API
      this.chatService.sendMessage(createMessageDto).subscribe({
        next: (message) => {
          this.newMessageContent = '';
          this.shouldScrollToBottom = true;
        },
        error: (error) => {
          console.error('Error sending message via REST:', error);
        }
      });
    }
  }

  // Conversation management
  selectConversation(conversation: ConversationDto): void {
    this.chatService.setActiveConversation(conversation);
    this.shouldScrollToBottom = true;
    
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
        }
      });
    }
  }

  async startNewConversation(): Promise<void> {
    if (!this.selectedUserId) return;

    const startConversationDto: StartConversationDto = {
      user1Id: this.currentUserId,
      user2Id: this.selectedUserId
    };

    try {
      // Try SignalR first
      await this.chatService.startConversationViaHub(startConversationDto);
      this.closeNewChatModal();
    } catch (error) {
      console.error('Error starting conversation via SignalR:', error);
      // Fallback to REST API
      this.chatService.startConversation(startConversationDto).subscribe({
        next: (conversation) => {
          this.chatService.setActiveConversation(conversation);
          this.closeNewChatModal();
        },
        error: (error) => {
          console.error('Error starting conversation:', error);
        }
      });
    }
  }

  private markConversationAsRead(conversationId: number): void {
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

  // Modal management
  showNewChat(): void {
    this.showNewChatModal = true;
    this.selectedUserId = '';
  }

  closeNewChatModal(): void {
    this.showNewChatModal = false;
    this.selectedUserId = '';
  }

  // Utility methods
  getOtherUserId(conversation: ConversationDto): string {
    return conversation.user1Id === this.currentUserId ? conversation.user2Id : conversation.user1Id;
  }

  getOtherUserName(conversation: ConversationDto): string {
    // You might want to implement user name resolution
    const otherUserId = this.getOtherUserId(conversation);
    const user = this.users.find(u => u.id === otherUserId);
    return user ? (user.firstName && user.lastName ? `${user.firstName} ${user.lastName}` : user.userName) : otherUserId;
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
}