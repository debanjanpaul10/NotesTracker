import { ApplicationConfig, importProvidersFrom } from '@angular/core';
import { provideRouter } from '@angular/router';
import { withInterceptors, provideHttpClient } from '@angular/common/http';
import { provideAuth0 } from '@auth0/auth0-angular';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { ToastrModule } from 'ngx-toastr';

import { routes } from './app.routes';
import { AuthInterceptor } from './services/auth.interceptor';
import { Auth0Credentials, LocalHostBaseUrl } from './helpers/config.constants';

/**
 * Determines the redirect uri for auth0 authentication.
 * @returns The redirect base URI.
 */
function determineRedirectUri (): string
{
  const currentHost = window.location.origin;

  if ( currentHost.includes( LocalHostBaseUrl ) )
  {
    return Auth0Credentials.RedirectBaseUris[ 0 ];
  } else
  {
    return Auth0Credentials.RedirectBaseUris[ 1 ];
  }
}

/**
 * The application configurations.
 */
export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter( routes ),
    provideAnimationsAsync(),
    provideHttpClient( withInterceptors( [ AuthInterceptor ] ) ),
    importProvidersFrom(
      ToastrModule.forRoot( {
        timeOut: 5000,
        positionClass: 'toast-bottom-right',
        preventDuplicates: true,
      } )
    ),
    provideAuth0( {
      domain: Auth0Credentials.Domain,
      clientId: Auth0Credentials.ClientId,
      authorizationParams: {
        audience: Auth0Credentials.Audience,
        redirect_uri: determineRedirectUri(),
      },
    } ),
  ],
};
