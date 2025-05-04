/**
 * The User model.
 */
export class User {
  /**
   * The user email address.
   */
  public userEmail: string;

  /**
   * The user name.
   */
  public userName: string;

  /**
   * The user id.
   */
  public userId: string;

  /**
   * The provider.
   */
  public provider: string;

  /**
   * The is social flag.
   */
  public isSocial: boolean;

  /**
   * Initializes a new instance of `User`
   * @param UserId The user id.
   * @param UserEmail The user email.
   * @param UserName The user name.
   * @param Provider The provider.
   * @param IsSocial The is social flag.
   */
  constructor(
    UserId: string,
    UserEmail: string,
    UserName: string,
    Provider: string,
    IsSocial: boolean,
  ) {
    this.userEmail = UserEmail;
    this.userId = UserId;
    this.userName = UserName;
    this.provider = Provider;
    this.isSocial = IsSocial;
  }
}
