import { Injectable, signal, WritableSignal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map, Observable } from 'rxjs';

import { ApiUrls, ExceptionMessages } from '../helpers/notestracker.constants';
import { ApiBaseUrl } from '../helpers/config.constants';
import { User } from '../models/user.model';
import { ResponseDTO } from '../models/dto/response-dto.class';
import { UserLoginDTO } from '../models/dto/user-login-dto.class';
import { UserRegisterDTO } from '../models/dto/user-register-dto.class';

/**
 * The Users Service class.
 */
@Injectable({
  providedIn: 'root',
})
class UsersService {
  /**
   * The user alias.
   */
  private userId: WritableSignal<string> = signal('');

  /**
   * The api url.
   */
  private apiUrl: string = `${ApiBaseUrl}/${ApiUrls.Users.BaseRoute}`;

  /**
   * The users routes.
   */
  private usersRoutes: any = ApiUrls.Users;

  /**
   * Sets the user id.
   * @param userId The passed user id.
   */
  public setUserId(id: string): void {
    this.userId.set(id);
  }

  /**
   * Gets the user id.
   * @returns The user id.
   */
  public getUserId(): string {
    return this.userId();
  }

  /**
   * Initializes a new instance of `UsersService`.
   * @param http The http client.
   */
  constructor(private http: HttpClient) {}

  /**
   * Gets the user data asynchronously.
   * @param userLoginData The user login data DTO.
   * @returns The api response.
   */
  public getUserAsync(userLoginData: UserLoginDTO): Observable<User> {
    const getUserUrl: string = `${this.apiUrl}${this.usersRoutes.GetUser_ApiRoute}`;
    return this.http.post<ResponseDTO>(getUserUrl, userLoginData).pipe(
      map((response: ResponseDTO) => {
        if (response.isSuccess && response.responseData !== null) {
          return response.responseData as User;
        } else {
          throw new Error(response.responseData);
        }
      })
    );
  }

  /**
   * Adds a new user data asynchronously.
   * @param newUser The new user data DTO.
   * @returns The api response.
   */
  public addNewUserAsync(newUser: UserRegisterDTO): Observable<boolean> {
    const addNewUserUrl: string = `${this.apiUrl}${this.usersRoutes.AddNewUser_ApiRoute}`;
    return this.http.post<ResponseDTO>(addNewUserUrl, newUser).pipe(
      map((response: ResponseDTO) => {
        if (response.isSuccess && response.responseData !== null) {
          return response.responseData as boolean;
        }

        throw new Error(ExceptionMessages.AddNewUserFailedMessage);
      })
    );
  }

  /**
   * Deletes an existing user data asynchronously.
   * @param userId The user id.
   * @returns The api response.
   */
  public deleteUserAsync(userId: number): Observable<boolean> {
    const deleteUserUrl: string = `${this.apiUrl}${this.usersRoutes.DeleteUser_ApiRoute}`;
    return this.http.post<ResponseDTO>(deleteUserUrl, userId).pipe(
      map((response: ResponseDTO) => {
        if (response.isSuccess && response.responseData !== null) {
          return response.responseData as boolean;
        }

        throw new Error(ExceptionMessages.UserFetchFailedMessage);
      })
    );
  }
}

export { UsersService };
