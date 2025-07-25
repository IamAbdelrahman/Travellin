import { IPropertyAmenity } from './iproperty-amenity';

export interface IPropertyAmenityItem {
  propertyId: string;
  amenity: IPropertyAmenity;
  metaData: {
    page: number;
    pageSize: number;
    total: number;
  };
}
