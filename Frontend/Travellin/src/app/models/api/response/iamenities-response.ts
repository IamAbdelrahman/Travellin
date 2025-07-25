import { IPropertyAmenity } from '../../domain/iproperty-amenity';

export interface IAmenitiesResponse {
  items: IPropertyAmenity[];
  metaData: {
    page: number;
    pageSize: number;
    total: number;
  };
}
