import { ICountry } from '../../domain/icountry';
import { IPhoto } from '../../domain/iphoto';

export interface ApiUserProfileRequest {
  userId?: string;
  userName?: string;
  email?: string;
  roles?: string[];
  firstName?: string;
  lastName?: string;
  phoneNumber?: string;
  bio?: string;
  birthDate?: string;
  country?: ICountry;
  photo?: IPhoto;
}
