import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiConstant } from '../utils/api-constant.util';
import { UpgradeHostRequest } from '../models/api/request/upgrade-host-request';

@Injectable({
  providedIn: 'root',
})
export class UpgradeServiceService {
  constructor(private http: HttpClient) {}

  submitHostUpgrade(formData: FormData): Observable<UpgradeHostRequest> {
    return this.http.post<UpgradeHostRequest>(
      ApiConstant.upgrade.upgrade,
      formData,
      { withCredentials: true }
    );
  }

  getUpgradeRequests(params: any): Observable<any> {
    let httpParams = new HttpParams()
      .set('page', params.page.toString())
      .set('pageSize', params.pageSize.toString());

    if (params.status) {
      httpParams = httpParams.set('status', params.status);
    }

    if (params.documentType) {
      httpParams = httpParams.set('documentType', params.documentType);
    }

    return this.http.get<any>(ApiConstant.upgrade.upgrade, {
      params: httpParams,
      withCredentials: true,
    });
  }

  approveRequest(requestId: string): Observable<any> {
    return this.http.patch(
      `${ApiConstant.upgrade.upgrade}/${requestId}/approve`,
      {},
      { withCredentials: true }
    );
  }

  // rejectRequest(requestId: string, reason: string): Observable<any> {
  //   return this.http.patch(`${ApiConstant.upgrade.upgrade}/${requestId}/reject`, { reason }, { withCredentials: true });
  // }

  rejectRequest(requestId: string, reason: string): Observable<any> {
    const requestBody = {
      RejectionReason: reason,
    };

    return this.http.patch(
      `${ApiConstant.upgrade.upgrade}/${requestId}/reject`,
      requestBody,
      { withCredentials: true }
    );
  }
}
