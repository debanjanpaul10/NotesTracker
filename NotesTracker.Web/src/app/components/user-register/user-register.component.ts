import { Component } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { UsersService } from '../../services/users.service';
import { ToasterService } from '../../services/toaster.service';
import { UserRegisterDTO } from '../../models/user-register-dto.class';
import {
  ExceptionMessages,
  SuccessMessages,
  UserRegisterModalConstants,
} from '../../helpers/Constants';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { Router } from '@angular/router';

/**
 * The User Register Component
 */
@Component({
  selector: 'app-user-register',
  standalone: true,
  imports: [CommonModule, FormsModule, MatButtonModule],
  templateUrl: './user-register.component.html',
  styleUrl: './user-register.component.scss',
})
class UserRegisterComponent {
  /**
   * The new user data dto.
   */
  public newUser: UserRegisterDTO = new UserRegisterDTO('', '', '');

  /**
   * The is loading boolean flag.
   */
  public isLoading: boolean = false;

  /**
   * The is user registered boolean flag.
   */
  public isUserRegistered: boolean = false;

  /**
   * The heading constants object.
   */
  public HeadingConstants = UserRegisterModalConstants.Headings;

  /**
   * The button constants object.
   */
  public ButtonConstants = UserRegisterModalConstants.Buttons;

  /**
   * Initializes a new instance of `UserRegisterComponent`
   * @param dialogRef The material dialog reference.
   * @param userService The users service.
   * @param toaster The toaster service.
   */
  constructor(
    public dialogRef: MatDialogRef<UserRegisterComponent>,
    private userService: UsersService,
    private toaster: ToasterService
  ) {}

  /**
   * Handles the dialog close event.
   */
  public closeDialog(): void {
    this.dialogRef.close();
  }

  /**
   * Registers a new user.
   * @param newUser The new user data dto.
   */
  public registerUser(newUser: UserRegisterDTO): void {
    this.isLoading = true;
    this.userService.addNewUserAsync(newUser).subscribe({
      next: (newUserResponse) => {
        this.isUserRegistered = newUserResponse;
        this.isLoading = false;
        if (this.isUserRegistered) {
          this.closeDialog();
          this.toaster.showSuccess(SuccessMessages.UserRegisteredSuccess);
        }
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
   * @param newUser The new user data dto.
   */
  public handleFormSubmit(newUser: UserRegisterDTO): void {
    if (
      newUser.userEmail !== '' &&
      newUser.userName !== '' &&
      newUser.userPassword !== ''
    ) {
      this.registerUser(newUser);
    } else {
      alert(ExceptionMessages.DefaultValidationFailedMessage);
    }
  }
}

export { UserRegisterComponent };
