import { IPropertyTypeRes } from './i-property-type-res';
export interface IpropertyTypeApiResponse {
  items: IPropertyTypeRes[];
  metaData: {
    page: number;
    pageSize: number;
    total: number;
  };
}
