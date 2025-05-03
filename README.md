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
    - In the file `src/app/helpers/config.constants.ts`, change the `ApiBaseUrl` to `https://localhost:41619` which is the local API url.
    - Open the terminal and perform an `npm install` task to install the node dependencies.
    - Once done, run the script `npm start` or `ng serve`. This will host the application on development server.
- Once the steps are done, visit the website on the localhost uri as mentioned in the terminal for the web root. The application must be up and running!

### For Electron Application
- Do the above mentioned steps first. Once done, open a new terminal and then go to `NotesTracker.Web > electron`.
- Run the command `npm start`.
- The Electron app will run.

# Infrastructure
## Build and Deploy
- The entire application is deployed via Azure services. Check the Wiki to find out more about the workflow diagram.
- Deployment will be completely to Azure Services with Github Actions. The pipeline `CI-CD for Notes Tracker` will be handling the deployments.
- As of now, only `main` and `dev` commits will be deployed. Feature branch integrations will be done later on.
- The Deployment will be in two stages:
    - The .NET stage will build the .NET app with .NET 9.0.x SDK and the build artifacts will be deployed to a specified Azure App Service via Github Actions connections and Deployment secret.
    - The Angular(Node.js) stage will install the npm packages and create html and respective css and js files. These files will be deployed to an Azure Static Web App via the build created artifacts.
    - TODO: The NotesTracker.Database will be deployed to Azure SQL Databases via DACPAC. 

## Branching strategy
- The `main` branch will be containing the stable code that will contain GA release.
- The `dev` branch will be the development branch containing latest nightly build. Might be unstable.

# Contribute
Nothing special. Just clone and code. Do whatever. Use your branches however you want. Add new features which even I don't know about. Use technologies that are latest, old, relic or your own even. 
Go ahead. Just one rule: HAVE FUN.

Happy hacking. Peace out!
