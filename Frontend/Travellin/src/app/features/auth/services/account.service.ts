import { ILoginReq } from './../models/ilogin-req.model';
import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { ApiConstant } from '../../../helpers/api-constant.helper';
import { IRegisterReq } from '../models/iregister-req.model';
import { IChangePassword  } from '../models/ichange-password.model';

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

  changePassword(dto: IChangePassword): Observable<HttpResponse<any>> {
    return this.http.post(
      `${ApiConstant.AccountsApi.changePassword}`, dto, {
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