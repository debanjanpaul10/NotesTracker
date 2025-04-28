import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { CacheKeys } from '../helpers/Constants';

@Injectable({
  providedIn: 'root',
})
class AuthService {
  private isLoggedInSubject = new BehaviorSubject<boolean>(
    localStorage.getItem(CacheKeys.LoggedInUser) !== null
  );

  public isLoggedIn$ = this.isLoggedInSubject.asObservable();

  public setLoggedInState(isLoggedIn: boolean): void {
    this.isLoggedInSubject.next(isLoggedIn);
  }

  constructor() {}
}

export { AuthService };
