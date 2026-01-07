# Task Management System API

A simple Task Management System API built with .NET 8, demonstrating clean architecture principles, role-based access control, and in-memory persistence.

//-----------------------------------------------------------------------Architecture Overview:

TaskManagementSystem
│
├── TaskManagementSystem.API
│   ├── Controllers
│   ├── Middlewares
│   ├── Attributes
│   └── Swagger
│
├── TaskManagementSystem.Application
│   ├── Services
│   ├── DTOs
│   ├── Authorization
│   └── Mapping
│
├── TaskManagementSystem.Domain
│   ├── Entities
│   ├── Enums
│   └── Constants
│
├── TaskManagementSystem.Infrastructure
│   ├── Context
│   ├── Repositories
│   └── Seed


//-----------------------------------------------------------------------Features:

CRUD operations for Users and Work Items
Role-based access control (Admin / User)
Permission-based authorization using custom middleware
In-memory database (Entity Framework Core)
Global exception handling
Centralized logging using Serilog
Swagger / OpenAPI documentation
Database seeding
Clean separation of concerns

//-----------------------------------------------------------------------Roles & Permissions

Roles:
Admin
User

Permissions Mapping
Permission	            Admin	    User
User.Search	             Yes	     No
User.View	               Yes	     Yes (self only)
User.Create	             Yes	     No
User.Update	             Yes	     No
User.Delete	             Yes	     No
WorkItem.Search	         Yes	     Yes (assigned only)
WorkItem.View	           Yes	     Yes (assigned only)
WorkItem.Create	         Yes	     No
WorkItem.Update	         Yes	     No
WorkItem.UpdateStatus	   Yes	     Yes
WorkItem.Delete	         Yes	     No

//-----------------------------------------------------------------------Authentication & Authorization:

This API uses custom header-based authentication.
Required Headers (for every request)

Header	                 Description
LoggedIn-UserId	         ID of the current user
LoggedIn-UserRole	       Role of the current user (Admin or User)

Example (Admin)
LoggedIn-UserId: 1
LoggedIn-UserRole: Admin

Example (User)
LoggedIn-UserId: 2
LoggedIn-UserRole: User

//-----------------------------------------------------------------------Seeded Data:

On application startup, the in-memory database is seeded with:

Users:
1)
{
  "id": 1,
  "name": "Admin User",
  "email": "admin@test.com",
  "role": 1 // Admin
}

2)
{
  "id": 2,
  "name": "Normal User",
  "email": "user@test.com",
  "role": 2 // User
}

UserRole:
1 => Admin
2 => User

Work Items (Tasks):
1)
{
   "id": 2,
   "title": "Create users module",
   "description": "Implement users CRUD",
   "status": 1,
   "referenceCode": "WI-20260107224703098-2EAF629",
   "assignedUserId": 2,
   "assignedUser": {
      "id": 2,
      "name": "Normal User",
      "email": "user@test.com",
      "role": 2
      }
}

2)
{
   "id": 1,
   "title": "Setup project",
   "description": "Initial project setup",
   "status": 1,
   "referenceCode": "WI-20260107224703081-1A78DA9",
   "assignedUserId": 1,
   "assignedUser": {
      "id": 1,
      "name": "Admin User",
      "email": "admin@test.com",
      "role": 1
    }
}

3)
{
   "id": 3,
   "title": "Create workItems module",
   "description": "Implement workItems CRUD",
   "status": 1,
   "referenceCode": "WI-20260107224703098-123EA92",
   "assignedUserId": 2,
   "assignedUser": {
      "id": 2,
      "name": "Normal User",
      "email": "user@test.com",
      "role": 2
    }
}

WorkItemStatus
1 => New
2 => Pending
3 => InProgress
4 => Completed

//-----------------------------------------------------------------------Testing:
The solution supports unit testing and includes:
Service-layer test coverage (business rules)
Controller-level test coverage

//-----------------------------------------------------------------------API Documentation (Swagger):
Swagger is enabled and available at:
https://localhost:7027/swagger/index.html

Swagger UI automatically documents:
All endpoints
Required headers
Request / response models

//-----------------------------------------------------------------------How to Run the Project:
Prerequisites
.NET 8 SDK
Visual Studio / VS Code

Steps:
git clone https://github.com/AhmadOmari98/TaskManagementSystem.git
cd TaskManagementSystem
dotnet restore
dotnet run

Then open:
https://localhost:7027/swagger/index.html
