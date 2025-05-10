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
   * The user user name.
   */
  public userName: string;

  /**
   * Initializes a new instance of `UpdateNoteDTO`
   * @param NoteId The note id.
   * @param NoteTitle The note title.
   * @param NoteDescription The note description.
   * @param UserName The user id.
   */
  constructor(
    NoteId: number,
    NoteTitle: string,
    NoteDescription: string,
    UserName: string,
  ) {
    this.noteId = NoteId;
    this.noteTitle = NoteTitle;
    this.noteDescription = NoteDescription;
    this.userName = UserName;
  }
}
