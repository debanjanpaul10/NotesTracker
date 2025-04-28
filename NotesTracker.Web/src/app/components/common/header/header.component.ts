import { Component, OnInit } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { Router, RouterLink } from '@angular/router';
import {
  CacheKeys,
  HeaderPageConstants,
  SuccessMessages,
} from '../../../helpers/Constants';
import { CommonModule } from '@angular/common';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { UserLoginComponent } from '../../user-login/user-login.component';
import { UserRegisterComponent } from '../../user-register/user-register.component';
import { AuthService } from '../../../services/auth-service.service';
import { ToasterService } from '../../../services/toaster.service';

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
   * @param dialog The Material dialog.
   * @param router The Router service.
   * @param authService The auth service.
   * @param toaster The toaster service.
   */
  constructor(
    private dialog: MatDialog,
    private router: Router,
    private authService: AuthService,
    private toaster: ToasterService
  ) {}

  ngOnInit(): void {
    const savedTheme =
      localStorage.getItem(CacheKeys.ThemeSettings) ||
      this.ThemeSettingsKeys.LightMode.Key;
    this.isDarkMode = savedTheme === this.ThemeSettingsKeys.DarkMode.Key;
    document.body.className = savedTheme;

    this.authService.isLoggedIn$.subscribe((isLoggedIn) => {
      this.isUserLoggedIn = isLoggedIn;
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
  public openLoginDialog(): void {
    this.dialog.open(UserLoginComponent, {
      width: '400px',
      disableClose: true,
    });
  }

  /**
   * Handles the register dialog open event.
   */
  public openRegisterDialog(): void {
    this.dialog.open(UserRegisterComponent, {
      width: '400px',
      disableClose: true,
    });
  }

  /**
   * Handle user logout event.
   */
  public handleUserLogout(): void {
    localStorage.removeItem(CacheKeys.LoggedInUser);
    this.authService.setLoggedInState(false);

    this.router.navigate(['/']).then(() => {
      this.toaster.showSuccess(SuccessMessages.UserLogoutSuccess);
    });
  }
}

export { HeaderComponent };
