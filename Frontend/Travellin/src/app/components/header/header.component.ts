import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { StickyNavDirective } from '../../directive/sticky-nav.directive';
import { AuthService } from '../../core/services/auth.service';
import { AccountService } from '../../services/account.service';
import {
  LucideAngularModule,
  Home,
  Heart,
  User,
  LogIn,
  LogOut,
  Menu,
} from 'lucide-angular';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule,
    StickyNavDirective,
    LucideAngularModule,
  ],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss',
})
export class HeaderComponent {
  readonly icons = {
    home: Home,
    heart: Heart,
    user: User,
    login: LogIn,
    logout: LogOut,
    menu: Menu,
  };

  constructor(
    private authService: AuthService,
    private accountService: AccountService,
    private router: Router
  ) {}

  get isAuthenticated(): boolean {
    return this.authService.isAuthenticated();
  }

  logout() {
    this.accountService.logout().subscribe({
      next: () => {
        this.authService.unsetAuthData();
        this.router.navigate(['/home']);
      },
      error: err => {
        console.error('Logout failed:', err);
        this.router.navigate(['/home']);
      },
    });
  }
}
