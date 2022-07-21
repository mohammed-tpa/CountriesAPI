# CountriesAPI
This is a WEB API solution that returns country public holidays

Users should be able to view below API's:
- countries list
- grouped by a month holidays list for a given country and year
- specific day status(workday, free day, holiday)
- the maximum number of free(free day + holiday) days in a row, which will be by a given country and year (API Structure is implemented , however maximum number of free days in a row Logic implementaion is pending )

To get data(countries, holidays) initially, application is using JSON API from https://kayaposoft.com/enrico/ . Results from https://kayaposoft.com/enrico/ api 
is normalized and saved to a database, so next time application read the information from the database.

API is deployed in Azure and is available on below public url:
https://api20220721112833.azurewebsites.net/swagger/index.html

SQL Server database is configured on Azure. 
If you'd like to run the application locally, just clone the repository and use Visual Studio to run it with .NET Core Web API and Swagger. Edit the the connection string to point to your local database server. As I am using EF Code first approach, application will create the database on execution. 

Uses xUnit as the test library for the test project


API documentation JSON file is available on below url:
https://api20220721112833.azurewebsites.net/swagger/v1/swagger.json

Screenshot:
![image](https://user-images.githubusercontent.com/65413897/180208306-46a7f67b-2b65-46c4-87b5-d1717cb35be3.png)
