export interface IPropertySpaces {
  id: number;
  name: string;
}

export interface IPropertySpacesRes {
  items: IPropertySpaces[];
  metaData: {
    page: number;
    pageSize: number;
    total: number;
  };
}
