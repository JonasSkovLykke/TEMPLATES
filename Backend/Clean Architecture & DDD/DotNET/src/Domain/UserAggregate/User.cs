using Domain.UserAggregate.Events;
using Domain.UserAggregate.ValueObjects;
using SharedKernel.Primitives;
using SharedKernel.ValueObjects;

namespace Domain.UserAggregate;

public sealed class User : AggregateRoot<UserId>, IAuditableEntity<int?>
{
    private readonly List<ApiKey> _apiKeys = [];
    
    public int IdentityUserId { get; private init; }

    public Email Email { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; } = PhoneNumber.Create();
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;

    public IReadOnlyList<ApiKey> ApiKeys => [.. _apiKeys];

    public DateTimeOffset CreatedDateTimeOffset { get; set; }
    public int? CreatedBy { get; set; }
    public DateTimeOffset? UpdatedDateTimeOffset { get; set; }
    public int? UpdatedBy { get; set; }

    private User(
        int identityUserId,
        Email email,
        PhoneNumber phoneNumber,
        string firstName,
        string lastName,
        UserId? userId = null)
        : base(userId ?? UserId.Create())
    {
        IdentityUserId = identityUserId;
        Email = email;
        PhoneNumber = phoneNumber;
        FirstName = firstName;
        LastName = lastName;
    }

    public static User Create(
        int identityUserId,
        Email email,
        PhoneNumber phoneNumber,
        string firstName,
        string lastName) => new(
            identityUserId,
            email,
            phoneNumber,
            firstName,
            lastName);

    public void Update(
        Email email,
        PhoneNumber phoneNumber,
        string firstName,
        string lastName,
        ICollection<int>? roles = null)
    {
        Email = email;
        PhoneNumber = phoneNumber;
        FirstName = firstName;
        LastName = lastName;

        RaiseDomainEvent(new UserUpdatedDomainEvent(
            Guid.NewGuid(),
            IdentityUserId,
            email,
            phoneNumber,
            roles));
    }

    public void Delete()
    {
        RaiseDomainEvent(new UserDeletedDomainEvent(
            Guid.NewGuid(),
            IdentityUserId));
    }

    /// <summary>
    /// Private constructor without parameters required for entity framework (EFCore) to work.
    /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
    private User() : base(default)
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
    }
}
