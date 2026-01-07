# Task Management System API

A robust, enterprise-grade Task Management System API built with .NET 8, implementing Clean Architecture principles with comprehensive role-based access control, logging, and testing.

## 📋 Table of Contents

- [Overview](#overview)
- [Features](#features)
- [Architecture](#architecture)
- [Technologies Used](#technologies-used)
- [Project Structure](#project-structure)
- [Prerequisites](#prerequisites)
- [Getting Started](#getting-started)
- [API Documentation](#api-documentation)
- [Authentication & Authorization](#authentication--authorization)
- [Database Seeding](#database-seeding)
- [Testing](#testing)
- [Logging](#logging)
- [API Endpoints](#api-endpoints)
- [Example Requests](#example-requests)

## 🎯 Overview

This Task Management System API provides a complete solution for managing users and tasks with sophisticated role-based access control. The system enforces strict permission-based authorization, ensuring that users can only access and modify resources according to their assigned roles.

### Key Highlights

- ✅ **Clean Architecture** - Separation of concerns with clear layer boundaries
- ✅ **Role-Based Access Control** - Fine-grained permissions for Admin and User roles
- ✅ **Comprehensive Testing** - Unit tests for services and controllers
- ✅ **Structured Logging** - Serilog integration with file and console logging
- ✅ **API Documentation** - Swagger/OpenAPI integration
- ✅ **In-Memory Database** - Entity Framework Core with in-memory provider
- ✅ **Dependency Injection** - Full DI container integration
- ✅ **Exception Handling** - Global exception handling middleware
- ✅ **Validation** - Domain-level validation with clear error messages

## ✨ Features

### User Management
- Create, read, update, and delete users
- Search and pagination support
- Role-based access control (Admin/User)
- Email and name validation

### Task (WorkItem) Management
- Create, read, update, and delete tasks
- Status management (New, Pending, InProgress, Completed)
- Task assignment to users
- Automatic reference code generation
- Role-based access restrictions:
  - **Admin**: Full access to all tasks
  - **User**: Can only view and update status of assigned tasks

### Security & Authorization
- Permission-based middleware for access control
- Header-based authentication (simulated)
- Role-based resource filtering
- Automatic permission validation

### Additional Features
- Structured logging with Serilog
- Global exception handling
- Request/response logging
- Database seeding with sample data
- Comprehensive unit tests

## 🏗️ Architecture

The project follows **Clean Architecture** principles with clear separation of concerns:

```
┌─────────────────────────────────────────────────────────┐
│                    API Layer                             │
│  (Controllers, Middlewares, Attributes, Swagger)         │
└──────────────────────┬──────────────────────────────────┘
                       │
┌──────────────────────▼──────────────────────────────────┐
│                Application Layer                         │
│  (Services, DTOs, Mappings, Authorization)              │
└──────────────────────┬──────────────────────────────────┘
                       │
┌──────────────────────▼──────────────────────────────────┐
│                  Domain Layer                            │
│  (Entities, Enums, Constants, Interfaces)               │
└──────────────────────┬──────────────────────────────────┘
                       │
┌──────────────────────▼──────────────────────────────────┐
│              Infrastructure Layer                         │
│  (DbContext, Repositories, Entity Configurations)        │
└──────────────────────────────────────────────────────────┘
```

### Layer Responsibilities

- **API Layer**: HTTP endpoints, middleware, request/response handling
- **Application Layer**: Business logic, service orchestration, DTOs
- **Domain Layer**: Core entities, business rules, domain validation
- **Infrastructure Layer**: Data access, repository implementations, database context

## 🛠️ Technologies Used

- **.NET 8.0** - Latest .NET framework
- **ASP.NET Core** - Web API framework
- **Entity Framework Core** - ORM with In-Memory Database
- **Serilog** - Structured logging
- **Swashbuckle (Swagger)** - API documentation
- **xUnit** - Unit testing framework
- **Moq** - Mocking framework for tests
- **Dependency Injection** - Built-in .NET DI container

## 📁 Project Structure

```
TaskManagementSystem/
├── TaskManagementSystem.API/              # API Layer
│   ├── Controllers/                        # API Controllers
│   │   ├── BaseApiController.cs
│   │   ├── UsersController.cs
│   │   └── WorkItemsController.cs
│   ├── Middlewares/                       # Custom Middleware
│   │   ├── ExceptionHandlingMiddleware.cs
│   │   ├── PermissionMiddleware.cs
│   │   └── SaveChangesMiddleware.cs
│   ├── Attributes/                        # Custom Attributes
│   │   └── RequirePermissionAttribute.cs
│   ├── Swagger/                           # Swagger Configuration
│   │   └── AddHeadersOperationFilter.cs
│   └── Program.cs                         # Application Entry Point
│
├── TaskManagementSystem.Application/       # Application Layer
│   ├── Services/                          # Business Logic Services
│   ├── DTOs/                              # Data Transfer Objects
│   ├── Mapping/                           # AutoMapper Profiles
│   └── Authorization/                     # Permission Definitions
│
├── TaskManagementSystem.Domain/           # Domain Layer
│   ├── Entities/                          # Domain Entities
│   ├── Enums/                             # Domain Enums
│   ├── Constants/                         # Domain Constants
│   └── Interface/                         # Domain Interfaces
│
├── TaskManagementSystem.Infrastructure/    # Infrastructure Layer
│   ├── Context/                           # DbContext
│   ├── Repositories/                      # Repository Implementations
│   ├── EntitiesConfigurations/            # EF Core Configurations
│   └── Seed/                              # Database Seeding
│
└── TaskManagementSystem.Tests/            # Test Project
    ├── API/                               # Controller Tests
    └── Application/                       # Service Tests
```

## 📦 Prerequisites

Before running this project, ensure you have the following installed:

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) or later
- [Visual Studio 2022](https://visualstudio.microsoft.com/) (recommended) or [Visual Studio Code](https://code.visualstudio.com/)
- Any REST client (Postman, Insomnia, or Swagger UI) for API testing

## 🚀 Getting Started

### 1. Clone the Repository

```bash
git clone <repository-url>
cd TaskManagementSystem
```

### 2. Restore Dependencies

```bash
dotnet restore
```

### 3. Build the Solution

```bash
dotnet build
```

### 4. Run the Application

```bash
cd TaskManagementSystem.API
dotnet run
```

Alternatively, you can run the project from Visual Studio by pressing `F5` or using the debugger.

### 5. Access the API

Once the application is running, you can access:

- **Swagger UI**: `https://localhost:<port>/swagger` (or `http://localhost:<port>/swagger` for HTTP)
- **API Base URL**: `https://localhost:<port>/api`

> **Note**: The actual port number will be displayed in the console when you run the application. Check the `launchSettings.json` file for default ports.

## 📚 API Documentation

The API is fully documented using Swagger/OpenAPI. Once the application is running:

1. Navigate to `/swagger` in your browser
2. Explore all available endpoints
3. Test endpoints directly from the Swagger UI
4. View request/response schemas

### Swagger Features

- Interactive API testing
- Request/response examples
- Schema definitions
- Header requirements documentation

## 🔐 Authentication & Authorization

### Authentication Mechanism

The API uses **header-based authentication** (simulated for this assignment). Each request must include the following headers:

- `LoggedIn-UserId`: The ID of the authenticated user (integer)
- `LoggedIn-UserRole`: The role of the authenticated user (`Admin` or `User`)

### Example Headers

```http
LoggedIn-UserId: 1
LoggedIn-UserRole: Admin
```

### Authorization Model

The system implements **permission-based authorization** with role mapping:

#### Admin Permissions
- ✅ Full access to all user operations (Create, Read, Update, Delete, Search)
- ✅ Full access to all task operations (Create, Read, Update, Delete, Search)
- ✅ Can view and manage all tasks regardless of assignment

#### User Permissions
- ✅ View own user profile
- ✅ View tasks assigned to them
- ✅ Update status of assigned tasks
- ❌ Cannot create, update, or delete tasks
- ❌ Cannot view tasks assigned to other users
- ❌ Cannot manage other users

### Permission Middleware

The `PermissionMiddleware` automatically:
1. Validates required headers
2. Extracts user context
3. Checks endpoint permissions
4. Enforces role-based access control
5. Returns `401 Unauthorized` or `403 Forbidden` when access is denied

## 🌱 Database Seeding

The application automatically seeds the database with sample data on startup:

### Seeded Users

1. **Admin User**
   - ID: 1
   - Name: "Admin User"
   - Email: "admin@test.com"
   - Role: Admin

2. **Normal User**
   - ID: 2
   - Name: "Normal User"
   - Email: "user@test.com"
   - Role: User

### Seeded Tasks

The system seeds 3 sample tasks:
- "Setup project" (assigned to Admin)
- "Create users module" (assigned to Normal User)
- "Create workItems module" (assigned to Normal User)

> **Note**: Since the database is in-memory, all data is reset when the application restarts.

## 🧪 Testing

### Running Tests

Execute all tests using the following command:

```bash
dotnet test
```

### Test Coverage

The test suite includes:

- **Service Layer Tests**: Business logic validation
- **Controller Tests**: API endpoint behavior verification
- **Mocking**: Dependency mocking using Moq framework

### Test Structure

```
TaskManagementSystem.Tests/
├── API/
│   └── UsersControllerTests.cs        # Controller unit tests
└── Application/
    └── UserServiceTests.cs             # Service layer unit tests
```

### Example Test Execution

```bash
# Run all tests
dotnet test

# Run tests with detailed output
dotnet test --verbosity normal

# Run tests for a specific project
dotnet test TaskManagementSystem.Tests/TaskManagementSystem.Tests.csproj
```

## 📝 Logging

The application uses **Serilog** for structured logging:

### Log Outputs

- **Console**: Real-time log output during development
- **File**: Daily rolling log files in `Logs/` directory
  - Format: `log-YYYYMMDD.txt`
  - Retention: 14 days

### Log Levels

- **Information**: General application flow
- **Warning**: Potential issues or denied access
- **Error**: Exceptions and errors

### Log Locations

- Console: Standard output
- Files: `TaskManagementSystem.API/Logs/`

## 📡 API Endpoints

### Users Endpoints

| Method | Endpoint | Permission | Description |
|--------|----------|------------|-------------|
| `POST` | `/api/users/search` | `User.Search` | Search and paginate users |
| `GET` | `/api/users/{id}` | `User.View` | Get user by ID |
| `POST` | `/api/users` | `User.Create` | Create a new user |
| `PUT` | `/api/users` | `User.Update` | Update user details |
| `DELETE` | `/api/users/{id}` | `User.Delete` | Delete a user |

### WorkItems (Tasks) Endpoints

| Method | Endpoint | Permission | Description |
|--------|----------|------------|-------------|
| `POST` | `/api/workitems/search` | `WorkItem.Search` | Search and paginate tasks |
| `GET` | `/api/workitems/{id}` | `WorkItem.View` | Get task by ID |
| `POST` | `/api/workitems` | `WorkItem.Create` | Create a new task (Admin only) |
| `PUT` | `/api/workitems` | `WorkItem.Update` | Update task details (Admin only) |
| `PATCH` | `/api/workitems/{id}/status` | `WorkItem.UpdateStatus` | Update task status |
| `DELETE` | `/api/workitems/{id}` | `WorkItem.Delete` | Delete a task (Admin only) |

### Access Control Summary

| Operation | Admin | User |
|-----------|-------|------|
| View All Users | ✅ | ❌ |
| View Own Profile | ✅ | ✅ |
| Create User | ✅ | ❌ |
| Update User | ✅ | ❌ |
| Delete User | ✅ | ❌ |
| View All Tasks | ✅ | ❌ |
| View Assigned Tasks | ✅ | ✅ |
| Create Task | ✅ | ❌ |
| Update Task (Full) | ✅ | ❌ |
| Update Task Status | ✅ | ✅ (Own tasks only) |
| Delete Task | ✅ | ❌ |

## 💡 Example Requests

### 1. Create a User (Admin Only)

```http
POST /api/users
Content-Type: application/json
LoggedIn-UserId: 1
LoggedIn-UserRole: Admin

{
  "name": "John Doe",
  "email": "john.doe@example.com",
  "role": "User"
}
```

### 2. Get User by ID

```http
GET /api/users/1
LoggedIn-UserId: 1
LoggedIn-UserRole: Admin
```

### 3. Search Users with Pagination

```http
POST /api/users/search
Content-Type: application/json
LoggedIn-UserId: 1
LoggedIn-UserRole: Admin

{
  "pageNumber": 1,
  "pageSize": 10,
  "filter": {
    "name": null,
    "email": null,
    "role": null
  }
}
```

### 4. Create a Task (Admin Only)

```http
POST /api/workitems
Content-Type: application/json
LoggedIn-UserId: 1
LoggedIn-UserRole: Admin

{
  "title": "Implement new feature",
  "description": "Add user authentication",
  "assignedUserId": 2
}
```

### 5. Get Task by ID

**As Admin** (can view any task):
```http
GET /api/workitems/1
LoggedIn-UserId: 1
LoggedIn-UserRole: Admin
```

**As User** (can only view assigned tasks):
```http
GET /api/workitems/2
LoggedIn-UserId: 2
LoggedIn-UserRole: User
```

### 6. Update Task Status (User can update own tasks)

```http
PATCH /api/workitems/2/status?status=InProgress
LoggedIn-UserId: 2
LoggedIn-UserRole: User
```

### 7. Search Tasks

**As Admin** (sees all tasks):
```http
POST /api/workitems/search
Content-Type: application/json
LoggedIn-UserId: 1
LoggedIn-UserRole: Admin

{
  "pageNumber": 1,
  "pageSize": 10,
  "filter": {
    "title": null,
    "status": null,
    "assignedUserId": null
  }
}
```

**As User** (sees only assigned tasks):
```http
POST /api/workitems/search
Content-Type: application/json
LoggedIn-UserId: 2
LoggedIn-UserRole: User

{
  "pageNumber": 1,
  "pageSize": 10,
  "filter": {
    "title": null,
    "status": null,
    "assignedUserId": null
  }
}
```

## 🔧 Configuration

### Application Settings

The application configuration is stored in `appsettings.json`:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

### Database Configuration

The application uses an **in-memory database** configured in `Program.cs`:

```csharp
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("TaskManagementSystemDb"));
```

> **Note**: Data is persisted only during the application's lifetime. Restarting the application will reset all data.

## 🎓 Design Patterns & Best Practices

### Patterns Implemented

- **Repository Pattern**: Data access abstraction
- **Dependency Injection**: Loose coupling and testability
- **Middleware Pattern**: Cross-cutting concerns
- **DTO Pattern**: Data transfer objects for API contracts
- **Domain-Driven Design**: Rich domain models with business logic

### Best Practices

- ✅ Separation of concerns
- ✅ Single Responsibility Principle
- ✅ Dependency Inversion Principle
- ✅ Domain validation
- ✅ Exception handling
- ✅ Structured logging
- ✅ API versioning ready
- ✅ Comprehensive error responses

## 📄 License

This project is developed as part of a technical assessment for Genovation AI.

## 👤 Author

Developed as a technical assessment demonstrating proficiency in:
- .NET 8 and ASP.NET Core
- Clean Architecture
- Entity Framework Core
- RESTful API design
- Role-based access control
- Unit testing
- API documentation

---

## 🚀 Quick Start Summary

```bash
# 1. Clone and navigate
git clone <repository-url>
cd TaskManagementSystem

# 2. Restore and build
dotnet restore
dotnet build

# 3. Run the API
cd TaskManagementSystem.API
dotnet run

# 4. Access Swagger
# Open browser: https://localhost:<port>/swagger

# 5. Run tests
dotnet test
```

**Default Seeded Users for Testing:**
- Admin: `LoggedIn-UserId: 1`, `LoggedIn-UserRole: Admin`
- User: `LoggedIn-UserId: 2`, `LoggedIn-UserRole: User`

---

For detailed API documentation, please refer to the Swagger UI at `/swagger` when the application is running.
