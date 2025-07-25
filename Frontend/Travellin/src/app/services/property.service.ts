import { Injectable } from '@angular/core';
import {
  HttpClient,
  HttpResponse,
  HttpHeaders,
  HttpParams,
} from '@angular/common/http';
import { expand, Observable, of, reduce } from 'rxjs';
import { ApiConstant } from '../utils/api-constant.util';
import { IPropertyTypeRes } from '../models/api/response/i-property-type-res';
import { IpropertyTypeApiResponse } from '../models/api/response/iproperty-type-api-res';
import { ISmartSearchReq } from '../models/api/request/ismartSearch-req';
import { ISmartSearchRes } from '../models/api/response/ismartSearch-res';
import { IGetAllReq } from '../models/api/request/iget-all-req';
import { IGuestType } from '../models/domain/iguest-type';
import { IGuestTypeResponse } from '../models/api/response/iguest-type-response';
import { IPropertyAmenity } from '../models/domain/iproperty-amenity';
import { IAminityCategory } from '../models/domain/IAminity-category';
import { IPropertyAmenityItem } from '../models/domain/iproperty-amenity-item';
import { IAmenitiesResponse } from '../models/api/response/iamenities-response';
import { IPropertyType } from '../models/domain/iproperty-type';

@Injectable({
  providedIn: 'root',
})
export class PropertyService {
  constructor(private http: HttpClient) {}

  showPropertyType(): Observable<HttpResponse<IpropertyTypeApiResponse>> {
    return this.http.get<IpropertyTypeApiResponse>(
      `${ApiConstant.PropertiesApi.getAllPropertyTypes}`,
      {
        observe: 'response',
        withCredentials: true,
      }
    );
  }

  currentPage = 1;
  pageSize = 10;
  getAmenitiesPaginated(
    page: number = 1,
    pageSize: number = 10
  ): Observable<IAmenitiesResponse> {
    return this.getAmenitiesPage(page, pageSize);
  }
  getAllProperty(queryParams?: IGetAllReq): Observable<HttpResponse<any>> {
    // Create HttpParams object from the query parameters
    let params = new HttpParams();

    if (queryParams) {
      // Add each query parameter to the HttpParams object
      Object.keys(queryParams).forEach(key => {
        const value = queryParams?.[key as keyof IGetAllReq];
        if (value !== undefined && value !== null) {
          params = params.append(key, value.toString());
        }
      });
    }
    return this.http.get<any>(`${ApiConstant.PropertiesApi.getAll}`, {
      observe: 'response',
      withCredentials: true,
      params,
    });
  }
  searchProperty(params: any): Observable<HttpResponse<any>> {
    return this.http.get<any>(`${ApiConstant.PropertiesApi.getAll}`, {
      params: params,
      observe: 'response',
      withCredentials: true,
    });
  }

  smartSearch(query: string): Observable<HttpResponse<any>> {
    return this.http.post(
      `${ApiConstant.PropertiesApi.smartSearch}`,
      JSON.stringify(query),
      {
        observe: 'response',
        withCredentials: true,
        headers: new HttpHeaders({
          'Content-Type': 'application/json', // <- stay as application/json
          Accept: 'application/json',
        }),
      }
    );
  }

  getPropertyById(id: string): Observable<HttpResponse<any>> {
    const url = ApiConstant.PropertiesApi.getById.replace('{id}', id);
    return this.http.get<any>(url, {
      observe: 'response',
      withCredentials: true,
    });
  }
  getPropertyAmenitiesById(id: string): Observable<HttpResponse<any>> {
    const url = ApiConstant.PropertiesApi.getPropertyAmenities.replace(
      '{id}',
      id
    );
    return this.http.get<any>(url, {
      observe: 'response',
      withCredentials: true,
    });
  }

  getPropertyAvailabilityById(id: string): Observable<HttpResponse<any>> {
    const url = ApiConstant.PropertiesApi.getPropertyAvailability.replace(
      '{id}',
      id
    );
    return this.http.get<any>(url, {
      observe: 'response',
      withCredentials: true,
    });
  }

  getPropertyFeesById(id: string): Observable<HttpResponse<any>> {
    const url = ApiConstant.PropertiesApi.getPropertyFees.replace('{id}', id);
    return this.http.get<any>(url, {
      observe: 'response',
      withCredentials: true,
    });
  }

  getAmenitiesCategories(): Observable<HttpResponse<any>> {
    return this.http.get<any>(
      `${ApiConstant.AmenitiesApi.getAllAmenitiesCategories}`,
      {
        observe: 'response',
        withCredentials: true,
      }
    );
  }
  getAllGuestTypes(): Observable<HttpResponse<IGuestTypeResponse>> {
    return this.http.get<IGuestTypeResponse>(
      `${ApiConstant.GuestType.GuestType}`,
      {
        observe: 'response',
      }
    );
  }
  // amenities
  getAmenitiesByPropertyId(
    propertyId: string
  ): Observable<IPropertyAmenityItem> {
    const url = ApiConstant.PropertiesApi.getPropertyAmenities.replace(
      '{id}',
      propertyId
    );
    return this.http.get<IPropertyAmenityItem>(url);
  }
  private getAmenitiesPage(
    page: number,
    pageSize: number
  ): Observable<{ items: IPropertyAmenity[]; metaData: any }> {
    let params = new HttpParams()
      .set('page', page.toString())
      .set('pageSize', pageSize.toString());
    return this.http.get<{ items: IPropertyAmenity[]; metaData: any }>(
      ApiConstant.AmenitiesApi.getAllAmenities,
      { params }
    );
  }

  // get all amenities from all pages
  getAllAmenities(): Observable<IPropertyAmenity[]> {
    const initialPage = 1;
    const pageSize = 10;
    return this.getAmenitiesPage(initialPage, pageSize).pipe(
      expand(response => {
        const currentPage = response.metaData.page;
        const totalPages = Math.ceil(
          response.metaData.total / response.metaData.pageSize
        );

        if (currentPage < totalPages) {
          return this.getAmenitiesPage(currentPage + 1, pageSize);
        }

        return of();
      }),

      reduce((acc: IPropertyAmenity[], response) => {
        return [...acc, ...response.items];
      }, [])
    );
  }

  getAmenityCategories(): Observable<IAminityCategory> {
    return this.http.get<IAminityCategory>(
      ApiConstant.AmenitiesApi.getAllAmenitiesCategories
    );
  }

  getPropertyTypes(): Observable<IPropertyType[]> {
    return this.http.get<IPropertyType[]>(
      ApiConstant.PropertiesApi.getAllPropertyTypes
    );
  }
  deleteProperty(id: string): Observable<any> {
    const url = `${ApiConstant.PropertiesApi.getAll}/${id}`;
    console.log('DELETE URL:', url);
    return this.http.delete(url, { withCredentials: true });
  }
}
