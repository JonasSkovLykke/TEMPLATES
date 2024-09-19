# DotNET API

## Security
- Security Scheme: Bearer
- Type: HTTP
- Description: Standard Authorization header using the Bearer scheme ("Bearer {token}")
- Scheme: Bearer
- Bearer Format: Bearer

## Authentication

### Register

#### Register Request

```js
POST /Authentication/register
```

```json
{
  "email": "john@doe.com",
  "password": "P@55w0rd"
}
```

#### Register Response

```js
200 OK
```

```js
400 Bad Request
```

```json
{
  "type": "https://tools.ietf.org/html/rfc9110#section-15.5.1",
  "title": "One or more validation errors occurred.",
  "status": 400,
  "errors": {
    "InvalidEmail": [
      "Email 'john@doe.com' is invalid."
    ],
    "PasswordRequiresNonAlphanumeric": [
      "Passwords must have at least one non alphanumeric character."
    ],
    "PasswordRequiresDigit": [
      "Passwords must have at least one digit ('0'-'9')."
    ],
    "PasswordRequiresUpper": [
      "Passwords must have at least one uppercase ('A'-'Z')."
    ]
  }
}
```

### Login

#### Login Request

```js
POST /Authentication/login?useCookies=<boolean>&useSessionCookies=<boolean>
```

```json
{
  "email": "john@doe.com",
  "password": "P@55w0rd",
  // "twoFactorCode": "string",
  // "twoFactorRecoveryCode": "string"
}
```

#### Login Response

```js
200 OK
```

```json
{
  "tokenType": "Bearer",
  "accessToken": "CfDJ8G33C5hpltNBrN5kSdgL_me3CTb36tzp2XSdoSNn0Tn0pSpQcTsgEi4VXf_WHMwHrxETJxKITneU7QyIVENIaexg0wid3_OCpeH-uW1aqvYdF2g3j2UpXcLkGd08b2i50r0AmJO9V9BWiOjUp-YqDVtNpj5Q4gZqQCA6yvHl7lL4NdxGyfsj3ZYUtsWrarr2pO6J2vjopZUxHRa16OYPp-g3AulP-OsxuwqI20Shn_m1snHroQtLF1rHAYZfnb4zpEWbE4w3HRwW3xBS2tWMA4iW1UVeOHM7j_EACb4JjSxP9rgmiQaq5E9nTzGum2r017_ilXhmNSYnQs_Wewr9yWD_oV8G-byAuSgLLUed4lVffAOntYEspPVn9_6H4TsKaQWgqwiZXFUoktSdhw5r1slnDVQFma7qxbMLY6LVknDO62vXQV1lFvFme2wF-OJ0dNv1Zj0whDFce9VvDkPcr9wLJZEomJmAcExWUkjKZIOG4Z1XZppMGHjumHdh4T2jcMYURG8isqK0rOFUVJLCfDBKfO_nFpSiJUs4BalFpjZxXyy8MR8Zs3VNZQzgPKdteWh0UfofPFo9rENRIvkyb3HKfsGKxxuj0AuEfmlSPUpicaP5HUOYvnB2BEjjq68OGDrbR1xPSROpr4YLP-KyBVw",
  "expiresIn": 3600,
  "refreshToken": "CfDJ8G33C5hpltNBrN5kSdgL_me3CTb36tzp2XSdoSNn0Tn0pSpQcTsgEi4VXf_WHMwHrxETJxKITneU7QyIVENIaexg0wid3_OCpeH-uW1aqvYdF2g3j2UpXcLkGd08b2i50r0AmJO9V9BWiOjUp-YqDVtNpj5Q4gZqQCA6yvHl7lL4NdxGyfsj3ZYUtsWrarr2pO6J2vjopZUxHRa16OYPp-g3AulP-OsxuwqI20Shn_m1snHroQtLF1rHAYZfnb4zpEWbE4w3HRwW3xBS2tWMA4iW1UVeOHM7j_EACb4JjSxP9rgmiQaq5E9nTzGum2r017_ilXhmNSYnQs_Wewr9yWD_oV8G-byAuSgLLUed4lVffAOntYEspPVn9_6H4TsKaQWgqwiZXFUoktSdhw5r1slnDVQFma7qxbMLY6LVknDO62vXQV1lFvFme2wF-OJ0dNv1Zj0whDFce9VvDkPcr9wLJZEomJmAcExWUkjKZIOG4Z1XZppMGHjumHdh4T2jcMYURG8isqK0rOFUVJLCfDBKfO_nFpSiJUs4BalFpjZxXyy8MR8Zs3VNZQzgPKdteWh0UfofPFo9rENRIvkyb3HKfsGKxxuj0AuEfmlSPUpicaP5HUOYvnB2BEjjq68OGDrbR1xPSROpr4YLP-KyBVw"
}
```

### RefreshToken

#### RefreshToken Request

```js
POST /Authentication/refresh
```

```json
{
  "refreshToken": "CfDJ8G33C5hpltNBrN5kSdgL_me3CTb36tzp2XSdoSNn0Tn0pSpQcTsgEi4VXf_WHMwHrxETJxKITneU7QyIVENIaexg0wid3_OCpeH-uW1aqvYdF2g3j2UpXcLkGd08b2i50r0AmJO9V9BWiOjUp-YqDVtNpj5Q4gZqQCA6yvHl7lL4NdxGyfsj3ZYUtsWrarr2pO6J2vjopZUxHRa16OYPp-g3AulP-OsxuwqI20Shn_m1snHroQtLF1rHAYZfnb4zpEWbE4w3HRwW3xBS2tWMA4iW1UVeOHM7j_EACb4JjSxP9rgmiQaq5E9nTzGum2r017_ilXhmNSYnQs_Wewr9yWD_oV8G-byAuSgLLUed4lVffAOntYEspPVn9_6H4TsKaQWgqwiZXFUoktSdhw5r1slnDVQFma7qxbMLY6LVknDO62vXQV1lFvFme2wF-OJ0dNv1Zj0whDFce9VvDkPcr9wLJZEomJmAcExWUkjKZIOG4Z1XZppMGHjumHdh4T2jcMYURG8isqK0rOFUVJLCfDBKfO_nFpSiJUs4BalFpjZxXyy8MR8Zs3VNZQzgPKdteWh0UfofPFo9rENRIvkyb3HKfsGKxxuj0AuEfmlSPUpicaP5HUOYvnB2BEjjq68OGDrbR1xPSROpr4YLP-KyBVw"
}
```

#### RefreshToken Response

```js
200 OK
```

```json
{
  "tokenType": "Bearer",
  "accessToken": "CfDJ8G33C5hpltNBrN5kSdgL_me3CTb36tzp2XSdoSNn0Tn0pSpQcTsgEi4VXf_WHMwHrxETJxKITneU7QyIVENIaexg0wid3_OCpeH-uW1aqvYdF2g3j2UpXcLkGd08b2i50r0AmJO9V9BWiOjUp-YqDVtNpj5Q4gZqQCA6yvHl7lL4NdxGyfsj3ZYUtsWrarr2pO6J2vjopZUxHRa16OYPp-g3AulP-OsxuwqI20Shn_m1snHroQtLF1rHAYZfnb4zpEWbE4w3HRwW3xBS2tWMA4iW1UVeOHM7j_EACb4JjSxP9rgmiQaq5E9nTzGum2r017_ilXhmNSYnQs_Wewr9yWD_oV8G-byAuSgLLUed4lVffAOntYEspPVn9_6H4TsKaQWgqwiZXFUoktSdhw5r1slnDVQFma7qxbMLY6LVknDO62vXQV1lFvFme2wF-OJ0dNv1Zj0whDFce9VvDkPcr9wLJZEomJmAcExWUkjKZIOG4Z1XZppMGHjumHdh4T2jcMYURG8isqK0rOFUVJLCfDBKfO_nFpSiJUs4BalFpjZxXyy8MR8Zs3VNZQzgPKdteWh0UfofPFo9rENRIvkyb3HKfsGKxxuj0AuEfmlSPUpicaP5HUOYvnB2BEjjq68OGDrbR1xPSROpr4YLP-KyBVw",
  "expiresIn": 3600,
  "refreshToken": "CfDJ8G33C5hpltNBrN5kSdgL_me3CTb36tzp2XSdoSNn0Tn0pSpQcTsgEi4VXf_WHMwHrxETJxKITneU7QyIVENIaexg0wid3_OCpeH-uW1aqvYdF2g3j2UpXcLkGd08b2i50r0AmJO9V9BWiOjUp-YqDVtNpj5Q4gZqQCA6yvHl7lL4NdxGyfsj3ZYUtsWrarr2pO6J2vjopZUxHRa16OYPp-g3AulP-OsxuwqI20Shn_m1snHroQtLF1rHAYZfnb4zpEWbE4w3HRwW3xBS2tWMA4iW1UVeOHM7j_EACb4JjSxP9rgmiQaq5E9nTzGum2r017_ilXhmNSYnQs_Wewr9yWD_oV8G-byAuSgLLUed4lVffAOntYEspPVn9_6H4TsKaQWgqwiZXFUoktSdhw5r1slnDVQFma7qxbMLY6LVknDO62vXQV1lFvFme2wF-OJ0dNv1Zj0whDFce9VvDkPcr9wLJZEomJmAcExWUkjKZIOG4Z1XZppMGHjumHdh4T2jcMYURG8isqK0rOFUVJLCfDBKfO_nFpSiJUs4BalFpjZxXyy8MR8Zs3VNZQzgPKdteWh0UfofPFo9rENRIvkyb3HKfsGKxxuj0AuEfmlSPUpicaP5HUOYvnB2BEjjq68OGDrbR1xPSROpr4YLP-KyBVw"
}
```

### Confirm Email

#### Confirm Email Request

```js
GET /Authentication/confirmEmail?userId=<string>&code=<string>&changedEmail=<string>
```

#### Confirm Email Response
```js
200 OK
```

### Resend Confirmation Email

#### Resend Confirmation Email Request

```js
POST /Authentication/resendConfirmationEmail
```

```json
{
  "email": "john@doe.com"
}
```

#### Resend Confirmation Email Response
```js
200 OK
```

### Forgot Password

#### Forgot Password Request

```js
POST /Authentication/forgotPassword
```

```json
{
  "email": "john@doe.com"
}
```

#### Forgot Password Response
```js
200 OK
```

```js
400 Bad Request
```

```json
{
  "type": "https://tools.ietf.org/html/rfc9110#section-15.5.1",
  "title": "One or more validation errors occurred.",
  "status": 400,
  "errors": {
    "InvalidEmail": [
      "Email 'john@doe.com' is invalid."
    ]
  }
}
```

### Reset Password

#### Reset Password Request

```js
POST /Authentication/resetPassword
```

```json
{
  "email": "john@doe.com",
  "code": "CfDJ8G33C5hpltNBrN5kSdgL_me3CTb36tzp2XSdoSNn0Tn0pSpQcTsgEi4VXf_WHMwHrxETJxKITneU7QyIVENIaexg0wid3_OCpeH-uW1aqvYdF2g3j2UpXcLkGd08b2i50r0AmJO9V9BWiOjUp-YqDVtNpj5Q4gZqQCA6yvHl7lL4NdxGyfsj3ZYUtsWrarr2pO6J2vjopZUxHRa16OYPp-g3AulP-OsxuwqI20Shn_m1snHroQtLF1rHAYZfnb4zpEWbE4w3HRwW3xBS2tWMA4iW1UVeOHM7j_EACb4JjSxP9rgmiQaq5E9nTzGum2r017_ilXhmNSYnQs_Wewr9yWD_oV8G-byAuSgLLUed4lVffAOntYEspPVn9_6H4TsKaQWgqwiZXFUoktSdhw5r1slnDVQFma7qxbMLY6LVknDO62vXQV1lFvFme2wF-OJ0dNv1Zj0whDFce9VvDkPcr9wLJZEomJmAcExWUkjKZIOG4Z1XZppMGHjumHdh4T2jcMYURG8isqK0rOFUVJLCfDBKfO_nFpSiJUs4BalFpjZxXyy8MR8Zs3VNZQzgPKdteWh0UfofPFo9rENRIvkyb3HKfsGKxxuj0AuEfmlSPUpicaP5HUOYvnB2BEjjq68OGDrbR1xPSROpr4YLP-KyBVw", // Confirmation code received in email
  "password": "NewP@55w0rd"
}
```

#### Reset Password Response
```js
200 OK
```

```js
400 Bad Request
```

```json
{
  "type": "https://tools.ietf.org/html/rfc9110#section-15.5.1",
  "title": "One or more validation errors occurred.",
  "status": 400,
  "errors": {
    "InvalidEmail": [
      "Email 'john@doe.com' is invalid."
    ]
  }
}
```

### Manage Two-Factor Authentication

#### Manage Two-Factor Authentication Request

```js
POST /Authentication/manage/2fa
```

```json
{
  "enable": "<boolean>",
  "twoFactorCode": "<string>",
  "resetSharedKey": "<boolean>",
  "resetRecoveryCodes": "<boolean>",
  "forgetMachine": "<boolean>"
}
```

#### Manage Two-Factor Authentication Response
```js
200 OK
```

```json
{
  "sharedKey": "<string>",
  "recoveryCodesLeft": "<integer>",
  "recoveryCodes": [
    "<string>",
    "<string>"
  ],
  "isTwoFactorEnabled": "<boolean>",
  "isMachineRemembered": "<boolean>"
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
  "instance": "<string>",
  "errors": {
    "magna_2": [
      "<string>",
      "<string>"
    ]
  },
  "adipisicing_62": {},
  "velite_": {},
  "ullamco756": {}
}
```

```js
404 Not Found
```

### Get User Information

#### Get User Information Request

```js
GET /Authentication/manage/info
```

#### Get User Information Response
```js
200 OK
```

```json
{
  "email": "<string>",
  "isEmailConfirmed": "<boolean>",
  "claims": {
    "sint_5": "<string>"
  }
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
  "instance": "<string>",
  "errors": {
    "magna_2": [
      "<string>",
      "<string>"
    ]
  },
  "adipisicing_62": {},
  "velite_": {},
  "ullamco756": {}
}
```

```js
404 Not Found
```

### Update User Information

#### Update User Information Request

```js
POST  /Authentication/manage/info
```

```json
{
  "newEmail": "john@doe.com",
  "newPassword": "NewP@55w0rd",
  "oldPassword": "NewP@55w0rd"
}
```

#### Update User Information Response
```js
200 OK
```

```json
{
  "email": "<string>",
  "isEmailConfirmed": "<boolean>",
  "claims": {
    "sint_5": "<string>"
  }
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
  "instance": "<string>",
  "errors": {
    "magna_2": [
      "<string>",
      "<string>"
    ]
  },
  "adipisicing_62": {},
  "velite_": {},
  "ullamco756": {}
}
```

```js
404 Not Found
```
