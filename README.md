# EXPENSE MANAGEMENT

## Description

>This project is an expense management application API provider. Created for patika.dev & Akbank .Net Bootcamp final-case.

### Build/Run Operations

>Run Command:

```powershell
dotnet run --project src/Presentation/API/ExpenseManagement.Api --launch-profile https
```

>Note: If, there is an SSL problem run command below

```powershell
dotnet dev-certs https --trust
```

>Description: This command is designed to build and execute the project, establish a database connection, and automatically generate tables using migrations. When initializing the project for the first time, it also adds two default users to the database.
>Note: The ConnectionString and other settings values are located within the user-secrets. Execute the command below to view them.

Command Prompt:

```powershell
cd src/Presentation/API/ExpenseManagement.Api && dotnet user-secrets list
```

PowerShell:

```powershell
cd src/Presentation/API/ExpenseManagement.Api; dotnet user-secrets list
```

### Documents of The Project

- [Api](/Documents/Api/)
  - [Authentication](/Documents/Api/Authentication.Api.md)
    - [SingUp](/Documents/Api/Authentication.Api.md#signup)
      - [SingUp Request](/Documents/Api/Authentication.Api.md#signup-request)
      - [SingUp Response](/Documents/Api/Authentication.Api.md#signup-response)
    - [SingIn](/Documents/Api/Authentication.Api.md#signin)
      - [SingIn Request](/Documents/Api/Authentication.Api.md#signin-request)
      - [SingIn Response](/Documents/Api/Authentication.Api.md#signin-response)

### Entities

- [Entities](/Documents/Entities/)
  - [BaseEntity Model](/Documents/Entities/BaseEntity.md)
  - [AppUser Entity Model](/Documents/Entities/AppUser.Entity.md)
  - [Expense Entity Model](/Documents/Entities/Expense.Entity.md)
  - [ExpenseApproval Entity Model](/Documents/Entities/ExpenseApproval.Entity.md)
  - [Payment Entity Model](/Documents/Entities/Payment.Entity.md)
