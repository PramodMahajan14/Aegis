# Adveshta

Adveshta is a .NET 8 Web API project built using a multi-project architecture. The goal of this project is to learn and implement enterprise-level backend development with Authentication, JWT, File Upload, Role-Based Authorization, and MySQL.

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
Adveshta
│
├── Adveshta.sln
│
├── Adveshta.Services
├── Adveshta.DataAccess
├── Adveshta.Model
├── Adveshta.Helpers
└── Adveshta.Utility
```

---

# Step 1 - Create Solution

```bash
mkdir Adveshta
cd Adveshta

dotnet new sln -n Adveshta
```

---

# Step 2 - Create Projects

```bash
dotnet new webapi -n Adveshta.Services

dotnet new classlib -n Adveshta.DataAccess

dotnet new classlib -n Adveshta.Model

dotnet new classlib -n Adveshta.Helpers

dotnet new classlib -n Adveshta.Utility
```

---

# Step 3 - Add Projects to Solution

```bash
dotnet sln add Adveshta.Services/Adveshta.Services.csproj
dotnet sln add Adveshta.DataAccess/Adveshta.DataAccess.csproj
dotnet sln add Adveshta.Model/Adveshta.Model.csproj
dotnet sln add Adveshta.Helpers/Adveshta.Helpers.csproj
dotnet sln add Adveshta.Utility/Adveshta.Utility.csproj
```

---

# Step 4 - Add Project References

```bash
dotnet add Adveshta.Services reference Adveshta.DataAccess
dotnet add Adveshta.Services reference Adveshta.Model
dotnet add Adveshta.Services reference Adveshta.Helpers
dotnet add Adveshta.Services reference Adveshta.Utility

dotnet add Adveshta.DataAccess reference Adveshta.Model
dotnet add Adveshta.DataAccess reference Adveshta.Helpers
dotnet add Adveshta.DataAccess reference Adveshta.Utility
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
dotnet run --project Adveshta.Services
```

or

```bash
cd Adveshta.Services
dotnet run
```

Open Swagger:

```
http://localhost:<port>/swagger
```

---

# Solution Architecture

```
Adveshta
│
├── Adveshta.Services
│   ├── Controllers
│   ├── Middleware
│   ├── Filters
│   ├── Program.cs
│   └── appsettings.json
│
├── Adveshta.DataAccess
│   ├── Data
│   ├── Initializer
│   └── Migrations
│
├── Adveshta.Model
│   ├── Entity
│   ├── DTO
│   ├── Request
│   ├── Response
│   └── Enum
│
├── Adveshta.Helpers
│   ├── JWT
│   ├── Password
│   ├── File
│   └── Email
│
└── Adveshta.Utility
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
