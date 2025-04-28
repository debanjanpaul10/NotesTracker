import { Component } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import {
  CacheKeys,
  ExceptionMessages,
  SuccessMessages,
  UserLoginModalConstants,
} from '../../helpers/Constants';
import { UserLoginDTO } from '../../models/user-login-dto.class';
import { UsersService } from '../../services/users.service';
import { User } from '../../models/user.model';
import { ToasterService } from '../../services/toaster.service';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth-service.service';

/**
 * The User Login Component
 */
@Component({
  selector: 'app-user-login',
  standalone: true,
  imports: [MatButtonModule, FormsModule],
  templateUrl: './user-login.component.html',
  styleUrl: './user-login.component.scss',
})
class UserLoginComponent {
  /**
   * The heading constants.
   */
  public HeadingConstants = UserLoginModalConstants.Headings;

  /**
   * The button constants.
   */
  public ButtonConstants = UserLoginModalConstants.Buttons;

  /**
   * The is loading boolean flag.
   */
  public isLoading: boolean = false;

  /**
   * The login user data.
   */
  public loginUserData: User = new User(0, '', '', '');

  /**
   * Initializes a new instance of `UserLoginComponent`
   * @param dialogRef The material dialog reference.
   * @param userService The users service.
   * @param toaster The toaster service.
   * @param router The router service.
   * @param authService The auth service.
   */
  constructor(
    public dialogRef: MatDialogRef<UserLoginComponent>,
    private userService: UsersService,
    private toaster: ToasterService,
    private router: Router,
    private authService: AuthService
  ) {}

  /**
   * Handle the dialog close event.
   */
  public closeDialog(): void {
    this.dialogRef.close();
  }

  /**
   * Logs in the current user.
   * @param loginUserData The user login data dto.
   */
  public loginUser(loginUserData: UserLoginDTO): void {
    this.isLoading = true;
    this.userService.getUserAsync(loginUserData).subscribe({
      next: (loggedInUser) => {
        this.handleUserLoginSuccess(loggedInUser);
      },
      error: (error) => {
        console.error(error);
        this.isLoading = false;
        this.toaster.showError(error);
      },
    });
  }

  /**
   * Handles the form submit event.
   * @param loginUserData The user login data dto.
   */
  public handleFormSubmit(loginUserData: UserLoginDTO): void {
    if (loginUserData.userEmail !== '' && loginUserData.userPassword !== '') {
      this.loginUser(loginUserData);
    } else {
      alert(ExceptionMessages.DefaultValidationFailedMessage);
    }
  }

  /**
   * Handle the success result of user login data.
   * @param loggedInUser The logged in user data.
   */
  private handleUserLoginSuccess(loggedInUser: User): void {
    this.loginUserData = loggedInUser;
    this.isLoading = false;

    localStorage.setItem(
      CacheKeys.LoggedInUser,
      JSON.stringify(this.loginUserData)
    );

    this.authService.setLoggedInState(true);

    this.router.navigate(['/']).then(() => {
      this.toaster.showSuccess(SuccessMessages.UserLoginSuccess);
    });

    this.closeDialog();
  }
}

export { UserLoginComponent };
