export interface Property {
  id: string;
  title: string;
  pricePerNight: number;
  latitude: number;
  longitude: number;
  owner: {
    id: string;
    userName: string;
  };
  location: {
    id: number;
    name: string;
    countryId: number;
  };
  propertyType: {
    id: number;
    name: string;
    icon: string;
  };
  photos: Array<{
    id: string;
    photoUrl: string;
  }>;
  averageRating: number;
  reviewCount: number;
  isActive: boolean;
  isDeleted: boolean;
}

export interface BookingGuest {
  bookingId: string;
  guestTypeId: number;
  guestCount: number;
}
export interface Bookings {
  id: string;
  userId: string;
  checkIn: string;
  checkOut: string;
  pricePerNight: number;
  totalFees: number;
  totalAmount: number;
  status: string;
  createdAt: string;
  updatedAt: string;
  bookingGuests: BookingGuest[];
  property: Property;
}
export interface GetBookingsResponse {
  items: Bookings[];
  metaData: {
    page: number;
    pageSize: number;
    total: number;
  };
}

export interface IGetBookingResponse extends Bookings {}
