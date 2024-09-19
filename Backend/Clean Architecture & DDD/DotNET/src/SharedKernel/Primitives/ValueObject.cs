namespace SharedKernel.Primitives;

/// <summary>
/// An abstract base class representing a value object in the domain model.
/// </summary>
/// <remarks>
/// Value objects are immutable and represent attributes of an entity or concept within the domain.
/// They have no distinct identity and are used for modeling concepts that have no individual lifecycle.
/// </remarks>
public abstract record ValueObject;
