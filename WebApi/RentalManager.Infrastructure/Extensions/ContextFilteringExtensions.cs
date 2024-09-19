using System.ComponentModel;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;

namespace RentalManager.Infrastructure.Extensions;

public static class ContextFilteringExtensions
{
    public static IQueryable<TEntity> Filter<TEntity, TResult>(this IQueryable<TEntity> entities,
        Expression<Func<TEntity, TResult>> propertyExpression,
        TResult value,
        FilterOperand filterOperand)
    {
        if (value is null)
        {
            return entities;
        }

        var memberExpression = GetMemberExpression(propertyExpression.Body);

        var propertyInfo = memberExpression.Member as PropertyInfo;
        if (propertyInfo == null)
        {
            throw new InvalidOperationException("The specified member is not a writable property.");
        }

        var propertyName = propertyInfo.Name;

        if (propertyInfo.DeclaringType != typeof(TEntity))
        {
            propertyName = GetExpressionString(memberExpression);
        }

        var commandString = filterOperand switch
        {
            FilterOperand.Contains => $"{propertyName}.Contains(@0)",
            FilterOperand.Equals => $"{propertyName} == @0",
            FilterOperand.GreaterThanOrEqualTo => $"{propertyName} >= @0",
            FilterOperand.LessThanOrEqualTo => $"{propertyName} <= @0",
            _ => throw new InvalidEnumArgumentException($"Enum {filterOperand.ToString()} is not mapped to filter action")
        };

        return entities.Where($"{commandString}", value);
    }

    private static MemberExpression GetMemberExpression(Expression expression)
    {
        var result = expression switch
        {
            UnaryExpression unaryExpression => unaryExpression.Operand as MemberExpression,
            _ => expression as MemberExpression
        };

        if (result is null)
        {
            throw new InvalidOperationException("The expression is not a member expression.");
        }

        return result;
    }

    private static string GetExpressionString(Expression propertyExpression)
    {
        var expressionString = propertyExpression.ToString();

        const string pat = @"(\w+\.){1}(.+)";

        var regex = new Regex(pat, RegexOptions.IgnoreCase);

        var match = regex.Match(expressionString);

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