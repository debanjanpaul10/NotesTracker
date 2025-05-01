import {
  Component,
  Inject,
  OnInit,
  signal,
  WritableSignal,
} from '@angular/core';
import { Router } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { AuthService } from '@auth0/auth0-angular';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';


import { NotesService } from '../../services/notes.service';
import { Notes } from '../../models/notes.model';
import {
  AngularRoutes,
  NotesPageConstants,
  SuccessMessages,
} from '../../helpers/notestracker.constants';

import { SpinnerComponent } from '../common/spinner/spinner.component';
import { UpdateNoteDTO } from '../../models/dto/update-note-dto.class';
import { ToasterService } from '../../services/toaster.service';

/**
 * The Notes Component.
 */
@Component({
  selector: 'app-note',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    SpinnerComponent,
    MatButtonModule,
  ],
  templateUrl: './note.component.html',
  styleUrl: './note.component.scss',
})
class NoteComponent implements OnInit {
  /**
   * The current note.
   */
  public currentNote: WritableSignal<Notes> = signal(
    new Notes(0, '', '', new Date(), new Date(), '')
  );

  /**
   * The is loading boolean flag.
   */
  public isLoading: WritableSignal<boolean> = signal(false);

  /**
   * The Notes Page Constants.
   */
  public notesConstants = NotesPageConstants;

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

  ngOnInit(): void {
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
  public getNoteById(noteId: number): void {
    this.isLoading.set(true);
    this.notesService.getNoteByIdAsync(noteId).subscribe({
      next: (response) => {
        this.currentNote.set(response);
        this.isLoading.set(false);
      },
      error: (err) => {
        console.error(err);
        this.isLoading.set(false);
      },
    });
  }

  /**
   * Handles the notes updation.
   * @param updateNote The updated note data
   */
  public handleNoteUpdate(updateNote: UpdateNoteDTO): void {
    this.isLoading.set(true);
    this.notesService.updateExistingNoteAsync(updateNote).subscribe({
      next: (response) => {
        this.currentNote.set(response);
        this.isLoading.set(false);
        this.toaster.showSuccess(SuccessMessages.NoteUpdatedSuccess);

        this.dialogRef.close({ success: true });
      },
      error: (err) => {
        console.error(err);
        this.isLoading.set(false);
      },
    });
  }

  /**
   * Closes the dialog.
   */
  public closeDialog(): void {
    this.dialogRef.close();
  }
}

export { NoteComponent };
