import { HostUpgradeStatus } from '../../core/enums/notification-type.enum';

export interface HostUpgradeNotification {
  requestId: string;
  userName: string;
  status: HostUpgradeStatus; // "pending", "approved", "rejected"
  requestDate: Date;
  adminMessage?: string;
}