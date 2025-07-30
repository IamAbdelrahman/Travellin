import {
  HttpClient,
  HttpErrorResponse,
  HttpEvent,
  HttpEventType,
  HttpHeaders,
  HttpParams,
  HttpResponse,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import {
  Observable,
  forkJoin,
  map,
  switchMap,
  catchError,
  of,
  throwError,
  expand,
  reduce,
  EMPTY,
} from 'rxjs';
import { IPropertyImage } from '../models/domain/iproperty-image';
import { IPropertyInfo } from '../models/domain/iproperty-info';
import { Router } from '@angular/router';
import { IPropertyAmenity } from '../models/domain/iproperty-amenity';
import { IPropertyAmenityItem } from '../models/domain/iproperty-amenity-item';
import { IAminityCategory } from '../models/domain/IAminity-category';
import { IPropertyType } from '../models/domain/iproperty-type';

import { IAmenitiesResponse } from '../models/api/response/iamenities-response';
import { IBookingRequest } from '../models/api/request/ibooking-req';
import { IBookingResponse } from '../models/api/response/ibooking-response';
import { IFeesResponse } from '../models/api/response/ifees-res';
import { ILocation } from '../models/domain/ilocation';
import { ApiConstant } from '../utils/api-constant.util';
import { IGuestTypeResponse } from '../models/api/response/iguest-type-response';
import {
  IPropertySpaces,
  IPropertySpacesRes,
} from '../models/domain/iproperty-spaces';
import { IPropertySpaceItem } from '../models/domain/iproperty-space-item';

@Injectable({
  providedIn: 'root',
})
export class PropertyService {
  private apiUrl = `${ApiConstant.baseUrl}/Properties`;
  private apiUrl1 = `${ApiConstant.baseUrl}/Properties/{id}`;
  private apiUrl2 = `${ApiConstant.baseUrl}/Amenities`;
  private apiUrl3 = `${ApiConstant.baseUrl}/AmenityCategories`;
  private apiUrl4 = `${ApiConstant.baseUrl}/PropertyTypes`;
  private apiUrl5 = `${ApiConstant.baseUrl}/Booking`;

  constructor(
    private http: HttpClient,
    private router: Router
  ) {}

  navigateToProperty(propertyId: string) {
    this.router.navigate(['/property', propertyId]);
  }

  getPropertyDetails(id: string): Observable<{
    propertyData: IPropertyInfo;
    propertyImages: IPropertyImage[];
  }> {
    if (!id) {
      return throwError(() => new Error('Invalid property ID'));
    }

    return forkJoin({
      property: this.getPropertyById(id),
      images: this.getImagesForProperty(id),
    }).pipe(
      map(({ property, images }) => ({
        propertyData: {
          ...property,
          averageRating: property.averageRating || 0,
          reviewCount: property.reviewCount || 0,
        },
        propertyImages: images,
      })),
      catchError(err => {
        console.error('Failed to load property details:', err);
        return throwError(() => new Error('Failed to load property details'));
      })
    );
  }

  private getImagesForProperty(
    propertyId: string
  ): Observable<IPropertyImage[]> {
    return this.http.get<IPropertyInfo>(`${this.apiUrl}/${propertyId}`).pipe(
      map(propertyInfo => {
        if (!propertyInfo.photos || propertyInfo.photos.length === 0) {
          return [];
        }

        return propertyInfo.photos
          .map(
            photo =>
              ({
                PropertyID: propertyId,
                PhotoId: photo.id,
                TouchedAt: new Date().toISOString(),
              }) as IPropertyImage
          )
          .sort(
            (a, b) =>
              new Date(b.TouchedAt).getTime() - new Date(a.TouchedAt).getTime()
          );
      }),
      catchError(() => of([] as IPropertyImage[]))
    );
  }

  getPropertyById(id: string): Observable<IPropertyInfo> {
    return this.http
      .get<IPropertyInfo>(`${this.apiUrl}/${id}`)
      .pipe(
        catchError(() => throwError(() => new Error('Property not found')))
      );
  }

  // ======================aminities============================================

  getAmenitiesByPropertyId(
    propertyId: string
  ): Observable<IPropertyAmenityItem> {
    return this.http.get<IPropertyAmenityItem>(
      `${this.apiUrl}/${propertyId}/Amenities`
    );
  }

  private getAmenitiesPage(
    page: number,
    pageSize: number
  ): Observable<{ items: IPropertyAmenity[]; metaData: any }> {
    let params = new HttpParams()
      .set('page', page.toString())
      .set('pageSize', pageSize.toString());

    return this.http.get<{ items: IPropertyAmenity[]; metaData: any }>(
      this.apiUrl2,
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
    return this.http.get<IAminityCategory>(`${this.apiUrl3}`);
  }

  //===================propertyType============================================
  getPropertyTypes(): Observable<{ items: IPropertyType[]; metaData: any }> {
    return this.http.get<{ items: IPropertyType[]; metaData: any }>(
      ApiConstant.PropertiesApi.getAllPropertyTypes
    );
  }

  // ========================get locations===========================================

  private getLocationPage(
    page: number,
    pageSize: number
  ): Observable<{ items: ILocation[]; metaData: any }> {
    const params = new HttpParams()
      .set('page', page.toString())
      .set('pageSize', pageSize.toString());

    return this.http.get<{ items: ILocation[]; metaData: any }>(
      ApiConstant.location.getAllLocations,
      { params }
    );
  }

  getAllLocations(): Observable<ILocation[]> {
    const initialPage = 1;
    const pageSize = 10;

    return this.getLocationPage(initialPage, pageSize).pipe(
      map(firstPage => {
        if (firstPage.metaData.total <= firstPage.metaData.pageSize) {
          return { items: firstPage.items, done: true };
        }
        return {
          items: firstPage.items,
          done: false,
          currentPage: firstPage.metaData.page,
        };
      }),
      // Only paginate if needed
      expand(({ items, done, currentPage }) => {
        if (done) return EMPTY;

        return this.getLocationPage(currentPage + 1, pageSize).pipe(
          map(response => {
            const totalPages = Math.ceil(
              response.metaData.total / response.metaData.pageSize
            );
            return {
              items: response.items,
              done: response.metaData.page >= totalPages,
              currentPage: response.metaData.page,
            };
          })
        );
      }),

      reduce((acc: ILocation[], { items }) => [...acc, ...items], []),

      catchError(error => {
        console.error('Error fetching locations:', error);
        return of([]); // Return empty array on error
      })
    );
  }

  // ====================================property availability==========================================

  getAvailability(propertyId: string): Observable<any> {
    return this.http.get(`${this.apiUrl}/${propertyId}/Availabilities`);
  }

  // ==============================property fees==========================================

  getPropertyFees(propertyId: string): Observable<IFeesResponse> {
    return this.http.get<IFeesResponse>(`${this.apiUrl}/${propertyId}/Fees`);
  }
  // =============================booking reserveation===================================
  createBooking(bookingData: IBookingRequest): Observable<IBookingResponse> {
    const token = localStorage.getItem('token') || '';

    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: `Bearer ${token}`,
    });

    const formattedBooking = {
      ...bookingData,
      checkIn: new Date(bookingData.checkIn).toISOString(),
      checkOut: new Date(bookingData.checkOut).toISOString(),
    };

    console.log('Making request to:', this.apiUrl5);
    console.log('Request headers:', headers);
    console.log('Request body:', bookingData);
    return this.http.post<IBookingResponse>(this.apiUrl5, bookingData, {
      headers: headers,
      withCredentials: true,
    });
  }

  //=============================amenities pagination(add-property)=================================
  currentPage = 1;
  pageSize = 10;

  getAmenitiesPaginated(
    page: number = 1,
    pageSize: number = 10
  ): Observable<IAmenitiesResponse> {
    return this.getAmenitiesPage(page, pageSize);
  }

  loadMoreAmenities(): Observable<IAmenitiesResponse> {
    return this.getAmenitiesPage(this.currentPage + 1, this.pageSize);
  }

  // ================get property Guests==================

  getAllGuestTypes(): Observable<IGuestTypeResponse> {
    return this.http.get<IGuestTypeResponse>(
      `${ApiConstant.GuestType.GuestType}`
    );
  }

  // ======================property spaces=============================
  private getSpacesPage(
    page: number,
    pageSize: number
  ): Observable<{ items: IPropertySpaces[]; metaData: any }> {
    let params = new HttpParams()
      .set('page', page.toString())
      .set('pageSize', pageSize.toString());

    return this.http.get<{ items: IPropertySpaces[]; metaData: any }>(
      ApiConstant.propertySpaces.spaces,
      { params }
    );
  }

  getPropertySpaces(): Observable<IPropertySpaces[]> {
    const initialPage = 1;
    const pageSize = 10;

    return this.getSpacesPage(initialPage, pageSize).pipe(
      expand(response => {
        const currentPage = response.metaData.page;
        const totalPages = Math.ceil(
          response.metaData.total / response.metaData.pageSize
        );

        if (currentPage < totalPages) {
          return this.getSpacesPage(currentPage + 1, pageSize);
        }

        return of();
      }),

      reduce((acc: IPropertySpaces[], response) => {
        return [...acc, ...response.items];
      }, [])
    );
  }

  private getSpacesItemPage(
    page: number,
    pageSize: number
  ): Observable<{ items: IPropertySpaceItem[]; metaData: any }> {
    let params = new HttpParams()
      .set('page', page.toString())
      .set('pageSize', pageSize.toString());

    return this.http.get<{ items: IPropertySpaceItem[]; metaData: any }>(
      ApiConstant.propertySpacesItems.spaceItems,
      { params }
    );
  }

  getPropertySpaceItem(): Observable<IPropertySpaceItem[]> {
    const initialPage = 1;
    const pageSize = 10;

    return this.getSpacesItemPage(initialPage, pageSize).pipe(
      expand(response => {
        const currentPage = response.metaData.page;
        const totalPages = Math.ceil(
          response.metaData.total / response.metaData.pageSize
        );

        if (currentPage < totalPages) {
          return this.getSpacesItemPage(currentPage + 1, pageSize);
        }

        return of();
      }),

      reduce((acc: IPropertySpaceItem[], response) => {
        return [...acc, ...response.items];
      }, [])
    );
  }
}
