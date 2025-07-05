export const ApiUrls = {
  Notes: {
    BaseRoute: 'notesapi/Notes/',
    GetAllNotes_ApiRoute: 'GetAllNotes',
    GetNoteById_ApiRoute: 'GetNoteById',
    AddNewNote_ApiRoute: 'AddNewNote',
    UpdateNote_ApiRoute: 'UpdateNote',
    DeleteNote_ApiRoute: 'DeleteNote',
  },
  Users: {
    BaseRoute: 'notesapi/Users/',
    GetUser_ApiRoute: 'GetUser',
    AddNewUser_ApiRoute: 'AddNewUser',
    DeleteUser_ApiRoute: 'DeleteUser',
  },
  NotesTracker: {
    BaseRoute: 'notesapi/NotesTracker/',
    GetAboutUsData_ApiRoute: 'GetAboutUsData',
    AddNewBugReport_ApiRoute: 'AddNewBugReport',
  },
};

export const AngularRoutes = {
  Home: {
    Name: '',
    Link: '/',
  },
  Note: {
    Name: 'notes/:noteId',
    Link: '/notes/:noteId',
  },
  AddNote: {
    Name: 'addnote',
    Link: '/addnote',
  },
  Error: {
    Name: 'error',
    Link: '/error',
  },
};

export const ExceptionMessages = {
  AllNoteFetchFailedMessage: 'Failed to fetch all the notes',
  NoteFetchFailedMessage: 'Failed to fetch the note',
  AddingNoteFailedMessage: 'Failed to add a new note',
  UserFetchFailedMessage: 'Failed to fetch the user data.',
  AddNewUserFailedMessage: 'Failed to add new user data.',
  DefaultValidationFailedMessage: 'Some fields are missing',
  DefaultErrorMessage: 'Something went terribly wrong!',
  CouldNotSubmitBugReport: 'Something went wrong while sending the bug report',
};

export const SuccessMessages = {
  NoteUpdatedSuccess: 'Note has been updated!',
  BugReportUpdatedSuccess: 'Bug report has been sent successfully',
};

export const HomePageConstants = {
  Headings: {
    WelcomeMessage: 'Welcome to the Notes Tracking Application',
    SubHeadingMessage: 'Manage your notes efficiently and effectively',
  },
  AppName: 'NotesTracker',
};

export const AddNotePageConstants = {
  Headings: {
    Header: 'Add a new Note',
    TitleBarPlaceHolder: 'Add the Note Title',
    DescriptionPlaceHolder: 'Add the Note Description',
    SaveButton: 'Save Note',
    CancelButton: 'Cancel',
  },
};

export const HeaderPageConstants = {
  Headings: {
    Title: 'Notes Tracker',
    LoginRegister: 'Login/Register',
    Logout: 'Logout',
  },
  ThemeSettings: {
    LightMode: {
      Key: 'light-theme',
      Name: 'Light Mode',
    },
    DarkMode: {
      Key: 'dark-theme',
      Name: 'Dark Mode',
    },
  },
};

export const CacheKeys = {
  ThemeSettings: 'cachedTheme',
  LoggedInUser: 'loggedInUser',
};

export const NotesContainerConstants = {
  Headings: {
    AddButtonText: 'Add New Note',
  },
  RouteLinks: {
    Notes: '/notes',
  },
  LoadingText: 'Loading notes ...',
};

export const NotesPageConstants = {
  Headings: {
    NoteTitle: 'Note Title',
    NoteDescription: 'Note Description',
  },
  ButtonTexts: {
    Update: 'Update',
    Cancel: 'Cancel',
  },
};

export const UserLoginModalConstants = {
  Headings: {
    Email: {
      Name: 'Email',
      Placeholder: 'Please enter your email address.',
    },
    Password: {
      Name: 'Password',
      Placeholder: 'Please enter your secured password.',
    },
  },
  Buttons: {
    Login: 'Login',
    Cancel: 'Cancel',
  },
};

export const UserRegisterModalConstants = {
  Headings: {
    Email: {
      Name: 'Email',
      Placeholder: 'Please enter your email address.',
    },
    Password: {
      Name: 'Password',
      Placeholder: 'Please enter your secured password.',
    },
    UserName: {
      Name: 'User Name',
      Placeholder: 'Please enter a nice username for you!',
    },
  },
  Buttons: {
    Register: 'Register',
    Cancel: 'Cancel',
  },
};

export const ErrorPageConstants = {
  PageNotFoundErrorMessage: 'Oops! The page could not be found!',
};

export const MadeWithSubTitle = 'Made with ❤️ and ';
export const AuthConstants = {
  AuthorizationConstant: 'Authorization',
  BearerConstant: 'Bearer',
  UserNameConstant: 'username',
};

export class BugReportDrawerConstants {
  public static readonly HeaderConstant = 'Report a bug';
  public static readonly DropdownMenuOptions = [
    { label: 'Low', value: 'Low' },
    { label: 'Medium', value: 'Medium' },
    { label: 'High', value: 'High' },
  ];
  public static readonly FormLabelConstants = {
    Title: {
      Name: 'Bug Title',
      Placeholder: 'Enter the bug title',
    },
    Description: {
      Name: 'Bug Description',
      Placeholder: 'Enter the bug description in about 100 words',
    },
    Severity: {
      Name: 'Bug Severity',
      Placeholder: 'Choose a severity',
    },
    Pageurl: {
      Name: 'Page URL',
      Placeholder: 'Enter the page URL',
    },
    SubmitButton: 'Submit',
  };
}
