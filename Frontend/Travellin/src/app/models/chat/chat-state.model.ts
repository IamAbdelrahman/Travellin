import { ConversationDto,InboxDto } from "./conversation.model";

export interface ChatState {
  currentUserId: string;
  activeConversation?: ConversationDto;
  conversations: ConversationDto[];
  inbox: InboxDto[];
  isConnected: boolean;
  unreadCount: number;
}