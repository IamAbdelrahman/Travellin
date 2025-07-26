import { ConversationDto } from './conversation.model';
import { InboxDto } from './conversation.model';

export interface ChatState {
  currentUserId: string;
  conversations: ConversationDto[];
  activeConversation?: ConversationDto;
  inbox: InboxDto[];
  isConnected: boolean;
  unreadCount: number;
}