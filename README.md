# HomeFinder -helps you find a home
Loom video: https://www.loom.com/share/41d12ff6e8b94a69b4522625036cacbe

The application helps you make an informed decision about your next home by providing you with the tools for reviewing different houses or apartments based on custom criteria. The location of the homes is shown on google maps. It also has 2 financial calculators.

Home finder is done using ASP.NET Core 3.1 framework. I used for database management PostgresSQL. The classes have corresponding tables in the database with "one to many" and "many to many" relations between them. https://docs.google.com/document/d/1vSNr2oB0H-PSguV3fUvJijvGyas8TD3zq_XlKqeNIsU/edit. Controllers acces methods that do CRUD using repository pattern and dependency injection. The UI part of the app was done using Bootstap 4 and the inntegration of google maps using Javascript.

Homes belong to an Area and are scored based on location preferences set by the user and based on actual grades on home features from each review. Location preferences have a field called Rank. The grade for location preferences is calculated using a logarithmic scale(base 10) taking into account the rank of each location preference. This grade is assigned to the Area that the home belongs to. When reviewing a home different home features will be graded. The grade from averaging home features grades will be added to the grade for the area and a general average grade will be calculated for that home.

The user can do CRUD on all his reviews, can order his preferences for a location, and can add custom location and home criteria. The reviews are shown on google maps based on coordinates or addresses (if coordinates are missing). The app also has 2 calculators that calculate how much you can afford to borrow and how much you will pay every month for a loan. 
