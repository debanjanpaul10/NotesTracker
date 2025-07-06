import { Injectable, signal } from '@angular/core';
import { AuthService, User } from '@auth0/auth0-angular';

/**
 * Users Service for managing user data and authentication state
 * 
 * This service provides centralized access to user information throughout the application.
 * It integrates with Auth0 for user authentication and manages user profile data,
 * token claims, and authentication state using Angular signals for reactive updates.
 * 
 * The service automatically subscribes to Auth0 user profile and token claims
 * observables to keep user data synchronized across the application.
 */
@Injectable({
  providedIn: 'root',
})
class UsersService {
  private _userName = signal('');
  private _currentUser = signal<User | null>(null);
  private _userTokenClaims = signal<any>(null);

  /**
   * Initializes a new instance of the UsersService
   * 
   * Sets up the service with dependency injection for Auth0 authentication
   * and automatically initializes user data subscriptions.
   * 
   * @param auth0 - The Auth0 authentication service for user management
   * 
   */
  constructor(private readonly auth0: AuthService) {
    this.initializeUserData();
  }

  /**
   * Gets the current user's username
   * 
   * Returns the username stored in the service's signal. This value
   * is automatically updated when the user profile changes.
   * 
   * @returns {string} The current user's username
   */
  public get userName(): string {
    return this._userName();
  }

  /**
   * Sets the current user's username
   * 
   * Updates the username signal with the provided value. This setter
   * is typically used internally by the service when user data changes.
   * 
   * @param user - The username to set
   */
  public set userName(user: string) {
    this._userName.set(user);
  }

  /**
   * Initializes user data subscriptions from Auth0
   * 
   * Sets up subscriptions to Auth0 user profile and ID token claims
   * observables to automatically update the service's user data signals
   * when authentication state or user information changes.
   * 
   * This method is called during service initialization and should not
   * be called externally.
   * 
   * @returns {void}
   */
  private initializeUserData(): void {
    // Subscribe to user profile changes
    this.auth0.user$.subscribe((user: User | null | undefined) => {
      this._currentUser.set(user || null);
      if (user) {
        this._userName.set(user.name || user.email || user.sub || '');
      }
    });

    // Subscribe to token claims changes
    this.auth0.idTokenClaims$.subscribe((claims: any) => {
      this._userTokenClaims.set(claims);
    });
  }
}

export { UsersService };
