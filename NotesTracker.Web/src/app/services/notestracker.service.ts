import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { firstValueFrom } from 'rxjs';

import { environment } from '@environments/environment';
import { ApiUrls } from '@shared/notestracker.constants';
import { ResponseDTO } from '@models/dto/response-dto.class';
import { AboutUs } from '@models/about-us-dto.class';

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
  public notesTrackerRoutes: any = ApiUrls.NotesTracker;

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
}

export { NotesTrackerService };
