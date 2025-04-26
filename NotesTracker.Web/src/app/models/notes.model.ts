export class Notes {
  public noteId: number;
  public noteTitle: string;
  public noteDescription: string;
  public createdDate: Date;
  public lastModifiedDate: Date;

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
