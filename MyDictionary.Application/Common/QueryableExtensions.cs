using System.Linq.Expressions;

public static class QueryableExtensions
{
    public static IQueryable<T> WhereIf<T>(
        this IQueryable<T> queryable,
        bool condition,
        Expression<Func<T, bool>> predicate)
    {
        if (condition)
            return queryable.Where(predicate);

        return queryable;
    }

    public static IQueryable<T> WhereIfHasKey<T>(
        this IQueryable<T> queryable,
        Guid? value,
        Expression<Func<T, bool>> predicate)
    {
        if (value.HasValue && value != Guid.Empty)
            return queryable.Where(predicate);

        return queryable;
    }

    public static IQueryable<T?> WhereIfNotEmpty<T>(
        this IQueryable<T> queryable,
        string? value,
        Expression<Func<T, bool>> predicate)
    {
        if (!string.IsNullOrEmpty(value))
            return queryable.Where(predicate);

        return queryable;
    }
}