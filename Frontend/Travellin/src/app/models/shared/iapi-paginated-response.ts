export interface IPaginationMetaData {
  page: number;
  pageSize: number;
  total: number;
}

export interface IApiPaginatedResponse<T> {
  items: T[];
  metaData: IPaginationMetaData;
}
