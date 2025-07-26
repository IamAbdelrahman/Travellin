import { IAmenity } from '../../domain/Iamenity';

export interface IAmenitiesResponse {
  items: IAmenity[];
  metaData: {
    page: number;
    pageSize: number;
    total: number;
  };
}
