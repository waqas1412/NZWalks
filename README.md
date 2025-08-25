# 🏔️ NZWalks API

[![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge&logo=.net&logoColor=white)](https://dotnet.microsoft.com/)
[![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-8.0-512BD4?style=for-the-badge&logo=asp.net&logoColor=white)](https://dotnet.microsoft.com/apps/aspnet)
[![Entity Framework](https://img.shields.io/badge/Entity%20Framework-8.0-512BD4?style=for-the-badge&logo=entity-framework&logoColor=white)](https://docs.microsoft.com/en-us/ef/)
[![SQL Server](https://img.shields.io/badge/SQL%20Server-2022-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white)](https://www.microsoft.com/en-us/sql-server/)
[![JWT](https://img.shields.io/badge/JWT-000000?style=for-the-badge&logo=JSON%20web%20tokens&logoColor=white)](https://jwt.io/)
[![Swagger](https://img.shields.io/badge/Swagger-85EA2D?style=for-the-badge&logo=swagger&logoColor=black)](https://swagger.io/)
[![License](https://img.shields.io/badge/License-MIT-yellow.svg?style=for-the-badge)](https://opensource.org/licenses/MIT)

A comprehensive RESTful API for managing New Zealand walking trails and regions. Built with ASP.NET Core 8.0, featuring JWT authentication, role-based authorization, local image storage, and modern development practices.

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

- **🔐 JWT Authentication** - Secure user authentication with 10-minute token expiration
- **👥 Role-Based Authorization** - Reader and Writer roles with granular permissions
- **🏔️ Walk Management** - CRUD operations for walking trails with advanced filtering and pagination
- **🗺️ Region Management** - Manage New Zealand regions with associated walks
- **📸 Local Image Storage** - Secure image upload with validation (JPG, PNG, max 10MB)
- **🔍 Advanced Filtering** - Search walks by name, sort by criteria, and paginate results
- **🛡️ Security** - Input validation, model validation, and comprehensive error handling
- **📝 Structured Logging** - Serilog integration with file and console output
- **📚 Interactive Documentation** - Swagger/OpenAPI with JWT authentication support
- **🏗️ Clean Architecture** - Repository pattern, dependency injection, and separation of concerns
- **⚡ Custom Middleware** - Global exception handling with unique error tracking

## 🏗️ Architecture

The application follows clean architecture principles with:

- **Controllers Layer** - Handle HTTP requests and responses with role-based authorization
- **Repository Layer** - Data access abstraction with repository pattern
- **Domain Models** - Core business entities (Region, Walk, Difficulty, Image)
- **DTOs** - Data transfer objects for API contracts
- **Custom Middleware** - Global exception handling and error tracking
- **Custom Filters** - Model validation and request processing

## 🛠️ Tech Stack

| Technology | Version | Purpose |
|------------|---------|---------|
| **.NET 8.0** | Latest | Runtime and framework |
| **ASP.NET Core** | 8.0 | Web API framework |
| **Entity Framework Core** | 8.0 | ORM and database access |
| **SQL Server** | 2022 | Primary database |
| **ASP.NET Core Identity** | 8.0 | User authentication and authorization |
| **JWT Bearer** | 8.0 | Token-based authentication (10-min expiry) |
| **AutoMapper** | 12.0.1 | Object-to-object mapping |
| **Serilog** | 3.1.1 | Structured logging (file + console) |
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
   - Update connection strings in `appsettings.json`:
   ```json
   "ConnectionStrings": {
     "NZWalksConnectionString": "Server=localhost;Database=NZWalksDb;Trusted_Connection=true;TrustServerCertificate=true",
     "NZWalksAuthConnectionString": "Server=localhost;Database=NZWalksAuthDb;Trusted_Connection=true;TrustServerCertificate=true"
   }
   ```

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
| `GET` | `/regions` | Get all regions | Public |
| `GET` | `/regions/{id}` | Get region by ID | Reader, Writer |
| `POST` | `/regions` | Create new region | Writer |
| `PUT` | `/regions/{id}` | Update region | Writer |
| `DELETE` | `/regions/{id}` | Delete region | Writer |

### Walks Endpoints

| Method | Endpoint | Description | Authorization |
|--------|----------|-------------|---------------|
| `GET` | `/walks` | Get all walks (with filtering) | Public |
| `GET` | `/walks/{id}` | Get walk by ID | Public |
| `POST` | `/walks` | Create new walk | Public |
| `PUT` | `/walks/{id}` | Update walk | Public |
| `DELETE` | `/walks/{id}` | Delete walk | Public |

### Images Endpoints

| Method | Endpoint | Description | Authorization |
|--------|----------|-------------|---------------|
| `POST` | `/images/upload` | Upload image (max 10MB, JPG/PNG) | Public |

### Query Parameters for Walks

The walks endpoint supports advanced filtering:

- `name` - Filter walks by name (partial match)
- `sortBy` - Sort by field (name, length, etc.)
- `isAscending` - Sort direction (true/false)
- `pageNumber` - Page number for pagination (default: 1)
- `pageSize` - Number of items per page (default: 10)

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

#### Image
```csharp
public class Image
{
    public Guid Id { get; set; }
    public IFormFile File { get; set; }
    public string FileName { get; set; }
    public string? FileDescription { get; set; }
    public string FileExtension { get; set; }
    public long FileSizeInBytes { get; set; }
    public string FilePath { get; set; }
}
```

## 🔐 Authentication & Authorization

### JWT Token Configuration
- **Expiration**: 10 minutes
- **Algorithm**: HMAC SHA256
- **Claims**: Email, Roles (Reader, Writer)

### Role-Based Access Control

| Role | Permissions |
|------|-------------|
| **Reader** | Read access to regions (by ID) |
| **Writer** | Full CRUD access to regions |

**Note**: Walks endpoints are currently public (no authorization required)

### Authentication Flow

1. **Register** - Create a new user account with roles
2. **Login** - Authenticate and receive JWT token
3. **Authorize** - Include token in Authorization header: `Bearer {token}`

## 📁 Project Structure

```
NZWalks/
├── NZWalks.API/                 # Main API project
│   ├── Controllers/             # API controllers
│   │   ├── AuthController.cs    # Authentication endpoints
│   │   ├── RegionsController.cs # Region management (role-based auth)
│   │   ├── WalksController.cs   # Walk management (public)
│   │   └── ImagesController.cs  # Image upload (public)
│   ├── Data/                    # Database contexts
│   │   ├── NZWalksDBContext.cs  # Main database context
│   │   └── NZWalksAuthDbContext.cs # Authentication context
│   ├── Models/                  # Domain models and DTOs
│   │   ├── Domain/              # Entity models
│   │   └── DTOs/                # Data transfer objects
│   ├── Repositories/            # Data access layer
│   │   ├── IRegionRepository.cs
│   │   ├── IWalkRepository.cs
│   │   ├── IImageRepository.cs
│   │   └── SQL implementations
│   ├── Mappings/                # AutoMapper profiles
│   ├── Middlewares/             # Custom middleware
│   │   └── ExceptionHandlerMiddleware.cs # Global error handling
│   ├── CustomActionFilters/     # Custom validation filters
│   │   └── ValidateModelAttribute.cs # Model validation
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

### Image Upload Features

- **Supported Formats**: JPG, JPEG, PNG
- **Maximum Size**: 10MB
- **Storage**: Local file system in `Images/` directory
- **Validation**: Automatic file type and size validation

### Error Handling

- **Global Exception Middleware**: Catches all unhandled exceptions
- **Unique Error IDs**: Each error gets a GUID for tracking
- **Structured Logging**: Errors logged with Serilog
- **Custom Error Response**: Consistent error format

### Logging Configuration

The application uses Serilog for structured logging:
- **Console Output**: For development debugging
- **File Logging**: Daily rotation in `Logs/` directory
- **Log Level**: Information and above
- **Format**: Structured JSON logging

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
- Test image upload functionality with various file types

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 🙏 Acknowledgments

- Built with ASP.NET Core 8.0
- Inspired by New Zealand's beautiful walking trails
- Uses modern development practices and patterns
- Implements secure authentication and authorization

---

**Made with ❤️ for the New Zealand walking community**

[![GitHub stars](https://img.shields.io/github/stars/yourusername/NZWalks?style=social)](https://github.com/yourusername/NZWalks/stargazers)
[![GitHub forks](https://img.shields.io/github/forks/yourusername/NZWalks?style=social)](https://github.com/yourusername/NZWalks/network)
[![GitHub issues](https://img.shields.io/github/issues/yourusername/NZWalks)](https://github.com/yourusername/NZWalks/issues)
[![GitHub pull requests](https://img.shields.io/github/issues-pr/yourusername/NZWalks)](https://github.com/yourusername/NZWalks/pulls)
