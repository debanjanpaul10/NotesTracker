export const ApiBaseUrl: string = 'https://localhost:41619';
export const ApiUrls = {
  BaseRoute: 'api/Notes/',
  GetAllNotes_ApiRoute: 'GetAllNotes',
  GetNoteById_ApiRoute: 'GetNoteById',
  AddNewNote_ApiRoute: 'AddNewNote',
  UpdateNote_ApiRoute: 'UpdateNote',
  DeleteNote_ApiRoute: 'DeleteNote',
};

export const ExceptionMessages = {
  AllNoteFetchFailedMessage: 'Failed to fetch all the notes',
  NoteFetchFailedMessage: 'Failed to fetch the note',
  AddingNoteFailedMessage: 'Failed to add a new note',
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
