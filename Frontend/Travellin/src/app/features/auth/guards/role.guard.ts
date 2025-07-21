import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { TokenStorageService } from '../../../core/services/token-storage.service';
@Injectable({
  providedIn: 'root'
})
export class RoleGuard implements CanActivate {
  constructor(private tokenStorage: TokenStorageService, private router: Router, private jwtHelper: JwtHelperService) {}

  canActivate(route: any): boolean {
    const token = this.tokenStorage.getToken();
    if (token && !this.jwtHelper.isTokenExpired(token)) {
      const decodedToken = this.jwtHelper.decodeToken(token);
      const userRole = decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
      const requiredRoles = route.data.roles;

      if (requiredRoles && requiredRoles.some((role: string) => userRole === role)) {
        return true;
      }
    }
    this.router.navigate(['/unauthorized']);
    return false;
  }
}