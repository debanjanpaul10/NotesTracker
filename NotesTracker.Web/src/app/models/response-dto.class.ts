export class ResponseDTO {
  public statusCode: number;
  public isSuccess: boolean;
  public responseData: any;

  constructor(statusCode: number, isSuccess: boolean, responseData: any) {
    this.statusCode = statusCode;
    this.isSuccess = isSuccess;
    this.responseData = responseData;
  }
}
