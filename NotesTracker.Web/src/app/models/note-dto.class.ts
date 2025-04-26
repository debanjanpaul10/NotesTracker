export class NoteDTO {
  public NoteTitle: string;
  public NoteDescription: string;

  constructor(
    noteTitle: string,
    noteDescription: string
  ) {
    this.NoteTitle = noteTitle;
    this.NoteDescription = noteDescription;
  }
}
