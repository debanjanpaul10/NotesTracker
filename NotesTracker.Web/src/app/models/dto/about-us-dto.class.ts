/**
 * The about us model.
 */
export class AboutUs {
  /**
   * The id.
   */
  public id: string;

  /**
   * The heading.
   */
  public heading: string;

  /**
   * The description.
   */
  public description: string;

  /**
   * The image.
   */
  public image: string;

  /**
   * The link.
   */
  public link: string;

  /**
   * Initializes a new instance of `AboutUs`
   * @param Id The id.
   * @param Heading The heading.
   * @param Description The description.
   * @param Image The image.
   * @param Link The link.
   */
  constructor(
    Id: string,
    Heading: string,
    Description: string,
    Image: string,
    Link: string
  ) {
    this.id = Id;
    this.heading = Heading;
    this.description = Description;
    this.image = Image;
    this.link = Link;
  }
}
