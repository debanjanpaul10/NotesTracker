/**
 * The Response DTO
 */
export class ResponseDTO {
  /**
   * The status code.
   */
  public statusCode: number;

  /**
   * The is success flag.
   */
  public isSuccess: boolean;

  /**
   * The response data.
   */
  public responseData: any;

  /**
   * Initializes a new instance of the `ResponseDTO`
   * @param StatusCode The Status Code.
   * @param IsSuccess The is Success flag.
   * @param ResponseData The Response Data.
   */
  constructor(StatusCode: number, IsSuccess: boolean, ResponseData: any) {
    this.statusCode = StatusCode;
    this.isSuccess = IsSuccess;
    this.responseData = ResponseData;
  }
}
