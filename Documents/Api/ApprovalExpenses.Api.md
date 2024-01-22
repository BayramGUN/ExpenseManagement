# ExpenseManagement API

- [ExpenseManagement API](#expensemanagement-api)
  - [ExpenseApprovals](#expenseapprovals)
    - [GetAllExpenseApprovals](#getallexpenseapprovals)
      - [GetAllExpenseApprovals Request](#getallexpenseapprovals-request)
      - [GetAllExpenseApprovals Response](#getallexpenseapprovals-response)
    - [GetExpenseApprovalById](#getexpenseapprovalbyid)
      - [GetExpenseApprovalById Request](#getexpenseapprovalbyid-request)
      - [GetExpenseApprovalById Response](#getexpenseapprovalbyid-response)
    - [GetExpenseApprovalsBy](#getexpenseapprovalsby)
      - [GetExpenseApprovalsBy Request](#getexpenseapprovalsby-request)
      - [GetExpenseApprovalsBy Response](#getexpenseapprovalsby-response)
    - [GetExpenseApprovalByAppUser](#getexpenseapprovalbyappuser)
      - [GetExpenseApprovalByAppUser Request](#getexpenseapprovalbyappuser-request)
      - [GetExpenseApprovalByAppUser Response](#getexpenseapprovalbyappuser-response)
    - [CreateExpenseApproval](#createexpenseapproval)
      - [CreateExpenseApproval Request](#createexpenseapproval-request)
      - [CreateExpenseApproval Response](#createexpenseapproval-response)
    - [UpdateExpenseApproval](#updateexpenseapproval)
      - [UpdateExpenseApproval Request](#updateexpenseapproval-request)
      - [UpdateExpenseApproval Response](#updateexpenseapproval-response)
    - [DeleteExpenseApproval](#deleteexpenseapproval)
      - [DeleteExpenseApproval Request](#deleteexpenseapproval-request)
      - [DeleteExpenseApproval Response](#deleteexpenseapproval-response)

## ExpenseApprovals

```http
https://localhost:7066/api/ExpenseApprovals
```

### GetAllExpenseApprovals

#### GetAllExpenseApprovals Request

```http
@token=ey.J.asd..
GET https://localhost:7066/api/ExpenseApprovals
Authorization: Bearer {{token}}
```

#### GetAllExpenseApprovals Response

```json
{
  "response": [
    {
      "id": 1,
      "approverName": "Basri Bal",
      "description": "Nasıl bilinmeyen bir harcama?",
      "status": "Rejected",
      "expenseTitle": "Fotoğraf"
    },
    {
      "id": 2,
      "approverName": "Basri Bal",
      "description": "Nasıl bilinmeyen bir harcama ule?",
      "status": "Rejected",
      "expenseTitle": "Fotoğraf Çekindik"
    }
  ],
  "success": true,
  "message": "Success",
  "serverDate": "2024-01-19T23:34:31.0867865Z",
  "referenceNo": "2cd56e7e-b136-46f0-be60-d445cc670eff"
}
```

> Go to [HTTP](/Responses/ExpenseApproval/GetAllExpenseApprovalsResponse.http) file.

### GetExpenseApprovalById

#### GetExpenseApprovalById Request

```http
@id=2
@token=ey.J.asd..
GET https://localhost:7066/api/ExpenseApprovals/{{id}}
Authorization: Bearer {{token}}
```

#### GetExpenseApprovalById Response

```json
{
  "response": {
    "id": 1,
    "approverName": "Basri Bal",
    "description": "Nasıl bilinmeyen bir harcama?",
    "status": "Rejected",
    "expenseTitle": "Fotoğraf"
  },
  "success": true,
  "message": "Success",
  "serverDate": "2024-01-19T23:34:31.0867865Z",
  "referenceNo": "2cd56e7e-b136-46f0-be60-d445cc670eff"
}
```

> Go to [HTTP](/Responses/ExpenseApproval/GetExpenseApprovalById.http) file.

### GetExpenseApprovalsBy

#### GetExpenseApprovalsBy Request

```http
#optional
@status=1
#optional
@expenseApprovaldDate="01-17-2024"
#optional
@amount=1230
@token=ey.J.asd..

GET https://localhost:7066/api/ExpenseApprovals/GetBy?Status={{status}}&ExpenseApprovaldDate={{expenseApprovaldDate}}&Amount={{amount}}
Authorization: Bearer {{token}}
```

#### GetExpenseApprovalsBy Response

```json
{
  "response": {
      "id": 1,
      "approverName": "Basri Bal",
      "description": "Nasıl bilinmeyen bir harcama?",
      "status": "Rejected",
      "expenseTitle": "Fotoğraf"
    },
  "success": true,
  "message": "Success",
  "serverDate": "2024-01-19T23:34:31.0867865Z",
  "referenceNo": "2cd56e7e-b136-46f0-be60-d445cc670eff"
}
```

> Go to [HTTP](/Responses/ExpenseApproval/GetExpenseApprovalsBy.http) file.

### GetExpenseApprovalByAppUser

#### GetExpenseApprovalByAppUser Request

```http
@appUserId=2
@token=ey.J.asd..
GET https://localhost:7066/api/ExpenseApprovals?AppUserId={{appUserId}}
Authorization: Bearer {{token}}
```

#### GetExpenseApprovalByAppUser Response

```json
{
  "response": {
      "id": 1,
      "approverName": "Basri Bal",
      "description": "Nasıl bilinmeyen bir harcama?",
      "status": "Rejected",
      "expenseTitle": "Fotoğraf"
},
  "success": true,
  "message": "Success",
  "serverDate": "2024-01-19T23:34:31.0867865Z",
  "referenceNo": "2cd56e7e-b136-46f0-be60-d445cc670eff"
}
```

> Go to [HTTP](/Responses/ExpenseApproval/GetExpenseApprovalsByAppUser.http) file.

### CreateExpenseApproval

#### CreateExpenseApproval Request

```http
@host=https://localhost:7066
@token=ey.JjLBw..

POST {{host}}/api/ExpenseApprovals/Create
Content-Type: application/json
Authorization: Bearer {{token}}

# userId: That is id value of the creator of user. 

{
  "accessToken": "abc123xyz456",
  "userId": 2,
  "requestTimestamp": "2024-01-19T23:23:12.962Z",
  "expenseId": 1,
  "description": "Dinner with clients",
  "approverId": 2,
  "approvalStatus": "Rejected"
}
```

#### CreateExpenseApproval Response

```json
{
  "success": true,
  "message": "Dinner with clients is created successfully!",
  "serverDate": "2024-01-19T23:23:12.964Z",
  "referenceNo": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
}
```

### UpdateExpenseApproval

#### UpdateExpenseApproval Request

```http
@host=https://localhost:7066
@token=ey.JjLBw..

PUT {{host}}/api/ExpenseApprovals/Update
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

#### UpdateExpenseApproval Response

```json
{
  "success": true,
  "message": "1 is updated successfully!",
  "serverDate": "2024-01-19T23:23:12.964Z",
  "referenceNo": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
}
```

> Go to [HTTP](/Requests/ExpenseApproval/UpdateExpenseApprovalRequest.http) file.

### DeleteExpenseApproval

#### DeleteExpenseApproval Request

```http
@host=https://localhost:7066
@id=1
@token=ey.JjLBw..

DELETE {{host}}/api/ExpenseApprovals/Delete/{{id}}
Authorization: Bearer {{token}}
```

#### DeleteExpenseApproval Response

```json
{
  "success": true,
  "message": "1 is deleted successfully!",
  "serverDate": "2024-01-19T23:23:12.964Z",
  "referenceNo": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
}
```

> Go to [HTTP](/Requests/ExpenseApproval/DeleteExpenseApprovalRequest.http) file.
> Back to [README.md](../../README.md) file.
