export class UserRegisterDTO {
  public userEmail: string;
  public userName: string;
  public userPassword: string;

  constructor(UserEmail: string, UserName: string, UserPassword: string) {
    this.userEmail = UserEmail;
    this.userName = UserName;
    this.userPassword = UserPassword;
  }
}
