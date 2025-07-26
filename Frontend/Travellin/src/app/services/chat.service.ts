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
  public errorReceived$ = new Subject<string>();
  public connectionStatus$ = new Subject<boolean>();

  constructor(
    private http: HttpClient,
    private tokenStorage: TokenStorageService
  ) {
    // Initialize current user ID
    this.updateCurrentUserId();
  }

  private updateCurrentUserId(): void {
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
      .withUrl(`${environment.apiUrl}/chathub`, {
        accessTokenFactory: () => token
      })
      .withAutomaticReconnect()
      .build();

    // Set up event handlers
    this.setupSignalRHandlers();

    try {
      await this.hubConnection.start();
      this.updateConnectionStatus(true);
      console.log('SignalR Connected');
    } catch (err) {
      console.error('Error while starting connection: ', err);
      this.updateConnectionStatus(false);
      throw err;
    }
  }

  public async stopConnection(): Promise<void> {
    if (this.hubConnection) {
      await this.hubConnection.stop();
      this.updateConnectionStatus(false);
      console.log('SignalR Disconnected');
    }
  }

  private setupSignalRHandlers(): void {
    if (!this.hubConnection) return;

    // Handle incoming messages
    this.hubConnection.on('ReceiveMessage', (message: MessageDto) => {
      this.messageReceived$.next(message);
      this.addMessageToState(message);
      this.updateUnreadCount();
    });

    // Handle message sent confirmation
    this.hubConnection.on('MessageSent', (message: MessageDto) => {
      this.messageSent$.next(message);
      this.addMessageToState(message);
    });

    // Handle new conversation started
    this.hubConnection.on('NewConversationStarted', (conversation: ConversationDto) => {
      this.conversationStarted$.next(conversation);
      this.addConversationToState(conversation);
    });

    // Handle errors
    this.hubConnection.on('ReceiveError', (error: string) => {
      this.errorReceived$.next(error);
    });

    // Handle connection state changes
    this.hubConnection.onclose(() => {
      this.updateConnectionStatus(false);
    });

    this.hubConnection.onreconnected(() => {
      this.updateConnectionStatus(true);
    });
  }

  // SignalR Methods
  public async sendMessageViaHub(createMessageDto: CreateMessageDto): Promise<void> {
    if (this.hubConnection && this.hubConnection.state === signalR.HubConnectionState.Connected) {
      await this.hubConnection.invoke('SendMessage', createMessageDto);
    } else {
      throw new Error('SignalR connection not established');
    }
  }

  public async startConversationViaHub(startConversationDto: StartConversationDto): Promise<void> {
    if (this.hubConnection && this.hubConnection.state === signalR.HubConnectionState.Connected) {
      await this.hubConnection.invoke('StartConversation', startConversationDto);
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
    return this.http.get<ConversationDto[]>(`${this.baseUrl}/conversations/by-user/${userId}`, {
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
    return this.http.get<InboxDto[]>(`${this.baseUrl}/conversations/inbox/${userId}`, {
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

  public setActiveConversation(conversation: ConversationDto | undefined): void {
    const currentState = this.chatStateSubject.value;
    this.chatStateSubject.next({
      ...currentState,
      activeConversation: conversation
    });
  }

  public loadUserConversations(): void {
    const currentUserId = this.chatStateSubject.value.currentUserId;
    if (!currentUserId) return;

    this.getUserConversations(currentUserId).subscribe({
      next: (conversations) => {
        const currentState = this.chatStateSubject.value;
        this.chatStateSubject.next({
          ...currentState,
          conversations
        });
      },
      error: (error) => {
        console.error('Error loading conversations:', error);
      }
    });
  }

  public loadInboxPreview(): void {
    const currentUserId = this.chatStateSubject.value.currentUserId;
    if (!currentUserId) return;

    this.getInboxPreview(currentUserId).subscribe({
      next: (inbox) => {
        const currentState = this.chatStateSubject.value;
        this.chatStateSubject.next({
          ...currentState,
          inbox
        });
      },
      error: (error) => {
        console.error('Error loading inbox:', error);
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
}
