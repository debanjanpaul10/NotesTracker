import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { NoteDTO } from '../../models/note-dto.class';
import { NotesService } from '../../services/notes.service';
import { AddNotePageConstants } from '../../helpers/Constants';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { Router, RouterLink } from '@angular/router';
import { ToasterService } from '../../services/toaster.service';

/**
 * The Add Note Component.
 */
@Component({
  selector: 'app-addnote',
  standalone: true,
  imports: [CommonModule, FormsModule, MatButtonModule, RouterLink],
  templateUrl: './addnote.component.html',
  styleUrl: './addnote.component.scss',
})
class AddNoteComponent {
  /**
   * The new note dto.
   */
  public newNote: NoteDTO = new NoteDTO('', '');

  /**
   * The is loading boolean flag.
   */
  public loading: boolean = false;

  /**
   * The is note saved boolean flag.
   */
  public isNoteSaved: boolean = false;

  /**
   * The add notes constants.
   */
  public AddNotesConstants = AddNotePageConstants.Headings;

  /**
   * Initializes a new instance of `AddNoteComponent`
   * @param notesService The notes service.
   */
  constructor(
    private notesService: NotesService,
    private router: Router,
    private toaster: ToasterService
  ) {}

  /**
   * Adds a new note asynchronously.
   * @param newNote The new note.
   */
  public addNewNote(newNote: NoteDTO): void {
    this.loading = true;
    this.notesService.addNewNoteAsync(newNote).subscribe({
      next: (noteSaveStatus) => {
        this.isNoteSaved = noteSaveStatus;
        this.loading = false;
        if (this.isNoteSaved) {
          this.router.navigate(['/']);
        }
      },
      error: (error) => {
        console.error(error);
        this.loading = false;
        this.toaster.showError(error);
      },
    });
  }

  /**
   * Handles the form submit event.
   * @param newNote The new note dto.
   */
  public handleFormSubmit(newNote: NoteDTO): void {
    if (newNote.noteTitle !== '' && newNote.noteDescription !== '') {
      this.addNewNote(newNote);
    } else {
      alert('Some Fields are missing!');
    }
  }
}

export { AddNoteComponent };
