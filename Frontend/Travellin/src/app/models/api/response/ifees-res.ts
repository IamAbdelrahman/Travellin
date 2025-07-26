import { IPropertyFees } from '../../domain/iproperty-fees';

export interface IFeesResponse {
  items: IPropertyFees[];
  metaData: {
    page: number;
    pageSize: number;
    total: number;
  };
}
