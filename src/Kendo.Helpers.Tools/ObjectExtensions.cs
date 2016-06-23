using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace Kendo.Helpers.Tools
{
    public static class ObjectExtensions
    {

        /// <summary>
        /// Performs a "safe cast" of the specified object to the specified type.
        /// </summary>
        /// <typeparam name="TSource">Type of the object to be converted</typeparam>
        /// <typeparam name="TDest">targeted type</typeparam>
        /// <param name="obj">The object to cast</param>
        /// <returns>The "safe cast" result</returns>
        /// <exception cref="ArgumentNullException">if <paramref name="obj"/> is <c>null</c></exception>
        public static TDest As<TSource, TDest>(this TSource obj)
        {
            return (TDest)As(obj, typeof(TDest));
        }


        /// <summary>
        /// Performs a "safe cast" of <paramref name="obj"/> to the type <paramref name="targetType"/>.
        /// </summary>
        /// <typeparam name="TSource">type of the object to cast </param>
        /// <param name="targetType">type to cast </param>
        /// <param name="obj">The object to cast</param>
        /// <returns>The "safe cast" result</returns>
        /// <exception cref="ArgumentNullException">if <paramref name="obj"/> or <paramref name="targetType"/> is <c>null</c></exception>
        public static object As<TSource>(this TSource obj, Type targetType)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            if (targetType == null)
            {
                throw new ArgumentNullException(nameof(targetType));
            }

            object safeCastResult = null;

            Type sourceType = typeof(TSource);

            if (targetType == sourceType || sourceType.GetTypeInfo().IsAssignableFrom(targetType))
            {
                ParameterExpression param = Expression.Parameter(obj.GetType());
                Expression asExpression = Expression.TypeAs(param, targetType);
                LambdaExpression expression = Expression.Lambda(asExpression, param);
                safeCastResult = expression.Compile().DynamicInvoke(obj);

            }

            return safeCastResult;
        }

    }
}
