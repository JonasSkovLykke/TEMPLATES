namespace SharedKernel.Primitives;

/// <summary>
/// An interface representing a strongly-typed identifier with a specified type.
/// </summary>
/// <typeparam name="T">The type of the underlying value of the identifier.</typeparam>
public interface IStronglyTypedId<T>
{
    /// <summary>
    /// Gets the underlying value of the strongly-typed identifier.
    /// </summary>
    T Value { get; }
}
