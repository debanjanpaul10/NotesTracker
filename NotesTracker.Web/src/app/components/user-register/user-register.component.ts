import { Component } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { UsersService } from '../../services/users.service';
import { ToasterService } from '../../services/toaster.service';
import { UserRegisterDTO } from '../../models/user-register-dto.class';
import { UserRegisterModalConstants } from '../../helpers/Constants';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { Router } from '@angular/router';

@Component({
  selector: 'app-user-register',
  standalone: true,
  imports: [CommonModule, FormsModule, MatButtonModule],
  templateUrl: './user-register.component.html',
  styleUrl: './user-register.component.scss',
})
class UserRegisterComponent {
  public newUser: UserRegisterDTO = new UserRegisterDTO('', '', '');
  public isLoading: boolean = false;
  public isUserRegistered: boolean = false;
  public HeadingConstants = UserRegisterModalConstants.Headings;
  public ButtonConstants = UserRegisterModalConstants.Buttons;

  constructor(
    public dialogRef: MatDialogRef<UserRegisterComponent>,
    private userService: UsersService,
    private toaster: ToasterService,
    private router: Router
  ) {}

  public closeDialog(): void {
    this.dialogRef.close();
  }

  public registerUser(newUser: UserRegisterDTO): void {
    this.isLoading = true;
    this.userService.addNewUserAsync(newUser).subscribe({
      next: (newUserResponse) => {
        this.isUserRegistered = newUserResponse;
        this.isLoading = false;
        if (this.isUserRegistered) {
          this.router.navigate(['/']);
        }
      },
      error: (error) => {
        console.error(error);
        this.isLoading = false;
        this.toaster.showError(error);
      },
    });
  }

  public handleFormSubmit(newUser: UserRegisterDTO): void {
    if (
      newUser.userEmail !== '' &&
      newUser.userName !== '' &&
      newUser.userPassword !== ''
    ) {
      this.registerUser(newUser);
    } else {
      alert('Some fields are missing!');
    }
  }
}

export { UserRegisterComponent };
