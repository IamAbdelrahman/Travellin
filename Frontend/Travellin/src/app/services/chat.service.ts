// src/app/services/chat.service.ts
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BehaviorSubject, Observable, Subject } from 'rxjs';
import * as signalR from '@microsoft/signalr';
import { TokenStorageService } from './token-storage.service';
import { environment } from '../../environments/environment';
import { 
  MessageDto, 
  CreateMessageDto, 
} from '../models/chat/message.model';
import { 
  ConversationDto, 
  StartConversationDto, 
  InboxDto, 
  ConversationSearchResultDto
} from '../models/chat/conversation.model';
import { ChatState } from '../models/chat/chat-state.model';

@Injectable({
  providedIn: 'root'
})
export class ChatService {
  private readonly baseUrl = `${environment.apiUrl}/api/v1`;
  private hubConnection: signalR.HubConnection | null = null;
  private reconnectAttempts = 0;
  private maxReconnectAttempts = 5;
  private reconnectDelay = 2000;

  // State management
  private chatStateSubject = new BehaviorSubject<ChatState>({
    currentUserId: '',
    conversations: [],
    inbox: [],
    isConnected: false,
    unreadCount: 0
  });

  // Observables for real-time updates
  public chatState$ = this.chatStateSubject.asObservable();
  public messageReceived$ = new Subject<MessageDto>();
  public messageSent$ = new Subject<MessageDto>();
  public conversationStarted$ = new Subject<ConversationDto>();
  public messageMarkedAsRead$ = new Subject<number>();
  public conversationMarkedAsRead$ = new Subject<number>();
  public joinedConversation$ = new Subject<number>();
  public leftConversation$ = new Subject<number>();
  public errorReceived$ = new Subject<string>();
  public connectionStatus$ = new Subject<boolean>();

  // Additional observables for the enhanced chat component
  public newMessage$ = new Subject<MessageDto>();
  public newConversation$ = new Subject<ConversationDto>();

  constructor(
    private http: HttpClient,
    private tokenStorage: TokenStorageService
  ) {
    // Initialize current user ID
    this.updateCurrentUserId();
  }

  public updateCurrentUserId(): void {
    const currentState = this.chatStateSubject.value;
    const userId = this.tokenStorage.getUserId();
    this.chatStateSubject.next({
      ...currentState,
      currentUserId: userId || ''
    });
  }

  private getHttpHeaders(): HttpHeaders {
    const token = this.tokenStorage.getAccessToken();
    return new HttpHeaders({
      'Authorization': `Bearer ${token}`,
      'Content-Type': 'application/json'
    });
  }

  // SignalR Connection Management
  public async startConnection(): Promise<void> {
    if (this.hubConnection && this.hubConnection.state === signalR.HubConnectionState.Connected) {
      return;
    }

    const token = this.tokenStorage.getAccessToken();
    if (!token) {
      throw new Error('No authentication token found');
    }

    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(`${environment.apiUrl}/hubs/chat`, {
        accessTokenFactory: () => token,
        transport: signalR.HttpTransportType.WebSockets
      })
      .withAutomaticReconnect([0, 2000, 5000, 10000, 30000])
      .configureLogging(signalR.LogLevel.Information)
      .build();

    this.setupSignalRHandlers();

    try {
      await this.hubConnection.start();
      this.updateConnectionStatus(true);
      this.reconnectAttempts = 0;
    } catch (error) {
      this.updateConnectionStatus(false);
      throw error;
    }
  }

  private handleConnectionError(): void {
    this.updateConnectionStatus(false);
    this.reconnectAttempts++;

    if (this.reconnectAttempts <= this.maxReconnectAttempts) {
      setTimeout(() => {
        this.startConnection().catch(() => this.handleConnectionError());
      }, this.reconnectDelay * this.reconnectAttempts);
    } else {
      this.errorReceived$.next('Connection failed after maximum attempts');
    }
  }

  public async stopConnection(): Promise<void> {
    if (this.hubConnection) {
      try {
        await this.hubConnection.stop();
        this.updateConnectionStatus(false);
      } catch (error) {
        console.error('Error stopping SignalR connection:', error);
      }
    }
  }

  private setupSignalRHandlers(): void {
    if (!this.hubConnection) return;

    this.hubConnection.on('ReceiveMessage', (message: MessageDto) => {
      this.messageReceived$.next(message);
      this.newMessage$.next(message);
      this.addMessageToState(message);
    });

    this.hubConnection.on('MessageSent', (message: MessageDto) => {
      this.messageSent$.next(message);
    });

    this.hubConnection.on('MessageMarkedAsRead', (messageId: number) => {
      this.messageMarkedAsRead$.next(messageId);
      this.updateMessageReadStatus(messageId, true);
    });

    this.hubConnection.on('ConversationMarkedAsRead', (conversationId: number) => {
      this.conversationMarkedAsRead$.next(conversationId);
      this.updateConversationReadStatus(conversationId);
    });

    this.hubConnection.on('JoinedConversation', (conversationId: number) => {
      this.joinedConversation$.next(conversationId);
    });

    this.hubConnection.on('LeftConversation', (conversationId: number) => {
      this.leftConversation$.next(conversationId);
    });

    this.hubConnection.on('NewConversation', (conversation: ConversationDto) => {
      this.conversationStarted$.next(conversation);
      this.newConversation$.next(conversation);
      this.addConversationToState(conversation);
    });

    this.hubConnection.on('TestResponse', (message: string) => {
      // Test response handler
    });

    this.hubConnection.on('ConnectedUsersResponse', (message: string) => {
      // Connected users response handler
    });

    this.hubConnection.onclose((error) => {
      this.updateConnectionStatus(false);
      if (error) {
        this.handleConnectionError();
      }
    });

    this.hubConnection.onreconnecting((error) => {
      this.updateConnectionStatus(false);
    });

    this.hubConnection.onreconnected((connectionId) => {
      this.updateConnectionStatus(true);
      this.reconnectAttempts = 0;
    });

    // SignalR handlers setup complete
  }

  // SignalR Hub Methods
  public async sendMessageViaHub(createMessageDto: CreateMessageDto): Promise<void> {
    if (this.hubConnection?.state === signalR.HubConnectionState.Connected) {
      await this.hubConnection.invoke('SendMessage', createMessageDto);
    } else {
      throw new Error('SignalR connection not available');
    }
  }

  public async startConversationViaHub(startConversationDto: StartConversationDto): Promise<void> {
    if (this.hubConnection?.state === signalR.HubConnectionState.Connected) {
      await this.hubConnection.invoke('StartConversation', startConversationDto);
    } else {
      throw new Error('SignalR connection not available');
    }
  }

  public async markMessageAsReadViaHub(messageId: number): Promise<void> {
    if (this.hubConnection?.state === signalR.HubConnectionState.Connected) {
      await this.hubConnection.invoke('MarkMessageAsRead', messageId);
    } else {
      throw new Error('SignalR connection not available');
    }
  }

  public async markConversationAsReadViaHub(conversationId: number): Promise<void> {
    if (this.hubConnection?.state === signalR.HubConnectionState.Connected) {
      await this.hubConnection.invoke('MarkConversationAsRead', conversationId);
    } else {
      throw new Error('SignalR connection not available');
    }
  }

  public async joinConversationViaHub(conversationId: number): Promise<void> {
    if (this.hubConnection?.state === signalR.HubConnectionState.Connected) {
      await this.hubConnection.invoke('JoinConversation', conversationId);
    } else {
      throw new Error('SignalR connection not available');
    }
  }

  public async leaveConversationViaHub(conversationId: number): Promise<void> {
    if (this.hubConnection?.state === signalR.HubConnectionState.Connected) {
      await this.hubConnection.invoke('LeaveConversation', conversationId);
    } else {
      throw new Error('SignalR connection not available');
    }
  }

  public async testConnection(): Promise<void> {
    if (this.hubConnection?.state === signalR.HubConnectionState.Connected) {
      await this.hubConnection.invoke('TestConnection');
    } else {
      throw new Error('SignalR connection not available');
    }
  }

  public async getConnectedUsers(): Promise<void> {
    if (this.hubConnection?.state === signalR.HubConnectionState.Connected) {
      await this.hubConnection.invoke('GetConnectedUsers');
    } else {
      throw new Error('SignalR connection not available');
    }
  }

  // REST API Methods
  public sendMessage(createMessageDto: CreateMessageDto): Observable<MessageDto> {
    return this.http.post<MessageDto>(`${this.baseUrl}/messages/send`, createMessageDto, { headers: this.getHttpHeaders() });
  }

  public startConversation(startConversationDto: StartConversationDto): Observable<ConversationDto> {
    return this.http.post<ConversationDto>(`${this.baseUrl}/conversations/start`, startConversationDto, { headers: this.getHttpHeaders() });
  }

  // Admin methods
  public getAllConversations(): Observable<ConversationDto[]> {
    return this.http.get<ConversationDto[]>(`${this.baseUrl}/conversations/admin/all`, { headers: this.getHttpHeaders() });
  }

  public sendMessageAsAdmin(createMessageDto: CreateMessageDto): Observable<MessageDto> {
    return this.http.post<MessageDto>(`${this.baseUrl}/conversations/admin/send-message`, createMessageDto, { headers: this.getHttpHeaders() });
  }

  public getUserConversations(userId: string): Observable<ConversationDto[]> {
    return this.http.get<ConversationDto[]>(`${this.baseUrl}/conversations/by-user/${userId}`, { headers: this.getHttpHeaders() });
  }

  public getConversationById(id: number): Observable<ConversationDto> {
    return this.http.get<ConversationDto>(`${this.baseUrl}/conversations/${id}`, { headers: this.getHttpHeaders() });
  }

  public getMessagesByConversationId(conversationId: number): Observable<MessageDto[]> {
    return this.http.get<MessageDto[]>(`${this.baseUrl}/messages/conversation/${conversationId}`, { headers: this.getHttpHeaders() });
  }

  public getMessagesByConversationIdForAdmin(conversationId: number): Observable<MessageDto[]> {
    return this.http.get<MessageDto[]>(`${this.baseUrl}/messages/admin/conversation/${conversationId}`, { headers: this.getHttpHeaders() });
  }

  public markConversationAsReadForAdmin(conversationId: number): Observable<void> {
    return this.http.put<void>(`${this.baseUrl}/messages/admin/mark-read/${conversationId}`, {}, { headers: this.getHttpHeaders() });
  }

  public markMessageAsRead(messageId: number): Observable<void> {
    return this.http.post<void>(`${this.baseUrl}/messages/${messageId}/mark-as-read`, {}, { headers: this.getHttpHeaders() });
  }

  public markAllMessagesAsRead(conversationId: number): Observable<void> {
    return this.http.put<void>(`${this.baseUrl}/messages/mark-read/${conversationId}`, {}, { headers: this.getHttpHeaders() });
  }

  // Add the missing method that the component expects
  public markConversationAsRead(conversationId: number): Observable<void> {
    return this.markAllMessagesAsRead(conversationId);
  }

  public getUnreadCount(): Observable<{ unreadCount: number }> {
    return this.http.get<{ unreadCount: number }>(`${this.baseUrl}/messages/unread/count`, { headers: this.getHttpHeaders() });
  }

  public getInboxPreview(userId: string): Observable<InboxDto[]> {
    return this.http.get<InboxDto[]>(`${this.baseUrl}/conversations/inbox/${userId}`, { headers: this.getHttpHeaders() });
  }

  public searchConversations(userId: string, query: string): Observable<ConversationSearchResultDto[]> {
    return this.http.get<ConversationSearchResultDto[]>(`${this.baseUrl}/conversations/search`, {
      headers: this.getHttpHeaders(),
      params: { userId, query }
    });
  }

  public deleteConversation(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/conversations/${id}`, { headers: this.getHttpHeaders() });
  }

  public deleteConversationAsAdmin(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/conversations/admin/${id}`, { headers: this.getHttpHeaders() });
  }

  // State Management Methods
  private updateConnectionStatus(isConnected: boolean): void {
    const currentState = this.chatStateSubject.value;
    this.chatStateSubject.next({
      ...currentState,
      isConnected
    });
    this.connectionStatus$.next(isConnected);
  }

  private addMessageToState(message: MessageDto): void {
    const currentState = this.chatStateSubject.value;
    
    // Find the conversation
    const conversationIndex = currentState.conversations.findIndex(c => c.id === message.conversationId);
    
    if (conversationIndex !== -1) {
      // Update existing conversation
      const updatedConversations = [...currentState.conversations];
      updatedConversations[conversationIndex] = {
        ...updatedConversations[conversationIndex],
        messages: [...(updatedConversations[conversationIndex].messages || []), message]
      };
      
      this.chatStateSubject.next({
        ...currentState,
        conversations: updatedConversations
      });
      
      // Update unread count after adding message
      this.updateUnreadCount();
    } else {
      // Conversation not found in state, message might be for a new conversation
      // This case should be handled by refreshing conversations from the server
    }
  }

  private addConversationToState(conversation: ConversationDto): void {
    const currentState = this.chatStateSubject.value;
    const conversations = [...currentState.conversations];
    
    const existingIndex = conversations.findIndex(c => c.id === conversation.id);
    if (existingIndex !== -1) {
      conversations[existingIndex] = conversation;
    } else {
      conversations.push(conversation);
    }

    this.chatStateSubject.next({
      ...currentState,
      conversations
    });
  }

  private updateMessageReadStatus(messageId: number, isRead: boolean): void {
    const currentState = this.chatStateSubject.value;
    const conversations = currentState.conversations.map(conversation => ({
      ...conversation,
      messages: conversation.messages?.map(message => 
        message.id === messageId ? { ...message, isRead } : message
      ) || []
    }));

    this.chatStateSubject.next({
      ...currentState,
      conversations
    });

    this.updateUnreadCount();
  }

  private updateConversationReadStatus(conversationId: number): void {
    const currentState = this.chatStateSubject.value;
    const conversations = currentState.conversations.map(conversation => {
      if (conversation.id === conversationId) {
        return {
          ...conversation,
          messages: conversation.messages?.map(message => ({ ...message, isRead: true })) || []
        };
      }
      return conversation;
    });

    this.chatStateSubject.next({
      ...currentState,
      conversations
    });

    this.updateUnreadCount();
  }

  public setActiveConversation(conversation: ConversationDto | undefined): void {
    const currentState = this.chatStateSubject.value;
    this.chatStateSubject.next({
      ...currentState,
      activeConversation: conversation
    });
  }

  public loadUserConversations(): void {
    const userId = this.tokenStorage.getUserId();
    if (!userId) {
      console.error('No user ID available for loading conversations');
      return;
    }

    this.getUserConversations(userId).subscribe({
      next: (conversations) => {
        const currentState = this.chatStateSubject.value;
        this.chatStateSubject.next({
          ...currentState,
          conversations
        });
      },
      error: (error) => {
        console.error('Error loading conversations:', error);
        this.errorReceived$.next('Failed to load conversations');
      }
    });
  }

  public loadInboxPreview(): void {
    const userId = this.tokenStorage.getUserId();
    if (!userId) {
      console.error('No user ID available for loading inbox');
      return;
    }

    this.getInboxPreview(userId).subscribe({
      next: (inbox) => {
        const currentState = this.chatStateSubject.value;
        this.chatStateSubject.next({
          ...currentState,
          inbox
        });
      },
      error: (error) => {
        console.error('Error loading inbox:', error);
        this.errorReceived$.next('Failed to load inbox');
      }
    });
  }

  private updateUnreadCount(): void {
    const currentState = this.chatStateSubject.value;
    const unreadCount = currentState.conversations.reduce((total, conversation) => {
      return total + (conversation.messages?.filter(m => !m.isRead && m.senderId !== currentState.currentUserId).length || 0);
    }, 0);

    this.chatStateSubject.next({
      ...currentState,
      unreadCount
    });
  }

  // Utility Methods
  public getCurrentUserId(): string {
    return this.chatStateSubject.value.currentUserId;
  }

  public isConnected(): boolean {
    return this.chatStateSubject.value.isConnected;
  }

  public getActiveConversation(): ConversationDto | undefined {
    return this.chatStateSubject.value.activeConversation;
  }

  public getConnectionState(): signalR.HubConnectionState | null {
    return this.hubConnection?.state || null;
  }

  // Public method to manually update unread count
  public refreshUnreadCount(): void {
    this.updateUnreadCount();
  }

  // Debug method to check unread count state
  public debugUnreadCount(): void {
    const currentState = this.chatStateSubject.value;
    console.log('=== UNREAD COUNT DEBUG ===');
    console.log('Current unread count in state:', currentState.unreadCount);
    console.log('Total conversations:', currentState.conversations.length);
    
    currentState.conversations.forEach((conv, index) => {
      const unreadMessages = conv.messages?.filter(m => !m.isRead && m.senderId !== currentState.currentUserId).length || 0;
      console.log(`Conversation ${index + 1} (ID: ${conv.id}): ${unreadMessages} unread messages`);
    });
  }
}
