import { Injectable } from '@angular/core';
import { catchError, Observable } from 'rxjs';
import { HttpClient, HttpEvent } from '@angular/common/http';
import { IPropertyInfo } from '../models/domain/iproperty-info';
import { IPropertyRequest } from '../models/api/request/iproperty-request';
import { ApiConstant } from '../utils/api-constant.util';
import { IPropertyResponse } from '../models/api/response/iproperty-response';
import { IPropertyAmenityReq } from '../models/api/request/iproperty-amenity-req';
import { IPropertyAvailability } from '../models/domain/iproperty-availability';
import { IPropertyavailabilityReq } from '../models/api/request/ipropertyavailability-req';
import { IPropertyGuestReq } from '../models/api/request/iproperty-guest-req';

@Injectable({
  providedIn: 'root',
})
export class AddPropertyService {
  private apiUrl = 'api/properties';

  constructor(private http: HttpClient) {}

  // add property info
  addProperty(property: IPropertyRequest): Observable<IPropertyResponse> {
    return this.http.post<IPropertyResponse>(
      ApiConstant.PropertiesApi.getAll,
      property,
      { withCredentials: true }
    );
  }

  // add property amenity
  addPropertyAmenity(data: IPropertyAmenityReq): Observable<any> {
    return this.http.post<IPropertyResponse>(
      ApiConstant.propertyAmenity.addAmenity,
      data,
      { withCredentials: true }
    );
  }

  // add property availabity
  addPropertyAvailability(data: IPropertyavailabilityReq): Observable<any> {
    return this.http.post(
      ApiConstant.propertyAvailability.addAvailability,
      data,
      { withCredentials: true }
    );
  }

  // add property Guest

  addPropertyGuests(data: IPropertyGuestReq): Observable<any> {
    return this.http.post(
      ApiConstant.propertyAvailability.addAvailability,
      data,
      { withCredentials: true }
    );
  }

  // add property photos

  uploadPropertyPhotos(formData: FormData): Observable<HttpEvent<any>> {
    return this.http.post(ApiConstant.propertyPhoto.addPhoto, formData, {
      reportProgress: true,
      observe: 'events',
      withCredentials: true,
    });
  }

  reorderPropertyPhotos(data: {
    propertyId: string;
    photoIds: string[];
  }): Observable<any> {
    return this.http.post(ApiConstant.photoReorder.reorder, data, {
      withCredentials: true,
    });
  }

  // add property & property space items

  addPropertySpace(space: {
    name: string;
    propertySpaceTypeId: number;
    propertyId: string;
    isShared: boolean;
  }): Observable<any> {
    return this.http.post(`${ApiConstant.propertySpaces.addSpaces}`, space, {
      withCredentials: true,
    });
  }

  addSpaceItem(item: {
    propertySpaceItemTypeId: number;
    propertySpaceId: string;
    quantity: number;
  }): Observable<any> {
    return this.http.post(
      `${ApiConstant.propertySpacesItems.addSpacesItem}`,
      item,
      { withCredentials: true }
    );
  }
}
