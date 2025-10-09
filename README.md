# ElsheenaEats Backend API

<div align="center">

![.NET Core](https://img.shields.io/badge/.NET-8.0-512BD4?style=flat-square&logo=.net)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-316192?style=flat-square&logo=postgresql&logoColor=white)
![Entity Framework](https://img.shields.io/badge/Entity%20Framework-512BD4?style=flat-square&logo=.net)
![JWT](https://img.shields.io/badge/JWT-000000?style=flat-square&logo=JSON%20web%20tokens)
![Swagger](https://img.shields.io/badge/Swagger-85EA2D?style=flat-square&logo=swagger&logoColor=black)
![Academic](https://img.shields.io/badge/Academic-Project-blue?style=flat-square&logo=graduation-cap)

*Backend Study Project #1 - Food Delivery Service API*

**Backend Course | 3rd Year, 1st Semester**

[🚀 Getting Started](https://github.com/elsheena/ElsheenaEats-Backend?tab=readme-ov-file#-getting-started) • [📖 API Documentation](https://github.com/elsheena/ElsheenaEats-Backend?tab=readme-ov-file#-api-documentation) • [🏗️ Architecture](https://github.com/elsheena/ElsheenaEats-Backend?tab=readme-ov-file#-architecture) • [📋 Requirements](https://github.com/elsheena/ElsheenaEats-Backend?tab=readme-ov-file#-project-requirements)

</div>

## 📋 Table of Contents

- [Overview](https://github.com/elsheena/ElsheenaEats-Backend?tab=readme-ov-file#-overview)
- [Features](https://github.com/elsheena/ElsheenaEats-Backend?tab=readme-ov-file#-features)
- [Technology Stack](https://github.com/elsheena/ElsheenaEats-Backend?tab=readme-ov-file#-technology-stack)
- [Architecture](https://github.com/elsheena/ElsheenaEats-Backend?tab=readme-ov-file#-architecture)
- [Getting Started](https://github.com/elsheena/ElsheenaEats-Backend?tab=readme-ov-file#-getting-started)
- [Configuration](https://github.com/elsheena/ElsheenaEats-Backend?tab=readme-ov-file#-configuration)
- [API Documentation](https://github.com/elsheena/ElsheenaEats-Backend?tab=readme-ov-file#-api-documentation)
- [Database Schema](https://github.com/elsheena/ElsheenaEats-Backend?tab=readme-ov-file#-database-schema)
- [Authentication](https://github.com/elsheena/ElsheenaEats-Backend?tab=readme-ov-file#-authentication)
- [Development Workflow](https://github.com/elsheena/ElsheenaEats-Backend?tab=readme-ov-file#-development-workflow)
- [Testing](https://github.com/elsheena/ElsheenaEats-Backend?tab=readme-ov-file#-testing)
- [Deployment](https://github.com/elsheena/ElsheenaEats-Backend?tab=readme-ov-file#-deployment)
- [Project Requirements](https://github.com/elsheena/ElsheenaEats-Backend?tab=readme-ov-file#-project-requirements)
- [Subject Domain](https://github.com/elsheena/ElsheenaEats-Backend?tab=readme-ov-file#-subject-domain)
- [Contributing](https://github.com/elsheena/ElsheenaEats-Backend?tab=readme-ov-file#-contributing)
- [Academic Notes](https://github.com/elsheena/ElsheenaEats-Backend?tab=readme-ov-file#-academic-notes)

## 🍕 Overview

ElsheenaEats Backend is a comprehensive RESTful API system developed as **Backend Study Project #1** for the Backend Development course (3rd year, 1st semester). This academic project implements a complete food delivery service backend with modern .NET technologies, demonstrating proficiency in web API development, database design, authentication, and clean architecture principles.

### 🎓 Academic Context
- **Course**: Backend Development
- **Level**: 3rd Year, 1st Semester 
- **Project Type**: Study Project #1
- **Objective**: Implement working API for food delivery service domain
- **Reference API**: [Food Delivery API Documentation](https://food-delivery.int.kreosoft.space/swagger/index.html)

The system follows Clean Architecture principles with clear separation of concerns, demonstrating best practices in enterprise application development.

## ✨ Features

### 🔐 Authentication & Authorization
- **JWT-based Authentication** with access and refresh tokens
- **Role-based Access Control** (Administrator, Manager, Cook, Customer)
- **ASP.NET Core Identity** integration with custom user model
- **Secure Password Policies** and validation
- **Token Refresh Mechanism** for enhanced security

### 🍽️ Menu Management
- **Full CRUD Operations** for dishes and menu items
- **Advanced Filtering** by category, price range, and dietary preferences
- **Multi-criteria Sorting** (name, price, rating)
- **Pagination Support** for large datasets
- **Category Management** (Pizza, Wok, Soup, Dessert, Drink)

### 📦 Order System
- **Shopping Cart Management** with persistent storage
- **Order Processing** with status tracking
- **Delivery Management** with time and address handling
- **Order History** and tracking for users

### ⭐ Rating & Review System
- **Dish Rating** with 1-5 star system
- **Text Reviews** with user feedback
- **Average Rating Calculation** for menu items
- **User Rating History**

### 🛡️ Security Features
- **Data Protection** with soft deletion
- **Audit Trail** with timestamps on all entities
- **Input Validation** and sanitization
- **HTTPS Enforcement** and security headers

### 📊 Advanced Features
- **Comprehensive Pagination** with metadata
- **Advanced Query Filters** and search capabilities
- **Automated Database Migrations**
- **Data Seeding** for initial setup
- **Swagger/OpenAPI Documentation**

## 🛠️ Technology Stack

### Backend Framework
- **.NET 8.0** - Latest LTS version of .NET
- **ASP.NET Core Web API** - High-performance web framework
- **Entity Framework Core 8.0** - Modern ORM with LINQ support

### Database
- **PostgreSQL 15+** - Robust relational database
- **Npgsql** - High-performance .NET PostgreSQL driver

### Authentication
- **ASP.NET Core Identity** - Identity management framework
- **JWT (JSON Web Tokens)** - Stateless authentication
- **System.IdentityModel.Tokens.Jwt** - JWT implementation

### Documentation & Testing
- **Swagger/OpenAPI 3.0** - Interactive API documentation
- **Swashbuckle.AspNetCore** - Swagger integration for .NET

### Development Tools
- **Visual Studio 2022** / **VS Code** - IDE support
- **Git** - Version control
- **GitHub** - Repository hosting
- **Redmine** - Project management

## 🏗️ Architecture

The project follows **Clean Architecture** principles with clear separation of concerns:

```
ElsheenaEats-Backend/
├── 🌐 API/                          # Presentation Layer
│   ├── Controllers/                 # API Controllers
│   │   ├── AuthController.cs        # Authentication endpoints
│   │   ├── DishesController.cs      # Menu management
│   │   └── ProfileController.cs     # User profile management
│   ├── Configuration/               # Startup configuration
│   │   ├── ConfigureIdentity.cs     # Identity & role setup
│   │   └── SeedData.cs             # Database seeding
│   ├── Properties/
│   ├── appsettings.json            # Application configuration
│   └── Program.cs                  # Application entry point
├── 💼 BusinessLogicLayer/           # Business Logic Layer
│   ├── Services/                   # Business services
│   │   ├── IDishService.cs         # Dish business logic
│   │   └── IUserService.cs         # User & auth logic
│   ├── DTOs/                       # Data Transfer Objects
│   │   ├── DishDetailsDto.cs       # Dish responses
│   │   ├── UserProfileDto.cs       # User responses
│   │   ├── PagedResult.cs          # Pagination wrapper
│   │   └── ...                     # Additional DTOs
│   ├── Configuration/              # Business configuration
│   │   └── JwtBearerTokenSettings.cs
│   └── Constants/                  # Application constants
├── 🎯 Core/                        # Domain Layer
│   ├── Models/                     # Domain entities
│   │   ├── User.cs                 # User entity
│   │   ├── Dish.cs                 # Menu item entity
│   │   ├── Order.cs                # Order entity
│   │   ├── Rating.cs               # Rating entity
│   │   ├── DishInCart.cs           # Shopping cart item
│   │   └── Role.cs                 # User roles
│   └── Common/                     # Shared components
│       ├── IBaseEntity.cs          # Base entity interface
│       └── SortBy.cs               # Sorting enumerations
└── 🗄️ DataAccess/                  # Data Access Layer
    ├── DBContext/
    │   └── ElsheenaDbContext.cs    # EF Core context
    └── Migrations/                 # Database migrations
```

### Layer Dependencies
```
API → BusinessLogicLayer → Core
API → DataAccess → Core
BusinessLogicLayer → DataAccess → Core
```

## 🚀 Getting Started

### Prerequisites

Before you begin, ensure you have the following installed:

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [PostgreSQL 15+](https://www.postgresql.org/download/)
- [Git](https://git-scm.com/downloads)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/elsheena/ElsheenaEats-Backend.git
   cd ElsheenaEats-Backend
   ```

2. **Set up PostgreSQL Database**
   ```sql
   -- Connect to PostgreSQL and create database
   CREATE DATABASE ElsheenaEats;
   CREATE USER elsheena_user WITH PASSWORD 'your_secure_password';
   GRANT ALL PRIVILEGES ON DATABASE ElsheenaEats TO elsheena_user;
   ```

3. **Configure Connection String**
   
   Update `API/appsettings.json`:
   ```json
   {
     "ConnectionStrings": {
       "Default": "Host=localhost;Port=5432;Database=ElsheenaEats;Username=elsheena_user;Password=your_secure_password"
     }
   }
   ```

4. **Install Dependencies**
   ```bash
   dotnet restore
   ```

5. **Run Database Migrations**
   ```bash
   dotnet ef database update --project DataAccess --startup-project API
   ```

6. **Run the Application**
   ```bash
   dotnet run --project API
   ```

7. **Access the API**
   - **API Base URL**: `https://localhost:7001`
   - **Swagger Documentation**: `https://localhost:7001/swagger`

### Quick Start with Docker (Optional)

```dockerfile
# Dockerfile for containerized deployment
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "API.dll"]
```

## ⚙️ Configuration

### Application Settings

The application uses `appsettings.json` for configuration:

```json
{
  "ConnectionStrings": {
    "Default": "Host=localhost;Port=5432;Database=ElsheenaEats;Username=postgres;Password=your_password"
  },
  "JwtBearerTokenSettings": {
    "SecretKey": "YourSecretKeyHere-MustBe256BitsLong-ForHS256Algorithm",
    "ExpiryTimeInMinutes": 60,
    "RefreshTokenExpiryTimeInDays": 7,
    "Audience": "https://localhost:7001",
    "Issuer": "https://localhost:7001"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

### Environment Variables

For production deployment, use environment variables:

```bash
export ConnectionStrings__Default="Host=prod-server;Database=ElsheenaEats;..."
export JwtBearerTokenSettings__SecretKey="your-production-secret-key"
export ASPNETCORE_ENVIRONMENT="Production"
```

### Default Accounts

The system automatically creates the following accounts on first run:

| Role | Email | Password | Description |
|------|-------|----------|-------------|
| Administrator | admin@elsheena.com | Admin123! | System administrator with full access |

## 📖 API Documentation

### Authentication Endpoints

| Method | Endpoint | Description | Auth Required |
|--------|----------|-------------|---------------|
| POST | `/api/auth/register` | Register new user | No |
| POST | `/api/auth/login` | User login | No |
| POST | `/api/auth/refresh-token` | Refresh JWT token | No |
| POST | `/api/auth/logout` | User logout | Yes |

### Dishes Endpoints

| Method | Endpoint | Description | Auth Required | Role Required |
|--------|----------|-------------|---------------|---------------|
| GET | `/api/dishes` | Get dishes with filters | No | - |
| GET | `/api/dishes/{id}` | Get dish details | No | - |
| POST | `/api/dishes` | Create new dish | Yes | Manager+ |
| PUT | `/api/dishes/{id}` | Update dish | Yes | Manager+ |
| DELETE | `/api/dishes/{id}` | Delete dish | Yes | Manager+ |

### Profile Endpoints

| Method | Endpoint | Description | Auth Required |
|--------|----------|-------------|---------------|
| GET | `/api/profile` | Get user profile | Yes |
| PUT | `/api/profile` | Update profile | Yes |

### Query Parameters (GET /api/dishes)

| Parameter | Type | Description | Example |
|-----------|------|-------------|---------|
| `page` | int | Page number (default: 1) | `?page=2` |
| `pageSize` | int | Items per page (default: 10) | `?pageSize=20` |
| `categories` | array | Filter by categories | `?categories=Pizza&categories=Dessert` |
| `isVegetarian` | bool | Filter vegetarian dishes | `?isVegetarian=true` |
| `sortBy` | enum | Sort order | `?sortBy=PriceAsc` |
| `minPrice` | double | Minimum price filter | `?minPrice=5.0` |
| `maxPrice` | double | Maximum price filter | `?maxPrice=25.0` |

### Response Examples

#### Successful Response
```json
{
  "items": [
    {
      "id": "123e4567-e89b-12d3-a456-426614174000",
      "name": "Margherita Pizza",
      "description": "Classic pizza with tomato sauce and mozzarella",
      "price": 12.99,
      "image": "https://example.com/pizza.jpg",
      "isVegetarian": true,
      "averageRating": 4.5,
      "category": 1
    }
  ],
  "totalCount": 25,
  "currentPage": 1,
  "pageSize": 10,
  "totalPages": 3,
  "hasNextPage": true,
  "hasPreviousPage": false
}
```

#### Error Response
```json
{
  "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
  "title": "Bad Request",
  "status": 400,
  "detail": "The request is invalid",
  "errors": {
    "Name": ["The Name field is required."]
  }
}
```

## 🗄️ Database Schema

### Core Entities

#### Users Table
```sql
CREATE TABLE "AspNetUsers" (
    "Id" uuid PRIMARY KEY,
    "FullName" text NOT NULL,
    "Email" text NOT NULL,
    "BirthDate" timestamp,
    "Gender" integer NOT NULL,
    "Address" text NOT NULL,
    "RefreshToken" text,
    "RefreshExpiration" timestamp NOT NULL,
    "CreateDateTime" timestamp NOT NULL,
    "ModifyDateTime" timestamp NOT NULL,
    "DeleteDate" timestamp
);
```

#### Dishes Table
```sql
CREATE TABLE "Dishes" (
    "Id" uuid PRIMARY KEY,
    "Name" text NOT NULL,
    "Description" text NOT NULL,
    "Price" double precision NOT NULL,
    "Image" text NOT NULL,
    "IsVegetarian" boolean NOT NULL,
    "AverageRating" real NOT NULL,
    "Category" integer NOT NULL,
    "CreateDateTime" timestamp NOT NULL,
    "ModifyDateTime" timestamp NOT NULL,
    "DeleteDate" timestamp
);
```

### Entity Relationships

```
User (1) ←→ (N) Orders
User (1) ←→ (N) DishInCart
User (1) ←→ (N) Ratings
Dish (1) ←→ (N) DishInCart
Dish (1) ←→ (N) Ratings
Order (1) ←→ (N) DishInCart
```

### Enumerations

```csharp
public enum Category
{
    Wok = 0,
    Pizza = 1,
    Soup = 2,
    Dessert = 3,
    Drink = 4
}

public enum Gender
{
    Female = 0,
    Male = 1
}

public enum Status
{
    InCart = 0,
    Ordered = 1,
    Kitchen = 2,
    Packaging = 3,
    Delivery = 4,
    Delivered = 5,
    Cancelled = 6
}
```

## 🔐 Authentication

### JWT Token Structure

The API uses JWT tokens with the following claims:

```json
{
  "sub": "user-guid",
  "name": "User Full Name",
  "email": "user@example.com",
  "role": ["Customer"],
  "iat": 1609459200,
  "exp": 1609462800
}
```

### Role Hierarchy

| Role | Permissions | Description |
|------|-------------|-------------|
| **Administrator** | Full system access | System administration |
| **Manager** | Dish management, reports | Restaurant management |
| **Cook** | Order status updates | Kitchen operations |
| **Customer** | Browse, order, rate | End users |

### Authorization Examples

```csharp
// Require authentication
[Authorize]
public class ProfileController : ControllerBase { }

// Require specific role
[Authorize(Roles = "Manager,Administrator")]
public async Task<ActionResult> CreateDish() { }

// Require policy
[Authorize(Policy = "ManagerOnly")]
public async Task<ActionResult> UpdateDish() { }
```

## 🔧 Development Workflow

### Branch Strategy

```
main                    # Production-ready code
├── develop            # Integration branch
├── feature/auth       # Feature branches
├── feature/dishes     # 
├── hotfix/security    # Critical fixes
└── release/v1.0       # Release preparation
```

### Commit Convention

```bash
feat: add JWT refresh token mechanism
fix: resolve pagination edge case
docs: update API documentation
test: add unit tests for DishService
refactor: improve error handling
```

### Development Commands

```bash
# Restore packages
dotnet restore

# Build solution
dotnet build

# Run application
dotnet run --project API

# Run with hot reload
dotnet watch --project API

# Add migration
dotnet ef migrations add MigrationName -p DataAccess -s API

# Update database
dotnet ef database update -p DataAccess -s API

# Generate migration script
dotnet ef migrations script -p DataAccess -s API
```

## 🧪 Testing

### Unit Testing Setup

```bash
# Add test project
dotnet new xunit -n ElsheenaEats.Tests
dotnet sln add ElsheenaEats.Tests

# Add test packages
dotnet add package Microsoft.EntityFrameworkCore.InMemory
dotnet add package Moq
dotnet add package FluentAssertions
```

### Example Test

```csharp
[Fact]
public async Task GetDishesAsync_WithValidQuery_ReturnsPagedResult()
{
    // Arrange
    var options = new DbContextOptionsBuilder<ElsheenaDbContext>()
        .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
        .Options;

    using var context = new ElsheenaDbContext(options);
    var service = new DishService(context);

    // Act
    var result = await service.GetDishListAsync(new DishQueryParamsDto());

    // Assert
    result.Should().NotBeNull();
    result.Items.Should().NotBeNull();
}
```

## 🚀 Deployment

### Production Checklist

- [ ] Update connection strings
- [ ] Set production JWT secret
- [ ] Enable HTTPS
- [ ] Configure logging
- [ ] Set up monitoring
- [ ] Configure CORS
- [ ] Set up backup strategy
- [ ] Configure environment variables

### Docker Deployment

```yaml
# docker-compose.yml
version: '3.8'
services:
  api:
    build: .
    ports:
      - "8080:80"
    environment:
      - ASPNETCORE_ENVIRONMENT=Production
      - ConnectionStrings__Default=Host=db;Database=ElsheenaEats;Username=postgres;Password=password
    depends_on:
      - db
  
  db:
    image: postgres:15
    environment:
      POSTGRES_DB: ElsheenaEats
      POSTGRES_USER: postgres
      POSTGRES_PASSWORD: password
    volumes:
      - postgres_data:/var/lib/postgresql/data

volumes:
  postgres_data:
```

## 🤝 Contributing

We welcome contributions! Please follow these guidelines:

### How to Contribute

1. **Fork** the repository
2. **Create** a feature branch (`git checkout -b feature/amazing-feature`)
3. **Commit** your changes (`git commit -m 'Add amazing feature'`)
4. **Push** to the branch (`git push origin feature/amazing-feature`)
5. **Open** a Pull Request

### Development Setup

1. Clone your fork
2. Install dependencies: `dotnet restore`
3. Create feature branch
4. Make changes
5. Test your changes
6. Submit pull request

### Code Style

- Follow C# coding conventions
- Use meaningful variable names
- Add XML documentation for public APIs
- Write unit tests for new features
- Update documentation as needed

### Issue Reporting

Please use the GitHub issue tracker to report bugs or request features. Include:

- **Environment details** (OS, .NET version, PostgreSQL version)
- **Steps to reproduce** the issue
- **Expected behavior**
- **Actual behavior**
- **Screenshots** if applicable

## 📊 Project Status

### Current Version: 1.0.0

- ✅ **Authentication System** - Complete
- ✅ **Dish Management** - Complete  
- ✅ **User Profiles** - Complete
- ✅ **Database Integration** - Complete
- ✅ **API Documentation** - Complete
- 🚧 **Order System** - In Progress
- 📋 **Rating System** - Planned


## 📞 Support

- **Documentation**: [Wiki](https://science.pm.kreosoft.space/projects/mina-mikhaeil-2025/wiki)
- **Issues**: [GitHub Issues](https://science.pm.kreosoft.space/projects/mina-mikhaeil-2025/issues)


## 👥 Authors

- **Mina Mikhaeil** - *Initial work* - [elsheena](https://github.com/elsheena)

## 🙏 Acknowledgments

- ASP.NET Core team for the excellent framework
- Entity Framework Core team for the robust ORM
- PostgreSQL community for the reliable database
- All contributors who help improve this project

---

<div align="center">

**[⬆ Back to Top](#elsheenaeats-backend-api)**

Made with ❤️ as the project for the Backend course (3rd year 1st semester)

</div>
