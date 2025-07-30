import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiConstant } from '../utils/api-constant.util';
export interface TransferRequest {
  paymentIntentId: string;
  hostStripeAccountId: string;
  amountInCents: number;
}

@Injectable({
  providedIn: 'root'
})
export class StripeTransferService {

  constructor(private http: HttpClient) {}

  transferToHost(transferData: TransferRequest): Observable<any> {
    return this.http.post(
    `$ApiConstant.payment.transferToHost`, 
    transferData);
  }
}

