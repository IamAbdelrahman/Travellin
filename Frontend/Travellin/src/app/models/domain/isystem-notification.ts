import { SystemNotificationType } from '../../core/enums/notification-type.enum';

export interface SystemNotification {
  title: string;
  message: string;
  type: SystemNotificationType; // "promotion", "maintenance", "security"
  expiresAt: Date;
  actionUrl?: string;
}