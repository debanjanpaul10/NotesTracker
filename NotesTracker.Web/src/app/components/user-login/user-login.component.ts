import { Component } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';

@Component({
  selector: 'app-user-login',
  standalone: true,
  imports: [MatFormFieldModule, MatButtonModule],
  templateUrl: './user-login.component.html',
  styleUrl: './user-login.component.scss',
})
class UserLoginComponent {
  constructor(public dialogRef: MatDialogRef<UserLoginComponent>) {}

  public closeDialog(): void {
    this.dialogRef.close();
  }
}

export { UserLoginComponent };
