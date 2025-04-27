/**
 * The update note DTO class.
 */
export class UpdateNoteDTO {
  /**
   * The note id.
   */
  public noteId: number;

  /**
   * The note title.
   */
  public noteTitle: string;

  /**
   * The note description.
   */
  public noteDescription: string;

  /**
   * Initializes a new instance of `UpdateNoteDTO`
   * @param NoteId The note id.
   * @param NoteTitle The note title.
   * @param NoteDescription The note description.
   */
  constructor(
    NoteId: number,
    NoteTitle: string,
    NoteDescription: string
  ) {
    this.noteId = NoteId;
    this.noteTitle = NoteTitle;
    this.noteDescription = NoteDescription;
  }
}
