# DotNET API

## Security
- Security Scheme: Bearer
- Type: HTTP
- Description: Standard Authorization header using the Bearer scheme ("Bearer {token}")
- Scheme: Bearer
- Bearer Format: Bearer

## Users

### Get Users

#### Get Users Request

```js
GET /v1/Users
```

#### Get Users Response

```js
200 OK
```

```json
[
    {
        "id": "00000000-0000-0000-0000-000000000000",
        "email": "john@doe.com",
        "firstName": "John",
        "lastName": "Doe"
    },
    ...
]
```

```js
400 Bad Request
```

```json
{
  "type": "<string>",
  "title": "<string>",
  "status": "<integer>",
  "detail": "<string>",
  "instance": "<string>"
}
```

### Create User

#### Create User Request

```js
POST /v1/Users
```

```json
{
  "email": "john@doe.com",
  "password": "P@55w0rd",
  "firstName": "John",
  "lastName": "Doe"
}
```

#### Create User Response

```js
201 Created
```

```json
{
    "id": "00000000-0000-0000-0000-000000000000",
    "email": "john@doe.com",
    "firstName": "John",
    "lastName": "Doe"
}
```

```js
400 Bad Request
```

```json
{
  "type": "<string>",
  "title": "<string>",
  "status": "<integer>",
  "detail": "<string>",
  "instance": "<string>"
}
```

### Get User

#### Get User Request

```js
GET /v1/Users/{id:guid}
```

#### Get Users Response

```js
200 OK
```

```json
[
    {
        "id": "00000000-0000-0000-0000-000000000000",
        "email": "john@doe.com",
        "firstName": "John",
        "lastName": "Doe",
        "roles": [
            "Administrator",
            "User"
        ]
    },
    ...
]
```

```js
400 Bad Request
```

```json
{
  "type": "<string>",
  "title": "<string>",
  "status": "<integer>",
  "detail": "<string>",
  "instance": "<string>"
}
```

```js
404 Not Found
```

### Update User

#### Update User Request

```js
PUT /v1/Users/{id:guid}
```

```json
{
  "email": "john@doe.com",
  "firstName": "John",
  "lastName": "Doe",
  "roles": [
      1,
      2
  ]
}
```

#### Update User Response

```js
204 No Content
```

```js
400 Bad Request
```

```json
{
  "type": "<string>",
  "title": "<string>",
  "status": "<integer>",
  "detail": "<string>",
  "instance": "<string>"
}
```

### Delete User

#### Delete User Request

```js
DELETE /v1/Users/{id:guid}
```

#### Delete User Response

```js
204 No Content
```

```js
400 Bad Request
```

```json
{
  "type": "<string>",
  "title": "<string>",
  "status": "<integer>",
  "detail": "<string>",
  "instance": "<string>"
}
```
