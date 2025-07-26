import { ICountry } from './icountry';
import { IPhoto } from './iphoto';

export interface IUserProfile {
  userId: string;
  userName: string;
  email: string;
  firstName: string;
  lastName: string;
  roles?: string[];
  phoneNumber: string;
  bio: string;
  birthDate: string;
  country: ICountry;
  photo: IPhoto;
}
