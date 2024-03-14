# Mortgage Rest Service
This application calculates monthly mortgage rates over a fixed amount of years at a specific rate written in c# and vue3 with a Mongo document DB

This is a first stab at building a backend ASP.NET rest application written in c#

The backend service uses [Dotnet 7](https://dotnet.microsoft.com/en-us/download/dotnet/7.0) and the NUnit test framework for the service layer implementation. 

## Application setup

 - Download [Dotnet 7](https://dotnet.microsoft.com/en-us/download/dotnet/7.0) 
 - Download npm verison 10.2.4 [Node 20](https://nodejs.org/en/download)
 - Download [Mongo Db Community](https://www.mongodb.com/try/download/community)


Once MongoDb community is installed you can view documents from Mongo DB compass. The application _should_ create the database and Applicant table automatically upon first run of the asp.net service.

If not create a DB named Mortgage and add a collection named Applicant

Next you should be able to navigate to `<repository-directory>/csharp-mortgage-rest-service/mortgage-application/` 

and run **dotnet run --project mortgage-application.csproj**

This should start a local service on port **5137**

Then navigate to `<repository-directory>/csharp-mortgage-rest-service/mortgage-application/web/mortgage-frontend` and run **npm i**

Then run **npm run dev** to start the development server at localhost:8080

Now you should see the mortgage application running at http://localhost:8080/

From here you should be able to generate mortgage applicants and see them persisted in mongo via MongoDbCompass

**Test suite**

For MortgageApplicationServices test suite navigate to `<repository-directory>/csharp-mortgage-rest-service/mortgage-application/ `

and run **dotnet test** to test mock transactions for the MortgageApplicationService, the main service used for generation applicants mortgage rates




