import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NotesService } from '../../services/notes.service';
import { Notes } from '../../models/notes.model';

@Component({
  selector: 'app-note',
  standalone: true,
  imports: [],
  templateUrl: './note.component.html',
  styleUrl: './note.component.scss',
})
export class NoteComponent implements OnInit {
  currentNote: Notes | null = null;
  isLoading: boolean = false;

  constructor(
    private route: ActivatedRoute,
    private notesService: NotesService
  ) {}

  ngOnInit(): void {
    const noteId = Number(this.route.snapshot.paramMap.get('noteId'));
    this.getNoteById(noteId);
  }

  getNoteById(noteId: number): void {
    this.isLoading = true;
    this.notesService.getNoteById(noteId).subscribe({
      next: (response) => {
        this.currentNote = response;
        this.isLoading = false;
      },
      error: (err) => {
        console.error(err);
        this.isLoading = false;
      },
    });
  }
}
