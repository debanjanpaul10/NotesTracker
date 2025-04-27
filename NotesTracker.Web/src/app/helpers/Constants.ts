export const ApiBaseUrl: string = 'https://localhost:41619';
export const ApiUrls = {
  Notes: {
    BaseRoute: 'api/Notes/',
    GetAllNotes_ApiRoute: 'GetAllNotes',
    GetNoteById_ApiRoute: 'GetNoteById',
    AddNewNote_ApiRoute: 'AddNewNote',
    UpdateNote_ApiRoute: 'UpdateNote',
    DeleteNote_ApiRoute: 'DeleteNote',
  },
  Users: {
    BaseRoute: 'api/Users/',
    GetUser_ApiRoute: 'GetUser',
    AddNewUser_ApiRoute: 'AddNewUser',
    DeleteUser_ApiRoute: 'DeleteUser',
  },
};

export const ExceptionMessages = {
  AllNoteFetchFailedMessage: 'Failed to fetch all the notes',
  NoteFetchFailedMessage: 'Failed to fetch the note',
  AddingNoteFailedMessage: 'Failed to add a new note',
  UserFetchFailedMessage: 'Failed to fetch the user data.',
  AddNewUserFailedMessage: 'Failed to add new user data.',
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
    Login: 'Login',
    Logout: 'Logout',
    AddNew: 'Add',
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
  Headings: {},
  NoteId: 'noteId',
};
