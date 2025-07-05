import {
  Component,
  OnInit,
  signal,
  ViewChild,
  WritableSignal,
} from '@angular/core';
import { RouterLink } from '@angular/router';
import { MatDialogModule } from '@angular/material/dialog';
import { CommonModule } from '@angular/common';
import { AuthService, User } from '@auth0/auth0-angular';
import { AvatarModule } from 'primeng/avatar';
import { Popover, PopoverModule } from 'primeng/popover';

import { CacheKeys, HeaderPageConstants } from '@shared/notestracker.constants';
import { ButtonModule } from 'primeng/button';

/**
 * Header Component for the Notes Tracker Application
 *
 * This component provides the main navigation header for the application, including:
 * - Application branding and title
 * - Theme toggle functionality (light/dark mode)
 * - User authentication status and profile management
 * - User avatar and profile dropdown menu
 * - Login/logout functionality
 *
 * The component integrates with Auth0 for user authentication and manages
 * user session state, theme preferences, and profile information display.
 */
@Component({
  selector: 'app-header',
  standalone: true,
  imports: [
    RouterLink,
    CommonModule,
    MatDialogModule,
    AvatarModule,
    ButtonModule,
    PopoverModule,
  ],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss',
})
export class HeaderComponent implements OnInit {
  @ViewChild('op') op!: Popover;

  public HeaderConstants = HeaderPageConstants;
  public ThemeSettingsKeys = HeaderPageConstants.ThemeSettings;
  public isDarkMode: WritableSignal<boolean> = signal(false);
  public isUserLoggedIn: WritableSignal<boolean> = signal(false);
  public currentLoggedInUser: WritableSignal<string> = signal('');
  public currentUserProfile: WritableSignal<User | null> = signal(null);
  public userTokenClaims: WritableSignal<any> = signal(null);
  public menuOptions: any = [];

  /**
   * Initializes a new instance of the HeaderComponent
   *
   * Sets up the component with dependency injection for Auth0 authentication
   * and user management services. Initializes theme settings from localStorage
   * and configures the user menu options.
   *
   * @param auth0 - The Auth0 authentication service for user management
   */
  constructor(private readonly auth0: AuthService) {
    const savedTheme =
      localStorage.getItem(CacheKeys.ThemeSettings) ||
      this.ThemeSettingsKeys.LightMode.Key;
    this.isDarkMode.set(savedTheme === this.ThemeSettingsKeys.DarkMode.Key);

    this.menuOptions = [
      {
        name: 'Profile Settings',
        onClick: () => this.handleProfilePageRedirection(),
        icon: 'pi pi-cog',
      },
      {
        name: 'Logout',
        onClick: () => this.handleUserLogout(),
        icon: 'pi pi-sign-out',
      },
      {
        name: 'Report a bug',
        onClick: () => this.handleBugReport(),
        icon: 'pi pi-flag',
      },
    ];
  }

  /**
   * Lifecycle hook that is called after data-bound properties are initialized
   *
   * Sets up authentication state monitoring, applies the current theme,
   * and initializes user data loading when a user is authenticated.
   */
  ngOnInit() {
    this.applyTheme(this.isDarkMode());

    // Subscribe to authentication state
    this.auth0.isAuthenticated$.subscribe((isAuthenticated: boolean) => {
      this.isUserLoggedIn.set(isAuthenticated);

      if (isAuthenticated) {
        this.loadUserData();
      } else {
        this.currentLoggedInUser.set('');
        this.currentUserProfile.set(null);
        this.userTokenClaims.set(null);
      }
    });
  }

  /**
   * Toggles between light and dark theme modes
   *
   * Updates the component's dark mode state, applies the theme to the DOM,
   * and persists the theme preference in localStorage for future sessions.
   *
   * @returns {void}
   */
  public toggleTheme(): void {
    this.isDarkMode.set(!this.isDarkMode());
    this.applyTheme(this.isDarkMode());
    const theme = this.isDarkMode()
      ? this.ThemeSettingsKeys.DarkMode.Key
      : this.ThemeSettingsKeys.LightMode.Key;
    localStorage.setItem(CacheKeys.ThemeSettings, theme);
  }

  /**
   * Initiates the user login process using Auth0
   *
   * Redirects the user to the Auth0 login page for authentication.
   * This method is called when a user clicks the login button in the header.
   *
   * @returns {void}
   */
  public handleUserLogin(): void {
    this.auth0.loginWithRedirect();
  }

  /**
   * Gets the current user's display name
   *
   * Returns the user's display name from their profile, falling back to
   * email, user ID, or a default value if no name is available.
   *
   * @returns {string} The user's display name or fallback value
   */
  public getUserDisplayName(): string {
    const user = this.currentUserProfile();
    if (user) {
      return user.name || user.email || user.sub || 'User';
    }
    return this.currentLoggedInUser() || 'User';
  }

  /**
   * Gets the current user's profile picture URL
   *
   * Returns the URL of the user's profile picture from their Auth0 profile.
   * Returns an empty string if no picture is available.
   *
   * @returns {string} The user's profile picture URL or empty string
   */
  public getUserPicture(): string {
    const user = this.currentUserProfile();
    return user?.picture || '';
  }

  /**
   * Handles the toggle event for the user actions popover
   *
   * Toggles the visibility of the user profile dropdown menu when
   * the user avatar is clicked.
   *
   * @param event - The click event that triggered the toggle
   *
   * @returns {void}
   */
  public toggle(event: any): void {
    this.op.toggle(event);
  }

  /**
   * Handles the user profile page redirection event
   *
   * Currently shows an alert indicating that profile settings are
   * a work in progress. This method can be extended to navigate
   * to a dedicated profile settings page.
   *
   * @returns {void}
   */
  public handleProfilePageRedirection(): void {
    alert('Work in progress');
  }

  /**
   * Applies the specified theme to the DOM
   *
   * Updates the HTML element and body classes to reflect the current
   * theme setting (light or dark mode). This method is called when
   * the theme is toggled or during component initialization.
   *
   * @param isDark - Boolean indicating whether dark mode should be applied
   *
   * @returns {void}
   */
  private applyTheme(isDark: boolean): void {
    const htmlElement = document.querySelector('html');
    if (isDark) {
      htmlElement?.classList.add('my-app-dark');
      document.body.className = this.ThemeSettingsKeys.DarkMode.Key;
    } else {
      htmlElement?.classList.remove('my-app-dark');
      document.body.className = this.ThemeSettingsKeys.LightMode.Key;
    }
  }

  /**
   * Handles the loading of user data when the component initializes
   *
   * Subscribes to Auth0 user profile and ID token claims observables
   * to populate the component's user data signals. This method is called
   * when a user is authenticated.
   *
   * @returns {void}
   */
  private loadUserData(): void {
    // Get user profile data
    this.auth0.user$.subscribe((user: User | null | undefined) => {
      this.currentUserProfile.set(user || null);
      if (user) {
        this.currentLoggedInUser.set(
          user.name || user.email || user.sub || 'User'
        );
      }
    });

    // Get ID token claims
    this.auth0.idTokenClaims$.subscribe((claims: any) => {
      this.userTokenClaims.set(claims);
    });
  }

  /**
   * Handles user logout functionality
   *
   * Logs out the current user from Auth0 and redirects them back to the
   * application's home page. Only executes if a user is currently logged in.
   *
   * @returns {void}
   */
  private handleUserLogout(): void {
    if (this.isUserLoggedIn()) {
      this.auth0.logout({
        logoutParams: {
          returnTo: document.location.origin,
        },
      });
    }
  }

  private handleBugReport(): void {
    alert('Feature being worked on');
  }
}
