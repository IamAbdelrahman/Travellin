import { ReminderType, NotificationStatus } from '../../core/enums/notification-type.enum';

export interface BookingRequestNotification {
  bookingId: string;
  guestName: string;
  propertyTitle: string;
  checkIn: Date;
  checkOut: Date;
  totalAmount: number;
  guestMessage?: string;
  guestCount: number;
}

export interface BookingResponseNotification {
  bookingId: string;
  hostName: string;
  propertyTitle: string;
  status: NotificationStatus; // "accepted" | "declined"
  checkIn: Date;
  checkOut: Date;
  hostMessage?: string;
}

export interface BookingReminderNotification {
  bookingId: string;
  propertyTitle: string;
  checkIn: Date;
  checkOut: Date;
  reminderType: ReminderType; // "checkin_tomorrow", "checkin_today", "checkout_tomorrow"
}