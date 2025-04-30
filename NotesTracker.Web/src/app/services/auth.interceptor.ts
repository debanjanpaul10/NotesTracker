import {
  HttpRequest,
  HttpHandlerFn,
  HttpInterceptorFn,
} from '@angular/common/http';
import { AuthService } from '@auth0/auth0-angular';
import { inject } from '@angular/core';
import { switchMap } from 'rxjs';

/**
 * The Authentication Interceptor Service Function.
 */
export const AuthInterceptor: HttpInterceptorFn = (
  req: HttpRequest<unknown>,
  next: HttpHandlerFn
) => {
  const auth0 = inject(AuthService);
  return auth0.idTokenClaims$.pipe(
    switchMap((claims) => {
      if (claims && claims.__raw) {
        const newReq = req.clone({
          headers: req.headers.set('Authorization', `Bearer ${claims.__raw}`),
        });

        return next(newReq);
      }

      return next(req);
    })
  );

  return next(req);
};
