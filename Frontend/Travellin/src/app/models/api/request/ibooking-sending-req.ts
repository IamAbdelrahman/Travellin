export interface IBookingSendingRequest {
  propertyId: string;
  checkIn: string;
  checkOut: string;
  guests: {
    guestTypeId: number;
    guestCount: number;
  }[];
}
