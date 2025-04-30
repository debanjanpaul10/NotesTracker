export class UserLoginDTO {
  public userEmail: string;
  public userPassword: string;

  constructor(
    UserEmail: string,
    UserPassword: string
  ) {
    this.userEmail = UserEmail;
    this.userPassword = UserPassword;
  }
}
