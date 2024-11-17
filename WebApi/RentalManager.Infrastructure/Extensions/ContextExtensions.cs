using System.ComponentModel;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;

namespace RentalManager.Infrastructure.Extensions;

public static class ContextExtensions
{
    public static IQueryable<TEntity> Filter<TEntity, TResult>(
        this IQueryable<TEntity> entities,
        Expression<Func<TEntity, TResult>> propertyExpression,
        TResult value,
        FilterOperand filterOperand)
    {
        if (value is null)
        {
            return entities;
        }

        var memberExpression = GetMemberExpression(propertyExpression.Body);
        if (memberExpression.Member is not PropertyInfo propertyInfo)
        {
            throw new InvalidOperationException("The specified member is not a writable property.");
        }

        var propertyName = propertyInfo.DeclaringType == typeof(TEntity)
            ? propertyInfo.Name
            : GetExpressionString(memberExpression);

        var commandString = filterOperand switch
        {
            FilterOperand.Contains => $"{propertyName}.Contains(@0)",
            FilterOperand.Equals => $"{propertyName} == @0",
            FilterOperand.GreaterThanOrEqualTo => $"{propertyName} >= @0",
            FilterOperand.LessThanOrEqualTo => $"{propertyName} <= @0",
            _ => throw new InvalidEnumArgumentException($"Enum {filterOperand} is not mapped to filter action")
        };

        return entities.Where(commandString, value);
    }

    public static IQueryable<TEntity> Sort<TEntity, TResult>(
        this IQueryable<TEntity> entities,
        Expression<Func<TEntity, TResult>> propertyExpression,
        SortOrder sortOrder)
    {
        var memberExpression = GetMemberExpression(propertyExpression.Body);
        if (memberExpression.Member is not PropertyInfo propertyInfo)
        {
            throw new InvalidOperationException("The specified member is not a writable property.");
        }

        var propertyName = propertyInfo.DeclaringType == typeof(TEntity)
            ? propertyInfo.Name
            : GetExpressionString(memberExpression);

        return entities.OrderBy($"{propertyName} {sortOrder}");
    }

    private static MemberExpression GetMemberExpression(Expression expression)
    {
        return expression switch
        {
            UnaryExpression unaryExpression => unaryExpression.Operand as MemberExpression
                                               ?? throw new InvalidOperationException("The expression is not a member expression."),
            MemberExpression memberExpression => memberExpression,
            _ => throw new InvalidOperationException("The expression is not a member expression.")
        };
    }

    private static string GetExpressionString(Expression propertyExpression)
    {
        var expressionString = propertyExpression.ToString();
        const string pattern = @"(\w+\.){1}(.+)";
        var match = Regex.Match(expressionString, pattern, RegexOptions.IgnoreCase | RegexOptions.NonBacktracking);

        return match.Groups[2].Value;
    }
}

public enum FilterOperand
{
    Equals,
    Contains,
    GreaterThanOrEqualTo,
    LessThanOrEqualTo
}

public enum SortOrder
{
    Asc,
    Desc
}