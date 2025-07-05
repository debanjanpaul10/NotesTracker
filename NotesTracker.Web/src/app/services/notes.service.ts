import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, first, firstValueFrom, map } from 'rxjs';

import { ApiUrls, ExceptionMessages } from '@shared/notestracker.constants';
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
  private notesRoutes = ApiUrls.Notes;

  /**
   * Initializes a new instance of `NotesService`.
   * @param http The http client.
   */
  constructor(private http: HttpClient) {}

  /**
   * Gets all the notes data asynchronously.
   * @returns The api response
   */
  public async getAllNotesAsync(): Promise<Notes[]> {
    const getNotesUrl: string = `${this.apiUrl}${this.notesRoutes.GetAllNotes_ApiRoute}`;
    var response = await firstValueFrom(
      this.http.get<ResponseDTO>(getNotesUrl)
    );
    if (response?.isSuccess && response?.responseData !== null) {
      return response.responseData as Notes[];
    } else {
      throw new Error(response.responseData);
    }
  }

  /**
   * Gets the note data by id asynchronously.
   * @param noteId The note id.
   * @returns The api response.
   */
  public async getNoteByIdAsync(noteId: number): Promise<Notes> {
    const getNoteByIdUrl: string = `${this.apiUrl}${this.notesRoutes.GetNoteById_ApiRoute}?noteId=${noteId}`;
    const response = await firstValueFrom(
      this.http.get<ResponseDTO>(getNoteByIdUrl)
    );
    if (response?.isSuccess && response?.responseData !== null) {
      return response.responseData as Notes;
    } else {
      throw new Error(ExceptionMessages.NoteFetchFailedMessage);
    }
  }

  /**
   * Adds a new note asynchronously.
   * @param newNote The new note data.
   * @returns The api response.
   */
  public async addNewNoteAsync(newNote: NoteDTO): Promise<boolean> {
    const addNewNoteUrl: string = `${this.apiUrl}${this.notesRoutes.AddNewNote_ApiRoute}`;
    var response = await firstValueFrom(
      this.http.post<ResponseDTO>(addNewNoteUrl, newNote)
    );
    if (response?.isSuccess && response?.responseData !== null) {
      return response.responseData as boolean;
    } else {
      throw new Error(ExceptionMessages.AddingNoteFailedMessage);
    }
  }

  /**
   * Updates an existing note data asynchronously.
   * @param updatedNote The updated note data.
   * @returns The api response.
   */
  public async updateExistingNoteAsync(
    updatedNote: UpdateNoteDTO
  ): Promise<Notes> {
    const updateNoteUrl: string = `${this.apiUrl}${this.notesRoutes.UpdateNote_ApiRoute}`;
    const response = await firstValueFrom(
      this.http.post<ResponseDTO>(updateNoteUrl, updatedNote)
    );
    if (response?.isSuccess && response?.responseData) {
      return response?.responseData as Notes;
    } else {
      throw new Error(ExceptionMessages.NoteFetchFailedMessage);
    }
  }

  /**
   * Deletes an existing note by id asynchronously.
   * @param noteId The note id.
   * @returns The api response.
   */
  public async deleteNoteAsync(noteId: number): Promise<boolean> {
    const deleteNoteUrl: string = `${this.apiUrl}${this.notesRoutes.DeleteNote_ApiRoute}`;
    const response = await firstValueFrom(
      this.http.post<ResponseDTO>(deleteNoteUrl, noteId)
    );
    if (response?.isSuccess && response?.responseData !== null) {
      return response.responseData as boolean;
    } else {
      throw new Error(ExceptionMessages.NoteFetchFailedMessage);
    }
  }
}

export { NotesService };
