import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { first, firstValueFrom } from 'rxjs';

import { environment } from '@environments/environment';
import { ApiUrls } from '@shared/notestracker.constants';
import { ResponseDTO } from '@models/dto/response-dto.class';
import { AboutUs } from '@models/dto/about-us-dto.class';

/**
 * The Notes Tracker Service class.
 */
@Injectable({
  providedIn: 'root',
})
class NotesTrackerService {
  /**
   * The notes routes.
   */
  public notesTrackerRoutes = ApiUrls.NotesTracker;

  /**
   * Initializes a new instance of `NotesTrackerService`
   * @param httpClient The http client service.
   */
  constructor(private httpClient: HttpClient) {}

  /**
   * The api base url.
   */
  private apiUrl: string = `${environment.apiBaseUrl}/${ApiUrls.NotesTracker.BaseRoute}`;

  /**
   * Gets the about us data.
   * @returns The api response.
   */
  public async getAboutUsDataAsync(): Promise<AboutUs[]> {
    const getAboutUsDataUrl: string = `${this.apiUrl}${this.notesTrackerRoutes.GetAboutUsData_ApiRoute}`;
    const response = await firstValueFrom(
      this.httpClient.get<ResponseDTO>(getAboutUsDataUrl)
    );
    if (response?.isSuccess && response?.responseData !== null) {
      return response?.responseData as AboutUs[];
    } else {
      throw new Error(response.responseData);
    }
  }

  /**
   * Creates a new bug report.
   * @param data The input data for bug report
   * @returns The api response.
   */
  public async addNewBugReportAsync(data: any): Promise<boolean> {
    const addNewBugReportDataUrl: string = `${this.apiUrl}${this.notesTrackerRoutes.AddNewBugReport_ApiRoute}`;
    const response = await firstValueFrom(
      this.httpClient.post<ResponseDTO>(addNewBugReportDataUrl, data)
    );
    if (response?.isSuccess && response?.responseData !== null) {
      return response?.responseData as boolean;
    } else {
      throw new Error(response.responseData);
    }
  }
}

export { NotesTrackerService };
