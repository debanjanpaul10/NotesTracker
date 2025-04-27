import { Component, OnInit } from '@angular/core';
import { Notes } from '../../models/notes.model';
import { NotesService } from '../../services/notes.service';
import { CommonModule } from '@angular/common';
import { Router, RouterLink } from '@angular/router';
import { NotesContainerConstants } from '../../helpers/Constants';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { LoaderComponent } from '../common/loader/loader.component';
import { ToasterService } from '../../services/toaster.service';

/**
 * The Notes Container component.
 */
@Component({
  selector: 'app-notescontainer',
  standalone: true,
  imports: [
    CommonModule,
    RouterLink,
    MatButtonModule,
    MatIconModule,
    LoaderComponent,
  ],
  templateUrl: './notescontainer.component.html',
  styleUrl: './notescontainer.component.scss',
})
class NotesContainerComponent implements OnInit {
  /**
   * The notes list.
   */
  public notesList: Notes[] = [];

  /**
   * The is loading boolean flag.
   */
  public loading: boolean = false;

  /**
   * The is delete operation success boolean flag.
   */
  public isDeleteOperationSuccess: boolean = false;

  /**
   * The notes container constants object.
   */
  public notesContainerConstants: any = NotesContainerConstants;

  /**
   * Initializes a new instance of `NotesContainerComponent`
   * @param notesService The notes service.
   * @param router The router.
   * @param toaster The toaster service.
   */
  constructor(
    private notesService: NotesService,
    private router: Router,
    private toaster: ToasterService
  ) {}

  ngOnInit(): void {
    this.getAllNotes();
  }

  /**
   * Gets all the notes.
   */
  public getAllNotes(): void {
    this.loading = true;
    this.notesService.getAllNotesAsync().subscribe({
      next: (notes) => {
        this.notesList = notes;
        this.loading = false;
      },
      error: (err) => {
        console.error(err);
        this.loading = false;
        this.toaster.showError(err)
      },
    });
  }

  /**
   * Deletes a note by note id.
   * @param noteId The note id.
   */
  public deleteNoteById(noteId: number): void {
    this.loading = true;
    this.notesService.deleteNoteAsync(noteId).subscribe({
      next: (response) => {
        this.isDeleteOperationSuccess = response;
        if (this.isDeleteOperationSuccess) {
          this.getAllNotes();
        }
        this.loading = false;
      },
      error: (err) => {
        console.error(err);
        this.loading = false;
        this.toaster.showError(err);
      },
    });
  }

  /**
   * Handles the navigation to edit note page.
   * @param noteId The note id.
   */
  public navigateToEditNote(noteId: number): void {
    this.router.navigate([NotesContainerConstants.RouteLinks.Notes, noteId]);
  }
}

export { NotesContainerComponent };
