export enum NotificationType {
  BookingRequest = 1,
  BookingResponse = 2,
  BookingReminder = 3,
  Payment = 4,
  Message = 5,
  Review = 6,
  HostUpgrade = 7,
  GuestArrival = 8,
  System = 9,
}

export type NotificationStatus = 'accepted' | 'declined';
export type ReminderType = 'checkin_tomorrow' | 'checkin_today' | 'checkout_tomorrow';
export type PaymentStatus = 'success' | 'failed' | 'pending';
export type HostUpgradeStatus = 'pending' | 'approved' | 'rejected';
export type SystemNotificationType = 'promotion' | 'maintenance' | 'security';