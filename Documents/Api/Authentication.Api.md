# ExpenseManagement API

> [Back to README.md](../../README.md)

- [ExpenseManagement API](#expensemanagement-api)
  - [Authentication](#authentication)
    - [SignUp](#signup)
      - [SignUp Request](#signup-request)
      - [SignUp Response](#signup-response)
    - [SignIn](#signin)
      - [SignIn Request](#signin-request)
      - [SignIn Response](#signin-response)

## Authentication

### SignUp

```js
POST https://localhost:7066/api/Authentications/SignUp
```

#### SignUp Request

>example:

```json
{
    "userName": "abuzer72",
  "password": "Abuzer!123",
  "iban": "TR240006233335524967892512",
  "firstName": "abuzer",
  "lastName": "kömürcü",
  "email": "abuzer@komurcu.com",
  "identityNumber": "12312322441",
  "phone": "1231232233",
  "role": "Admin"
}
```

#### SignUp Response

```json
{
  "response": {
    "token": "eyJ.asd..",
    "email": "abuzer@komurcu.com"
  },
  "success": true,
  "message": "Success",
  "serverDate": "2024-01-18T00:29:44.8030844Z",
  "referenceNo": "f76c3553-d58b-4760-ae30-601de17827b9"
}
```

>Go to [HTTP](../../Requests/Authentication/SignUpRequest.http) file

### SignIn

```js
POST https://localhost:7066/api/Authentications/SignIn
```

#### SignIn Request

>example:

```json
{
  "userName": "abuzer72", // optional
  "password": "Abuzer!123", // optional
  "email": "abuzer@komurcu.com", // must
  "identityNumber": "12312322441", // optional
  "phone": "1231232233" // must
}
```

#### SignIn Response

```json
{
  "response": {
    "token": "eyJ.asd..",
    "email": "abuzer@komurcu.com"
  },
  "success": true,
  "message": "Success",
  "serverDate": "2024-01-18T00:29:44.8030844Z",
  "referenceNo": "f76c3553-d58b-4760-ae30-601de17827b9"
}
```

>Go to [HTTP](../../Requests/Authentication/SignInRequest.http) file
