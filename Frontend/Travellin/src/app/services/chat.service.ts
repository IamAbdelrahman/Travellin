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
      console.log('SignalR Connected');
    } catch (err) {
      console.error('Error while starting connection: ', err);
      this.updateConnectionStatus(false);
      this.handleConnectionError();
      throw err;
    }
  }

  private handleConnectionError(): void {
    if (this.reconnectAttempts < this.maxReconnectAttempts) {
      this.reconnectAttempts++;
      setTimeout(() => {
        console.log(`Attempting to reconnect... (${this.reconnectAttempts}/${this.maxReconnectAttempts})`);
        this.startConnection().catch(err => {
          console.error('Reconnection failed:', err);
        });
      }, this.reconnectDelay * this.reconnectAttempts);
    } else {
      console.error('Max reconnection attempts reached');
      this.errorReceived$.next('Connection failed. Please refresh the page.');
    }
  }

  public async stopConnection(): Promise<void> {
    if (this.hubConnection) {
      await this.hubConnection.stop();
      this.updateConnectionStatus(false);
      this.reconnectAttempts = 0;
      console.log('SignalR Disconnected');
    }
  }

  private setupSignalRHandlers(): void {
    if (!this.hubConnection) return;

    this.hubConnection.on('ReceiveMessage', (message: MessageDto) => {
      this.messageReceived$.next(message);
      this.addMessageToState(message);
      this.updateUnreadCount();
    });

    this.hubConnection.on('MessageSent', (message: MessageDto) => {
      this.messageSent$.next(message);
      this.addMessageToState(message);
    });

    this.hubConnection.on('NewConversationStarted', (conversation: ConversationDto) => {
      this.conversationStarted$.next(conversation);
      this.addConversationToState(conversation);
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

    this.hubConnection.on('ReceiveError', (error: string) => {
      this.errorReceived$.next(error);
    });

    this.hubConnection.onclose((error) => {
      console.log('SignalR connection closed:', error);
      this.updateConnectionStatus(false);
      if (error) {
        this.handleConnectionError();
      }
    });

    this.hubConnection.onreconnected((connectionId) => {
      console.log('SignalR reconnected:', connectionId);
      this.updateConnectionStatus(true);
      this.reconnectAttempts = 0;
    });

    this.hubConnection.onreconnecting((error) => {
      console.log('SignalR reconnecting:', error);
      this.updateConnectionStatus(false);
    });
  }

  public async sendMessageViaHub(createMessageDto: CreateMessageDto): Promise<void> {
    if (this.hubConnection?.state === signalR.HubConnectionState.Connected) {
      await this.hubConnection.invoke('SendMessage', createMessageDto);
    } else {
      throw new Error('SignalR connection not established');
    }
  }

  public async startConversationViaHub(startConversationDto: StartConversationDto): Promise<void> {
    if (this.hubConnection?.state === signalR.HubConnectionState.Connected) {
      await this.hubConnection.invoke('StartConversation', startConversationDto);
    } else {
      throw new Error('SignalR connection not established');
    }
  }

  public async markMessageAsReadViaHub(messageId: number): Promise<void> {
    if (this.hubConnection?.state === signalR.HubConnectionState.Connected) {
      await this.hubConnection.invoke('MarkMessageAsRead', messageId);
    } else {
      throw new Error('SignalR connection not established');
    }
  }

  public async markConversationAsReadViaHub(conversationId: number): Promise<void> {
    if (this.hubConnection?.state === signalR.HubConnectionState.Connected) {
      await this.hubConnection.invoke('MarkConversationAsRead', conversationId);
    } else {
      throw new Error('SignalR connection not established');
    }
  }

  public async joinConversationViaHub(conversationId: number): Promise<void> {
    if (this.hubConnection?.state === signalR.HubConnectionState.Connected) {
      await this.hubConnection.invoke('JoinConversation', conversationId);
    } else {
      throw new Error('SignalR connection not established');
    }
  }

  public async leaveConversationViaHub(conversationId: number): Promise<void> {
    if (this.hubConnection?.state === signalR.HubConnectionState.Connected) {
      await this.hubConnection.invoke('LeaveConversation', conversationId);
    } else {
      throw new Error('SignalR connection not established');
    }
  }

  // REST API Methods
  public sendMessage(createMessageDto: CreateMessageDto): Observable<MessageDto> {
    return this.http.post<MessageDto>(`${this.baseUrl}/messages/send`, createMessageDto, {
      headers: this.getHttpHeaders()
    });
  }

  public startConversation(startConversationDto: StartConversationDto): Observable<ConversationDto> {
    return this.http.post<ConversationDto>(`${this.baseUrl}/conversations/start`, startConversationDto, {
      headers: this.getHttpHeaders()
    });
  }

  public getUserConversations(userId: string): Observable<ConversationDto[]> {
    const url = `${this.baseUrl}/conversations/by-user/${userId}`;
    console.log('Calling getUserConversations:', url);
    return this.http.get<ConversationDto[]>(url, {
      headers: this.getHttpHeaders()
    });
  }

  public getConversationById(id: number): Observable<ConversationDto> {
    return this.http.get<ConversationDto>(`${this.baseUrl}/conversations/${id}`, {
      headers: this.getHttpHeaders()
    });
  }

  public getMessagesByConversationId(conversationId: number): Observable<MessageDto[]> {
    return this.http.get<MessageDto[]>(`${this.baseUrl}/messages/conversation/${conversationId}`, {
      headers: this.getHttpHeaders()
    });
  }

  public markMessageAsRead(messageId: number): Observable<void> {
    return this.http.post<void>(`${this.baseUrl}/messages/${messageId}/mark-as-read`, {}, {
      headers: this.getHttpHeaders()
    });
  }

  public markAllMessagesAsRead(conversationId: number): Observable<void> {
    return this.http.put<void>(`${this.baseUrl}/messages/mark-read/${conversationId}`, {}, {
      headers: this.getHttpHeaders()
    });
  }

  public getUnreadCount(): Observable<{ unreadCount: number }> {
    return this.http.get<{ unreadCount: number }>(`${this.baseUrl}/messages/unread/count`, {
      headers: this.getHttpHeaders()
    });
  }

  public getInboxPreview(userId: string): Observable<InboxDto[]> {
    const url = `${this.baseUrl}/conversations/inbox/${userId}`;
    console.log('Calling getInboxPreview:', url);
    return this.http.get<InboxDto[]>(url, {
      headers: this.getHttpHeaders()
    });
  }

  public searchConversations(userId: string, query: string): Observable<ConversationSearchResultDto[]> {
    return this.http.get<ConversationSearchResultDto[]>(`${this.baseUrl}/conversations/search`, {
      params: { userId, query },
      headers: this.getHttpHeaders()
    });
  }

  public deleteConversation(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/conversations/${id}`, {
      headers: this.getHttpHeaders()
    });
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
    const conversations = [...currentState.conversations];

    const conversationIndex = conversations.findIndex(c => c.id === message.conversationId);
    if (conversationIndex !== -1) {
      const conversation = { ...conversations[conversationIndex] };
      const messageExists = conversation.messages.some(m => m.id === message.id);

      if (!messageExists) {
        conversation.messages = [...conversation.messages, message];
        conversations[conversationIndex] = conversation;
      }
    }

    this.chatStateSubject.next({
      ...currentState,
      conversations
    });
  }

  private addConversationToState(conversation: ConversationDto): void {
    const currentState = this.chatStateSubject.value;
    const existingIndex = currentState.conversations.findIndex(c => c.id === conversation.id);

    let conversations: ConversationDto[];
    if (existingIndex !== -1) {
      conversations = [...currentState.conversations];
      conversations[existingIndex] = conversation;
    } else {
      conversations = [...currentState.conversations, conversation];
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
      messages: conversation.messages.map(message => 
        message.id === messageId ? { ...message, isRead } : message
      )
    }));

    this.chatStateSubject.next({
      ...currentState,
      conversations
    });
  }

  private updateConversationReadStatus(conversationId: number): void {
    const currentState = this.chatStateSubject.value;
    const conversations = currentState.conversations.map(conversation => {
      if (conversation.id === conversationId) {
        return {
          ...conversation,
          messages: conversation.messages.map(message => ({
            ...message,
            isRead: message.receiverId === currentState.currentUserId ? true : message.isRead
          }))
        };
      }
      return conversation;
    });

    this.chatStateSubject.next({
      ...currentState,
      conversations
    });
  }

  public setActiveConversation(conversation: ConversationDto | undefined): void {
    const currentState = this.chatStateSubject.value;
    this.chatStateSubject.next({
      ...currentState,
      activeConversation: conversation
    });
  }

  public loadUserConversations(): void {
    const currentUserId = this.chatStateSubject.value.currentUserId;
    console.log('Loading conversations for user:', currentUserId);
    if (!currentUserId) {
      console.error('No current user ID available');
      return;
    }

    this.getUserConversations(currentUserId).subscribe({
      next: (conversations) => {
        console.log('Received conversations:', conversations);
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
    const currentUserId = this.chatStateSubject.value.currentUserId;
    console.log('Loading inbox for user:', currentUserId);
    if (!currentUserId) {
      console.error('No current user ID available');
      return;
    }

    this.getInboxPreview(currentUserId).subscribe({
      next: (inbox) => {
        console.log('Received inbox:', inbox);
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
    this.getUnreadCount().subscribe({
      next: (result) => {
        const currentState = this.chatStateSubject.value;
        this.chatStateSubject.next({
          ...currentState,
          unreadCount: result.unreadCount
        });
      },
      error: (error) => {
        console.error('Error updating unread count:', error);
      }
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
}
