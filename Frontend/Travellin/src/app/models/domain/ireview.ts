import { IUserProfile } from './iuser-profile';

export interface IReview {
  id: string;
  comment: string;
  cleanliness: number;
  accuracy: number;
  checkIn: number;
  communication: number;
  location: number;
  value: number;
  propertyId: string;
  guestId: string;
  createdAt: Date;
  updatedAt: Date;

  guest: IUserProfile;
}
