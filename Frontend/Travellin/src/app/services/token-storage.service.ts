import { Injectable } from '@angular/core';
import { jwtDecode } from 'jwt-decode';
interface JwtPayload {
  sub?: string;
  nameid?: string;
  unique_name?: string;
  [key: string]: any;
}
@Injectable({
  providedIn: 'root'
})
export class TokenStorageService {
  private TOKEN_KEY = 'accessToken';
  private ROLE_KEY = 'accessRole';

  public saveToken(token: string): void {
    localStorage.setItem(this.TOKEN_KEY, token);
  }

  public getToken(): string | null {
    return localStorage.getItem(this.TOKEN_KEY);
  }

  public saveRole(role: string): void {
    localStorage.setItem(this.ROLE_KEY, role);
  }

  public getRole(): string | null {
    return localStorage.getItem(this.ROLE_KEY);
  }

  public clear(): void {
    localStorage.clear();
  }
  // Add this method for chat service compatibility
  public getAccessToken(): string | null {
    return this.getToken();
  }

  // Add this method to extract user ID from JWT token
  public getUserId(): string | null {
    const token = this.getToken();
    if (!token) {
      return null;
    }

    try {
      const decoded: JwtPayload = jwtDecode(token);
      // Try different claim names that might contain the user ID
      return decoded.nameid || decoded.sub || decoded.unique_name || null;
    } catch (error) {
      console.error('Error decoding JWT token:', error);
      return null;
    }
  }
  public isTokenValid(): boolean {
    const token = this.getToken();
    return token !== null && token !== '';
  }
}