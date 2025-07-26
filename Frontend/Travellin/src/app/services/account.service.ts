import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { ApiConstant } from '../utils/api-constant.util';
import { IChangePasswordReq } from '../models/api/request/ichange-password-req';
import { ILoginReq } from '../models/api/request/ilogin-req'
import { IRegisterReq } from '../models/api/request/iregister-req';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  constructor(private http: HttpClient) {}

  register(dto: IRegisterReq): Observable<HttpResponse<any>> {
    return this.http.post(`${ApiConstant.AccountsApi.register}`, dto, {
      observe: 'response',
      withCredentials: true,
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        Accept: 'application/json',
      }),
    });
  }

  login(dto: ILoginReq): Observable<HttpResponse<any>> {
    return this.http.post(`${ApiConstant.AccountsApi.login}`, dto, {
      observe: 'response',
      withCredentials: true,
    });
  }

  logout(): Observable<HttpResponse<any>> {
    return this.http.post(
      `${ApiConstant.AccountsApi.logout}`,
      {},
      {
        observe: 'response',
        withCredentials: true,
      }
    );
  }

  changePassword(dto: IChangePasswordReq): Observable<HttpResponse<any>> {
    return this.http.post(
      `${ApiConstant.AccountsApi['change-password']}`,
      dto,
      {
        observe: 'response',
        withCredentials: true,
        headers: new HttpHeaders({
          'Content-Type': 'application/json',
          Accept: 'application/json',
        }),
      }
    );
  }
}
