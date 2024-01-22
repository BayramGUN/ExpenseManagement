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

>[![image](/Documents/Images/postman.svg)
See More On Postman](https://documenter.getpostman.com/view/17157290/2s9YymFPsU#intro)

#### API

- [Api](/Documents/Api/)
  - [Authentication](/Documents/Api/Authentication.Api.md)
    - [SingUp](/Documents/Api/Authentication.Api.md#signup)
      - [SingUp Request](/Documents/Api/Authentication.Api.md#signup-request)
      - [SingUp Response](/Documents/Api/Authentication.Api.md#signup-response)
    - [SingIn](/Documents/Api/Authentication.Api.md#signin)
      - [SingIn Request](/Documents/Api/Authentication.Api.md#signin-request)
      - [SingIn Response](/Documents/Api/Authentication.Api.md#signin-response)
  - [Expenses](/Documents/Api/Expenses.Api.md#expenses)
    - [GetAllExpenses](/Documents/Api/Expenses.Api.md#getallexpenses)
      - [GetAllExpenses Request](/Documents/Api/Expenses.Api.md#getallexpenses-request)
      - [GetAllExpenses Response](/Documents/Api/Expenses.Api.md#getallexpenses-response)
    - [GetExpenseById](/Documents/Api/Expenses.Api.md#getexpensebyid)
      - [GetExpenseById Request](/Documents/Api/Expenses.Api.md#getexpensebyid-request)
      - [GetExpenseById Response](/Documents/Api/Expenses.Api.md#getexpensebyid-response)
    - [GetExpenseBy](/Documents/Api/Expenses.Api.md#getexpenseby)
      - [GetExpenseBy Request](/Documents/Api/Expenses.Api.md#getexpenseby-request)
      - [GetExpenseBy Response](/Documents/Api/Expenses.Api.md#getexpenseby-response)
    - [GetExpenseByAppUser](/Documents/Api/Expenses.Api.md#getexpensebyappuser)
      - [GetExpenseByAppUser Request](/Documents/Api/Expenses.Api.md#getexpensebyappuser-request)
      - [GetExpenseByAppUser Response](/Documents/Api/Expenses.Api.md#getexpensebyappuser-response)
    - [GetExpenseByAppUser](/Documents/Api/Expenses.Api.md#getexpensebyappuser)
      - [GetExpenseByAppUser Request](/Documents/Api/Expenses.Api.md#getexpensebyappuser-request)
      - [GetExpenseByAppUser Response](/Documents/Api/Expenses.Api.md#getexpensebyappuser-response)
    - [CreateExpense](/Documents/Api/Expenses.Api.md#createexpense)
      - [CreateExpense Request](/Documents/Api/Expenses.Api.md#createexpense-request)
      - [CreateExpense Response](/Documents/Api/Expenses.Api.md#createexpense-response)
    - [UpdateExpense](/Documents/Api/Expenses.Api.md#updateexpense)
      - [UpdateExpense Request](/Documents/Api/Expenses.Api.md#updateexpense-request)
      - [UpdateExpense Response](/Documents/Api/Expenses.Api.md#updateexpense-response)
    - [ApproveExpense](/Documents/Api/Expenses.Api.md#approveexpense)
      - [ApproveExpense Request](/Documents/Api/Expenses.Api.md#approveexpense-request)
      - [ApproveExpense Response](/Documents/Api/Expenses.Api.md#approveexpense-response)
    - [DeleteExpense](/Documents/Api/Expenses.Api.md#deleteexpense)
      - [DeleteExpense Request](/Documents/Api/Expenses.Api.md#deleteexpense-request)
      - [DeleteExpense Response](/Documents/Api/Expenses.Api.md#deleteexpense-response)
  - [AppUsers](/Documents/Api/AppUsers.Api.md#appusers)
    - [GetAllAppUsers](/Documents/Api/AppUsers.Api.md#getallappusers)
      - [GetAllAppUsers Request](/Documents/Api/AppUsers.Api.md#getallappusers-request)
      - [GetAllAppUsers Response](/Documents/Api/AppUsers.Api.md#getallappusers-response)
    - [GetAppUserById](/Documents/Api/AppUsers.Api.md#getappuserbyid)
      - [GetAppUserById Request](/Documents/Api/AppUsers.Api.md#getappuserbyid-request)
      - [GetAppUserById Response](/Documents/Api/AppUsers.Api.md#getappuserbyid-response)
    - [GetAppUserBy](/Documents/Api/AppUsers.Api.md#getappuserby)
      - [GetAppUserBy Request](/Documents/Api/AppUsers.Api.md#getappuserby-request)
      - [GetAppUserBy Response](/Documents/Api/AppUsers.Api.md#getappuserby-response)
    - [GetAppUsersBy](/Documents/Api/AppUsers.Api.md#getappusersby)
      - [GetAppUsersBy Request](/Documents/Api/AppUsers.Api.md#getappusersby-request)
      - [GetAppUsersBy Response](/Documents/Api/AppUsers.Api.md#getappusersby-response)
    - [CreateAppUser](/Documents/Api/AppUsers.Api.md#createappuser)
      - [CreateAppUser Request](/Documents/Api/AppUsers.Api.md#createappuser-request)
      - [CreateAppUser Response](/Documents/Api/AppUsers.Api.md#createappuser-response)
    - [UpdateAppUser](/Documents/Api/AppUsers.Api.md#updateappuser)
      - [UpdateAppUser Request](/Documents/Api/AppUsers.Api.md#updateappuser-request)
      - [UpdateAppUser Response](/Documents/Api/AppUsers.Api.md#updateappuser-response)
    - [DeleteAppUser](/Documents/Api/AppUsers.Api.md#deleteappuser)
      - [DeleteAppUser Request](/Documents/Api/AppUsers.Api.md#deleteappuser-request)
      - [DeleteAppUser Response](/Documents/Api/AppUsers.Api.md#deleteappuser-response)
    - [ExpenseManagement API](/Documents/Api/ApprovalExpenses.Api.md#expensemanagement-api)
  - [ExpenseApprovals](/Documents/Api/ApprovalExpenses.Api.md#expenseapprovals)
    - [GetAllExpenseApprovals](/Documents/Api/ApprovalExpenses.Api.md#getallexpenseapprovals)
      - [GetAllExpenseApprovals Request](/Documents/Api/ApprovalExpenses.Api.md#getallexpenseapprovals-request)
      - [GetAllExpenseApprovals Response](/Documents/Api/ApprovalExpenses.Api.md#getallexpenseapprovals-response)
    - [GetExpenseApprovalById](/Documents/Api/ApprovalExpenses.Api.md#getexpenseapprovalbyid)
      - [GetExpenseApprovalById Request](/Documents/Api/ApprovalExpenses.Api.md#getexpenseapprovalbyid-request)
      - [GetExpenseApprovalById Response](/Documents/Api/ApprovalExpenses.Api.md#getexpenseapprovalbyid-response)
    - [GetExpenseApprovalsBy](/Documents/Api/ApprovalExpenses.Api.md#getexpenseapprovalsby)
      - [GetExpenseApprovalsBy Request](/Documents/Api/ApprovalExpenses.Api.md#getexpenseapprovalsby-request)
      - [GetExpenseApprovalsBy Response](/Documents/Api/ApprovalExpenses.Api.md#getexpenseapprovalsby-response)
    - [GetExpenseApprovalByAppUser](/Documents/Api/ApprovalExpenses.Api.md#getexpenseapprovalbyappuser)
      - [GetExpenseApprovalByAppUser Request](/Documents/Api/ApprovalExpenses.Api.md#getexpenseapprovalbyappuser-request)
      - [GetExpenseApprovalByAppUser Response](/Documents/Api/ApprovalExpenses.Api.md#getexpenseapprovalbyappuser-response)
    - [CreateExpenseApproval](/Documents/Api/ApprovalExpenses.Api.md#createexpenseapproval)
      - [CreateExpenseApproval Request](/Documents/Api/ApprovalExpenses.Api.md#createexpenseapproval-request)
      - [CreateExpenseApproval Response](/Documents/Api/ApprovalExpenses.Api.md#createexpenseapproval-response)
    - [UpdateExpenseApproval](/Documents/Api/ApprovalExpenses.Api.md#updateexpenseapproval)
      - [UpdateExpenseApproval Request](/Documents/Api/ApprovalExpenses.Api.md#updateexpenseapproval-request)
      - [UpdateExpenseApproval Response](/Documents/Api/ApprovalExpenses.Api.md#updateexpenseapproval-response)
    - [DeleteExpenseApproval](/Documents/Api/ApprovalExpenses.Api.md#deleteexpenseapproval)
      - [DeleteExpenseApproval Request](/Documents/Api/ApprovalExpenses.Api.md#deleteexpenseapproval-request)
      - [DeleteExpenseApproval Response](/Documents/Api/ApprovalExpenses.Api.md#deleteexpenseapproval-response)

#### Entities

- [Entities](/Documents/Entities/)
  - [BaseEntity Model](/Documents/Entities/BaseEntity.md)
  - [AppUser Entity Model](/Documents/Entities/AppUser.Entity.md)
  - [Expense Entity Model](/Documents/Entities/Expense.Entity.md)
  - [ExpenseApproval Entity Model](/Documents/Entities/ExpenseApproval.Entity.md)
  - [Payment Entity Model](/Documents/Entities/Payment.Entity.md)

![Database Diagram](/Documents/Images/DbDiagram.png)

#### TechStack

- [.net 7.0](https://learn.microsoft.com/tr-tr/dotnet/core/whats-new/dotnet-7)
- [EntityFramework Core](https://learn.microsoft.com/en-us/ef/core/)
- [FluentValidation](https://learn.microsoft.com/en-us/ef/core/)
- [AutoMapper](https://automapper.org/)
- [Microsoft SQL Server](https://learn.microsoft.com/en-us/sql/sql-server/?view=sql-server-ver16)
- [RabbitMq](https://www.rabbitmq.com/documentation.html)
- [MassTransit](https://masstransit.io/documentation/concepts)
- [Dapper](https://github.com/DapperLib/Dapper)
- [Redis](https://redis.io/docs/connect/clients/dotnet/)
