import { Component, OnInit } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { RouterLink } from '@angular/router';
import { CacheKeys, HeaderPageConstants } from '../../../helpers/Constants';
import { CommonModule } from '@angular/common';
import { MatDialogModule } from '@angular/material/dialog';
import { AuthService } from '@auth0/auth0-angular';

/**
 * The Header Component.
 */
@Component({
  selector: 'app-header',
  standalone: true,
  imports: [RouterLink, CommonModule, MatButtonModule, MatDialogModule],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss',
})
class HeaderComponent implements OnInit {
  /**
   * The header constants.
   */
  public HeaderConstants = HeaderPageConstants;

  /**
   * The is dark mode boolean flag.
   */
  public isDarkMode: boolean = false;

  /**
   * The theme settings constants.
   */
  public ThemeSettingsKeys = HeaderPageConstants.ThemeSettings;

  /**
   * The boolean flag to check if user is logged in or not.
   */
  public isUserLoggedIn: boolean = false;

  /**
   * Initializes a new instance of `HeaderComponent`
   * @param auth0 The auth service.
   */
  constructor(private auth0: AuthService) {}

  ngOnInit(): void {
    const savedTheme =
      localStorage.getItem(CacheKeys.ThemeSettings) ||
      this.ThemeSettingsKeys.LightMode.Key;
    this.isDarkMode = savedTheme === this.ThemeSettingsKeys.DarkMode.Key;
    document.body.className = savedTheme;

    this.auth0.isAuthenticated$.subscribe((isAuthenticated: boolean) => {
      this.isUserLoggedIn = isAuthenticated;
    });
  }

  /**
   * Handles the theme toggle event.
   */
  public toggleTheme(): void {
    this.isDarkMode = !this.isDarkMode;
    const theme = this.isDarkMode
      ? this.ThemeSettingsKeys.DarkMode.Key
      : this.ThemeSettingsKeys.LightMode.Key;
    document.body.className = theme;

    localStorage.setItem(CacheKeys.ThemeSettings, theme);
  }

  /**
   * Handles the login dialog open event.
   */
  public handleUserLogin(): void {
    this.auth0.loginWithRedirect();
  }

  /**
   * Handle user logout event.
   */
  public handleUserLogout(): void {
    if (this.isUserLoggedIn) {
      this.auth0.logout({
        logoutParams: {
          returnTo: document.location.origin,
        },
      });
    }
  }
}

export { HeaderComponent };
