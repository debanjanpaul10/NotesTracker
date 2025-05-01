import { Component, OnInit, signal, WritableSignal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { AuthService } from '@auth0/auth0-angular';
import { Router, RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatChipsModule } from '@angular/material/chips';

import { Notes } from '../../models/notes.model';
import { NotesService } from '../../services/notes.service';
import {
  AngularRoutes,
  ExceptionMessages,
  NotesContainerConstants,
} from '../../helpers/notestracker.constants';
import { LoaderComponent } from '../common/loader/loader.component';
import { ToasterService } from '../../services/toaster.service';
import { MadeWithComponent } from '../made-with/made-with.component';

/**
 * The Notes Container component.
 */
@Component({
  selector: 'app-notescontainer',
  standalone: true,
  imports: [
    CommonModule,
    RouterLink,
    MatButtonModule,
    MatIconModule,
    LoaderComponent,
    MadeWithComponent,
    MatCardModule,
    MatChipsModule,
  ],
  templateUrl: './notescontainer.component.html',
  styleUrl: './notescontainer.component.scss',
})
class NotesContainerComponent implements OnInit {
  /**
   * The notes list.
   */
  public notesList: WritableSignal<Notes[]> = signal([]);

  /**
   * The is loading boolean flag.
   */
  public loading: WritableSignal<boolean> = signal(false);

  /**
   * The is delete operation success boolean flag.
   */
  public isDeleteOperationSuccess: WritableSignal<boolean> = signal(false);

  /**
   * The notes container constants object.
   */
  public notesContainerConstants = NotesContainerConstants;

  /**
   * The Angular routes constants
   */
  public angularRoutesConstants = AngularRoutes;

  /**
   * The boolean flag to check user authenticated.
   */
  public isUserAuthenticated: WritableSignal<boolean> = signal(false);

  /**
   * Initializes a new instance of `NotesContainerComponent`
   * @param notesService The notes service.
   * @param router The router.
   * @param toaster The toaster service.
   * @param auth0 The auth service.
   */
  constructor(
    private notesService: NotesService,
    private router: Router,
    private toaster: ToasterService,
    private auth0: AuthService
  ) {}

  ngOnInit(): void {
    this.auth0.isAuthenticated$.subscribe((isAuth: boolean) => {
      this.isUserAuthenticated.set(isAuth);
    });

    if (this.isUserAuthenticated()) {
      this.getAllNotes();
    }
  }

  /**
   * Gets all the notes.
   */
  public getAllNotes(): void {
    this.loading.set(true);
    this.notesService.getAllNotesAsync().subscribe({
      next: (notes) => {
        this.notesList.set(notes);
        this.loading.set(false);
      },
      error: (err) => {
        console.error(err);
        this.loading.set(false);
        if (err?.error?.title) {
          this.toaster.showError(err.error.title);
        } else {
          this.toaster.showError(ExceptionMessages.DefaultErrorMessage);
        }
      },
    });
  }

  /**
   * Deletes a note by note id.
   * @param noteId The note id.
   */
  public deleteNoteById(noteId: number): void {
    this.loading.set(true);
    this.notesService.deleteNoteAsync(noteId).subscribe({
      next: (response) => {
        this.isDeleteOperationSuccess.set(response);
        this.loading.set(false);
        if (this.isDeleteOperationSuccess()) {
          this.router.navigate([AngularRoutes.Home.Link]);
        }
      },
      error: (err) => {
        console.error(err);
        this.loading.set(false);
        this.toaster.showError(err);
      },
    });
  }

  /**
   * Handles the navigation to edit note page.
   * @param noteId The note id.
   */
  public navigateToEditNote(noteId: number): void {
    this.router.navigate([NotesContainerConstants.RouteLinks.Notes, noteId]);
  }
}

export { NotesContainerComponent };
