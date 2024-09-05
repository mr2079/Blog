using System.Linq.Expressions;

namespace BuildingBlocks.Extensions;

public static class ExpressionExtensions
{
    public static Expression<Func<T, bool>> And<T>(
        this Expression<Func<T, bool>>? left,
        Expression<Func<T, bool>> right)
    {
        if (left == null) return right;

        var visitor = new ParameterUpdateVisitor(right.Parameters.First(), left.Parameters.First());

        right = (Expression<Func<T, bool>>)visitor.Visit(right);

        var binaryExpression = Expression.And(left.Body, right.Body);

        return Expression.Lambda<Func<T, bool>>(binaryExpression, right.Parameters);
    }

    public static Expression<Func<T, bool>> Or<T>(
        this Expression<Func<T, bool>>? left,
        Expression<Func<T, bool>> right)
    {
        if (left == null) return right;

        var visitor = new ParameterUpdateVisitor(right.Parameters.First(), left.Parameters.First());

        right = (Expression<Func<T, bool>>)visitor.Visit(right);

        var binaryExpression = Expression.Or(left.Body, right.Body);

        return Expression.Lambda<Func<T, bool>>(binaryExpression, right.Parameters);
    }
}

internal class ParameterUpdateVisitor(
    ParameterExpression oldParameter,
    ParameterExpression newParameter) : ExpressionVisitor
{
    protected override Expression VisitParameter(ParameterExpression node)
    {
        return ReferenceEquals(node, oldParameter) ? newParameter : base.VisitParameter(node);
    }
}