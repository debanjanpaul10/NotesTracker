/**
 * The Note DTO.
 */
export class NoteDTO {
  /**
   * The note title.
   */
  public noteTitle: string;

  /**
   * The note description.
   */
  public noteDescription: string;

  /**
   * The user id.
   */
  public userId: string;

  /**
   * Initializes a new instance of `NoteDTO`
   * @param NoteTitle The note title.
   * @param NoteDescription The note description.
   * @param UserId The user id.
   */
  constructor(NoteTitle: string, NoteDescription: string, UserId: string) {
    this.noteTitle = NoteTitle;
    this.noteDescription = NoteDescription;
    this.userId = UserId;
  }
}
