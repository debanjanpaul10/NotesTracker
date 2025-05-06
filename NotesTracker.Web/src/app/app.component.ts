import { Component, OnInit, signal, WritableSignal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { CommonModule } from '@angular/common';
import { MsalService } from '@azure/msal-angular';

import { HeaderComponent } from './components/common/header/header.component';
import { SpinnerComponent } from './components/common/spinner/spinner.component';
import { passwordResetRequest } from './services/azure-auth-config';

/**
 * The Main app component.
 */
@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, HeaderComponent, CommonModule, SpinnerComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
class AppComponent implements OnInit {
  /**
   * The is loading boolean flag.
   */
  public isLoading: WritableSignal<boolean> = signal(true);

  /**
   * Initializes a new instance of `AppComponent`
   * @param msalService The MSAL service.
   */
  constructor(private msalService: MsalService) {}

  ngOnInit(): void {
    // Wait for MSAL instance to initialize and handle redirect
    this.msalService.instance
      .initialize()
      .then(() => {
        return this.msalService.instance.handleRedirectPromise();
      })
      .then((response) => {
        if (response && response.account) {
          // Set the active account after successful login
          this.msalService.instance.setActiveAccount(response.account);
        }
      })
      .catch((error) => {
        if (error.errorMessage && error.errorMessage.includes('AADB2C90118')) {
          // Handle password reset event
          console.warn(
            'Password reset requested. Redirecting to password reset flow...'
          );
          this.msalService.loginRedirect(passwordResetRequest);
        } else {
          console.error(
            'Error during MSAL initialization or redirect handling:',
            error
          );
        }
      })
      .finally(() => {
        this.isLoading.set(false);
      });
  }
}

export { AppComponent };
