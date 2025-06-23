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
};

export const SuccessMessages = {
  NoteUpdatedSuccess: 'Note has been updated!',
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

export const MadeWithComponentConstants = {
  Items: {
    DotNet: {
      Heading: '.NET',
      Description: `
        <div>.NET is a free, cross-platform, open-source developer platform used for building various types of applications, including web apps, mobile apps, desktop apps, and more. It is maintained by Microsoft and the .NET Foundation on GitHub.</div>
        <br />`,
      Image: 'assets/dotnet-img.png',
      Link: 'https://dotnet.microsoft.com/en-us/',
    },
    Angular: {
      Heading: 'Angular',
      Description: `
      <div>Angular is a TypeScript-based free and open-source single-page web application framework. It is developed by Google and by a community of individuals and corporations. Angular is a complete rewrite from the same team that built AngularJS.</div>
      <br />`,
      Image: 'assets/angular-img.png',
      Link: 'https://dotnet.microsoft.com/en-us/',
    },
    SQLServer: {
      Heading: 'Microsoft SQL Server',
      Description: `
        <div>Get the flexibility you need to use integrated solutions and apps with your data—in the cloud, on-premises, or at the edge.
        SQL Server 2022 is the most Azure-enabled release yet, with innovation across performance, security, and availability.</div>
        <br />`,
      Image: 'assets/sqlserver-img.png',
      Link: 'https://www.microsoft.com/en-in/sql-server',
    },
    Auth0: {
      Heading: 'Auth0',
      Description: `
        <div>Auth0 is a flexible, drop-in solution to add authentication and authorization services to your applications. Your team and organization can avoid the cost, time, 
        and risk that come with building your own solution to authenticate and authorize users.</div>
        <br />`,
      Image: 'assets/auth0-img.png',
      Link: 'https://auth0.com/',
    },
    Azure: {
      Heading: 'Azure PaaS',
      Description: `
      <div>Microsoft Azure, or just Azure is the cloud computing platform developed by Microsoft. It has management, access and development of applications and services to individuals, companies, and governments through its global infrastructure</div>
      <br />`,
      Image: 'assets/azure-img.png',
      Link: 'https://azure.microsoft.com/',
    },
  },

  SubTitle: 'Made with ❤️ and ',
};

export const AuthConstants = {
  AuthorizationConstant: 'Authorization',
  BearerConstant: 'Bearer',
  UserNameConstant: 'username',
};
