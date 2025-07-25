export interface IPropertySpaceItem {
  id: number;
  name: string;
  propertySpaceTypeId: number;
}

export interface IPropertySpaceItemRes {
  items: IPropertySpaceItem[];
  metaData: {
    page: number;
    pageSize: number;
    total: number;
  };
}
