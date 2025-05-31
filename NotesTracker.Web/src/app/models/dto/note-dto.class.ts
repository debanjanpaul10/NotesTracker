/**
 * The Note DTO.
 */
export class NoteDTO
{
  /**
   * The note title.
   */
  public noteTitle: string;

  /**
   * The note description.
   */
  public noteDescription: string;

  /**
   * The user name.
   */
  public userName: string;

  /**
   * Initializes a new instance of `NoteDTO`
   * @param NoteTitle The note title.
   * @param NoteDescription The note description.
   * @param UserName The user name.
   */
  constructor( NoteTitle: string, NoteDescription: string, UserName: string )
  {
    this.noteTitle = NoteTitle;
    this.noteDescription = NoteDescription;
    this.userName = UserName;
  }
}
