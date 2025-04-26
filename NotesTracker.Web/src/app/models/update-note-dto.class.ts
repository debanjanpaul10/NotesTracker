export class UpdateNoteDTO {
  public NoteId: number;
  public NoteTitle: string;
  public NoteDescription: string;

  constructor(
    noteId: number,
    noteTitle: string,
    noteDescription: string
  ) {
    this.NoteId = noteId;
    this.NoteTitle = noteTitle;
    this.NoteDescription = noteDescription;
  }
}
