# Attendance Worker Service

A .NET Core Worker Service designed to manage employee attendance, analyze attendance data, generate reports, and send email notifications.

## Features
- Verifies employee attendance from a SQL Server database.
- Generates reports for:
  - Absent employees
  - Employees with incomplete attendance
  - Employees who arrived late
- Sends reports to administrators via email.
- Implements a structured, maintainable layered architecture.

## Technologies Used
- .NET Core 8 Worker Service
- Entity Framework Core 8 for database operations
- SQL Server as the database
- Dependency Injection for service management
- Elasticsearch for logging
- User Secrets for managing sensitive data
- SMTP for sending email notifications

## Setup Instructions

### 1. Prerequisites
Ensure the following tools are installed:
- [.NET SDK](https://dotnet.microsoft.com/download)
- SQL Server
- Elasticsearch (if logging is enabled)

### 2. Configure Secrets
Sensitive information, such as passwords and connection strings, must be stored securely using User Secrets. 

#### Set Connection String:
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "YourConnectionString"

#### Set Email Password:
dotnet user-secrets set "EmailSettings:Password" "YourEmailPassword"

#### View Existing Secrets:
dotnet user-secrets list

### 3. Database Setup
Run the migrations to set up the database schema and seed data:
dotnet ef database update

### 4. Run the Service
To start the worker service, use:
dotnet run

## Project Structure
AttendanceWorkerService/
├── src/
│   ├── Core/
│   │   ├── Entities/                # Entity classes (Employee, AttendanceRecord, etc.)
│   │   ├── Enums/                   # Enums for AttendanceStatus
│   │   ├── Interfaces/              # Interface definitions (IUnitOfWork, IEmailService, etc.)
│   ├── Infrastructure/
│   │   ├── Data/                    # DbContext and repository implementations
│   │   ├── Repositories/            # Repository classes (e.g., AttendanceRepository)
│   ├── Worker/                      # Background worker logic
├── appsettings.json                 # Non-sensitive configuration values
├── Program.cs                       # Entry point of the application

## Key Commands

### Entity Framework Core Commands
- Add a new migration:
  dotnet ef migrations add <MigrationName>
- Update the database:
  dotnet ef database update

### User Secrets Commands
- Initialize User Secrets:
  dotnet user-secrets init
- Add or update a secret:
  dotnet user-secrets set "<Key>" "<Value>"
- List all secrets:
  dotnet user-secrets list

## Best Practices
- Do not store sensitive information in `appsettings.json`.
- Use User Secrets for development and environment variables or secure secrets management tools (e.g., Azure Key Vault) for production.

## Contributing
1. Fork the repository.
2. Create a feature branch:
   git checkout -b feature/<feature-name>
3. Commit changes and push to the branch.
4. Submit a pull request.

## License
This project is a PoC work from ClickMinds Solutions LLP.