import { MessageDto } from './message.model';

export interface ConversationDto {
  id: number;
  user1Id: string;
  user2Id: string;
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
  sentAt: Date;
  isUnread: boolean;
}

export interface ConversationSearchResultDto {
  conversationId: number;
  participant: string;
  matchedMessage?: string;
}