import { Component, OnInit, signal, WritableSignal } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { RouterLink } from '@angular/router';
import { MatDialogModule } from '@angular/material/dialog';
import { CommonModule } from '@angular/common';
import { MsalService } from '@azure/msal-angular';

import {
  CacheKeys,
  HeaderPageConstants,
} from '../../../helpers/notestracker.constants';

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
  public isDarkMode: WritableSignal<boolean> = signal(false);

  /**
   * The theme settings constants.
   */
  public ThemeSettingsKeys = HeaderPageConstants.ThemeSettings;

  /**
   * The boolean flag to check if user is logged in or not.
   */
  public isUserLoggedIn: WritableSignal<boolean> = signal(false);

  /**
   * Initializes a new instance of `HeaderComponent`
   * @param msalService The MSAL service.
   */
  constructor(private msalService: MsalService) {}

  ngOnInit(): void {
    const savedTheme =
      localStorage.getItem(CacheKeys.ThemeSettings) ||
      this.ThemeSettingsKeys.LightMode.Key;
    this.isDarkMode.set(savedTheme === this.ThemeSettingsKeys.DarkMode.Key);
    document.body.className = savedTheme;

    this.isUserLoggedIn.set(
      this.msalService.instance.getActiveAccount() !== null
    );
  }

  /**
   * Handles the theme toggle event.
   */
  public toggleTheme(): void {
    this.isDarkMode.set(!this.isDarkMode());
    const theme = this.isDarkMode()
      ? this.ThemeSettingsKeys.DarkMode.Key
      : this.ThemeSettingsKeys.LightMode.Key;
    document.body.className = theme;

    localStorage.setItem(CacheKeys.ThemeSettings, theme);
  }

  /**
   * Handles the login dialog open event.
   */
  public handleUserLogin(): void {
    this.msalService.loginRedirect();
  }

  /**
   * Handle user logout event.
   */
  public handleUserLogout(): void {
    if (this.isUserLoggedIn()) {
      this.msalService.logoutRedirect({
        postLogoutRedirectUri: document.location.origin,
      });
    }
  }
}

export { HeaderComponent };
