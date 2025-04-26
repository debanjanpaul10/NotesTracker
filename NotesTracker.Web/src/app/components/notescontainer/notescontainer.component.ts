import { Component, OnInit } from '@angular/core';
import { Notes } from '../../models/notes.model';
import { NotesService } from '../../services/notes.service';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';

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

  constructor(private notesService: NotesService) {}

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
}
