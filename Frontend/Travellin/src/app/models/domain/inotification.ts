import { NotificationType } from '../../core/enums/notification-type.enum'

export interface Notification {
  id: number;
  userId: string;
  name: string;
  content: string;
  isRead: boolean;
  createdAt: Date;
  type: NotificationType;
  relatedEntityId?: string; // BookingId, MessageId, etc.
  metadata?: { [key: string]: any };
}