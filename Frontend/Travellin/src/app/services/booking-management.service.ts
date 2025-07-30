import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiConstant } from '../utils/api-constant.util';

export interface BookingManagementResponse {
  items: any[];
  metaData: {
    page: number;
    pageSize: number;
    total: number;
  };
}

@Injectable({
  providedIn: 'root'
})
export class BookingManagementService {
  constructor(private http: HttpClient) {}

  // Host booking management
  getHostBookings(page: number = 1, pageSize: number = 10, status?: string): Observable<BookingManagementResponse> {
    let params = new HttpParams()
      .set('page', page.toString())
      .set('pageSize', pageSize.toString());
    
    if (status) {
      params = params.set('status', status);
    }

    return this.http.get<BookingManagementResponse>(`${ApiConstant.booking.hostBookings}`, { 
      params, 
      withCredentials: true 
    });
  }

  getHostPendingBookings(page: number = 1, pageSize: number = 10): Observable<BookingManagementResponse> {
    const params = new HttpParams()
      .set('page', page.toString())
      .set('pageSize', pageSize.toString());

    return this.http.get<BookingManagementResponse>(`${ApiConstant.booking.hostPendingBookings}`, { 
      params, 
      withCredentials: true 
    });
  }

  getPropertyBookings(propertyId: string, page: number = 1, pageSize: number = 10): Observable<BookingManagementResponse> {
    const params = new HttpParams()
      .set('page', page.toString())
      .set('pageSize', pageSize.toString());

    const url = ApiConstant.booking.propertyBookings.replace('{propertyId}', propertyId);
    return this.http.get<BookingManagementResponse>(url, { 
      params, 
      withCredentials: true 
    });
  }

  getHostPendingCount(): Observable<number> {
    return this.http.get<number>(`${ApiConstant.booking.hostPendingCount}`, { 
      withCredentials: true 
    });
  }

  // Admin booking management
  getAllBookingsForAdmin(page: number = 1, pageSize: number = 10, status?: string): Observable<BookingManagementResponse> {
    let params = new HttpParams()
      .set('page', page.toString())
      .set('pageSize', pageSize.toString());
    
    if (status) {
      params = params.set('status', status);
    }

    return this.http.get<BookingManagementResponse>(`${ApiConstant.booking.adminAllBookings}`, { 
      params, 
      withCredentials: true 
    });
  }

  getAdminPendingBookings(page: number = 1, pageSize: number = 10): Observable<BookingManagementResponse> {
    const params = new HttpParams()
      .set('page', page.toString())
      .set('pageSize', pageSize.toString());

    return this.http.get<BookingManagementResponse>(`${ApiConstant.booking.adminPendingBookings}`, { 
      params, 
      withCredentials: true 
    });
  }

  getAdminPendingCount(): Observable<number> {
    return this.http.get<number>(`${ApiConstant.booking.adminPendingCount}`, { 
      withCredentials: true 
    });
  }

  // Booking actions
  acceptBooking(bookingId: string): Observable<any> {
    return this.http.post(`${ApiConstant.booking.acceptBooking.replace('{bookingId}', bookingId)}`, {}, { 
      withCredentials: true 
    });
  }

  declineBooking(bookingId: string): Observable<any> {
    return this.http.post(`${ApiConstant.booking.declineBooking.replace('{bookingId}', bookingId)}`, {}, { 
      withCredentials: true 
    });
  }

  cancelBooking(bookingId: string): Observable<any> {
    return this.http.delete(`${ApiConstant.booking.cancelBooking.replace('{id}', bookingId)}`, { 
      withCredentials: true 
    });
  }
} 