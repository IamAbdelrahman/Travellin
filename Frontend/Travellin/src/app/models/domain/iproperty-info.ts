import { IImage } from './iimage';
import { ILocation } from './ilocation';
import { IPropertyAmenityRes } from '../api/response/iproperty-amenity-res';
import { IPropertyImage } from './iproperty-image';
import { IPropertyTypeRes } from '../api/response/i-property-type-res';
import { ISpaceItemSummary } from './ispace-item-summary';
import { ISpaceSummary } from './ispace-summary';
import { IUserProfile } from '../domain/iuser-profile';

export interface IPropertyInfo {
  id: string;
  title: string;
  description: string;
  safteyInfo: string;
  houseRules: string;
  cancellationPolicy: string;
  maxGuestCount: number;
  pricePerNight: number;
  latitude: number;
  longitude: number;
  averageRating: number;
  reviewCount: number;

  amenities?: IPropertyAmenityRes[];
  owner: IUserProfile;
  location: ILocation;
  propertyType: IPropertyTypeRes;
  photos: IImage[];
  spaceSummaries: ISpaceSummary[];
  spaceItemSummaries: ISpaceItemSummary[];
}
