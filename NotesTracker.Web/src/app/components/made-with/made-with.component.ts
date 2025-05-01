import { Component, signal } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { MatChipsModule } from '@angular/material/chips';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';

import { MadeWithComponentConstants } from '../../helpers/notestracker.constants';

/**
 * The Made With Component
 */
@Component({
  selector: 'app-made-with',
  standalone: true,
  imports: [MatCardModule, MatChipsModule, CommonModule, MatButtonModule],
  templateUrl: './made-with.component.html',
  styleUrl: './made-with.component.scss',
})
class MadeWithComponent {
  /**
   * The MWC Data
   */
  public mwcObjects = signal(MadeWithComponentConstants);

  /**
   * Handles link redirection
   * @param link The Link URL
   */
  public redirectTo(link: string): void {
    window.open(link, '_blank');
  }
}

export { MadeWithComponent };
