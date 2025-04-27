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
   * Initializes a new instance of `NoteDTO`
   * @param NoteTitle The note title.
   * @param NoteDescription The note description.
   */
  constructor(NoteTitle: string, NoteDescription: string) {
    this.noteTitle = NoteTitle;
    this.noteDescription = NoteDescription;
  }
}
