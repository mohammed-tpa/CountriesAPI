# CountriesAPI
This is a WEB API solution that returns country public holidays

Users should be able to view below API's:
- countries list
- grouped by a month holidays list for a given country and year
- specific day status(workday, free day, holiday)
- the maximum number of free(free day + holiday) days in a row, which will be by a given country and year

To get data(countries, holidays) initially, application is using JSON API from https://kayaposoft.com/enrico/ . Results from https://kayaposoft.com/enrico/ api 
is normalized and saved to a database, so next time application read the information from the database.

API is deployed in Azure and is available on below public url:
https://api20220721112833.azurewebsites.net/swagger/index.html

Screenshot:
![image](https://user-images.githubusercontent.com/65413897/180208306-46a7f67b-2b65-46c4-87b5-d1717cb35be3.png)
