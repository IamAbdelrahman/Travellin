export interface MessageDto {
  id: number;
  content: string;
  isRead: boolean;
  sentAt: Date;
  translatedContent?: string;
  conversationId: number;
  senderId: string;
  receiverId: string;
}

export interface CreateMessageDto {
  senderId: string;
  receiverId: string;
  content: string;
  conversationId: number; // Added for admin message sending
}