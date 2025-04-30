import { Injectable } from '@angular/core';
import { ApiBaseUrl, ApiUrls, ExceptionMessages } from '../helpers/Constants';
import { HttpClient } from '@angular/common/http';
import { map, Observable } from 'rxjs';
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
  private userAlias: string = '';

  /**
   * The api url.
   */
  private apiUrl: string = `${ApiBaseUrl}/${ApiUrls.Users.BaseRoute}`;

  /**
   * The users routes.
   */
  private usersRoutes: any = ApiUrls.Users;

  /**
   * Sets the user alias.
   * @param alias The passed user alias.
   */
  public setUserAlias(alias: string): void {
    this.userAlias = alias;
  }

  /**
   * Gets the user alias.
   * @returns The user alias.
   */
  public getUserAlias(): string {
    return this.userAlias;
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
