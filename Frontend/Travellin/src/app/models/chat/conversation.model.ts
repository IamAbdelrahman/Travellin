import { MessageDto } from './message.model';

export interface ConversationDto {
  id: number;
  user1Id: string;
  user2Id: string;
  user1Name?: string;
  user2Name?: string;
  messages: MessageDto[];
}

export interface StartConversationDto {
  user1Id: string;
  user2Id: string;
}

export interface InboxDto {
  conversationId: number;
  participant: string;
  lastMessage?: string;
  lastMessageTime?: Date;
  sentAt: Date;
  isUnread: boolean;
  unreadCount?: number;
}

export interface ConversationSearchResultDto {
  conversationId: number;
  participant: string;
  matchedMessage?: string;
}