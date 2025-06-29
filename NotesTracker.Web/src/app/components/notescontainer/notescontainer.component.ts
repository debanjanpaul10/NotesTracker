import { Component, OnInit, signal, WritableSignal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthService } from '@auth0/auth0-angular';
import { RouterLink } from '@angular/router';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { CardModule } from 'primeng/card';
import { ButtonModule } from 'primeng/button';

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
import { NoteComponent } from '../note/note.component';

/**
 * The Notes Container component.
 */
@Component({
  selector: 'app-notescontainer',
  standalone: true,
  imports: [
    CommonModule,
    RouterLink,
    ButtonModule,
    LoaderComponent,
    MadeWithComponent,
    CardModule,
    MatDialogModule,
  ],
  templateUrl: './notescontainer.component.html',
  styleUrl: './notescontainer.component.scss',
})
class NotesContainerComponent implements OnInit {
  /**
   * The notes container constants object.
   */
  public notesContainerConstants = NotesContainerConstants;

  /**
   * The Angular routes constants
   */
  public angularRoutesConstants = AngularRoutes;

  /**
   * The notes list.
   */
  public notesList: WritableSignal<Notes[]> = signal([]);

  /**
   * The is loading boolean flag.
   */
  public loading: WritableSignal<boolean> = signal(false);

  /**
   * The boolean flag to check user authenticated.
   */
  public isUserAuthenticated: WritableSignal<boolean> = signal(false);

  /**
   * The is delete operation success boolean flag.
   */
  public isDeleteOperationSuccess: WritableSignal<boolean> = signal(false);

  /**
   * Initializes a new instance of `NotesContainerComponent`
   * @param notesService The notes service.
   * @param router The router service.
   * @param toaster The toaster service.
   * @param auth0 The auth service.
   * @param dialog The material dialog service.
   */
  constructor(
    private notesService: NotesService,
    private toaster: ToasterService,
    private auth0: AuthService,
    private dialog: MatDialog
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
        this.toaster.showError(ExceptionMessages.AllNoteFetchFailedMessage);
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
          this.getAllNotes();
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
   * Handles the edit note event.
   * @param noteId
   */
  public handleNoteEdit(noteId: number): void {
    const dialogRef = this.dialog.open(NoteComponent, {
      width: '400px',
      disableClose: true,
      data: { noteId },
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result?.success) {
        this.getAllNotes();
      }
    });
  }
}

export { NotesContainerComponent };
