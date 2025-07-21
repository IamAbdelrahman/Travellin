import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  constructor() {}

  public setAuthData(userId: string, userName: string, accessToken: string) {
    localStorage.setItem('userId', userId);
    localStorage.setItem('userName', userName);
    localStorage.setItem('accessToken', accessToken);
  }

  public unsetAuthData() {
    localStorage.removeItem('userId');
    localStorage.removeItem('userName');
    localStorage.removeItem('accessToken');
  }

  public isAuthenticated(): boolean {
    let authToken = localStorage.getItem('accessToken');
    return Boolean(authToken);
  }
}