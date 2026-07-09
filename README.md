# Aegis

Aegis is a .NET 8 Web API project built using a multi-project architecture. The goal of this project is to learn and implement enterprise-level backend development with Authentication, JWT, File Upload, Role-Based Authorization, and MySQL.

---

# Tech Stack

- .NET 8
- ASP.NET Core Web API
- C#
- Swagger (OpenAPI)
- MySQL (Coming Soon)
- Entity Framework Core (Coming Soon)

---

# Project Structure

```text
Aegis
│
├── Aegis.sln
│
├── Aegis.Services
├── Aegis.DataAccess
├── Aegis.Model
├── Aegis.Helpers
└── Aegis.Utility
```

---

# Step 1 - Create Solution

```bash
mkdir Aegis
cd Aegis

dotnet new sln -n Aegis
```

---

# Step 2 - Create Projects

```bash
dotnet new webapi -n Aegis.Services

dotnet new classlib -n Aegis.DataAccess

dotnet new classlib -n Aegis.Model

dotnet new classlib -n Aegis.Helpers

dotnet new classlib -n Aegis.Utility
```

---

# Step 3 - Add Projects to Solution

```bash
dotnet sln add Aegis.Services/Aegis.Services.csproj
dotnet sln add Aegis.DataAccess/Aegis.DataAccess.csproj
dotnet sln add Aegis.Model/Aegis.Model.csproj
dotnet sln add Aegis.Helpers/Aegis.Helpers.csproj
dotnet sln add Aegis.Utility/Aegis.Utility.csproj
```

---

# Step 4 - Add Project References

```bash
dotnet add Aegis.Services reference Aegis.DataAccess
dotnet add Aegis.Services reference Aegis.Model
dotnet add Aegis.Services reference Aegis.Helpers
dotnet add Aegis.Services reference Aegis.Utility

dotnet add Aegis.DataAccess reference Aegis.Model
dotnet add Aegis.DataAccess reference Aegis.Helpers
dotnet add Aegis.DataAccess reference Aegis.Utility
```

---

# Step 5 - Open in VS Code

```bash
code .
```

---

# Step 6 - Run Project

From the solution folder:

```bash
dotnet run --project Aegis.Services
```

or

```bash
cd Aegis.Services
dotnet run
```

Open Swagger:

```
http://localhost:<port>/swagger
```

---

# Solution Architecture

```
Aegis
│
├── Aegis.Services
│   ├── Controllers
│   ├── Middleware
│   ├── Filters
│   ├── Program.cs
│   └── appsettings.json
│
├── Aegis.DataAccess
│   ├── Data
│   ├── Initializer
│   └── Migrations
│
├── Aegis.Model
│   ├── Entity
│   ├── DTO
│   ├── Request
│   ├── Response
│   └── Enum
│
├── Aegis.Helpers
│   ├── JWT
│   ├── Password
│   ├── File
│   └── Email
│
└── Aegis.Utility
    ├── Constants
    ├── Extensions
    └── Common
```

---

# Current Progress

- [x] Solution Created
- [x] Multi-Project Architecture
- [x] Project References Added
- [x] Swagger Configured
- [x] AuthController Created
- [x] API Running Successfully

---

# Next Roadmap

- [ ] MySQL Configuration
- [ ] Entity Framework Core
- [ ] AppDbContext
- [ ] User Entity
- [ ] Authentication
- [ ] JWT
- [ ] Refresh Token
- [ ] Role Management
- [ ] File Upload
- [ ] Authorization
- [ ] Global Exception Middleware
- [ ] Logging
- [ ] Docker
- [ ] Unit Testing
- [ ] Deployment
