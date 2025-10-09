# Elsheena Food Delivery API Documentation

This is a complete food delivery backend API built with ASP.NET Core, Entity Framework, and JWT authentication.

## Architecture

- **API Layer**: Controllers, Configuration, Authentication
- **Business Logic Layer**: Services, DTOs, Business Rules  
- **Core Layer**: Domain Models, Enums, Interfaces
- **Data Access Layer**: Entity Framework Context, Migrations

## Features Implemented

✅ **JWT Authentication & Authorization**
- User registration and login
- Role-based access control (Administrator, Manager, Cook, Customer)
- JWT token generation and refresh token functionality

✅ **User Management**
- User profiles with full details
- Identity management with ASP.NET Core Identity
- Secure password policies

✅ **Dish Management**
- Full CRUD operations for dishes
- Advanced filtering (category, vegetarian, price range)
- Sorting by name, price, rating
- Pagination support

✅ **Database Integration**
- PostgreSQL database with Entity Framework Core
- Automatic migrations
- Data seeding for initial setup

## API Endpoints

### Authentication

#### POST /api/auth/register
Register a new user account.

```json
{
  "email": "user@example.com",
  "password": "Password123!",
  "fullName": "John Doe",
  "birthDate": "1990-01-01",
  "gender": 0,
  "address": "123 Main St",
  "phoneNumber": "+1234567890"
}
```

#### POST /api/auth/login
Login with existing credentials.

```json
{
  "email": "user@example.com",
  "password": "Password123!"
}
```

#### POST /api/auth/refresh-token
Refresh an expired access token.

```json
{
  "refreshToken": "your-refresh-token-here"
}
```

#### POST /api/auth/logout
Logout and invalidate refresh token (requires authentication).

### Dishes

#### GET /api/dishes
Get paginated list of dishes with optional filtering.

Query Parameters:
- `page` (int): Page number (default: 1)
- `pageSize` (int): Items per page (default: 10)
- `categories` (array): Filter by categories [Wok, Pizza, Soup, Dessert, Drink]
- `isVegetarian` (bool): Filter by vegetarian status
- `sortBy` (enum): Sort by [NameAsc, NameDesc, PriceAsc, PriceDesc, RatingAsc, RatingDesc]
- `minPrice` (double): Minimum price filter
- `maxPrice` (double): Maximum price filter

Example: `/api/dishes?page=1&pageSize=10&categories=Pizza&categories=Dessert&isVegetarian=true&sortBy=PriceAsc&minPrice=5.0&maxPrice=20.0`

#### GET /api/dishes/{id}
Get detailed information about a specific dish.

#### POST /api/dishes (Manager+ required)
Create a new dish.

```json
{
  "name": "Margherita Pizza",
  "description": "Classic pizza with tomato sauce and mozzarella",
  "price": 12.99,
  "image": "https://example.com/pizza.jpg",
  "isVegetarian": true,
  "category": 1
}
```

#### PUT /api/dishes/{id} (Manager+ required)
Update an existing dish.

#### DELETE /api/dishes/{id} (Manager+ required)
Soft delete a dish.

### Profile

#### GET /api/profile
Get current user's profile (requires authentication).

#### PUT /api/profile
Update current user's profile (requires authentication).

```json
{
  "fullName": "John Doe",
  "email": "john@example.com",
  "birthDate": "1990-01-01",
  "gender": 0,
  "address": "123 Main St",
  "phoneNumber": "+1234567890"
}
```

## Configuration

The application requires the following configuration in `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "Default": "Host=localhost;Port=5432;Database=ElsheenaEats;Username=postgres;Password=your_password"
  },
  "JwtBearerTokenSettings": {
    "SecretKey": "your-secret-key-here",
    "ExpiryTimeInMinutes": 60,
    "RefreshTokenExpiryTimeInDays": 7,
    "Audience": "https://localhost:7001",
    "Issuer": "https://localhost:7001"
  }
}
```

## Initial Data

The application automatically:
- Creates required roles (Administrator, Manager, Cook, Customer)
- Creates an admin user (admin@elsheena.com / Admin123!)
- Seeds sample dish data

## Security Features

- JWT-based authentication
- Role-based authorization policies
- Secure password requirements (8+ chars, uppercase, lowercase, digits)
- Soft deletion for data integrity
- Audit fields on all entities (CreateDateTime, ModifyDateTime, DeleteDate)

## Testing

Use tools like Postman or curl to test the endpoints. Remember to include the JWT token in the Authorization header for protected endpoints:

```
Authorization: Bearer your-jwt-token-here
```

## Running the Application

1. Ensure PostgreSQL is running and accessible
2. Update the connection string in appsettings.json
3. Run `dotnet run` from the API project directory
4. Navigate to https://localhost:7001/swagger for API documentation

The application will automatically apply migrations and seed initial data on startup.