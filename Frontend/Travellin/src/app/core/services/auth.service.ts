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

  public getUserId(): string | null {
    return localStorage.getItem('userId');
  }

  public getUserName(): string | null {
    return localStorage.getItem('userName');
  }

  public getAccessToken(): string | null {
    return localStorage.getItem('accessToken');
  }

  public getUserRole(): string | null {
    const token = this.getAccessToken();
    if (!token) return null;
    
    try {
      const payload = JSON.parse(atob(token.split('.')[1]));
      return payload['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] || null;
    } catch (error) {
      console.error('Error decoding JWT token:', error);
      return null;
    }
  }

  public isAdmin(): boolean {
    const role = this.getUserRole();
    return role === 'Admin';
  }
}