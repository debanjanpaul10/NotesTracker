import { Component, OnInit } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { RouterLink } from '@angular/router';
import { CacheKeys, HeaderPageConstants } from '../../../helpers/Constants';
import { CommonModule } from '@angular/common';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { UserLoginComponent } from '../../user-login/user-login.component';

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
  public HeaderConstants: any = HeaderPageConstants;

  /**
   * The is dark mode boolean flag.
   */
  public isDarkMode: boolean = false;

  /**
   * The theme settings constants.
   */
  public ThemeSettingsKeys: any = HeaderPageConstants.ThemeSettings;

  constructor(private dialog: MatDialog) {}

  ngOnInit(): void {
    const savedTheme =
      localStorage.getItem(CacheKeys.ThemeSettings) ||
      this.ThemeSettingsKeys.LightMode.Key;
    this.isDarkMode = savedTheme === this.ThemeSettingsKeys.DarkMode.Key;
    document.body.className = savedTheme;
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

  public openLoginDialog(): void {
    this.dialog.open(UserLoginComponent, {
      width: '400px',
      disableClose: true,
    });
  }
}

export { HeaderComponent };
