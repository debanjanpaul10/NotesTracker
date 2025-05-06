import { ApplicationConfig, importProvidersFrom } from '@angular/core';
import { provideRouter } from '@angular/router';
import { withInterceptors, provideHttpClient } from '@angular/common/http';
import {
  MSAL_GUARD_CONFIG,
  MSAL_INSTANCE,
  MSAL_INTERCEPTOR_CONFIG,
  MsalGuardConfiguration,
  MsalInterceptorConfiguration,
  MsalService,
} from '@azure/msal-angular';
import { InteractionType, PublicClientApplication } from '@azure/msal-browser';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { ToastrModule } from 'ngx-toastr';

import { msalConfig, protectedResources } from './services/azure-auth-config';
import { routes } from './app.routes';
import { AuthInterceptor } from './services/auth.interceptor';

/**
 * Configures the MSAL Instance factory.
 * @returns The public client application config.
 */
function MSALInstanceFactory(): PublicClientApplication {
  const msalInstance = new PublicClientApplication(msalConfig);

  // Ensure the instance is initialized before use
  msalInstance.initialize().catch((error) => {
    console.error('MSAL initialization failed', error);
  });

  return msalInstance;
}

/**
 * Configures the MSAL Guard configuration factory.
 * @returns The MSAL guard configuration.
 */
function MSALGuardConfigFactory(): MsalGuardConfiguration {
  return {
    interactionType: InteractionType.Redirect,
    authRequest: {
      scopes: ['openid', 'profile', 'offline_access'],
    },
  };
}

/**
 * Configures the MSAL interceptor configuration factory.
 * @returns The MSAL interceptor configuration.
 */
function MSALInterceptorConfigFactory(): MsalInterceptorConfiguration {
  return {
    interactionType: InteractionType.Redirect,
    protectedResourceMap: new Map([
      [
        protectedResources.notesApi.endpoint[0],
        protectedResources.notesApi.scopes,
      ],
    ]),
  };
}

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
    {
      provide: MSAL_INSTANCE,
      useFactory: MSALInstanceFactory,
    },
    {
      provide: MSAL_GUARD_CONFIG,
      useFactory: MSALGuardConfigFactory,
    },
    {
      provide: MSAL_INTERCEPTOR_CONFIG,
      useFactory: MSALInterceptorConfigFactory,
    },
    MsalService,
  ],
};
