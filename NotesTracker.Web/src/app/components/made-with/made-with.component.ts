import { Component, OnInit, signal, WritableSignal } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { MatChipsModule } from '@angular/material/chips';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MdbCarouselModule } from 'mdb-angular-ui-kit/carousel';
import { SkeletonModule } from 'primeng/skeleton';

import { MadeWithSubTitle } from '../../helpers/notestracker.constants';
import { NotesTrackerService } from '../../services/notestracker.service';
import { AboutUs } from '../../models/about-us-dto.class';
import { ToasterService } from '../../services/toaster.service';

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
    SkeletonModule,
  ],
  templateUrl: './made-with.component.html',
  styleUrl: './made-with.component.scss',
})
class MadeWithComponent implements OnInit {
  /**
   * The subtitle constant.
   */
  subtitleConstant = MadeWithSubTitle;

  /**
   * The about us data.
   */
  aboutUsData: WritableSignal<AboutUs[]> = signal([]);

  /**
   * The boolean flag for loading.
   */
  loading = signal<boolean>(false);

  /**
   * Initializes a new instance of `MadeWithComponent`
   * @param notesTrackerService The notes tracker service.
   */
  constructor(
    private notesTrackerService: NotesTrackerService,
    private toasterService: ToasterService
  ) {}

  ngOnInit(): void {
    this.getAboutUsData();
  }

  /**
   * Handles link redirection
   * @param link The Link URL
   */
  public redirectTo(link: string): void {
    window.open(link, '_blank');
  }

  /**
   * Handles the api call for get about us data.
   */
  private getAboutUsData(): void {
    this.loading.set(true);
    this.notesTrackerService.getAboutUsDataAsync().subscribe({
      next: (response) => {
        this.aboutUsData.set(response || []);
      },
      error: (err) => {
        console.error(err);
        this.toasterService.showError(err?.message);
      },
      complete: () => {
        this.loading.set(false);
      },
    });
  }
}

export { MadeWithComponent };
