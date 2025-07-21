import { 
  ApplicationConfig, 
  provideBrowserGlobalErrorListeners
} from '@angular/core';
import { provideRouter } from '@angular/router';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { routes } from './app.routes';
import { TokenStorageService } from './core/services/token-storage.service';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { JwtInterceptor } from './core/interceptors/auth.interceptor';
import { AuthGuard } from './features/auth/guards/auth.guard';
import { RoleGuard } from './features/auth/guards/role.guard';
export const appConfig: ApplicationConfig = {
  providers: [
    provideHttpClient(withInterceptorsFromDi()),
    {
      provide: HTTP_INTERCEPTORS,
      useClass: JwtInterceptor,
      multi: true
    },
    provideRouter(routes),
    AuthGuard,
    // RoleGuard,
    provideBrowserGlobalErrorListeners(),
    TokenStorageService
  ]
};