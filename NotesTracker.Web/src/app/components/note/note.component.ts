import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { NotesService } from '../../services/notes.service';
import { Notes } from '../../models/notes.model';
import { NotesPageConstants } from '../../helpers/notestracker.constants';

/**
 * The Notes Component.
 */
@Component({
  selector: 'app-note',
  standalone: true,
  imports: [],
  templateUrl: './note.component.html',
  styleUrl: './note.component.scss',
})
class NoteComponent implements OnInit {
  /**
   * The current note.
   */
  public currentNote: Notes | null = null;

  /**
   * The is loading boolean flag.
   */
  public isLoading: boolean = false;

  /**
   * Initializes a new instance of `NoteComponent`
   * @param route The activated route.
   * @param notesService The notes service.
   */
  constructor(
    private route: ActivatedRoute,
    private notesService: NotesService
  ) {}

  ngOnInit(): void {
    const noteId = Number(
      this.route.snapshot.paramMap.get(NotesPageConstants.NoteId)
    );
    this.getNoteById(noteId);
  }

  /**
   * Gets the note by id.
   * @param noteId The note id.
   */
  public getNoteById(noteId: number): void {
    this.isLoading = true;
    this.notesService.getNoteByIdAsync(noteId).subscribe({
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

export { NoteComponent };
