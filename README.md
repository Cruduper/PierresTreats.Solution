# _Pierre's Treats_

#### By _**Eric Crudup**_

#### _An application for managing treats and their flavors in a bakery_

## Technologies Used

* _C#_
* _.NET_
* _ASP.NET MVC_
* _MSTest_
* _Markdown_
* _Razor_
* _Entity Framework Core (EF Core)_
* _MySQL_   
* _MySQL Workbench_
* _LINQ_
* _Identity_

## Description

This was the 5th C# weekly project in my programming bootcamp, and is intended to display the skills we learned this week with using Identity for user authorization and authentication as well as many-to-many relationships using EF Core and connecting our C# objects to a MySQL database using the CRUD paradigm. We also resume our use of ASP .NET MVC routing and LINQ querying.    

This is a program that will allow Pierre, a bakery owner, to add new treats to his menu, and add new flavors for his treats. Treats can come in many flavors, and different treats can have the same flavor. 


## Setup/Installation Requirements

* _clone git repository to a local machine using command_ ```$ git clone [repository-URL].git```
* _navigate to root folder of the project using terminal_

#### _Intalling Entity Framework Core (EF Core)_
* _navigate to program folder of the repo, the one that holds the your Program.cs file (named PierresTreats in this repo) using the terminal_
* _Use these commands in your terminal inside the program folder directory:_  
``` 
  $ dotnet add package Microsoft.EntityFrameworkCore -v 5.0.0   
  $ dotnet add package Pomelo.EntityFrameworkCore.MySql -v 5.0.0-alpha.2   
```

#### _Installing and running your database_
* _Follow instructions [HERE](https://www.learnhowtoprogram.com/c-and-net/getting-started-with-c/installing-and-configuring-mysql) to install MySQL, & MySQL Workbench_   
* _Open MySQL Workbench on your machine and access Local instance 3306 under MySQL Connections_
* _Look for a "MySQL Connections" box, click on the one that says "localhost: 3306"_
* _Now a MySQL server is running on your computer, ready to recieve or retrieve data!_

#### _Creating an appsettings.json file:_

  The appsettings.json file will contain personal credentials for your MySQL server, as such, you cannot use my appsettings.json file, and will have to create your own. I'm very sorry, but I will give you instructions to help you on your path:

* _Navigate to program folder of the repo, the one that holds the your Program.cs file (named PierresTreats in this repo) using the command line_
* _Create the appsettings file using the command:_ 
```
$ touch appsettings.json 
```  
* _(you may also be able to create the same file using the GUI file explorer in your OS or within your code editor)_
* _add the following code to the appsettings.json file:_ 
```  
  {
    "ConnectionStrings": {
        "DefaultConnection": "Server=localhost;Port=3306;database=eric_crudup;uid=[USER ID];pwd=[PASSWORD];"
    }
  }
```
* _replace "[PASSWORD]" and "[USER ID]" with the password and user id you chose during the MySQL installation process_

#### _Using dotnet ef tool to create migrations:_

* _type in ```$ dotnet tool install --global dotnet-ef --version 5.0.1``` to install dotnet ef tool_   
* _Navigate to the inside the program folder of the repo, the one that holds the your Program.cs file (named PierresTreats in this repo)_    
* _Then, while still inside that same program folder in terminal, use command ```$ dotnet ef database update```_    
* _Following these steps should create a migrations folder in your project, and create a new schema inside of MySQL with the name "eric_crudup" (which comes from the appsettings.json file your created) that contains all the tables for the project_      
* _More details can be found [HERE](https://www.learnhowtoprogram.com/c-and-net/many-to-many-relationships/code-first-development-and-migrations)_


#### _To Run Program_
* _navigate to program folder of the repo, the one that holds the your Program.cs file (named Factory in this repo) using the terminal_
* _type in ```$ dotnet build``` command_
* _type in ```$ dotnet run``` command_
* _get awesome_

## Known Bugs and issues

* _No known bugs_

## License

[MIT](https://opensource.org/licenses/MIT)    
If you have any issues or questions, contact me at Cruduper@users.noreply.github.com  

Copyright (c) _2022_  _Eric Crudup_