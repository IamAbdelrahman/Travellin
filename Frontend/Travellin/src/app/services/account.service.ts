import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { ApiConstant } from '../utils/api-constant.util';
import { IChangePasswordReq } from '../models/api/request/ichange-password-req';
import { ILoginReq } from '../models/api/request/ilogin-req'
import { IRegisterReq } from '../models/api/request/iregister-req';
import { Auth, signInWithPopup, GoogleAuthProvider } from '@angular/fire/auth';
import { Router } from '@angular/router';
@Injectable({
  providedIn: 'root',
})
export class AccountService {
  constructor(private http: HttpClient, private fireauth: Auth, private router: Router) { }

  register(dto: IRegisterReq): Observable<HttpResponse<any>> {
    localStorage.setItem('email', dto.email);
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
    localStorage.setItem('email', dto.email);
    console.log('Email saved to localStorage:', dto.email);
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
async continueWithGoogle() {
    try {
      const result = await signInWithPopup(this.fireauth, new GoogleAuthProvider());

      if (!result.user.email) {
        alert('No email returned from Google');
        return;
      }

      const payload = {
        email: result.user.email,
        fullName: result.user.displayName,
        photoUrl: result.user.photoURL,
        providerId: result.user.uid
      };

      // Send to backend
      this.http.post(`${ApiConstant.AccountsApi.googleLogin}`, payload)
        .subscribe((res: any) => {
          localStorage.setItem('token', res.token);
          this.router.navigate(['/home']);
        });

    } catch (error: any) {
      alert(error.message);
    }
  }
}
