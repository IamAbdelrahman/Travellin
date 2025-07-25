import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { ApiConstant } from '../utils/api-constant.util';
import { IBookingResponse } from '../models/api/response/ibooking-response';
import { IBookingSendingRequest } from '../models/api/request/ibooking-sending-req';

@Injectable({
  providedIn: 'root',
})
export class BookingService {
  constructor(private http: HttpClient) {}

  createBooking(bookingData: IBookingSendingRequest): Observable<any> {
    return this.http.post(`${ApiConstant.booking.createBooking}`, bookingData, {
      observe: 'response',
      withCredentials: true,
    });
  }
}
