using System.Linq.Expressions;
using System.Reflection;

public static class IQueryableExtensions
{
    public static IQueryable<T> OrderByProperty<T>(this IQueryable<T> source, Expression<Func<T, object>> propertyExpression, bool ascending)
    {
        //var propertyName = ExpressionHelper.GetPropertyName(propertyExpression);
        var propertyName = string.Empty;
        var properties = propertyExpression.ToString().Split('.');
        if (properties.Length <= 1)
        {
            throw new ArgumentException($"Property '{propertyExpression.ToString()}' not found on type '{typeof(T).Name}'");
        }
        //else if (propArray.Length == 2)
        //{
        //    propertyName = propArray[1];
        //}
        //else if (propArray.Length == 3)
        //{
        //    propertyName = propArray[2];
        //}
        //else if (propArray.Length > 3)
        //{
        //    throw new ArgumentException($"Property '{propertyExpression.ToString()}' not support 3 levels, ajust the method IQueryableExtensions->OrderByProperty");
        //}
        var parameter = Expression.Parameter(typeof(T), "p");
        Expression propertyAccess = parameter;

        for (var i = 1; i < properties.Length; i++)
        {
            var prop = properties[i].Replace(", Object)", "");
            if (propertyAccess.Type == null)
                throw new ArgumentException($"Cannot access property '{prop}' on null type");

            var property = propertyAccess.Type.GetProperty(prop,
            BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

            if (property == null)
                throw new ArgumentException(
                    $"Property '{prop}' not found on type '{propertyAccess.Type.Name}'");

            propertyAccess = Expression.MakeMemberAccess(propertyAccess, property);
        }

        //var property = typeof(T).GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
        //if (property == null)
        //{
        //    throw new ArgumentException($"Property '{propertyName}' not found on type '{typeof(T).Name}'");
        //}

        //var propertyAccess = Expression.MakeMemberAccess(parameter, property);
        var orderByExpression = Expression.Lambda(propertyAccess, parameter);

        var methodName = ascending ? "OrderBy" : "OrderByDescending";
        var method = typeof(Queryable).GetMethods()
            .Where(m => m.Name == methodName && m.GetParameters().Length == 2)
            .Single()
            .MakeGenericMethod(typeof(T), propertyAccess.Type);
            //.MakeGenericMethod(typeof(T), property.PropertyType);

        return (IQueryable<T>)method.Invoke(null, new object[] { source, orderByExpression });
    }
}