import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';
import { provideHttpClient } from '@angular/common/http';
import { routes } from './app.routes';
import { importProvidersFrom } from '@angular/core';
import { JwtHelperService, JwtModule } from '@auth0/angular-jwt';

export const appConfig: ApplicationConfig = {
  providers: [provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),
    provideHttpClient(),
    JwtHelperService,
    importProvidersFrom(JwtModule.forRoot({
      config: {
        tokenGetter: () => {
          const matches = document.cookie.match(new RegExp('(^| )accessToken=([^;]+)'));
          return matches ? matches[2] : null;
        }
    } }))]
};
