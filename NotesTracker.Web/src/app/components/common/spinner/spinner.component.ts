import { Component, Input } from '@angular/core';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

/**
 * The Spinner component.
 */
@Component({
  selector: 'app-spinner',
  standalone: true,
  imports: [MatProgressSpinnerModule],
  templateUrl: './spinner.component.html',
  styleUrl: './spinner.component.scss',
})
class SpinnerComponent {
  /**
   * The color of the spinner
   */
  @Input() color: 'primary' | 'accent' | 'warn' = 'primary';
}

export { SpinnerComponent };
