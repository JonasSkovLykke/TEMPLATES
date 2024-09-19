using System.Linq.Expressions;

namespace Infrastructure.Persistence.Extensions;

/// <summary>
/// Extension methods for IQueryable to conditionally apply the WHERE clause based on a specified condition.
/// </summary>
public static class QueryableExtensions
{
    /// <summary>
    /// Conditionally applies a WHERE clause to the IQueryable based on the specified condition.
    /// </summary>
    /// <typeparam name="T">The type of elements in the IQueryable.</typeparam>
    /// <param name="queryable">The IQueryable to filter.</param>
    /// <param name="condition">The condition to determine whether to apply the WHERE clause.</param>
    /// <param name="predicate">The predicate used in the WHERE clause.</param>
    /// <returns>
    /// If the condition is true, returns a new IQueryable with the WHERE clause applied;
    /// otherwise, returns the original IQueryable.
    /// </returns>
    public static IQueryable<T> WhereIf<T>(
        this IQueryable<T> queryable,
        bool condition,
        Expression<Func<T, bool>> predicate)
    {
        // Check if the condition is true
        if (condition)
        {
            // If true, apply the WHERE clause using the specified predicate
            return queryable.Where(predicate);
        }

        // If the condition is false, return the original IQueryable without modification
        return queryable;
    }
}
