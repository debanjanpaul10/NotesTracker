import { Component } from '@angular/core';
import { ProgressSpinnerModule } from 'primeng/progressspinner';

/**
 * The Spinner component.
 */
@Component({
  selector: 'app-spinner',
  standalone: true,
  imports: [ProgressSpinnerModule],
  templateUrl: './spinner.component.html',
  styleUrl: './spinner.component.scss',
})
class SpinnerComponent {}

export { SpinnerComponent };
