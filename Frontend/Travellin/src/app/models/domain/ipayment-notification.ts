import { PaymentStatus } from '../../core/enums/notification-type.enum';

export interface PaymentNotification {
  bookingId: string;
  propertyTitle: string;
  amount: number;
  currency: string;
  status: PaymentStatus; // "success", "failed", "pending"
  paymentDate: Date;
  transactionId?: string;
}