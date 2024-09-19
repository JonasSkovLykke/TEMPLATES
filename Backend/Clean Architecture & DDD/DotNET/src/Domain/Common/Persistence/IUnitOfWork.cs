namespace Domain.Common.Persistence;

/// <summary>
/// Represents a unit of work for persisting changes to the underlying data store.
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// Asynchronously saves changes to the underlying data store.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task representing the asynchronous save operation.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
