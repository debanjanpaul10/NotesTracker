/**
 * Bug Report Validation Configuration
 * 
 * Contains validation rules and error messages for bug report form fields.
 * This class provides centralized validation constants and user-friendly
 * error messages that can be used across the application for consistent
 * form validation.
 */
export class BugReportValidations {
  /**
   * Length validation constants for form fields
   * 
   * Defines the minimum and maximum character limits for bug report fields.
   * These constants ensure consistent validation across the application
   * and provide clear boundaries for user input.
   */
  public static readonly LengthConstants = {
    /** Minimum required characters for bug title (5 characters) */
    TITLE_MIN_LENGTH: 5,
    /** Maximum allowed characters for bug title (20 characters) */
    TITLE_MAX_LENGTH: 20,
    /** Minimum required characters for bug description (10 characters) */
    DESCRIPTION_MIN_LENGTH: 10,
    /** Maximum allowed characters for bug description (100 characters) */
    DESCRIPTION_MAX_LENGTH: 100
  };

  /**
   * Validation error messages for form fields
   * 
   * Provides user-friendly error messages for different validation scenarios.
   * Messages are organized by field type and validation rule for easy
   * maintenance and internationalization support.
   */
  public static readonly ValidationMessages = {
    /**
     * Validation messages for the bug title field
     */
    title: {
      /** Message displayed when title field is empty */
      required: 'Title is required.',
      /** Message displayed when title is shorter than minimum length */
      minLength: 'Title must be at least 5 characters.',
      /** Message displayed when title exceeds maximum length */
      maxLength: 'Title must be no more than 20 characters.',
      /** Message displayed when title length is outside valid range */
      range: 'Title must be between 5 and 20 characters.'
    },
    /**
     * Validation messages for the bug description field
     */
    description: {
      /** Message displayed when description field is empty */
      required: 'Description is required.',
      /** Message displayed when description is shorter than minimum length */
      minLength: 'Description must be at least 10 characters.',
      /** Message displayed when description exceeds maximum length */
      maxLength: 'Description must be no more than 100 characters.',
      /** Message displayed when description length is outside valid range */
      range: 'Description must be between 10 and 100 characters.'
    },
    /**
     * Validation messages for the bug severity field
     */
    severity: {
      /** Message displayed when no severity level is selected */
      required: 'Severity is required.'
    },
    /**
     * Validation messages for the page URL field
     */
    pageUrl: {
      /** Message displayed when page URL field is empty */
      required: 'Page URL is required.'
    }
  };
}
