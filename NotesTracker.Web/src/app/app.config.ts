import { ApplicationConfig, importProvidersFrom } from '@angular/core';
import { provideRouter } from '@angular/router';
import { withInterceptors, provideHttpClient } from '@angular/common/http';
import { provideAuth0 } from '@auth0/auth0-angular';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { ToastrModule } from 'ngx-toastr';

import { routes } from './app.routes';
import { AuthInterceptor } from './services/auth.interceptor';
import { ConfigurationConstants } from "./helpers/config.constants";

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
      domain: ConfigurationConstants.Auth0.Domain,
      clientId: ConfigurationConstants.Auth0.ClientId,
      authorizationParams: {
        audience: ConfigurationConstants.Auth0.Audience,
        redirect_uri: ConfigurationConstants.Auth0.RedirectBaseUri,
      },
    }),
  ],
};
