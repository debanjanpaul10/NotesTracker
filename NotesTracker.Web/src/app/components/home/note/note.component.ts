import { Component, Inject, OnInit, signal } from '@angular/core';
import { Router } from '@angular/router';
import { ButtonModule } from 'primeng/button';
import { AuthService } from '@auth0/auth0-angular';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { NotesService } from '@services/notes.service';
import { Notes } from '@models/notes.model';
import {
  AngularRoutes,
  NotesPageConstants,
  SuccessMessages,
} from '@shared/notestracker.constants';
import { SpinnerComponent } from '@components/common/spinner/spinner.component';
import { UpdateNoteDTO } from '@models/dto/update-note-dto.class';
import { ToasterService } from '@core/services/toaster.service';

/**
 * The Notes Component.
 */
@Component({
  selector: 'app-note',
  standalone: true,
  imports: [CommonModule, FormsModule, SpinnerComponent, ButtonModule],
  templateUrl: './note.component.html',
  styleUrl: './note.component.scss',
})
class NoteComponent implements OnInit {
  /**
   * The Notes Page Constants.
   */
  public notesConstants = NotesPageConstants;

  /**
   * The current note.
   */
  public currentNote = signal<Notes>(
    new Notes(0, '', '', new Date(), new Date(), '')
  );

  /**
   * The is loading boolean flag.
   */
  public isLoading = signal(false);

  /**
   * Initializes a new instance of `NoteComponent`
   * @param notesService The notes service.
   * @param auth0Service The auth0 service.
   * @param routerService The router service.
   * @param data The data passed from parent component.
   */
  constructor(
    private notesService: NotesService,
    private auth0Service: AuthService,
    private routerService: Router,
    @Inject(MAT_DIALOG_DATA) public data: { noteId: number },
    private dialogRef: MatDialogRef<NoteComponent>,
    private toaster: ToasterService
  ) {}

  ngOnInit() {
    this.auth0Service.isAuthenticated$.subscribe((isAuth: boolean) => {
      if (!isAuth) {
        this.routerService.navigate([AngularRoutes.Error.Link]);
      } else {
        this.getNoteById(this.data.noteId);
      }
    });
  }

  /**
   * Gets the note by id.
   * @param noteId The note id.
   */
  public async getNoteById(noteId: number): Promise<void> {
    try {
      this.isLoading.set(true);
      const response = await this.notesService.getNoteByIdAsync(noteId);
      this.currentNote.set(response);
    } catch (error) {
      console.error(error);
    } finally {
      this.isLoading.set(false);
    }
  }

  /**
   * Handles the notes updation.
   * @param updateNote The updated note data
   */
  public async handleNoteUpdate(updateNote: UpdateNoteDTO): Promise<void> {
    try {
      this.isLoading.set(true);
      const response = await this.notesService.updateExistingNoteAsync(
        updateNote
      );
      this.currentNote.set(response);
      this.toaster.showSuccess(SuccessMessages.NoteUpdatedSuccess);
      this.dialogRef.close({ success: true });
    } catch (error) {
      console.error(error);
      this.isLoading.set(false);
    } finally {
      this.isLoading.set(false);
    }
  }

  /**
   * Closes the dialog.
   */
  public closeDialog(): void {
    this.dialogRef.close();
  }
}

export { NoteComponent };
