
using Kendo.Helpers.Tools;

namespace System.Linq.Expressions
{
    public static class ExpressionExtensions
    {
        public static Expression<Func<T1, T3>> Combine<T1, T2, T3>(this Expression<Func<T1, T2>> first, Expression<Func<T2, T3>> second)
        {
            ParameterExpression pe = Expression.Parameter(typeof(T1), "param");

            Expression newFirst = new ReplaceVisitor(first.Parameters.First(), pe)
                .Visit(first.Body);
            Expression newSecond = new ReplaceVisitor(second.Parameters.First(), newFirst)
                .Visit(second.Body);

            return Expression.Lambda<Func<T1, T3>>(newSecond, pe);

        }
    }
}
