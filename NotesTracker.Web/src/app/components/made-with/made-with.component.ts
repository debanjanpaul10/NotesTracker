import {
  Component,
  inject,
  OnInit,
  signal,
  WritableSignal,
} from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { MatChipsModule } from '@angular/material/chips';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MdbCarouselModule } from 'mdb-angular-ui-kit/carousel';

import { MadeWithSubTitle } from '../../helpers/notestracker.constants';
import { NotesTrackerService } from '../../services/notestracker.service';
import { AboutUs } from '../../models/about-us-dto.class';
import { firstValueFrom } from 'rxjs';

/**
 * The Made With Component
 */
@Component({
  selector: 'app-made-with',
  standalone: true,
  imports: [
    MatCardModule,
    MatChipsModule,
    CommonModule,
    MatButtonModule,
    MdbCarouselModule,
  ],
  templateUrl: './made-with.component.html',
  styleUrl: './made-with.component.scss',
})
class MadeWithComponent implements OnInit {
  notesTrackerService = inject(NotesTrackerService);

  /**
   * The subtitle constant.
   */
  subtitleConstant = MadeWithSubTitle;

  /**
   * The about us data.
   */
  aboutUsData: WritableSignal<AboutUs[]> = signal([]);

  async ngOnInit(): Promise<void> {
    const aboutUsData = await firstValueFrom(
      this.notesTrackerService.getAboutUsDataAsync()
    );
    this.aboutUsData.set(aboutUsData || []);
  }

  /**
   * Handles link redirection
   * @param link The Link URL
   */
  public redirectTo(link: string): void {
    window.open(link, '_blank');
  }
}

export { MadeWithComponent };
