import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';

import { environment } from '@environments/environment';
import { ApiUrls } from '@app/shared/notestracker.constants';
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
   * The api base url.
   */
  apiUrl: string = `${environment.apiBaseUrl}/${ApiUrls.NotesTracker.BaseRoute}`;

  /**
   * The notes routes.
   */
  notesTrackerRoutes: any = ApiUrls.NotesTracker;

  /**
   * The http client injected.
   */
  httpClient = inject(HttpClient);

  /**
   * Gets the about us data.
   * @returns The api response.
   */
  public getAboutUsDataAsync(): Observable<AboutUs[]> {
    const getAboutUsDataUrl: string = `${this.apiUrl}${this.notesTrackerRoutes.GetAboutUsData_ApiRoute}`;
    return this.httpClient.get<ResponseDTO>(getAboutUsDataUrl).pipe(
      map((response: ResponseDTO) => {
        if (response.isSuccess && response.responseData !== null) {
          return response.responseData as AboutUs[];
        } else {
          throw new Error(response.responseData);
        }
      })
    );
  }
}

export { NotesTrackerService };
