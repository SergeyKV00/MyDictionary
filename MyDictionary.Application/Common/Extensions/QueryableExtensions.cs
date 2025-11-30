using System.Linq.Expressions;
using System.Reflection;

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

    public static IQueryable<T> ApplySort<T>(
        this IQueryable<T> queryable,
        string? sortField = "Id",
        string? sortOrder = null)
    {
        if (sortField == null) 
            return queryable;

        var propertyInfo = typeof(T).GetProperty(sortField,
            BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

        if (propertyInfo == null)
            return queryable;

        var parameter = Expression.Parameter(typeof(T));
        var property = Expression.Property(parameter, propertyInfo);
        var lambda = Expression.Lambda(property, parameter);

        string methodName = sortOrder?.ToLower() == "desc"
            ? "OrderByDescending"
            : "OrderBy";

        var result = Expression.Call(
            typeof(Queryable),
            methodName,
            new[] { typeof(T), property.Type },
            queryable.Expression,
            Expression.Quote(lambda));

        return queryable.Provider.CreateQuery<T>(result);
    }
}