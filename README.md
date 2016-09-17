# SimpleHealthTracking
I've been tracking basic health stuff via a spreadsheet for a while and want to get it into a web service/web app. The information collected is very simple as I don't need anything fancy. The project will be developed using the .NET Web API Framework and will have an MVC website frontend.
Features
--------------
+ Sleep Tracking
  The system will have the ability to track sleep/wake times and be able to report off of these entries.
+ Weight/Heartrate Tracking
  The system will be able to accept manually entered weight and heartrate entries. A timer for checking heartrates will be added to the page.
+ Medicine
  Users will be able to create a medicine list and track whether they took it or not.
+ Data Import From Old Spreadsheet
  A Python script will be written to convert the old spreadsheet entries into the new format. The script will call the REST functions to generate the proper records.
