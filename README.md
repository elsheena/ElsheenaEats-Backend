
# Food Delivery Backend

## Overview
This project is a backend implementation for a **Food Delivery System**.  
It provides a RESTful API for managing users, menu items, baskets, orders, and ratings.  
The backend is built with **ASP.NET Core**, uses **Entity Framework Core** for database access, and runs on **PostgreSQL**.

## Features
- User authentication and registration with **JWT tokens**.  
- Role-based access control (**Admin** and **User** roles).  
- Menu management: categories, dishes, filters, and ratings.  
- Basket and order management with validation and status tracking.  
- PostgreSQL database integration with **Entity Framework Core migrations**.  

## Technologies
- **.NET 8**  
- **ASP.NET Core Web API**  
- **Entity Framework Core** with **PostgreSQL**  
- **ASP.NET Core Identity** for authentication and authorization  
- **Swagger/OpenAPI** for API documentation  

## Database
- PostgreSQL database with entities for Users, Roles, Dishes, Orders, Ratings, and Basket items.  
- Identity framework integration for secure authentication.  
- See the [Database Documentation](https://science.pm.kreosoft.space/projects/mina-mikhaeil-2025/wiki/Database) for schema and ERD.  

## Documentation
Project documentation is available in the Wiki:  
- [API Documentation](https://science.pm.kreosoft.space/projects/mina-mikhaeil-2025/wiki/Food_Delivery_API_Documentation)  
- [Diagrams](https://science.pm.kreosoft.space/projects/mina-mikhaeil-2025/wiki/Diagrams_–_Food_Delivery_System)  
- [Classes](https://science.pm.kreosoft.space/projects/mina-mikhaeil-2025/wiki/Classes)  
- [Database](https://science.pm.kreosoft.space/projects/mina-mikhaeil-2025/wiki/Database)  

## Getting Started

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download)  
- [PostgreSQL](https://www.postgresql.org/download/)  

### Installation
1. Clone the repository:
   ```bash
   git clone https://github.com/<your-username>/<your-repo>.git
   cd <your-repo>
    ```

2. Configure the database connection string in `appsettings.json`.
3. Apply migrations:

   ```bash
   dotnet ef database update
   ```
4. Run the application:

   ```bash
   dotnet run
   ```

### API Documentation

Once the app is running, open Swagger UI at:

```
https://localhost:5001/swagger/index.html
```

## Project Structure

* `Core/Models` – domain entities (User, Dish, Order, etc.)
* `DataAccess` – EF Core DbContext and migrations
* `WebAPI` – controllers and API endpoints





