export class BugReportValidations {
  public static readonly LengthConstants = {
    TITLE_MIN_LENGTH: 5,
    TITLE_MAX_LENGTH: 20,
    DESCRIPTION_MIN_LENGTH: 10,
    DESCRIPTION_MAX_LENGTH: 100
  };

  public static readonly ValidationMessages = {
    title: {
      required: 'Title is required.',
      minLength: 'Title must be at least 5 characters.',
      maxLength: 'Title must be no more than 20 characters.',
      range: 'Title must be between 5 and 20 characters.'
    },
    description: {
      required: 'Description is required.',
      minLength: 'Description must be at least 10 characters.',
      maxLength: 'Description must be no more than 100 characters.',
      range: 'Description must be between 10 and 100 characters.'
    },
    severity: {
      required: 'Severity is required.'
    },
    pageUrl: {
      required: 'Page URL is required.'
    }
  };
}
