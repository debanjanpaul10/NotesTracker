import { Component, OnInit } from '@angular/core';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { RouterLink } from '@angular/router';
import { CacheKeys, HeaderPageConstants } from '../../../helpers/Constants';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [RouterLink, MatSlideToggleModule, CommonModule],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss',
})
export class HeaderComponent implements OnInit {
  HeaderConstants: any = HeaderPageConstants;
  isDarkMode: boolean = false;
  ThemeSettingsKeys: any = HeaderPageConstants.ThemeSettings;

  ngOnInit(): void {
    const savedTheme =
      localStorage.getItem(CacheKeys.ThemeSettings) ||
      this.ThemeSettingsKeys.LightMode.Key;
    this.isDarkMode = savedTheme === this.ThemeSettingsKeys.DarkMode.Key;
    document.body.className = savedTheme;
  }

  toggleTheme(): void {
    this.isDarkMode = !this.isDarkMode;
    const theme = this.isDarkMode
      ? this.ThemeSettingsKeys.DarkMode.Key
      : this.ThemeSettingsKeys.LightMode.Key;
    document.body.className = theme;

    localStorage.setItem(CacheKeys.ThemeSettings, theme);
  }
}
