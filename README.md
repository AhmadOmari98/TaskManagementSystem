# Task Management System API

A simple **Task Management System API** built with **.NET 8**, demonstrating **Clean Architecture principles**, **role-based access control**, and **in-memory persistence**.

---

## Architecture Overview

```text
TaskManagementSystem
├── TaskManagementSystem.API
│   ├── Controllers
│   ├── Middlewares
│   ├── Attributes
│   └── Swagger
├── TaskManagementSystem.Application
│   ├── Services
│   ├── DTOs
│   ├── Authorization
│   └── Mapping
├── TaskManagementSystem.Domain
│   ├── Entities
│   ├── Enums
│   └── Constants
└── TaskManagementSystem.Infrastructure
    ├── Context
    ├── Repositories
    └── Seed


    ## Features

- CRUD operations for **Users**
- CRUD operations for **Work Items (Tasks)**
- Role-based access control (**Admin / User**)
- Permission-based authorization using **custom middleware**
- In-memory database using **Entity Framework Core**
- Global exception handling middleware
- Centralized logging using **Serilog**
- Swagger / OpenAPI documentation
- Database seeding on application startup
- Clean separation of concerns following **Clean Architecture**

---

## Roles & Permissions

### Roles
- **Admin**
- **User**

### Permissions Matrix

| Permission | Admin | User |
|-----------|-------|------|
| User.Search | Yes | No |
| User.View | Yes | Yes (self only) |
| User.Create | Yes | No |
| User.Update | Yes | No |
| User.Delete | Yes | No |
| WorkItem.Search | Yes | Yes (assigned only) |
| WorkItem.View | Yes | Yes (assigned only) |
| WorkItem.Create | Yes | No |
| WorkItem.Update | Yes | No |
| WorkItem.UpdateStatus | Yes | Yes |
| WorkItem.Delete | Yes | No |

---

## Authentication & Authorization

This API uses **custom header-based authentication** (no JWT).

### Required Headers (for every request)

| Header | Description |
|------|------------|
| `LoggedIn-UserId` | ID of the current user |
| `LoggedIn-UserRole` | Role of the current user (`Admin` or `User`) |

### Example (Admin)

```text
LoggedIn-UserId: 1
LoggedIn-UserRole: Admin
