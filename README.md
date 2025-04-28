# EF Core Web API Project

## Overview
This project is a Web API built using ASP.NET Core and Entity Framework Core, designed to manage a simple organizational structure with departments, employees, projects, and salaries.

## Project Structure
- **Controllers**: Contains the API controllers that handle HTTP requests.
  - `DepartmentsController.cs`: Manages CRUD operations for departments.
  
- **Data**: Contains the database context and migration files.
  - `AppDbContext.cs`: Configures the database context and sets up relationships.
  - `Migrations`: Contains migration files for database schema changes.
  - `SeedData.cs`: Seeds initial data into the Departments table.

- **Models**: Contains the data models representing the application's entities.
  - `Department.cs`: Represents a department with properties for Id and Name.
  - `Employee.cs`: Represents an employee with properties for Id, Name, DepartmentId, and JoinedDate.
  - `Project.cs`: Represents a project with properties for Id and Name.
  - `ProjectEmployee.cs`: Represents the join table for the many-to-many relationship between projects and employees.
  - `Salary.cs`: Represents an employee's salary with properties for Id and EmployeeId.

- **Properties**: Contains project properties and launch settings.
  - `launchSettings.json`: Configures profiles for launching the application.

- **Configuration Files**:
  - `appsettings.json`: Contains configuration settings, including connection strings for SQL Server.
  - `EFCoreWebAPI.csproj`: Project file specifying dependencies and settings.

- **Program.cs**: The entry point of the application, setting up the web application and configuring services.

## Setup Instructions
1. **Clone the Repository**: Clone this repository to your local machine.
2. **Install Dependencies**: Navigate to the project directory and run `dotnet restore` to install the necessary packages.
3. **Update Connection String**: Modify the connection string in `appsettings.json` to point to your SQL Server database.
4. **Run Migrations**: Execute `dotnet ef migrations add InitialCreate` followed by `dotnet ef database update` to create the database schema.
5. **Seed Data**: Ensure that the `SeedData` class is called during application startup to populate the Departments table with initial data.
6. **Run the Application**: Use `dotnet run` to start the application. Access the API at `https://localhost:5001` or `http://localhost:5000`.

## Usage
- **Departments API**: Access the departments endpoint to perform CRUD operations on departments.
- **Weather Forecast API**: A sample endpoint is provided to demonstrate API functionality.

## Contributing
Contributions are welcome! Please submit a pull request or open an issue for any enhancements or bug fixes.

## License
This project is licensed under the MIT License. See the LICENSE file for details.