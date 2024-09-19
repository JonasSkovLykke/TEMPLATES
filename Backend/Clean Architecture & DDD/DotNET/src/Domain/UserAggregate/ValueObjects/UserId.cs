using SharedKernel.Primitives;

namespace Domain.UserAggregate.ValueObjects;

/// <summary>
/// A sealed record representing a strongly-typed unique identifier for a user entity.
/// </summary>
public sealed record UserId(Guid Value) : ValueObject, IStronglyTypedId<Guid>
{
    /// <summary>
    /// Creates a new unique user ID with a random Guid value.
    /// </summary>
    /// <returns>A new instance of <see cref="UserId"/> representing a unique user identifier.</returns>
    public static UserId Create()
        => new(Guid.NewGuid());
}
