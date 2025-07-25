import { IApiPaginatedResponse } from '../../shared/iapi-paginated-response';
import { IFavoriteProperty } from '../../domain/ifaviorate-property';

export interface IGetFavoritePropertiesRes
  extends IApiPaginatedResponse<IFavoriteProperty> {}
