using System.ComponentModel;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;

namespace RentalManager.Infrastructure.Extensions;

public static partial class ContextFilteringExtensions
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
            propertyName = GetExpressionString(propertyExpression);
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
        
        var regex = MyRegex();
        
        var match = regex.Match(expressionString);
        
        return match.Groups[2].Value;
    }

    [GeneratedRegex(@"(\w+\.){1}(.+)", RegexOptions.IgnoreCase, "en-GB")]
    private static partial Regex MyRegex();
}