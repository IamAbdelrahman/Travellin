export interface ICheckoutBookingRequest {
  bookingId: string;
  guest: {
    id: string;
    email: string;
  };
  property: {
    id: string;
    title: string;
    description: string | null;
    mainPhotoUrl: string;
    locationName: string;
  };
  bookingPeriod: {
    checkInDate: string;
    checkOutDate: string;
    nights: number;
  };
  pricing: {
    pricePerNight: number;
    totalFees: number;
    totalAmount: number;
  };
  metadata: {
    additionalProp1: string;
    additionalProp2: string;
    additionalProp3: string;
  };
  totalAmount: number;
}
