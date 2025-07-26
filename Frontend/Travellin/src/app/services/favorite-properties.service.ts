import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IGetFavoritePropertiesRes } from '../models/api/response/iget-favorite-properties-res';
import { ApiConstant } from '../utils/api-constant.util';
import { IGetFavoritePropertiesReq } from '../models/api/request/iget-favorite-properties-req';

@Injectable({
  providedIn: 'root',
})
export class FavoritePropertiesService {
  constructor(private http: HttpClient) {}

  getAll(
    queryParams: IGetFavoritePropertiesReq
  ): Observable<IGetFavoritePropertiesRes> {
    // Create HttpParams object from the query parameters
    let params = new HttpParams();

    // Add each query parameter to the HttpParams object
    Object.keys(queryParams).forEach(key => {
      const value = queryParams[key as keyof IGetFavoritePropertiesReq];
      if (value !== undefined && value !== null) {
        params = params.append(key, value.toString());
      }
    });

    return this.http.get<IGetFavoritePropertiesRes>(
      ApiConstant.FavoritePropertiesApi.getAll,
      {
        withCredentials: true,
        params: params,
      }
    );
  }

  addToFavorites(propertyId: string): Observable<void> {
    return this.http.post<void>(
      ApiConstant.FavoritePropertiesApi.add,
      { propertyId },
      {
        withCredentials: true,
      }
    );
  }

  removeFromFavorites(propertyId: string): Observable<void> {
    return this.http.delete<void>(
      `${ApiConstant.FavoritePropertiesApi.delete}/${propertyId}`,
      {
        withCredentials: true,
      }
    );
  }
}
