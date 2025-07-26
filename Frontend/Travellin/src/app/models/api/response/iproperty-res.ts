import { IProperty } from '../../domain/iproperty';
export interface IpropertyRes {
  items: IProperty[];
  metaData: {
    page: number;
    pageSize: number;
    total: number;
  };
}
