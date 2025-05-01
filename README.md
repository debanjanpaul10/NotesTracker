# Introduction

It is a simple ToDo Notes Application that help track your todos and notes. You can add custom notes to this application, edit them and delete simply. It also has user authentication so that notes will not be overlapped for users.

# Getting Started
## Pre-requisites
- .NET SDK (latest)
- Node js and npm
- Angular CLI
- SQL Server Management Studio 18 or latest
- Visual Studio 2022+ IDE
- Visual Studio Code or any text editor of choice

## Installation process
- Install the above mentioned softwares as the first step.
- For the Dotnet backend:
    - Go to the `NotesTracker.API` folder. This will be the api solution root.
    - Open the terminal and run `dotnet build` to install the packages and build the solution.
    - Once the build is successful, run the project by `dotnet run`.
- Once done, for the UI:
    - Go to the `NotesTracker.Web` folder. This will be the web solution root.
    - Open the terminal and perform an `npm install` task to install the node dependencies.
    - Once done, run the script `npm start` or `ng serve`. This will host the application on development server.
- Once the steps are done, visit the website on the localhost uri as mentioned in the terminal for the web root. The application must be up and running!

# Infrastructure
## Build and Deploy
- TODO: The build and deploy steps are a WIP. Will be released once done.

## Branching strategy
- The `main` branch will be containing the stable code that will contain GA release.
- The `dev` branch will be the development branch containing latest nightly build. Might be unstable.

# Contribute
Nothing special. Just clone and code. Do whatever. Use your branches however you want. Add new features which even I don't know about. Use technologies that are latest, old, relic or your own even. 
Go ahead. Just one rule: HAVE FUN.

Happy hacking. Peace out!