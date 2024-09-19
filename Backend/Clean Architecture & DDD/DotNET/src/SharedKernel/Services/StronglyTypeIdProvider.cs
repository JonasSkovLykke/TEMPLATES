using SharedKernel.Primitives;

namespace SharedKernel.Services;

/// <summary>
/// A utility class that provides methods to work with strongly-typed IDs, particularly for converting between
/// strongly-typed IDs and nullable <see cref="Guid"/> values.
/// </summary>
/// <remarks>
/// This class supports operations for working with domain-specific, strongly-typed identifiers 
/// that wrap <see cref="Guid"/> values, offering conversion methods for easy manipulation and use.
/// </remarks>
public static class StronglyTypeIdProvider
{
    /// <summary>
    /// Converts a strongly-typed ID to a nullable <see cref="Guid"/> value.
    /// </summary>
    /// <typeparam name="T">The type of the strongly-typed ID that implements <see cref="IStronglyTypedId{Guid}"/>.</typeparam>
    /// <param name="id">The strongly-typed ID to be converted.</param>
    /// <returns>
    /// The nullable <see cref="Guid"/> value of the strongly-typed ID, or <c>null</c> if the provided ID is <c>null</c>.
    /// </returns>
    /// <remarks>
    /// This method allows the extraction of the underlying <see cref="Guid"/> value from a strongly-typed ID.
    /// </remarks>
    public static Guid? ConvertToGuid<T>(T? id)
        where T : IStronglyTypedId<Guid>
    {
        // If the provided ID is null, return null.
        if (id is null)
        {
            return null;
        }

        // Otherwise, return the Guid value of the strongly-typed ID.
        return id.Value;
    }

    /// <summary>
    /// Converts a nullable <see cref="Guid"/> value to a strongly-typed ID.
    /// </summary>
    /// <typeparam name="TId">The type of the strongly-typed ID that implements <see cref="IStronglyTypedId{Guid}"/>.</typeparam>
    /// <param name="guid">The nullable <see cref="Guid"/> value to be converted.</param>
    /// <returns>
    /// An instance of the strongly-typed ID, or <c>default(TId)</c> if the provided <see cref="Guid"/> is <c>null</c>.
    /// </returns>
    /// <remarks>
    /// This method uses <see cref="Activator.CreateInstance"/> to dynamically instantiate the strongly-typed ID from the provided <see cref="Guid"/>.
    /// </remarks>
    public static TId? ConvertToStronglyTypedId<TId>(Guid? guid)
        where TId : IStronglyTypedId<Guid>
    {
        // If the provided Guid is not null, create an instance of the strongly-typed ID using Activator.CreateInstance.
        if (guid.HasValue)
        {
            return (TId)Activator.CreateInstance(typeof(TId), guid)!;
        }

        // If the provided Guid is null, return default(TId).
        return default;
    }
}
