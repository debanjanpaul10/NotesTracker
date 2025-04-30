import {
  ApplicationConfig,
  importProvidersFrom,
} from '@angular/core';
import { provideRouter } from '@angular/router';
import { withInterceptors, provideHttpClient } from '@angular/common/http';

import { routes } from './app.routes';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { ToastrModule } from 'ngx-toastr';
import { AuthInterceptor } from './services/auth.interceptor';
import { provideAuth0 } from '@auth0/auth0-angular';

/**
 * The application configurations.
 */
export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes),
    provideAnimationsAsync(),
    provideHttpClient(withInterceptors([AuthInterceptor])),
    importProvidersFrom(
      ToastrModule.forRoot({
        timeOut: 5000,
        positionClass: 'toast-bottom-right',
        preventDuplicates: true,
      })
    ),
    provideAuth0({
      domain: 'dev-debanjan-app.uk.auth0.com',
      clientId: 'onIb50nBAs96sTV0aC3lP2ivt2NCJQXX',
      authorizationParams: {
        audience: '1f39bae6-a069-452d-8633-1a726157d0a2',
        redirect_uri: 'http://localhost:4200',
      },
    }),
  ],
};
