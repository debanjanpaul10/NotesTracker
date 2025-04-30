import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { NoteDTO } from '../../models/dto/note-dto.class';
import { NotesService } from '../../services/notes.service';
import { AddNotePageConstants } from '../../helpers/Constants';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { Router, RouterLink } from '@angular/router';
import { ToasterService } from '../../services/toaster.service';
import { UsersService } from '../../services/users.service';
import { AuthService } from '@auth0/auth0-angular';

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
class AddNoteComponent implements OnInit {
  /**
   * The new note dto.
   */
  public newNote: NoteDTO = new NoteDTO('', '', '');

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
   * @param router The router.
   * @param toaster The toaster.
   * @param userService The users service.
   */
  constructor(
    private notesService: NotesService,
    private router: Router,
    private toaster: ToasterService,
    private userService: UsersService,
    private auth0: AuthService
  ) {}

  ngOnInit(): void {
    this.auth0.isAuthenticated$.subscribe((isAuth: boolean) => {
      if (!isAuth) {
        this.router.navigate(['/error']);
      }
    });
  }

  /**
   * Handles the form submit event.
   * @param newNote The new note dto.
   */
  public handleFormSubmit(newNote: NoteDTO): void {
    if (newNote.noteTitle !== '' && newNote.noteDescription !== '') {
      newNote.userName = this.userService.getUserAlias();
      this.addNewNote(newNote);
    } else {
      alert('Some Fields are missing!');
    }
  }

  /**
   * Adds a new note asynchronously.
   * @param newNote The new note.
   */
  private addNewNote(newNote: NoteDTO): void {
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
}

export { AddNoteComponent };
