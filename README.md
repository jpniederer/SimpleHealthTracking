# SimpleHealthTracking
I've been tracking basic health stuff via a spreadsheet for a while and want to get it into a web service/web app. The information collected is very simple as I don't need anything fancy. The project is developed using the ASP.NET Web API and MVC Frameworks. All data is manually entered.
Features
--------------
+ Sleep Tracking

  The system has the ability to track sleep/wake times and be able to report off of these entries.
  
+ Weight/Heartrate Tracking

  The system is able to accept manually entered weight and heartrate entries. A timer for checking heartrates will be added to the page.
  
+ Medicine

  Users are able to create a medicine list and track whether they took it or not.
  
+ Data Import From Old Spreadsheet

  A feature exists to convert the old spreadsheet entries into the new database format. The script transforms old, spreadsheet row based records into the database objects.

Deployment
--------------
The current version of SimpleHealthTracking is live on Azure: http://www.simplehealthtracking.com/home/landing.

Setup
--------------
To setup your development environment locally, you'll have to create a local mdb file by running the "Update-Database" command in the Visual Studio Package Manager Console for both the SimpleHealthTracking.Repository and SimpleHealthTracking projects.
