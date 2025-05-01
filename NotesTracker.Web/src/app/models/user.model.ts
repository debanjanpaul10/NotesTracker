/**
 * The User model.
 */
export class User {
  /**
   * The user id.
   */
  public userId: number;

  /**
   * The user email address.
   */
  public userEmail: string;

  /**
   * The user name.
   */
  public userName: string;

  /**
   * The user password.
   */
  public userPassword: string;

  /**
   * Initializes a new instance of `User`
   * @param UserId The user id.
   * @param UserEmail The user email.
   * @param UserName The user name.
   * @param UserPassword The user password.
   */
  constructor(
    UserId: number,
    UserEmail: string,
    UserName: string,
    UserPassword: string,
  ) {
    this.userEmail = UserEmail;
    this.userId = UserId;
    this.userName = UserName;
    this.userPassword = UserPassword;
  }
}
