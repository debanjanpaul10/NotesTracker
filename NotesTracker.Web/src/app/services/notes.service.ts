import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';

import { ApiUrls, ExceptionMessages } from '@helpers/notestracker.constants';
import { Notes } from '@models/notes.model';
import { ResponseDTO } from '@models/dto/response-dto.class';
import { NoteDTO } from '@models/dto/note-dto.class';
import { UpdateNoteDTO } from '@models/dto/update-note-dto.class';
import { environment } from '@environments/environment';

/**
 * The Notes Service class.
 */
@Injectable({
  providedIn: 'root',
})
class NotesService {
  /**
   * The api url.
   */
  private apiUrl: string = `${environment.apiBaseUrl}/${ApiUrls.Notes.BaseRoute}`;

  /**
   * The notes routes.
   */
  private notesRoutes: any = ApiUrls.Notes;

  /**
   * Initializes a new instance of `NotesService`.
   * @param http The http client.
   */
  constructor(private http: HttpClient) {}

  /**
   * Gets all the notes data asynchronously.
   * @returns The api response
   */
  public getAllNotesAsync(): Observable<Notes[]> {
    const getNotesUrl: string = `${this.apiUrl}${this.notesRoutes.GetAllNotes_ApiRoute}`;
    return this.http.get<ResponseDTO>(getNotesUrl).pipe(
      map((response: ResponseDTO) => {
        if (response.isSuccess && response.responseData !== null) {
          return response.responseData as Notes[];
        } else {
          throw new Error(response.responseData);
        }
      })
    );
  }

  /**
   * Gets the note data by id asynchronously.
   * @param noteId The note id.
   * @returns The api response.
   */
  public getNoteByIdAsync(noteId: number): Observable<Notes> {
    const getNoteByIdUrl: string = `${this.apiUrl}${this.notesRoutes.GetNoteById_ApiRoute}?noteId=${noteId}`;
    return this.http.get<ResponseDTO>(getNoteByIdUrl).pipe(
      map((response: ResponseDTO) => {
        if (response.isSuccess && response.responseData !== null) {
          return response.responseData as Notes;
        }

        throw new Error(ExceptionMessages.NoteFetchFailedMessage);
      })
    );
  }

  /**
   * Adds a new note asynchronously.
   * @param newNote The new note data.
   * @returns The api response.
   */
  public addNewNoteAsync(newNote: NoteDTO): Observable<boolean> {
    const addNewNoteUrl: string = `${this.apiUrl}${this.notesRoutes.AddNewNote_ApiRoute}`;
    return this.http.post<ResponseDTO>(addNewNoteUrl, newNote).pipe(
      map((response: ResponseDTO) => {
        if (response.isSuccess && response.responseData !== null) {
          return response.responseData as boolean;
        }

        throw new Error(ExceptionMessages.AddingNoteFailedMessage);
      })
    );
  }

  /**
   * Updates an existing note data asynchronously.
   * @param updatedNote The updated note data.
   * @returns The api response.
   */
  public updateExistingNoteAsync(
    updatedNote: UpdateNoteDTO
  ): Observable<Notes> {
    const updateNoteUrl: string = `${this.apiUrl}${this.notesRoutes.UpdateNote_ApiRoute}`;
    return this.http.post<ResponseDTO>(updateNoteUrl, updatedNote).pipe(
      map((response: ResponseDTO) => {
        if (response.isSuccess && response.responseData !== null) {
          return response.responseData as Notes;
        }

        throw new Error(ExceptionMessages.NoteFetchFailedMessage);
      })
    );
  }

  /**
   * Deletes an existing note by id asynchronously.
   * @param noteId The note id.
   * @returns The api response.
   */
  public deleteNoteAsync(noteId: number): Observable<boolean> {
    const deleteNoteUrl: string = `${this.apiUrl}${this.notesRoutes.DeleteNote_ApiRoute}`;
    return this.http.post<ResponseDTO>(deleteNoteUrl, noteId).pipe(
      map((response: ResponseDTO) => {
        if (response.isSuccess && response.responseData !== null) {
          return response.responseData as boolean;
        }

        throw new Error(ExceptionMessages.NoteFetchFailedMessage);
      })
    );
  }
}

export { NotesService };
