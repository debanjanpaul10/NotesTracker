import { Component, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthService } from '@auth0/auth0-angular';
import { RouterLink } from '@angular/router';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { CardModule } from 'primeng/card';
import { ButtonModule } from 'primeng/button';

import { Notes } from '@models/notes.model';
import { NotesService } from '@services/notes.service';
import {
  AngularRoutes,
  ExceptionMessages,
  NotesContainerConstants,
} from '@shared/notestracker.constants';
import { LoaderComponent } from '@components/common/loader/loader.component';
import { ToasterService } from '@core/services/toaster.service';
import { MadeWithComponent } from '@components/home/made-with/made-with.component';
import { NoteComponent } from '@components/home/note/note.component';
import { firstValueFrom } from 'rxjs';

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
  public notesList = signal<Notes[]>([]);

  /**
   * The is loading boolean flag.
   */
  public loading = signal(false);

  /**
   * The boolean flag to check user authenticated.
   */
  public isUserAuthenticated = signal(false);

  /**
   * The is delete operation success boolean flag.
   */
  public isDeleteOperationSuccess = signal(false);

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
  public async getAllNotes(): Promise<void> {
    try {
      this.loading.set(true);
      const response = await this.notesService.getAllNotesAsync();
      this.notesList.set(response);
    } catch (error) {
      console.error(error);
      this.toaster.showError(ExceptionMessages.AllNoteFetchFailedMessage);
    } finally {
      this.loading.set(false);
    }
  }

  /**
   * Deletes a note by note id.
   * @param noteId The note id.
   */
  public async deleteNoteById(noteId: number): Promise<void> {
    try {
      this.loading.set(true);
      const response = await this.notesService.deleteNoteAsync(noteId);
      this.isDeleteOperationSuccess.set(response);
      if (response) {
        this.getAllNotes();
      }
    } catch (error) {
      console.error(error);
      this.toaster.showError(ExceptionMessages.AllNoteFetchFailedMessage);
    } finally {
      this.loading.set(false);
    }
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
