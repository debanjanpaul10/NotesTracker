export class BugReportDTO {
  public bugTitle: string;

  public bugDescription: string;

  public bugSeverity: string;

  public pageUrl: string;

  constructor(
    BugTitle: string,
    BugDescription: string,
    BugSeverity: string,
    PageUrl: string
  ) {
    this.bugTitle = BugTitle;
    this.bugDescription = BugDescription;
    this.bugSeverity = BugSeverity;
    this.pageUrl = PageUrl;
  }
}
