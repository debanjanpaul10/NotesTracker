import { Injectable } from '@angular/core';
import { IndividualConfig, ToastrService } from 'ngx-toastr';

/**
 * The Toaster Service.
 */
@Injectable({
  providedIn: 'root',
})
class ToasterService {
  /**
   * Initializes a new instance of `ToasterService`
   * @param toaster The toaster service.
   */
  constructor(private toaster: ToastrService) {}

  /**
   * Handles the success message toaster event.
   * @param message The success message.
   */
  public showSuccess(message: string): void {
    this.toaster.success(message);
  }

  /**
   * Handles the error message toaster event.
   * @param message The error messsage.
   */
  public showError(message: string): void {
    const options: Partial<IndividualConfig> = {
      disableTimeOut: true,
      closeButton: true,
    };
    this.toaster.error(message, '', options);
  }
}

export { ToasterService };
