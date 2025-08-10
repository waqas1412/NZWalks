# 🏔️ NZWalks API

[![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge&logo=.net&logoColor=white)](https://dotnet.microsoft.com/)
[![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-8.0-512BD4?style=for-the-badge&logo=asp.net&logoColor=white)](https://dotnet.microsoft.com/apps/aspnet)
[![Entity Framework](https://img.shields.io/badge/Entity%20Framework-8.0-512BD4?style=for-the-badge&logo=entity-framework&logoColor=white)](https://docs.microsoft.com/en-us/ef/)
[![SQL Server](https://img.shields.io/badge/SQL%20Server-2022-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white)](https://www.microsoft.com/en-us/sql-server/)
[![JWT](https://img.shields.io/badge/JWT-000000?style=for-the-badge&logo=JSON%20web%20tokens&logoColor=white)](https://jwt.io/)
[![Swagger](https://img.shields.io/badge/Swagger-85EA2D?style=for-the-badge&logo=swagger&logoColor=black)](https://swagger.io/)
[![License](https://img.shields.io/badge/License-MIT-yellow.svg?style=for-the-badge)](https://opensource.org/licenses/MIT)

A comprehensive RESTful API for managing New Zealand walking trails and regions. Built with ASP.NET Core 8.0, featuring JWT authentication, role-based authorization, and modern development practices.

## 📋 Table of Contents

- [Features](#-features)
- [Architecture](#-architecture)
- [Tech Stack](#-tech-stack)
- [Getting Started](#-getting-started)
- [API Documentation](#-api-documentation)
- [Database Schema](#-database-schema)
- [Authentication & Authorization](#-authentication--authorization)
- [Project Structure](#-project-structure)
- [Development](#-development)
- [Contributing](#-contributing)
- [License](#-license)

## ✨ Features

- **🔐 JWT Authentication & Authorization** - Secure user authentication with role-based access control
- **🏔️ Walk Management** - CRUD operations for walking trails with filtering, sorting, and pagination
- **🗺️ Region Management** - Manage New Zealand regions with associated walks
- **📸 Image Upload** - Local image storage for walks and regions
- **📊 Advanced Filtering** - Search walks by name, sort by various criteria, and paginate results
- **🛡️ Security** - Input validation, model validation, and comprehensive error handling
- **📝 Logging** - Structured logging with Serilog for monitoring and debugging
- **📚 API Documentation** - Interactive Swagger/OpenAPI documentation
- **🧪 Clean Architecture** - Repository pattern, dependency injection, and separation of concerns

## 🏗️ Architecture

The application follows clean architecture principles with:

- **Controllers Layer** - Handle HTTP requests and responses
- **Repository Layer** - Data access abstraction with repository pattern
- **Service Layer** - Business logic and data transformation
- **Domain Models** - Core business entities
- **DTOs** - Data transfer objects for API contracts
- **Middleware** - Cross-cutting concerns like exception handling

## 🛠️ Tech Stack

| Technology | Version | Purpose |
|------------|---------|---------|
| **.NET 8.0** | Latest | Runtime and framework |
| **ASP.NET Core** | 8.0 | Web API framework |
| **Entity Framework Core** | 8.0 | ORM and database access |
| **SQL Server** | 2022 | Primary database |
| **ASP.NET Core Identity** | 8.0 | User authentication and authorization |
| **JWT Bearer** | 8.0 | Token-based authentication |
| **AutoMapper** | 12.0.1 | Object-to-object mapping |
| **Serilog** | 3.1.1 | Structured logging |
| **Swagger/OpenAPI** | 6.5.0 | API documentation |

## 🚀 Getting Started

### Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (LocalDB or Express)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/yourusername/NZWalks.git
   cd NZWalks
   ```

2. **Configure the database**
   - Update connection strings in `appsettings.json` or `appsettings.Development.json`
   - Ensure SQL Server is running and accessible

3. **Run database migrations**
   ```bash
   cd NZWalks.API
   dotnet ef database update
   ```

4. **Configure JWT settings**
   - Update JWT configuration in `appsettings.json`:
   ```json
   "Jwt": {
     "Key": "your-secret-key-here",
     "Issuer": "https://localhost:7044/",
     "Audience": "https://localhost:7044"
   }
   ```

5. **Run the application**
   ```bash
   dotnet run
   ```

6. **Access the API**
   - API: `https://localhost:7044`
   - Swagger Documentation: `https://localhost:7044/swagger`

## 📚 API Documentation

### Base URL
```
https://localhost:7044
```

### Authentication Endpoints

| Method | Endpoint | Description | Authorization |
|--------|----------|-------------|---------------|
| `POST` | `/auth/register` | Register a new user | Public |
| `POST` | `/auth/login` | Authenticate user and get JWT token | Public |

### Regions Endpoints

| Method | Endpoint | Description | Authorization |
|--------|----------|-------------|---------------|
| `GET` | `/regions` | Get all regions | Reader, Writer |
| `GET` | `/regions/{id}` | Get region by ID | Reader, Writer |
| `POST` | `/regions` | Create new region | Writer |
| `PUT` | `/regions/{id}` | Update region | Writer |
| `DELETE` | `/regions/{id}` | Delete region | Writer |

### Walks Endpoints

| Method | Endpoint | Description | Authorization |
|--------|----------|-------------|---------------|
| `GET` | `/walks` | Get all walks (with filtering) | Public |
| `GET` | `/walks/{id}` | Get walk by ID | Public |
| `POST` | `/walks` | Create new walk | Writer |
| `PUT` | `/walks/{id}` | Update walk | Writer |
| `DELETE` | `/walks/{id}` | Delete walk | Writer |

### Images Endpoints

| Method | Endpoint | Description | Authorization |
|--------|----------|-------------|---------------|
| `POST` | `/images/upload` | Upload image | Writer |

### Query Parameters for Walks

The walks endpoint supports advanced filtering:

- `name` - Filter walks by name (partial match)
- `sortBy` - Sort by field (name, length, etc.)
- `isAscending` - Sort direction (true/false)
- `pageNumber` - Page number for pagination
- `pageSize` - Number of items per page

**Example:**
```
GET /walks?name=mountain&sortBy=length&isAscending=false&pageNumber=1&pageSize=10
```

## 🗄️ Database Schema

### Core Entities

#### Region
```csharp
public class Region
{
    public Guid Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public string? RegionImageUrl { get; set; }
}
```

#### Walk
```csharp
public class Walk
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double LengthInKm { get; set; }
    public string? WalkImgUrl { get; set; }
    public Guid DifficultyId { get; set; }
    public Guid RegionId { get; set; }
    
    // Navigation Properties
    public Difficulty Difficulty { get; set; }
    public Region Region { get; set; }
}
```

#### Difficulty
```csharp
public class Difficulty
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}
```

## 🔐 Authentication & Authorization

### JWT Token Structure
The API uses JWT tokens for authentication with the following claims:
- User ID
- Email
- Roles (Reader, Writer)

### Role-Based Access Control

| Role | Permissions |
|------|-------------|
| **Reader** | Read access to regions and walks |
| **Writer** | Full CRUD access to all resources |

### Authentication Flow

1. **Register** - Create a new user account
2. **Login** - Authenticate and receive JWT token
3. **Authorize** - Include token in Authorization header: `Bearer {token}`

## 📁 Project Structure

```
NZWalks/
├── NZWalks.API/                 # Main API project
│   ├── Controllers/             # API controllers
│   │   ├── AuthController.cs    # Authentication endpoints
│   │   ├── RegionsController.cs # Region management
│   │   ├── WalksController.cs   # Walk management
│   │   └── ImagesController.cs  # Image upload
│   ├── Data/                    # Database contexts
│   │   ├── NZWalksDBContext.cs  # Main database context
│   │   └── NZWalksAuthDbContext.cs # Authentication context
│   ├── Models/                  # Domain models and DTOs
│   │   ├── Domain/              # Entity models
│   │   └── DTOs/                # Data transfer objects
│   ├── Repositories/            # Data access layer
│   │   ├── IRegionRepository.cs
│   │   ├── IWalkRepository.cs
│   │   └── SQL implementations
│   ├── Mappings/                # AutoMapper profiles
│   ├── Middlewares/             # Custom middleware
│   ├── CustomActionFilters/     # Custom validation filters
│   └── Migrations/              # Entity Framework migrations
└── README.md
```

## 🛠️ Development

### Running in Development Mode

```bash
# Set environment to development
set ASPNETCORE_ENVIRONMENT=Development

# Run the application
dotnet run
```

### Database Migrations

```bash
# Create a new migration
dotnet ef migrations add MigrationName

# Update database
dotnet ef database update

# Remove last migration
dotnet ef migrations remove
```

### Testing the API

1. **Using Swagger UI**
   - Navigate to `https://localhost:7044/swagger`
   - Authenticate using the `/auth/login` endpoint
   - Copy the JWT token and click "Authorize"
   - Test other endpoints

2. **Using HTTP Client**
   - Import the `NZWalks.API.http` file into your IDE
   - Configure environment variables for base URL
   - Execute requests directly from the file

### Logging

The application uses Serilog for structured logging:
- Console output for development
- File logging with daily rotation
- Log files stored in `Logs/` directory

## 🤝 Contributing

We welcome contributions! Please follow these steps:

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

### Development Guidelines

- Follow C# coding conventions
- Add appropriate XML documentation
- Include unit tests for new features
- Update API documentation
- Ensure all tests pass before submitting PR

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 🙏 Acknowledgments

- Built with ASP.NET Core 8.0
- Inspired by New Zealand's beautiful walking trails
- Uses modern development practices and patterns

---

**Made with ❤️ for the New Zealand walking community**

[![GitHub stars](https://img.shields.io/github/stars/yourusername/NZWalks?style=social)](https://github.com/yourusername/NZWalks/stargazers)
[![GitHub forks](https://img.shields.io/github/forks/yourusername/NZWalks?style=social)](https://github.com/yourusername/NZWalks/network)
[![GitHub issues](https://img.shields.io/github/issues/yourusername/NZWalks)](https://github.com/yourusername/NZWalks/issues)
[![GitHub pull requests](https://img.shields.io/github/issues-pr/yourusername/NZWalks)](https://github.com/yourusername/NZWalks/pulls)
