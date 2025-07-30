import { MessageDto } from './message.model';

export interface ConversationDto {
  id: number;
  user1Id: string;
  user2Id: string;
  user1Name?: string;
  user2Name?: string;
  propertyId?: string; // Added for property context
  propertyTitle?: string; // Added for property context
  messages: MessageDto[];
}

export interface StartConversationDto {
  user1Id: string;
  user2Id: string;
  propertyId?: string; // Added for property context
}

export interface InboxDto {
  conversationId: number;
  participant: string;
  lastMessage?: string;
  lastMessageTime?: Date;
  sentAt: Date;
  isUnread: boolean;
  unreadCount?: number;
  propertyId?: string; // Added for property context
  propertyTitle?: string; // Added for property context
}

export interface ConversationSearchResultDto {
  conversationId: number;
  participant: string;
  matchedMessage?: string;
}