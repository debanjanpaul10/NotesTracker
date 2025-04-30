import { Component } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { MatChipsModule } from '@angular/material/chips';
import { MadeWithComponentConstants } from '../../helpers/Constants';
import { CommonModule } from '@angular/common';

/**
 * The Made With Component
 */
@Component({
  selector: 'app-made-with',
  standalone: true,
  imports: [MatCardModule, MatChipsModule, CommonModule],
  templateUrl: './made-with.component.html',
  styleUrl: './made-with.component.scss',
})
class MadeWithComponent {
  public mwcConstants = MadeWithComponentConstants;
}

export { MadeWithComponent };
