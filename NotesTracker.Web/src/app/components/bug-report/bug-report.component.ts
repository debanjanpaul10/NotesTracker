import { CommonModule } from '@angular/common';
import { Component, signal, WritableSignal } from '@angular/core';
import { DrawerModule } from 'primeng/drawer';
import { FormsModule } from '@angular/forms';
import { SelectModule } from 'primeng/select';
import { InputGroupModule } from 'primeng/inputgroup';
import { InputGroupAddonModule } from 'primeng/inputgroupaddon';
import { ButtonModule } from 'primeng/button';

import { ToasterService } from '@core/services/toaster.service';
import { CommonService } from '@services/common.service';
import { NotesTrackerService } from '@services/notestracker.service';
import {
  BugReportDrawerConstants,
  ExceptionMessages,
  SuccessMessages,
} from '@shared/notestracker.constants';
import { BugReportDTO } from '@models/dto/bug-report-dto.class';
import { BugReportValidations } from '@shared/validations';
import { SpinnerComponent } from '@components/common/spinner/spinner.component';

/**
 * Bug Report Component
 * 
 * A drawer-based component that allows users to submit bug reports for the application.
 * The component provides a form with validation for bug title, description, severity level,
 * and automatically captures the current page URL. It integrates with the NotesTracker
 * service to submit reports and displays success/error messages via the toaster service.
 * 
 * Features:
 * - Real-time form validation with custom validation messages
 * - Severity level selection via dropdown
 * - Loading state management during submission
 * - Automatic page URL capture
 * - Form reset after successful submission
 * 
 * @example
 * ```html
 * <app-bug-report></app-bug-report>
 * ```
 * 
 * @since 1.0.0
 * @author NotesTracker Team
 */
@Component({
  selector: 'app-bug-report',
  imports: [
    CommonModule,
    DrawerModule,
    FormsModule,
    SelectModule,
    InputGroupModule,
    InputGroupAddonModule,
    ButtonModule,
    SpinnerComponent,
  ],
  templateUrl: './bug-report.component.html',
  styleUrl: './bug-report.component.scss',
})
export class BugReportComponent {
  /** Constants for bug report drawer configuration */
  public BugReportConstants = BugReportDrawerConstants;
  
  /** Signal indicating whether the component is in a loading state during form submission */
  public isLoading: WritableSignal<boolean> = signal(false);
  
  /** The bug report data object containing form values */
  public bugReport: BugReportDTO = new BugReportDTO(
    '',
    '',
    BugReportDrawerConstants.DropdownMenuOptions[0].value,
    window.location.origin
  );
  
  /** Available severity options for the dropdown menu */
  public dropdownMenuOptions = BugReportDrawerConstants.DropdownMenuOptions;
  
  /** Form label constants for internationalization */
  public FormLabelConstants = BugReportDrawerConstants.FormLabelConstants;

  /** Validation constants for field length requirements */
  private LengthValidationConstants = BugReportValidations.LengthConstants;
  
  /** Validation message templates for form fields */
  private ValidationMessages = BugReportValidations.ValidationMessages;

  /**
   * Creates an instance of BugReportComponent.
   * 
   * @param commonService - Service for managing shared application state
   * @param notesTrackerService - Service for interacting with the NotesTracker API
   * @param toaster - Service for displaying toast notifications
   */
  constructor(
    private readonly commonService: CommonService,
    private readonly notesTrackerService: NotesTrackerService,
    private readonly toaster: ToasterService
  ) {}

  /**
   * Gets the visibility state of the bug report drawer.
   * 
   * @returns {boolean} True if the drawer is visible, false otherwise
   */
  public get visible(): boolean {
    return this.commonService.isBugFlyoutVisible;
  }

  /**
   * Sets the visibility state of the bug report drawer.
   * 
   * @param {boolean} value - The visibility state to set
   */
  public set visible(value: boolean) {
    this.commonService.isBugFlyoutVisible = value;
  }

  /**
   * Validates the bug title field.
   * 
   * Checks if the title meets the minimum and maximum length requirements
   * defined in the validation constants.
   * 
   * @returns {boolean} True if the title is valid, false otherwise
   * 
   * @example
   * ```typescript
   * if (this.isTitleValid()) {
   *   // Proceed with form submission
   * }
   * ```
   */
  public isTitleValid(): boolean {
    return (
      this.bugReport.bugTitle.length >=
        this.LengthValidationConstants.TITLE_MIN_LENGTH &&
      this.bugReport.bugTitle.length <=
        this.LengthValidationConstants.TITLE_MAX_LENGTH
    );
  }

  /**
   * Validates the bug description field.
   * 
   * Checks if the description meets the minimum and maximum length requirements
   * defined in the validation constants.
   * 
   * @returns {boolean} True if the description is valid, false otherwise
   * 
   * @example
   * ```typescript
   * if (this.isDescriptionValid()) {
   *   // Proceed with form submission
   * }
   * ```
   */
  public isDescriptionValid(): boolean {
    return (
      this.bugReport.bugDescription.length >=
        this.LengthValidationConstants.DESCRIPTION_MIN_LENGTH &&
      this.bugReport.bugDescription.length <=
        this.LengthValidationConstants.DESCRIPTION_MAX_LENGTH
    );
  }

  /**
   * Validates the bug severity field.
   * 
   * Checks if a severity level has been selected from the dropdown.
   * 
   * @returns {boolean} True if a severity is selected, false otherwise
   */
  public isSeverityValid(): boolean {
    return !!this.bugReport.bugSeverity;
  }

  /**
   * Validates the page URL field.
   * 
   * Checks if the page URL is present and not empty.
   * 
   * @returns {boolean} True if the page URL is valid, false otherwise
   */
  public isPageUrlValid(): boolean {
    return !!this.bugReport.pageUrl;
  }

  /**
   * Validates the entire bug report form.
   * 
   * Performs validation on all form fields: title, description, severity, and page URL.
   * All fields must pass validation for the form to be considered valid.
   * 
   * @returns {boolean} True if all form fields are valid, false otherwise
   * 
   * @example
   * ```typescript
   * if (this.isFormValid()) {
   *   await this.submitBugReport();
   * } else {
   *   // Show validation errors
   * }
   * ```
   */
  public isFormValid(): boolean {
    return (
      this.isTitleValid() &&
      this.isDescriptionValid() &&
      this.isSeverityValid() &&
      this.isPageUrlValid()
    );
  }

  /**
   * Gets the validation message for the title field.
   * 
   * Returns appropriate error messages based on the current state of the title field:
   * - Required field message if empty
   * - Minimum length message if too short
   * - Maximum length message if too long
   * - Empty string if valid
   * 
   * @returns {string} The validation message for the title field
   * 
   * @example
   * ```typescript
   * const message = this.getTitleValidationMessage();
   * if (message) {
   *   this.showError(message);
   * }
   * ```
   */
  public getTitleValidationMessage(): string {
    if (!this.bugReport.bugTitle) {
      return this.ValidationMessages.title.required;
    }
    if (
      this.bugReport.bugTitle.length <
      this.LengthValidationConstants.TITLE_MIN_LENGTH
    ) {
      return `${this.ValidationMessages.title.minLength} Current: ${this.bugReport.bugTitle.length}`;
    }
    if (
      this.bugReport.bugTitle.length >
      this.LengthValidationConstants.TITLE_MAX_LENGTH
    ) {
      return `${this.ValidationMessages.title.maxLength} Current: ${this.bugReport.bugTitle.length}`;
    }
    return '';
  }

  /**
   * Gets the validation message for the description field.
   * 
   * Returns appropriate error messages based on the current state of the description field:
   * - Required field message if empty
   * - Minimum length message if too short
   * - Maximum length message if too long
   * - Empty string if valid
   * 
   * @returns {string} The validation message for the description field
   * 
   * @example
   * ```typescript
   * const message = this.getDescriptionValidationMessage();
   * if (message) {
   *   this.showError(message);
   * }
   * ```
   */
  public getDescriptionValidationMessage(): string {
    if (!this.bugReport.bugDescription) {
      return this.ValidationMessages.description.required;
    }
    if (
      this.bugReport.bugDescription.length <
      this.LengthValidationConstants.DESCRIPTION_MIN_LENGTH
    ) {
      return `${this.ValidationMessages.description.minLength} Current: ${this.bugReport.bugDescription.length}`;
    }
    if (
      this.bugReport.bugDescription.length >
      this.LengthValidationConstants.DESCRIPTION_MAX_LENGTH
    ) {
      return `${this.ValidationMessages.description.maxLength} Current: ${this.bugReport.bugDescription.length}`;
    }
    return '';
  }

  /**
   * Gets the validation message for the severity field.
   * 
   * @returns {string} The validation message for the severity field
   */
  public getSeverityValidationMessage(): string {
    return this.ValidationMessages.severity.required;
  }

  /**
   * Gets the validation message for the page URL field.
   * 
   * @returns {string} The validation message for the page URL field
   */
  public getPageUrlValidationMessage(): string {
    return this.ValidationMessages.pageUrl.required;
  }

  /**
   * Submits the bug report to the backend API.
   * 
   * This method performs the following operations:
   * 1. Validates the form before submission
   * 2. Sets loading state to true
   * 3. Calls the NotesTracker service to submit the bug report
   * 4. Shows success message and resets form on successful submission
   * 5. Shows error message if submission fails
   * 6. Closes the drawer after successful submission
   * 7. Resets loading state to false
   * 
   * @returns {Promise<void>} A promise that resolves when the submission is complete
   * 
   * @throws {Error} When the API call fails or validation fails
   * 
   * @example
   * ```typescript
   * // Called from template or other component method
   * await this.submitBugReport();
   * ```
   */
  public async submitBugReport(): Promise<void> {
    if (!this.isFormValid()) {
      return;
    }
    try {
      this.isLoading.set(true);
      const response = await this.notesTrackerService.addNewBugReportAsync(
        this.bugReport
      );
      if (response) {
        this.toaster.showSuccess(SuccessMessages.BugReportUpdatedSuccess);
        this.bugReport = new BugReportDTO('', '', '', window.location.origin);
        this.visible = false;
      }
    } catch (error: any) {
      console.error(error);
      this.toaster.showError(ExceptionMessages.CouldNotSubmitBugReport);
    } finally {
      this.isLoading.set(false);
    }
  }
}
