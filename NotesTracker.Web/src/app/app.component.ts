import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { AuthService } from '@auth0/auth0-angular';
import { CommonModule } from '@angular/common';

import { HeaderComponent } from '@components/common/header/header.component';
import { SpinnerComponent } from '@components/common/spinner/spinner.component';
import { ToasterComponent } from '@components/common/toaster/toaster.component';
import { BugReportComponent } from '@components/bug-report/bug-report.component';

/**
 * The Main app component.
 */
@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterOutlet,
    HeaderComponent,
    CommonModule,
    SpinnerComponent,
    ToasterComponent,
    BugReportComponent,
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
class AppComponent implements OnInit {
  /**
   * The is loading boolean flag.
   */
  public isLoading: boolean = true;

  /**
   * Initializes a new instance of `AppComponent`
   * @param auth0 The Auth0 service.
   */
  constructor(private readonly auth0: AuthService) {}

  ngOnInit(): void {
    this.auth0.isAuthenticated$.subscribe((isAuth: boolean) => {
      if (isAuth) {
        this.auth0.idTokenClaims$.subscribe((value: any) => {
          if (value) {
            this.isLoading = false;
          }
        });
      } else {
        this.isLoading = false;
      }
    });
  }
}

export { AppComponent };
