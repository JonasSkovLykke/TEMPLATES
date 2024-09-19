namespace SharedKernel.Primitives;

/// <summary>
/// An abstract base class representing a generic enumeration with an identifier and a name.
/// </summary>
public abstract class BaseEnumEntity
{
    /// <summary>
    /// Gets or initializes the unique identifier of the enumeration.
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// Gets or initializes the name of the enumeration.
    /// </summary>
    public string Name { get; init; } = null!;
}
