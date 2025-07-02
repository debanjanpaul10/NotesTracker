import { Injectable, signal } from '@angular/core';

/**
 * The Users Service class.
 */
@Injectable({
  providedIn: 'root',
})
class UsersService {
  /**
   * The user alias.
   */
  private _userName = signal('');

  /**
   * Sets the user name.
   * @param userName The passed user id.
   */
  public setUserName(userName: string): void {
    this._userName.set(userName);
  }

  /**
   * Gets the user name.
   * @returns The user name.
   */
  public getUserName(): string {
    return this._userName();
  }
}

export { UsersService };
