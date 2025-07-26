import { IBookingGuest } from '../../domain/ibooking-guest';
import { IPropertyInfo } from '../../domain/iproperty-info';

export interface IBookingResponse {
  id: string;
  userId: string;
  checkIn: Date;
  checkOut: Date;
  pricePerNight: number;
  totalFees: number;
  totalAmount: number;
  status: string;
  createdAt: Date;
  updatedAt: Date;
  bookingGuests: IBookingGuest[];
  property: IPropertyInfo;
}
