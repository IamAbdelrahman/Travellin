import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiConstant } from '../utils/api-constant.util';

export interface CancellationRequest {
  bookingId: string;
  cancelledByUserId: string;
  isHostCancellation: boolean;
  cancellationReason?: string;
  refundAmount?: number;
}

export interface CancellationResult {
  isSuccessful: boolean;
  message: string;
  refundAmount?: number;
  refundId?: string;
  newBookingStatus: string;
  newPaymentStatus?: string;
}

export interface CanCancelResponse {
  canCancel: boolean;
  refundAmount: number;
  isWithinCancellationWindow: boolean;
}

export interface RefundRequest {
  amount: number;
  reason?: string;
}

@Injectable({
  providedIn: 'root'
})
export class CancellationService {
  constructor(private http: HttpClient) {}

  // Check if booking can be cancelled
  canCancelBooking(bookingId: string): Observable<CanCancelResponse> {
    const url = ApiConstant.booking.canCancel.replace('{id}', bookingId);
    return this.http.get<CanCancelResponse>(url, { withCredentials: true });
  }

  // Enhanced cancellation with refund support
  cancelBookingEnhanced(request: CancellationRequest): Observable<CancellationResult> {
    const url = ApiConstant.booking.cancelEnhanced.replace('{id}', request.bookingId);
    return this.http.post<CancellationResult>(url, request, { withCredentials: true });
  }

  // Process refund
  processRefund(bookingId: string, refundRequest: RefundRequest): Observable<CancellationResult> {
    const url = ApiConstant.booking.refund.replace('{id}', bookingId);
    return this.http.post<CancellationResult>(url, refundRequest, { withCredentials: true });
  }

  // Legacy cancellation (existing functionality)
  cancelBooking(bookingId: string): Observable<any> {
    const url = ApiConstant.booking.cancelBooking.replace('{id}', bookingId);
    return this.http.delete(url, { withCredentials: true });
  }
} 