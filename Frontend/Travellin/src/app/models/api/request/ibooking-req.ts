export interface IBookingRequest {
  propertyId: string;
  checkIn: string;
  checkOut: string;
  pricePerNight: number;
  totalFees: number;
  totalAmount: number;
  bookingGuests: {
    guestTypeId: number;
    guestCount: number;
  }[];
}
