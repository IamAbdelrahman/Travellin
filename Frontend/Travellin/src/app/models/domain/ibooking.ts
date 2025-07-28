export interface iBooking {
  id: string;
  guest: {
    id: string;
    email: string;
  };
  property: {
    id: string;
    title: string;
    description: string;
    mainPhotoUrl: string;
    locationName: string;
    pricePerNight?: number;
    photos?: { photoUrl: string }[];
  };
  bookingPeriod: {
    checkInDate: string;
    checkOutDate: string;
    nights: number;
  };
  pricing?: {
    pricePerNight?: number;
    totalFees?: number;
    totalAmount?: number;
  };
  totalFees?: number;
  metadata?: {
    [key: string]: any;
  };
  bookingGuests?: {
    guestCount: number;
  }[];
}
