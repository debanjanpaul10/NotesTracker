import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { NoteDTO } from '../../models/note-dto.class';
import { NotesService } from '../../services/notes.service';
import { AddNotePageConstants } from '../../helpers/Constants';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-addnote',
  standalone: true,
  imports: [CommonModule, FormsModule, MatButtonModule, RouterLink],
  templateUrl: './addnote.component.html',
  styleUrl: './addnote.component.scss',
})
export class AddNoteComponent {
  newNote: NoteDTO = new NoteDTO('', '');
  loading: boolean = false;
  isNoteSaved: boolean = false;
  addNotesConstants: any = AddNotePageConstants.Headings;

  constructor(private notesService: NotesService) {}

  addNewNoteAsync(newNote: NoteDTO): void {
    this.loading = true;
    this.notesService.addNewNote(newNote).subscribe({
      next: (noteSaveStatus) => {
        this.isNoteSaved = noteSaveStatus;
        this.loading = false;
      },
      error: (error) => {
        console.error(error);
        this.loading = false;
      },
    });
  }

  handleFormChange(event: Event, field: keyof NoteDTO): void {
    const inputElement = event.target as HTMLInputElement;
    this.newNote[field] = inputElement.value;
  }

  handleFormSubmit(newNote: NoteDTO): void {
    if (newNote.NoteTitle !== '' && newNote.NoteDescription !== '') {
      this.addNewNoteAsync(newNote);
    } else {
      alert('Some Fields are missing!');
    }
  }
}
