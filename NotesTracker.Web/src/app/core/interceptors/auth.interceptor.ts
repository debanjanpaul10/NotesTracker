import {
  HttpRequest,
  HttpHandlerFn,
  HttpInterceptorFn,
} from '@angular/common/http';
import { AuthService } from '@auth0/auth0-angular';
import { inject } from '@angular/core';
import { switchMap } from 'rxjs';

import { UsersService } from '@core/services/users.service';
import { AuthConstants } from '@shared/notestracker.constants';

/**
 * The Authentication Interceptor Service Function.
 */
const AuthInterceptor: HttpInterceptorFn = (
  req: HttpRequest<unknown>,
  next: HttpHandlerFn
) => {
  const auth0 = inject(AuthService);
  const usersService = inject(UsersService);
  const { AuthorizationConstant, BearerConstant, UserNameConstant } =
    AuthConstants;

  return auth0.idTokenClaims$.pipe(
    switchMap((claims) => {
      if (claims && claims.__raw) {
        const userName = claims[UserNameConstant];
        if (userName) {
          usersService.userName = userName;
        }
        const newReq = req.clone({
          headers: req.headers.set(
            AuthorizationConstant,
            `${BearerConstant} ${claims.__raw}`
          ),
        });

        return next(newReq);
      }

      return next(req);
    })
  );
};

export { AuthInterceptor };
