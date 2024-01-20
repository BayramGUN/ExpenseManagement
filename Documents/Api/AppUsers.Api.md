# ExpenseManagement API

- [ExpenseManagement API](#expensemanagement-api)
  - [AppUsers](#appusers)
    - [GetAllAppUsers](#getallappusers)
      - [GetAllAppUsers Request](#getallappusers-request)
      - [GetAllAppUsers Response](#getallappusers-response)
    - [GetAppUserById](#getappuserbyid)
      - [GetAppUserById Request](#getappuserbyid-request)
      - [GetAppUserById Response](#getappuserbyid-response)
    - [GetAppUserBy](#getappuserby)
      - [GetAppUserBy Request](#getappuserby-request)
      - [GetAppUserBy Response](#getappuserby-response)
    - [GetAppUsersBy](#getappusersby)
      - [GetAppUsersBy Request](#getappusersby-request)
      - [GetAppUsersBy Response](#getappusersby-response)
    - [CreateAppUser](#createappuser)
      - [CreateAppUser Request](#createappuser-request)
      - [CreateAppUser Response](#createappuser-response)
    - [UpdateAppUser](#updateappuser)
      - [UpdateAppUser Request](#updateappuser-request)
      - [UpdateAppUser Response](#updateappuser-response)
    - [DeleteAppUser](#deleteappuser)
      - [DeleteAppUser Request](#deleteappuser-request)
      - [DeleteAppUser Response](#deleteappuser-response)

## AppUsers

```http
https://localhost:7066/api/AppUsers
```

### GetAllAppUsers

#### GetAllAppUsers Request

```http
@host=https://localhost:7066
@token=ey.JjLBw..

GET {{host}}/api/AppUsers
Authorization: Bearer {{token}}
```

#### GetAllAppUsers Response

```json
{
  "response": [
    {
      "userName": "DefaultAdmin",
      "fullName": "Basri Bal",
      "email": "default@admin.com",
      "role": "Admin",
      "lastActivityDate": "0001-01-01T00:00:00",
      "passwordRetryCount": 0,
      "status": 0
    },
    {
      "userName": "DefaultEmployee",
      "fullName": "Hayri Petek",
      "email": "default@employee.com",
      "role": "Employee",
      "lastActivityDate": "0001-01-01T00:00:00",
      "passwordRetryCount": 0,
      "status": 0
    }
  ],
  "success": true,
  "message": "Success",
  "serverDate": "2024-01-18T00:28:28.8659534Z",
  "referenceNo": "412467d4-72b8-4215-ab9e-48a9f577fdbc"
}
```

> Go to [HTTP](/Responses/AppUser/GetAllUsersResponse.http) file.

### GetAppUserById

#### GetAppUserById Request

```http
@host=https://localhost:7066
@token=ey.JjLBw..

@id=2

GET {{host}}/api/AppUsers/GetAppUserBy?Id={{id}}
Authorization: Bearer {{token}}
```

#### GetAppUserById Response

```json
{
  "response": {
      "userName": "DefaultEmployee",
      "fullName": "Hayri Petek",
      "email": "default@employee.com",
      "role": "Employee",
      "lastActivityDate": "0001-01-01T00:00:00",
      "passwordRetryCount": 0,
      "status": 0
  },
  "success": true,
  "message": "Success",
  "serverDate": "2024-01-19T23:34:31.0867865Z",
  "referenceNo": "2cd56e7e-b136-46f0-be60-d445cc670eff"
}
```

> Go to [HTTP](/Responses/AppUser/GetAppUserById.http) file.

### GetAppUserBy

#### GetAppUserBy Request

```http
@host=https://localhost:7066
@token=ey.JjLBw..

#optional
@userName=DefaultEmployee
#optional
@phone=5555555554
#optional
@identityNumber=11111111102
#optional
@email=default@employee.com
#optional
@id=2


GET {{host}}/api/AppUsers/GetAppUserBy?Id={{id}}&Email={{email}}&Phone={{phone}}&UserName={{userName}}&IdentityNumber={{identityNumber}}
Authorization: Bearer {{token}}
```

#### GetAppUserBy Response

```json
{
  "response": {
      "userName": "DefaultEmployee",
      "fullName": "Hayri Petek",
      "email": "default@employee.com",
      "role": "Employee",
      "lastActivityDate": "0001-01-01T00:00:00",
      "passwordRetryCount": 0,
      "status": 0
  },
  "success": true,
  "message": "Success",
  "serverDate": "2024-01-19T23:34:31.0867865Z",
  "referenceNo": "2cd56e7e-b136-46f0-be60-d445cc670eff"
}
```

> Go to [HTTP](/Responses/AppUser/GetAppUserBy.http) file.

### GetAppUsersBy

#### GetAppUsersBy Request

```http
@host=https://localhost:7066
@token=ey.JjLBw..

#optional
@firstName=Hayri
#optional
@role=1
#optional
@status=false
#optional
@isActive=true
#optional
@lastName=Petek


GET {{host}}/api/AppUsers/GetBy?LastName={{lastName}}&isActive={{isActive}}&role={{role}}&firstName={{firstName}}&status={{status}}
Authorization: Bearer {{token}}
```

#### GetAppUsersBy Response

```json
{
  "response": {
      "userName": "DefaultEmployee",
      "fullName": "Hayri Petek",
      "email": "default@employee.com",
      "role": "Employee",
      "lastActivityDate": "0001-01-01T00:00:00",
      "passwordRetryCount": 0,
      "status": 0
  },
  "success": true,
  "message": "Success",
  "serverDate": "2024-01-19T23:34:31.0867865Z",
  "referenceNo": "2cd56e7e-b136-46f0-be60-d445cc670eff"
}
```

> Go to [HTTP](/Responses/AppUser/GetAppUserByAppUser.http) file.

### CreateAppUser

#### CreateAppUser Request

```http
@host=https://localhost:7066
@token=ey.JjLBw..

POST {{host}}/api/AppUsers/Create
Content-Type: application/json
Authorization: Bearer {{token}}

# userId: That is id value of the creator of user. 

{
  "accessToken": "abc123xyz456",
  "userId": 2,
  "requestTimestamp": "2024-01-19T23:23:12.962Z",
  "amount": 100.23,
  "description": "Dinner with clients",
  "title": "Business Meal",
  "receiptPhotoUrl": "https://example.com/receipt123.jpg"
}
```

#### CreateAppUser Response

```json
{
  "success": true,
  "message": "Business Meal is created successfully!",
  "serverDate": "2024-01-19T23:23:12.964Z",
  "referenceNo": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
}
```

### UpdateAppUser

#### UpdateAppUser Request

```http
@host=https://localhost:7066
@token=ey.JjLBw..

PUT {{host}}/api/AppUsers/Update
Content-Type: application/json
Authorization: Bearer {{token}}

# userId: That is id value of the Updater of user. 

{
  "id": 1,
  "accessToken": "abc123xyz456",
  "userId": 2,
  "requestTimestamp": "2024-01-19T23:23:12.962Z",
  "amount": 100.23,
  "description": "Dinner with clients, at the !",
  "title": "Business Meal",
  "receiptPhotoUrl": "https://example.com/receipt123.jpg"
}
```

#### UpdateAppUser Response

```json
{
  "success": true,
  "message": "1 is updated successfully!",
  "serverDate": "2024-01-19T23:23:12.964Z",
  "referenceNo": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
}
```

> Go to [HTTP](/Requests/AppUser/UpdateAppUserRequest.http) file.

### DeleteAppUser

#### DeleteAppUser Request

```http
@host=https://localhost:7066
@id=17
@token=ey.JjLBw..

DELETE {{host}}/api/AppUsers/Delete/{{id}}
Authorization: Bearer {{token}}
```

#### DeleteAppUser Response

```json
{
  "success": true,
  "message": "17 is deleted successfully!",
  "serverDate": "2024-01-19T23:23:12.964Z",
  "referenceNo": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
}
```

> Go to [HTTP](/Requests/AppUser/ApproveAppUserRequest.http) file.
> Back to [README.md](../../README.md) file.
