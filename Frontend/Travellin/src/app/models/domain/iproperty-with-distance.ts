import { IProperty } from './iproperty';
export interface IPropertyWithDistance extends IProperty {
  distanceFromMe: string;
  isActive?: boolean;
  isDeleted?: boolean;
}
