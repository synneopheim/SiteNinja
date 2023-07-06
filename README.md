# SiteNinja
An API that consumes building limits and height plateaus, splits up the building limits according to the height plateaus, and stores these three entities.

# Hosting
The app service is deployed to Azure App Service using Git Deployment. To see the swagger docs, go to eployment to https://siteninja.azurewebsites.net/swagger/index.html.

# Build the app locally

Before getting started, ensure that you have the following prerequisites installed:

.NET 7 SDK
ASP.NET

Clone the repository or download the source code.
Open a command-line interface (CLI) and navigate to the folder of the .csproj file for the app service.

Run the following commands in the CLI: 
1. dotnet restore
2. dotnet build
3. dotnet run

The application will start, and you will see the URL where it is hosted. Typically, it will be something like'http://localhost:5000'.


