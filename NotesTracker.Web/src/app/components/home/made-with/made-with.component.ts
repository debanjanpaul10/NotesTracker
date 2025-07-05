import { Component, OnInit, signal } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { MatChipsModule } from '@angular/material/chips';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MdbCarouselModule } from 'mdb-angular-ui-kit/carousel';
import { SkeletonModule } from 'primeng/skeleton';

import { MadeWithSubTitle } from '@shared/notestracker.constants';
import { NotesTrackerService } from '@services/notestracker.service';
import { AboutUs } from '@models/about-us-dto.class';
import { ToasterService } from '@core/services/toaster.service';

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
export class MadeWithComponent implements OnInit {
  public subtitleConstant = MadeWithSubTitle;
  public aboutUsData = signal<AboutUs[]>([]);
  public loading = signal<boolean>(false);

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
  private async getAboutUsData(): Promise<void> {
    try {
      this.loading.set(true);
      const response = await this.notesTrackerService.getAboutUsDataAsync();
      this.aboutUsData.set(response || []);
    } catch (error: any) {
      console.error(error);
      this.toasterService.showError(error?.message);
    } finally {
      this.loading.set(false);
    }
  }
}
