export interface IAminityCategory {
  id: string;
  name: string;

  metaData: {
    page: number;
    pageSize: number;
    total: number;
  };
}
