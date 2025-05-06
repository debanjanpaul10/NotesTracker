import {
  HttpRequest,
  HttpHandlerFn,
  HttpInterceptorFn,
} from '@angular/common/http';
import { MsalService } from '@azure/msal-angular';
import { inject } from '@angular/core';
import { from, switchMap } from 'rxjs';

import { UsersService } from './users.service';

/**
 * The Authentication Interceptor Service Function.
 */
const AuthInterceptor: HttpInterceptorFn = (
  req: HttpRequest<unknown>,
  next: HttpHandlerFn
) => {
  const usersService = inject(UsersService);
  const msalService = inject(MsalService);

  return from(
    msalService.instance.acquireTokenSilent({
      account: msalService.instance.getActiveAccount() || undefined,
      scopes: [
        'openid',
        'profile',
        'https://debanjanlab.onmicrosoft.com/notes-tracker-api/Notes.Read',
      ], // Replace with your required scopes
    })
  ).pipe(
    switchMap((response) => {
      if (response && response.accessToken) {
        const userName = response.account?.username || null;
        if (userName) {
          usersService.setUserAlias(userName);
        }

        const newReq = req.clone({
          headers: req.headers.set(
            'Authorization',
            `Bearer ${response.accessToken}`
          ),
        });

        return next(newReq);
      }

      return next(req);
    })
  );
};

export { AuthInterceptor };
