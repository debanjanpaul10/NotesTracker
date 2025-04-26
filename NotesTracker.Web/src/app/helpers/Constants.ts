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
    WelcomeMessage: "Welcome to the Notes Tracking Application",
    SubHeadingMessage: "Manage your notes efficiently and effectively"
  }
}