export interface MessageNotification {
  messageId: string;
  conversationId: string;
  senderName: string;
  content: string;
  sentAt: Date;
  isRead: boolean;
}