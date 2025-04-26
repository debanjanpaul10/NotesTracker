import { Component, OnInit } from '@angular/core';
import { Notes } from '../../models/notes.model';
import { NotesService } from '../../services/notes.service';
import { CommonModule } from '@angular/common';
import { Router, RouterLink } from '@angular/router';

@Component({
  selector: 'app-notescontainer',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './notescontainer.component.html',
  styleUrl: './notescontainer.component.scss',
})
export class NotesContainerComponent implements OnInit {
  notesList: Notes[] = [];
  loading: boolean = false;
  isDeleteOperationSuccess: boolean = false;

  constructor(private notesService: NotesService, private router: Router) {}

  ngOnInit(): void {
    this.getAllNotesAsync();
  }

  getAllNotesAsync(): void {
    this.loading = true;
    this.notesService.getAllNotes().subscribe({
      next: (notes) => {
        this.notesList = notes;
        this.loading = false;
      },
      error: (err) => {
        console.error(err);
        this.loading = false;
      },
    });
  }

  deleteNoteById(noteId: number): void {
    this.loading = true;
    this.notesService.deleteNote(noteId).subscribe({
      next: (response) => {
        this.isDeleteOperationSuccess = response;
        if (this.isDeleteOperationSuccess) {
          this.getAllNotesAsync();
        }
        this.loading = false;
      },
      error: (err) => {
        console.error(err);
        this.loading = false;
      },
    });
  }

  navigateToEditNote(noteId: number): void {
    this.router.navigate(['/notes', noteId]);
  }
}
