import { CommonModule } from '@angular/common';
import { Component, OnInit, signal, WritableSignal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { Router, RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatChipsModule } from '@angular/material/chips';
import { MsalService } from '@azure/msal-angular';

import { NoteDTO } from '../../models/dto/note-dto.class';
import { NotesService } from '../../services/notes.service';
import {
  AddNotePageConstants,
  AngularRoutes,
} from '../../helpers/notestracker.constants';
import { ToasterService } from '../../services/toaster.service';
import { UsersService } from '../../services/users.service';

/**
 * The Add Note Component.
 */
@Component({
  selector: 'app-addnote',
  standalone: true,
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
   * The new note dto.
   */
  public newNote: NoteDTO = new NoteDTO('', '', '');

  /**
   * The is loading boolean flag.
   */
  public loading: WritableSignal<boolean> = signal(false);

  /**
   * The is note saved boolean flag.
   */
  public isNoteSaved: WritableSignal<boolean> = signal(false);

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
   * @param msalService The MSAL service.
   */
  constructor(
    private notesService: NotesService,
    private router: Router,
    private toaster: ToasterService,
    private userService: UsersService,
    private msalService: MsalService
  ) {}

  ngOnInit(): void {
    if (this.msalService.instance.getActiveAccount() === null) {
      this.router.navigate([AngularRoutes.Error.Link]);
    }
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
    this.loading.set(true);
    this.notesService.addNewNoteAsync(newNote).subscribe({
      next: (noteSaveStatus) => {
        this.isNoteSaved.set(noteSaveStatus);
        this.loading.set(false);
        if (this.isNoteSaved()) {
          this.router.navigate([AngularRoutes.Home.Link]);
        }
      },
      error: (error) => {
        console.error(error);
        this.loading.set(false);
        this.toaster.showError(error);
      },
    });
  }
}

export { AddNoteComponent };
