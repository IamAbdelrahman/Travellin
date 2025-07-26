import { IGuestType } from '../../domain/iguest-type';

export interface IGuestTypeResponse {
  items: IGuestType[];
  metaData?: {
    page: number;
    pageSize: number;
    total: number;
  };
}
