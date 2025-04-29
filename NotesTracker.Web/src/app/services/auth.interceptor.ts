import {
  HttpRequest,
  HttpHandlerFn,
  HttpInterceptorFn,
} from '@angular/common/http';
import { CacheKeys } from '../helpers/Constants';

/**
 * The Authentication Interceptor Service Function.
 */
export const AuthInterceptor: HttpInterceptorFn = (
  req: HttpRequest<unknown>,
  next: HttpHandlerFn
) => {
  const loggedInUser = localStorage.getItem(CacheKeys.LoggedInUser);

  if (loggedInUser) {
    const userId = JSON.parse(loggedInUser).userId;

    // Clone the request and set the X-User-Id header
    const newReq = req.clone({
      headers: req.headers.append('X-User-Id', userId.toString()),
    });

    return next(newReq);
  }

  return next(req);
};
