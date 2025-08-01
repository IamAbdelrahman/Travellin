export interface GuestArrivalNotification {
  bookingId: string;
  guestName: string;
  propertyTitle: string;
  checkIn: Date;
  guestMessage?: string;
}