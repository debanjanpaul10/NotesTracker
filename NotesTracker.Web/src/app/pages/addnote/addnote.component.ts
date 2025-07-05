import { CommonModule } from '@angular/common';
import { Component, OnInit, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '@auth0/auth0-angular';
import { MatCardModule } from '@angular/material/card';
import { MatChipsModule } from '@angular/material/chips';

import { NoteDTO } from '@models/dto/note-dto.class';
import { NotesService } from '@services/notes.service';
import {
  AddNotePageConstants,
  AngularRoutes,
} from '@shared/notestracker.constants';
import { ToasterService } from '@core/services/toaster.service';
import { UsersService } from '@core/services/users.service';

/**
 * The Add Note Component.
 */
@Component({
  selector: 'app-addnote',
  imports: [
    CommonModule,
    FormsModule,
    MatButtonModule,
    RouterLink,
    MatCardModule,
    MatChipsModule,
  ],
  templateUrl: './addnote.component.html',
  styleUrl: './addnote.component.scss',
})
class AddNoteComponent implements OnInit {
  /**
   * The add notes constants.
   */
  public AddNotesConstants = AddNotePageConstants.Headings;

  /**
   * The new note dto.
   */
  public newNote = signal(new NoteDTO('', '', ''));

  /**
   * The is loading boolean flag.
   */
  public loading = signal(false);

  /**
   * The is note saved boolean flag.
   */
  public isNoteSaved = signal(false);

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
        this.router.navigate([AngularRoutes.Error.Link]);
      }
    });
  }

  /**
   * Handles the form submit event.
   * @param newNote The new note dto.
   */
  public handleFormSubmit(newNote: NoteDTO): void {
    if (newNote.noteTitle !== '' && newNote.noteDescription !== '') {
      newNote.userName = this.userService.userName;
      this.addNewNote(newNote);
    } else {
      alert('Some Fields are missing!');
    }
  }

  /**
   * Adds a new note asynchronously.
   * @param newNote The new note.
   */
  private async addNewNote(newNote: NoteDTO): Promise<void> {
    try {
      this.loading.set(true);
      const response = await this.notesService.addNewNoteAsync(newNote);
      this.isNoteSaved.set(response);
      if (response) {
        this.router.navigate([AngularRoutes.Home.Link]);
      }
    } catch (error: any) {
      console.error(error);
      this.toaster.showError(error);
    } finally {
      this.loading.set(false);
    }
  }
}

export { AddNoteComponent };
