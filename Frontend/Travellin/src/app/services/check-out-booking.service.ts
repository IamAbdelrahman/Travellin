import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiConstant } from '../utils/api-constant.util';
import {
  GetBookingsResponse,
  IGetBookingResponse,
} from '../models/api/request/iget-bookings';
import { PropertyDetails } from '../models/api/request/iget-propertiesDetails';
import { IProperty } from '../models/domain/iproperty';

@Injectable({
  providedIn: 'root',
})
export class CheckOutBookingService {
  constructor(private http: HttpClient) {}

  getAllBookings(): Observable<HttpResponse<any>> {
    return this.http.get<any>(ApiConstant.booking.getAllBookings, {
      observe: 'response',
      withCredentials: true,
    });
  }
  getBookingById(id: string): Observable<IGetBookingResponse> {
    const url = ApiConstant.booking.getBookingById.replace('{id}', id);
    return this.http.get<IGetBookingResponse>(url, { withCredentials: true });
  }
  createBooking(bookingData: any): Observable<any> {
    return this.http.post(ApiConstant.booking.createBooking, bookingData, {
      withCredentials: true,
    });
  }
  getPropertyDetails(id: string): Observable<PropertyDetails> {
    return this.http.get<PropertyDetails>(
      `${ApiConstant.PropertiesApi.getAll}/${id}`
    );
  }
  getAllProperties(): Observable<{ items: IProperty[] }> {
    return this.http.get<{ items: IProperty[] }>(
      `${ApiConstant.baseUrl}/Properties`
    );
  }
  checkOut(bookingId: string): Observable<any> {
    return this.http.post<any>(
      ApiConstant.Booking.checkout.replace('{bookingId}', bookingId),
      {},
      {
        withCredentials: true,
      }
    );
  }
  getUserRole(): Observable<HttpResponse<any>> {
    return this.http.get<any>(ApiConstant.UserProfile.User, {
      observe: 'response',
      withCredentials: true,
    });
  }
  cancelBookings(id: string): Observable<any> {
    const url2 = ApiConstant.booking.cancelBooking.replace('{id}', id);
    return this.http.post<any>(url2, {}, { withCredentials: true });
  }
}
