import { Injectable } from '@angular/core';
import { ApiBaseUrl, ApiUrls, ExceptionMessages } from '../helpers/Constants';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { Notes } from '../models/notes.model';
import { ResponseDTO } from '../models/response-dto.class';
import { NoteDTO } from '../models/note-dto.class';
import { UpdateNoteDTO } from '../models/update-note-dto.class';

@Injectable({
  providedIn: 'root',
})
export class NotesService {
  private apiUrl: string = `${ApiBaseUrl}/${ApiUrls.BaseRoute}`;

  constructor(private http: HttpClient) {}

  getAllNotes(): Observable<Notes[]> {
    const getNotesUrl: string = `${this.apiUrl}${ApiUrls.GetAllNotes_ApiRoute}`;
    return this.http.get<ResponseDTO>(getNotesUrl).pipe(
      map((response: ResponseDTO) => {
        if (response.isSuccess && response.responseData !== null) {
          return response.responseData as Notes[];
        }

        throw new Error(ExceptionMessages.AllNoteFetchFailedMessage);
      })
    );
  }

  getNoteById(noteId: number): Observable<Notes> {
    const getNoteByIdUrl: string = `${this.apiUrl}${ApiUrls.GetNoteById_ApiRoute}?noteId=${noteId}`;
    return this.http.get<ResponseDTO>(getNoteByIdUrl).pipe(
      map((response: ResponseDTO) => {
        if (response.isSuccess && response.responseData !== null) {
          return response.responseData as Notes;
        }

        throw new Error(ExceptionMessages.NoteFetchFailedMessage);
      })
    );
  }

  addNewNote(newNote: NoteDTO): Observable<boolean> {
    const addNewNoteUrl: string = `${this.apiUrl}${ApiUrls.AddNewNote_ApiRoute}`;
    return this.http.post<ResponseDTO>(addNewNoteUrl, newNote).pipe(
      map((response: ResponseDTO) => {
        if (response.isSuccess && response.responseData !== null) {
          return response.responseData as boolean;
        }

        throw new Error(ExceptionMessages.AddingNoteFailedMessage);
      })
    );
  }

  updateExistingNote(updatedNote: UpdateNoteDTO): Observable<Notes> {
    const updateNoteUrl: string = `${this.apiUrl}${ApiUrls.UpdateNote_ApiRoute}`;
    return this.http.post<ResponseDTO>(updateNoteUrl, updatedNote).pipe(
      map((response: ResponseDTO) => {
        if (response.isSuccess && response.responseData !== null) {
          return response.responseData as Notes;
        }

        throw new Error(ExceptionMessages.NoteFetchFailedMessage);
      })
    );
  }

  deleteNote(noteId: number): Observable<boolean> {
    const deleteNoteUrl: string = `${this.apiUrl}${ApiUrls.DeleteNote_ApiRoute}`;
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
