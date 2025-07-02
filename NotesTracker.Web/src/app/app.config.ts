import { ApplicationConfig } from '@angular/core';
import { provideRouter } from '@angular/router';
import { withInterceptors, provideHttpClient } from '@angular/common/http';
import { provideAuth0 } from '@auth0/auth0-angular';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { MessageService } from 'primeng/api';
import { providePrimeNG } from 'primeng/config';
import Aura from '@primeng/themes/aura';

import { routes } from './app.routes';
import { AuthInterceptor } from '@core/auth.interceptor';
import { environment } from '@environments/environment';

/**
 * The application configurations.
 */
export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes),
    provideAnimationsAsync(),
    provideHttpClient(withInterceptors([AuthInterceptor])),
    MessageService,
    provideAuth0({
      domain: environment.Auth0Credentials.Domain,
      clientId: environment.Auth0Credentials.ClientId,
      authorizationParams: {
        audience: environment.Auth0Credentials.Audience,
        redirect_uri: environment.baseUrl,
      },
    }),
    providePrimeNG({
      ripple: true,
      theme: {
        preset: Aura,
        options: {
          darkModeSelector: '.my-app-dark',
        },
      },
    }),
  ],
};
