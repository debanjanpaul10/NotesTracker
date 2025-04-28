/**
 * The notes model.
 */
export class Notes {
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
   * The note created date.
   */
  public createdDate: Date;

  /**
   * The note last modified date.
   */
  public lastModifiedDate: Date;

  /**
   * Initializes a new instance of `Notes`
   * @param NoteId The note id.
   * @param NoteTitle The note title.
   * @param NoteDescription The note description.
   * @param CreatedDate The note created date.
   * @param LastModifiedDate The note last modified date.
   */
  constructor(
    NoteId: number,
    NoteTitle: string,
    NoteDescription: string,
    CreatedDate: Date,
    LastModifiedDate: Date
  ) {
    this.noteId = NoteId;
    this.noteTitle = NoteTitle;
    this.noteDescription = NoteDescription;
    this.createdDate = CreatedDate;
    this.lastModifiedDate = LastModifiedDate;
  }
}
