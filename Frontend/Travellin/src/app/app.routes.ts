import { Routes } from '@angular/router';
import { HomePageComponent } from './pages/home-page/home-page.component';
import { PropertyInfoComponent } from './pages/property-info-page/property-info.component';
import { BlankLayoutComponent } from './layouts/blank-layout/blank-layout.component';
import { MainLayoutComponent } from './layouts/main-layout/main-layout.component';
import { AuthGuard } from './core/guards/auth.guard'
import { BookingHistoryComponent } from './pages/booking-history/booking-history.component';
import { BookingPageComponent } from './pages/booking-page/booking-page.component';
import { ProfileComponent } from './pages/profile-page/profile/profile.component';
import { UpdateProfileComponent } from './pages/profile-page/update-profile/update-profile.component';
import { HostUpgradeAprovalComponent } from './pages/host-upgrade-approval/host-upgrade-aproval.component';
import { AddPropertyComponent } from './pages/add-property/add-property.component';
import { UsersAdminComponent } from './components/users-admin/users-admin.component';
import { PropertyAdminComponent } from './components/property-admin/property-admin.component';
import { FavoritesPageComponent } from './pages/favorites-page/favorites-page.component';
import { ToastContainerComponent } from './components/toast-container/toast-container.component';
export const routes: Routes = [
  {
    path: '',
    component: MainLayoutComponent,
    children: [
      { path: '', redirectTo: 'home', pathMatch: 'full' },
      { path: 'home', component: HomePageComponent },
      { path: 'property/:id', component: PropertyInfoComponent },
    ],
  },
  {
    path: '',
    component: MainLayoutComponent,
    canActivate: [AuthGuard], // Applies to all child routes
    children: [
      {
        path: 'favorites',
        loadComponent: () =>
          import('./pages/favorites-page/favorites-page.component').then(
            m => m.FavoritesPageComponent
          ),
      },
      {
        path: 'bookingHistory',
        loadComponent: () =>
          import('./pages/booking-history/booking-history.component').then(
            m => m.BookingHistoryComponent
          ),
      },
      {
        path: 'booking/:id',
        loadComponent: () =>
          import('./pages/booking-page/booking-page.component').then(
            m => m.BookingPageComponent
          ),
      },
      {
        path: 'upgrade',
        loadComponent: () =>
          import(
            './pages/upgradeToHost/upgrade-tohost/upgrade-tohost.component'
          ).then(m => m.UpgradeTohostComponent),
      },
      {
        path: 'profile',
        loadComponent: () =>
          import('./pages/profile-page/profile/profile.component').then(
            m => m.ProfileComponent
          ),
      },
      {
        path: 'user',
        loadComponent: () =>
          import(
            './pages/profile-page/update-profile/update-profile.component'
          ).then(m => m.UpdateProfileComponent),
      },

      {
        path: 'hostApproval',
        loadComponent: () =>
          import(
            './pages/host-upgrade-approval/host-upgrade-aproval.component'
          ).then(m => m.HostUpgradeAprovalComponent),
      },
      {
        path: 'addproperty',
        loadComponent: () =>
          import('./pages/add-property/add-property.component').then(
            m => m.AddPropertyComponent
          ),
      },
      {
        path: 'chat',
        loadComponent: () =>
          import('./pages/chat-page/chat-page.component').then(
            m => m.ChatPageComponent
          ),
      },
       {
      path: 'hubs/chat',
      loadComponent: () =>
        import('./pages/chat-page/chat-page.component').then(
          m => m.ChatPageComponent
        ),
    },
      {
        path: 'admin',
        loadComponent: () =>
          import('./pages/admin/admin.component').then(m => m.AdminComponent),
        children: [
          { path: '', redirectTo: 'users', pathMatch: 'full' },
          { path: 'users', component: UsersAdminComponent },
          { path: 'properties', component: PropertyAdminComponent },
          { path: 'requests', component: HostUpgradeAprovalComponent },
        ],
      },
    ],
  },
  {
    path: '',
    component: BlankLayoutComponent,
    children: [
      {
        path: 'auth',
        loadComponent: () =>
          import('./pages/auth-page/auth-page.component').then(
            m => m.AuthPageComponent
          ),
      },
    ],
  },
];
