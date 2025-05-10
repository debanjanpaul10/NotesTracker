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
   * The user userId.
   */
  public userId: string;

  /**
   * Initializes a new instance of `UpdateNoteDTO`
   * @param NoteId The note id.
   * @param NoteTitle The note title.
   * @param NoteDescription The note description.
   * @param UserId The user id.
   */
  constructor(
    NoteId: number,
    NoteTitle: string,
    NoteDescription: string,
    UserId: string,
  ) {
    this.noteId = NoteId;
    this.noteTitle = NoteTitle;
    this.noteDescription = NoteDescription;
    this.userId = UserId;
  }
}
