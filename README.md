# Task Management System API

A robust, enterprise-grade Task Management System API built with .NET 8, implementing Clean Architecture principles with comprehensive role-based access control, logging, and testing.

## Overview

This Task Management System API provides a complete solution for managing users and workItems with sophisticated role-based access control. The system enforces strict permission-based authorization, ensuring that users can only access and modify resources according to their assigned roles.

### Key Highlights

- **Clean Architecture** - Separation of concerns with clear layer boundaries
- **Role-Based Access Control** - Fine-grained permissions for Admin and User roles
- **Comprehensive Testing** - Unit tests for services and controllers
- **Structured Logging** - Serilog integration with file and console logging
- **API Documentation** - Swagger/OpenAPI integration
- **In-Memory Database** - Entity Framework Core with in-memory provider
- **Dependency Injection** - Full DI container integration
- **Exception Handling** - Global exception handling middleware
- **Validation** - Domain-level validation with clear error messages

## Features

### User Management
- Create, read, update, and delete users
- Search and pagination support
- Role-based access control (Admin/User)
- Email and name validation

### WorkItem (Task) Management
- Create, read, update, and delete workItems
- Status management (New, Pending, InProgress, Completed)
- WorkItem assignment to users
- Automatic reference code generation
- Role-based access restrictions:
  - **Admin**: Full access to all workItems
  - **User**: Can only view and update status of assigned workItems

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

## Architecture

The project follows **Clean Architecture** principles with clear separation of concerns and dependency flow:

### Architecture Layers & Dependencies

```
┌─────────────────────────────────────────────────────────┐
│                    API Layer                             │
│  (Controllers, Middlewares, Attributes, Swagger)         │
│                                                          │
│  Dependencies: → Application                            │
└──────────────────────┬──────────────────────────────────┘
                       │ depends on
                       ▼
┌─────────────────────────────────────────────────────────┐
│                Application Layer                         │
│  (Services, DTOs, Mappings, Authorization)              │
│                                                          │
│  Dependencies: → Infrastructure                          │
└──────────────────────┬──────────────────────────────────┘
                       │ depends on
                       ▼
┌─────────────────────────────────────────────────────────┐
│              Infrastructure Layer                         │
│  (DbContext, Repositories, Entity Configurations)        │
│                                                          │
│  Dependencies: → Domain                                   │
└──────────────────────┬──────────────────────────────────┘
                       │ depends on
                       ▼
┌─────────────────────────────────────────────────────────┐
│                  Domain Layer                            │
│  (Entities, Enums, Constants, Interfaces)               │
│                                                          │
│  Dependencies: None (Core Layer)                         │
└──────────────────────────────────────────────────────────┘
```

### Dependency Flow

**API → Application → Infrastructure → Domain**

- **API Layer** depends on **Application Layer**
- **Application Layer** depends on **Infrastructure Layer**
- **Infrastructure Layer** depends on **Domain Layer**
- **Domain Layer** has no dependencies (pure business logic)

### Layer Responsibilities

- **API Layer**: HTTP endpoints, middleware, request/response handling, Swagger configuration
- **Application Layer**: Business logic orchestration, service implementations, DTOs, mappings, authorization logic
- **Infrastructure Layer**: Data access implementations, repository pattern, Entity Framework configurations, database context
- **Domain Layer**: Core business entities, domain rules, validation logic, enums, constants, repository interfaces

## Technologies Used

- **.NET 8.0** - Latest .NET framework
- **ASP.NET Core** - Web API framework
- **Entity Framework Core** - ORM with In-Memory Database
- **Serilog** - Structured logging
- **Swashbuckle (Swagger)** - API documentation
- **xUnit** - Unit testing framework
- **Moq** - Mocking framework for tests
- **Dependency Injection** - Built-in .NET DI container

##  Project Structure

```
TaskManagementSystem/
│
├── TaskManagementSystem.API/              # API Layer (depends on Application)
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
├── TaskManagementSystem.Application/       # Application Layer (depends on Infrastructure)
│   ├── Services/                          # Business Logic Services
│   │   ├── Interface/                     # Service Interfaces
│   │   └── Implementation/                # Service Implementations
│   ├── DTOs/                              # Data Transfer Objects
│   │   ├── Request/                       # Request DTOs
│   │   ├── Response/                     # Response DTOs
│   │   └── Filter/                        # Filter DTOs
│   ├── Mapping/                           # Entity-DTO Mappings
│   ├── Authorization/                     # Permission Definitions
│   └── DependencyInjection_AddServices.cs # DI Registration
│
├── TaskManagementSystem.Infrastructure/    # Infrastructure Layer (depends on Domain)
│   ├── Context/                           # DbContext
│   │   └── ApplicationDbContext.cs
│   ├── Repositories/                      # Repository Implementations
│   │   └── Repository.cs                  # Generic Repository
│   ├── EntitiesConfigurations/            # EF Core Configurations
│   │   ├── UserConfigration.cs
│   │   └── WorkItemConfigration.cs
│   ├── Seed/                              # Database Seeding
│   │   └── DbSeeder.cs
│   └── DependencyInjection_AddRepositories.cs # DI Registration
│
├── TaskManagementSystem.Domain/           # Domain Layer (No Dependencies)
│   ├── Entities/                          # Domain Entities
│   │   ├── DomainEntity.cs                # Base Entity
│   │   ├── User.cs
│   │   └── WorkItem.cs
│   ├── Enums/                             # Domain Enums
│   │   └── Enums.cs                       # UserRole, WorkItemStatus
│   ├── Constants/                         # Domain Constants
│   │   ├── UserConstraints.cs
│   │   └── WorkItemConstraints.cs
│   └── Interface/                         # Domain Interfaces
│       └── Repositories/
│           └── IRepository.cs             # Repository Interface
│
└── TaskManagementSystem.Tests/            # Test Project
    ├── API/                               # Controller Tests
    │   └── UsersControllerTests.cs
    └── Application/                       # Service Tests
        └── UserServiceTests.cs
```

### Dependency Relationships

```
API Layer
  └─→ Application Layer
        └─→ Infrastructure Layer
              └─→ Domain Layer (Core - No Dependencies)
```

## Prerequisites

Before running this project, ensure you have the following installed:

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) or later
- [Visual Studio 2022](https://visualstudio.microsoft.com/) (recommended) or [Visual Studio Code](https://code.visualstudio.com/)
- Any REST client (Postman or Swagger UI) for API testing

## Getting Started

### 1. Clone the Repository

```bash

git clone https://github.com/AhmadOmari98/TaskManagementSystem
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

- **Swagger UI**: `https://localhost:7027/swagger/index.html`
- **API Base URL**: `https://localhost:7027/api`

> **Note**: The actual port number will be displayed in the console when you run the application. Check the `launchSettings.json` file for default ports.

## API Documentation

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

## Authentication & Authorization

### Authentication Mechanism

The API uses **header-based authentication** (simulated for this assignment). Each request must include the following headers:

- `LoggedIn-UserId`: The ID of the authenticated user (integer)
- `LoggedIn-UserRole`: The role of the authenticated user (`Admin` or `User`)

### Example Headers

```http
LoggedIn-UserId: 1
LoggedIn-UserRole: Admin
```

```http
LoggedIn-UserId: 2
LoggedIn-UserRole: User
```

### Authorization Model

The system implements **permission-based authorization** with role mapping:

#### Admin Permissions
- Full access to all user operations (Create, Read, Update, Delete, Search)
- Full access to all workItem operations (Create, Read, Update, Delete, Search)
- Can view and manage all workItems regardless of assignment

#### User Permissions
- View own user profile
- View workItems assigned to them
- Update status of assigned workItems
- Cannot create, update, or delete workItems
- Cannot view workItems assigned to other users
- Cannot manage other users

### Permission Middleware

The `PermissionMiddleware` automatically:
1. Validates required headers
2. Extracts user context
3. Checks endpoint permissions
4. Enforces role-based access control
5. Returns `401 Unauthorized` or `403 Forbidden` when access is denied

## Database Seeding

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
  
**User Role**
- 1 => Admin
- 2 => User

### Seeded WorkItems

1. **Setup project**
   - ID: 1
   - Title: "Setup project"
   - Description: "Initial project setup"
   - Status: New
   - ReferenceCode: "WI-20260108104636938-B0E8576"
   - AssignedUserId: 1

1. **Create users module**
   - ID: 2
   - Title: "Create users module"
   - Description: "Implement users CRUD"
   - Status: New
   - ReferenceCode: "WI-20260108104636952-65D24A2"
   - AssignedUserId: 2
  
1. **Create users module**
   - ID: 3
   - Title: "Create workItems module"
   - Description: "Implement workItems CRUD"
   - Status: New
   - ReferenceCode: "WI-20260108104636952-5422B49"
   - AssignedUserId: 2

      
**WorkItem Status**
- 1 => New
- 2 => Pending
- 3 => InProgress
- 4 => Completed
  
> **Note**: Since the database is in-memory, all data is reset when the application restarts.

## Testing

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

## Logging

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

## API Endpoints

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
| `POST` | `/api/workitems/search` | `WorkItem.Search` | Search and paginate workItems |
| `GET` | `/api/workitems/{id}` | `WorkItem.View` | Get workItem by ID |
| `POST` | `/api/workitems` | `WorkItem.Create` | Create a new workItem (Admin only) |
| `PUT` | `/api/workitems` | `WorkItem.Update` | Update workItem details (Admin only) |
| `PATCH` | `/api/workitems/{id}/status` | `WorkItem.UpdateStatus` | Update workItem status |
| `DELETE` | `/api/workitems/{id}` | `WorkItem.Delete` | Delete a workItem (Admin only) |

### Access Control Summary

| Operation | Admin | User |
|-----------|-------|------|
| View All Users | Yes | No |
| View Own Profile | Yes | Yes |
| Create User | Yes | No |
| Update User | Yes | No |
| Delete User | Yes | No |
| View All workItems | Yes | No |
| View Assigned workItems | Yes | Yes |
| Create workItem | Yes | No |
| Update workItem (Full) | Yes | No |
| Update workItem Status | Yes | Yes (Own workItems only) |
| Delete workItem | Yes | No |

## Example Requests

### 1. Create a User (Admin Only)

```http
POST /api/users
Content-Type: application/json
LoggedIn-UserId: 1
LoggedIn-UserRole: Admin

{
  "name": "Normal User 2",
  "email": "user2@test.com",
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

### 4. Create a workItem (Admin Only)

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

### 5. Get workItem by ID

**As Admin** (can view any workItem):
```http
GET /api/workitems/1
LoggedIn-UserId: 1
LoggedIn-UserRole: Admin
```

**As User** (can only view assigned workItems):
```http
GET /api/workitems/2
LoggedIn-UserId: 2
LoggedIn-UserRole: User
```

### 6. Update WorkItem Status (User can update own workItems)

```http
PATCH /api/workitems/2/status?status=InProgress
LoggedIn-UserId: 2
LoggedIn-UserRole: User
```

### 7. Search WorkItems

**As Admin** (sees all workItems):
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
    "referenceCode": null,
    "status": null,
    "assignedUserId": null
  }
}
```

**As User** (sees only assigned workItems):
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
    "referenceCode": null,
    "status": null,
    "assignedUserId": null
  }
}
```

## Configuration

### Database Configuration

The application uses an **in-memory database** configured in `Program.cs`:

```csharp
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("TaskManagementSystemDb"));
```

> **Note**: Data is persisted only during the application's lifetime. Restarting the application will reset all data.

## Design Patterns & Best Practices

### Architecture Pattern: Clean Architecture

The project implements **Clean Architecture** with the following dependency flow:

```
┌─────────────┐
│  API Layer  │  ← Entry Point (HTTP Requests)
└──────┬──────┘
       │ References
       ▼
┌──────────────────┐
│ Application      │  ← Business Logic Orchestration
│ Layer            │
└──────┬───────────┘
       │ References
       ▼
┌──────────────────┐
│ Infrastructure   │  ← Data Access & External Services
│ Layer            │
└──────┬───────────┘
       │ References
       ▼
┌──────────────────┐
│ Domain Layer     │  ← Core Business Logic (No Dependencies)
│ (Core)           │
└──────────────────┘
```

### Dependency Rules

1. **API Layer** → References **Application Layer** only
2. **Application Layer** → References **Infrastructure Layer** only
3. **Infrastructure Layer** → References **Domain Layer** only
4. **Domain Layer** → No dependencies (Pure C# code)

### Patterns Implemented

- **Repository Pattern**: Data access abstraction via `IRepository<T>` interface in Domain, implementation in Infrastructure
- **Dependency Injection**: Full DI container integration for loose coupling and testability
- **Middleware Pattern**: Cross-cutting concerns (Exception handling, Permissions, SaveChanges)
- **DTO Pattern**: Data transfer objects for API contracts, separation between API and Domain models
- **Domain-Driven Design**: Rich domain models with encapsulated business logic and validation

### Best Practices

- **Separation of concerns** - Each layer has a single, well-defined responsibility
- **Single Responsibility Principle** - Classes and methods have focused responsibilities
- **Dependency Inversion Principle** - Dependencies point inward toward Domain
- **Domain validation** - Business rules enforced at domain entity level
- **Exception handling** - Global exception handling middleware
- **Structured logging** - Comprehensive logging with Serilog
- **Repository abstraction** - Data access abstracted through interfaces
- **Comprehensive error responses** - Clear error messages for API consumers

---

## Quick Start Summary

```bash
# 1. Clone and navigate
git clone https://github.com/AhmadOmari98/TaskManagementSystem
cd TaskManagementSystem

# 2. Restore and build
dotnet restore
dotnet build

# 3. Run the API
cd TaskManagementSystem.API
dotnet run

# 4. Access Swagger
# Open browser: https://localhost:7027/swagger/index.html

# 5. Run tests
dotnet test
```

**Default Seeded Users for Testing:**
- Admin: `LoggedIn-UserId: 1`, `LoggedIn-UserRole: Admin`
- User: `LoggedIn-UserId: 2`, `LoggedIn-UserRole: User`

---

For detailed API documentation, please refer to the Swagger UI at `/swagger` when the application is running.
