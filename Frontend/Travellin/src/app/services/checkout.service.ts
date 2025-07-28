import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, tap } from 'rxjs/operators';
import { throwError, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class CheckOutBookingService {
  private baseUrl = 'https://localhost:7242/api/v1/Payments';

  constructor(private http: HttpClient) {}

  checkOut(bookingData: any): Observable<{ sessionUrl: string }> {
  console.log('📤 Sending checkout request with full booking data:', bookingData);

  return this.http.post<{ sessionUrl: string }>(
    `${this.baseUrl}/create-checkout-session`,
    bookingData
  ).pipe(
    tap(response => {
      console.log('✅ Received sessionUrl:', response.sessionUrl);
    }),
    catchError(error => {
      console.error('❌ Checkout error:', error);
      return throwError(() => error);
    })
  );
}

}
