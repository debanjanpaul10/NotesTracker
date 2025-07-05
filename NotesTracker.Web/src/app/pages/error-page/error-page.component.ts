import { Component } from '@angular/core';

import { ErrorPageConstants } from '@shared/notestracker.constants';

/**
 * The Error Page Component
 */
@Component({
  selector: 'app-error-page',
  templateUrl: './error-page.component.html',
  styleUrl: './error-page.component.scss',
})
export class ErrorPageComponent {
  /**
   * The error page constants.
   */
  public errorPageConstants = ErrorPageConstants;
}
