# ExpenseManagement API

- [ExpenseManagement API](#expensemanagement-api)
  - [Expenses](#expenses)
    - [GetAllExpenses](#getallexpenses)
      - [GetAllExpenses Request](#getallexpenses-request)
      - [GetAllExpenses Response](#getallexpenses-response)
    - [GetExpenseById](#getexpensebyid)
      - [GetExpenseById Request](#getexpensebyid-request)
      - [GetExpenseById Response](#getexpensebyid-response)
    - [GetExpenseBy](#getexpenseby)
      - [GetExpenseBy Request](#getexpenseby-request)
      - [GetExpenseBy Response](#getexpenseby-response)
    - [GetExpenseByAppUser](#getexpensebyappuser)
      - [GetExpenseByAppUser Request](#getexpensebyappuser-request)
      - [GetExpenseByAppUser Response](#getexpensebyappuser-response)
    - [CreateExpense](#createexpense)
      - [CreateExpense Request](#createexpense-request)
      - [CreateExpense Response](#createexpense-response)
    - [UpdateExpense](#updateexpense)
      - [UpdateExpense Request](#updateexpense-request)
      - [UpdateExpense Response](#updateexpense-response)
    - [ApproveExpense](#approveexpense)
      - [ApproveExpense Request](#approveexpense-request)
      - [ApproveExpense Response](#approveexpense-response)
    - [DeleteExpense](#deleteexpense)
      - [DeleteExpense Request](#deleteexpense-request)
      - [DeleteExpense Response](#deleteexpense-response)

## Expenses

```http
https://localhost:7066/api/Expenses
```

### GetAllExpenses

#### GetAllExpenses Request

```http
@token=ey.J.asd..
GET https://localhost:7066/api/Expenses
Authorization: Bearer {{token}}
```

#### GetAllExpenses Response

```json
{
  "response": [
    {
      "id": 1,
      "employeeName": "Hayri Petek",
      "amount": 1230,
      "expensedDate": "2024-01-19T01:26:21.234",
      "description": "Nasıl bilinmeyen bir harcama?",
      "status": "Rejected",
      "receiptPhotoUrl": "Fotoğraf çekincek ekhleneceh deyolla. Baken olce mi?"
    },
    {
      "id": 2,
      "employeeName": "Hayri Petek",
      "amount": 1230,
      "expensedDate": "2024-01-19T01:26:21.234",
      "description": "Çok gerekliydi aldım.",
      "status": "Approved",
      "receiptPhotoUrl": "Fotoğraf çekincek ekhleneceh deyolla. Baken olce mi?"
    },
    {
      "id": 3,
      "employeeName": "abuzer kömürcü",
      "amount": 10,
      "expensedDate": "2024-01-19T05:29:55.275",
      "description": "acayip harcadım!",
      "status": "Approved",
      "receiptPhotoUrl": "asdasdasdqweqwe"
    }
  ],
  "success": true,
  "message": "Success",
  "serverDate": "2024-01-19T23:34:31.0867865Z",
  "referenceNo": "2cd56e7e-b136-46f0-be60-d445cc670eff"
}
```

> Go to [HTTP](/Responses/Expense/GetAllExpensesResponse.http) file.

### GetExpenseById

#### GetExpenseById Request

```http
@id=2
@token=ey.J.asd..
GET https://localhost:7066/api/Expenses/{{id}}
Authorization: Bearer {{token}}
```

#### GetExpenseById Response

```json
{
  "response": {
    "id": 2,
    "employeeName": "Hayri Petek",
    "amount": 1230,
    "expensedDate": "2024-01-19T01:26:21.234",
    "description": "Çok gerekliydi aldım.",
    "status": "Approved",
    "receiptPhotoUrl": "Fotoğraf çekincek ekhleneceh deyolla. Baken olce mi?"
  },
  "success": true,
  "message": "Success",
  "serverDate": "2024-01-19T23:34:31.0867865Z",
  "referenceNo": "2cd56e7e-b136-46f0-be60-d445cc670eff"
}
```

> Go to [HTTP](/Responses/Expense/GetExpenseById.http) file.

### GetExpenseBy

#### GetExpenseBy Request

```http
#optional
@status=1
#optional
@expensedDate="01-17-2024"
#optional
@amount=1230
@token=ey.J.asd..

GET https://localhost:7066/api/Expenses/GetBy?Status={{status}}&ExpensedDate={{expensedDate}}&Amount={{amount}}
Authorization: Bearer {{token}}
```

#### GetExpenseBy Response

```json
{
  "response": {
    "": 2,
    "employeeName": "Hayri Petek",
    "amount": 1230,
    "expensedDate": "2024-01-19T01:26:21.234",
    "description": "Çok gerekliydi aldım.",
    "status": "Approved",
    "receiptPhotoUrl": "Fotoğraf çekincek ekhleneceh deyolla. Baken olce mi?"
  },
  "success": true,
  "message": "Success",
  "serverDate": "2024-01-19T23:34:31.0867865Z",
  "referenceNo": "2cd56e7e-b136-46f0-be60-d445cc670eff"
}
```

> Go to [HTTP](/Responses/Expense/GetExpenseBy.http) file.

### GetExpenseByAppUser

#### GetExpenseByAppUser Request

```http
@appUserId=2
@token=ey.J.asd..
GET https://localhost:7066/api/Expenses?AppUserId={{appUserId}}
Authorization: Bearer {{token}}
```

#### GetExpenseByAppUser Response

```json
{
  "response": {
    "id": 2,
    "employeeName": "Hayri Petek",
    "amount": 1230,
    "expensedDate": "2024-01-19T01:26:21.234",
    "description": "Çok gerekliydi aldım.",
    "status": "Approved",
    "receiptPhotoUrl": "Fotoğraf çekincek ekhleneceh deyolla. Baken olce mi?"
  },
  "success": true,
  "message": "Success",
  "serverDate": "2024-01-19T23:34:31.0867865Z",
  "referenceNo": "2cd56e7e-b136-46f0-be60-d445cc670eff"
}
```

> Go to [HTTP](/Responses/Expense/GetExpenseByAppUser.http) file.

### CreateExpense

#### CreateExpense Request

```http
@host=https://localhost:7066
@token=ey.JjLBw..

POST {{host}}/api/Expenses/Create
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

#### CreateExpense Response

```json
{
  "success": true,
  "message": "Business Meal is created successfully!",
  "serverDate": "2024-01-19T23:23:12.964Z",
  "referenceNo": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
}
```

### UpdateExpense

#### UpdateExpense Request

```http
@host=https://localhost:7066
@token=ey.JjLBw..

PUT {{host}}/api/Expenses/Update
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

#### UpdateExpense Response

```json
{
  "success": true,
  "message": "1 is updated successfully!",
  "serverDate": "2024-01-19T23:23:12.964Z",
  "referenceNo": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
}
```

> Go to [HTTP](/Requests/Expense/UpdateExpenseRequest.http) file.

### ApproveExpense

#### ApproveExpense Request

```http
@host=https://localhost:7066
@token=ey.JjLBw..

PATCH  {{host}}/api/Expenses/Approve
Content-Type: application/json
Authorization: Bearer {{token}}


# status: [0]pending, [1]accepted, [2]rejected
# expenseId: The id value of approving expense
# userId: The value of approverId.
# description (optional): If reject the expense you have to fill.
{
  "accessToken": "asdasd",
  "userId": 1,
  "requestTimestamp": "2024-01-19T23:39:01.831Z",
  "status": 2,
  "expenseId": 1,
  "description": "Bana saçma geldi!"
}
```

#### ApproveExpense Response

```json
{
  "success": true,
  "message": "1 is approved successfully!",
  "serverDate": "2024-01-19T23:23:12.964Z",
  "referenceNo": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
}
```

> Go to [HTTP](/Requests/Expense/ApproveExpenseRequest.http) file.

### DeleteExpense

#### DeleteExpense Request

```http
@host=https://localhost:7066
@id=1
@token=ey.JjLBw..

DELETE {{host}}/api/Expenses/Delete/{{id}}
Authorization: Bearer {{token}}
```

#### DeleteExpense Response

```json
{
  "success": true,
  "message": "1 is deleted successfully!",
  "serverDate": "2024-01-19T23:23:12.964Z",
  "referenceNo": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
}
```

> Go to [HTTP](/Requests/Expense/DeleteExpenseRequest.http) file.
> Back to [README.md](../../README.md) file.
