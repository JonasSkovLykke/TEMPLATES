# Domain Aggregates

## User

```csharp
class User
{
    User Create(int identityUserId, Email email, string firstName, string lastName, UserId? userId = null);
    void Update(mail email, string firstName, string lastName, ICollection<int>? roles);
    void Delete();
}
```

```json
{
    "id": "00000000-0000-0000-0000-000000000000",
    "email": "john@doe.com",
    "password": "P@55w0rd",
    "firstName": "John",
    "lastName": "Doe",
    "createdDateTime": "0000-00-00T00:00:00.0000000Z",
    "updatedDateTime": "0000-00-00T00:00:00.0000000Z"
}