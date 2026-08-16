# Employee Management System — Backend API

RESTful API backend for Employee Management System built with **.NET 8**, **Entity Framework Core 8**, and **PostgreSQL 17**.

---

## Features

- **Authentication & Authorization**: JWT Bearer Tokens with Role-Based Access Control (Admin, HR, Manager, Employee).
- **Password Security**: BCrypt password hashing (`BCrypt.Net-Next`).
- **Database ORM**: Entity Framework Core with PostgreSQL provider (`Npgsql.EntityFrameworkCore.PostgreSQL`).
- **Validation**: FluentValidation with automatic model state validation.
- **Error Handling**: Global Exception Handling middleware returning consistent JSON errors.
- **API Documentation**: Swagger / OpenAPI with Bearer Authorization.
- **Database Seeding**: Automatic seeding of roles, departments, employees, and users on startup.
- **CORS**: Pre-configured for Angular frontend (`http://localhost:4200`).

---

## Tech Stack

- **.NET 8.0 SDK** (C#)
- **ASP.NET Core Web API**
- **Entity Framework Core 8.0**
- **PostgreSQL 17**
- **Npgsql.EntityFrameworkCore.PostgreSQL**
- **BCrypt.Net-Next**
- **FluentValidation.AspNetCore**
- **Swashbuckle.AspNetCore**

---

## Project Structure

```
backend/
└── EmployeeAPI/
    ├── Controllers/          # REST API Controllers (Auth, Employees, Departments, Roles, Dashboard)
    ├── Data/                 # AppDbContext, SeedData (Automated DB Seeding on startup)
    ├── DTOs/                 # DTOs for Auth, Employees, Departments, Roles, Dashboard, Common
    ├── Extensions/           # ServiceExtensions (DI, JWT, Swagger, CORS)
    ├── Helpers/              # Entity <-> DTO MappingExtensions
    ├── Middleware/           # ExceptionMiddleware (Global error handling)
    ├── Migrations/           # EF Core PostgreSQL Migrations (InitialCreate)
    ├── Models/               # Employee, Department, Role, User, Enums
    ├── Properties/           # launchSettings.json (Port 5000)
    ├── Repositories/         # Repository Pattern implementations
    ├── Services/             # Business Logic & JWT Token Services
    ├── Validators/           # FluentValidation rules
    ├── Program.cs            # App configuration & middleware pipeline
    ├── appsettings.json      # Base configuration
    └── appsettings.Development.json # Development secrets & connection string
```

---

## Setup & Running

### 1. Database Configuration
Update your PostgreSQL credentials in `EmployeeAPI/appsettings.Development.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=EmployeeManagement;Username=postgres;Password=YOUR_ACTUAL_PASSWORD"
  }
}
```

### 2. Apply Migrations
```bash
cd EmployeeAPI
dotnet ef database update
```

### 3. Run the API
```bash
cd EmployeeAPI
dotnet restore
dotnet build
dotnet run
```

- **API URL**: `http://localhost:5000`
- **Swagger Docs**: `http://localhost:5000/swagger`

---

## Default Development Accounts

| Role | Email | Password |
|------|-------|----------|
| **Admin** | `admin@company.com` | `Admin@123` |
| **HR** | `hr@company.com` | `Hr@12345` |
| **Manager** | `manager@company.com` | `Manager@123` |
| **Employee** | `employee@company.com` | `Employee@123` |
