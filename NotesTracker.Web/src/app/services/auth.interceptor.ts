import {
  HttpRequest,
  HttpHandlerFn,
  HttpInterceptorFn,
} from '@angular/common/http';
import { AuthService } from '@auth0/auth0-angular';
import { inject } from '@angular/core';

/**
 * The Authentication Interceptor Service Function.
 */
export const AuthInterceptor: HttpInterceptorFn = (
  req: HttpRequest<unknown>,
  next: HttpHandlerFn
) => {
  const auth0 = inject(AuthService);
  // Subscribe to isAuthenticated$ to check if user is logged in
  auth0.idTokenClaims$.subscribe((claims) => {
    if (claims && claims.__raw) {
      const newReq = req.clone({
        headers: req.headers.set('Authorization', `Bearer ${claims.__raw}`),
      });
      console.log(newReq.headers);
      return next(newReq);
    } else {
      return next(req);
    }
  });

  return next(req);
};
