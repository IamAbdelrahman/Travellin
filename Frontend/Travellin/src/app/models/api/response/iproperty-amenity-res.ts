import { IPropertyAmenity } from '../../domain/iproperty-amenity';

export interface IPropertyAmenityRes {
  items: IPropertyAmenity[];
  metaData: {
    page: number;
    pageSize: number;
    total: number;
  };
}
