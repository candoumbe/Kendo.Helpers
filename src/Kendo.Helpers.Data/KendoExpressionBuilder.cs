﻿using System;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.CodeAnalysis.CSharp;
using static System.Linq.Expressions.Expression;

namespace Kendo.Helpers.Data
{
    /// <summary>
    /// Utility class which allow to build expression out of a <see cref="IKendo"/>
    /// </summary>
    public static class KendoExpressionBuilder
    {
        /// <summary>
        /// Builds an <see cref="Expression{Func{T}}"/> tree from a <see cref="IKendoFilter"/> instance
        /// </summary>
        /// <typeparam name="T">Type of the </typeparam>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> ToExpression<T>(this IKendoFilter filter)
        {
            if (filter == null)
            {
                throw new ArgumentException(nameof(filter), $"{nameof(filter)} cannot be null");
            }
            Expression<Func<T, bool>> filterExpression = null;

            if (filter is KendoFilter)
            {
                KendoFilter kf = filter as KendoFilter;
                Type type = typeof(T);
                ParameterExpression pe = Parameter(type, "param");
                ConstantExpression constantExpression = Constant(kf.Value);
                
                string[] fields = kf.Field.Split(new []{'.'});
                MemberExpression property = null;
                foreach (string field in fields)
                {
                    property = property == null
                        ? Property(pe, field)
                        : property = Property(property, field);
                }

                
                Expression body;

                switch (kf.Operator)
                {
                    case KendoFilterOperator.NotEqualTo:
                        body = NotEqual(property, constantExpression);
                        break;
                    case KendoFilterOperator.IsNull:
                        body = Equal(property, Constant(null));
                        break;
                    case KendoFilterOperator.IsNotNull:
                        body = NotEqual(property, Constant(null));
                        break;
                    case KendoFilterOperator.LessThan:
                        body = LessThan(property, constantExpression);
                        break;
                    case KendoFilterOperator.GreaterThan:
                        body = LessThan(property, constantExpression);
                        break;
                    case KendoFilterOperator.GreaterThanOrEqual:
                        body = GreaterThanOrEqual(property, constantExpression);
                        break;
                    case KendoFilterOperator.StartsWith:
                        body = Call(property, typeof(string).GetMethod(nameof(string.StartsWith)), constantExpression);
                        break;
                    case KendoFilterOperator.EndsWith:
                        body = Call(property, typeof(string).GetMethod(nameof(string.EndsWith)), constantExpression);
                        break;
                    case KendoFilterOperator.Contains:
                        body = Call(property, typeof(string).GetMethod(nameof(string.Contains)), constantExpression);
                        break;
                    case KendoFilterOperator.IsEmpty:
                        body = Equal(property, Constant(string.Empty));
                        break;
                    case KendoFilterOperator.IsNotEmpty:
                        body = NotEqual(property, Constant(string.Empty));
                        break;
                    default:
                        body = Equal(property, constantExpression);
                        break;
                }

                filterExpression = Lambda<Func<T, bool>>(body, pe);
            }
            else if(filter is KendoCompositeFilter)
            {
                KendoCompositeFilter kcf = filter as KendoCompositeFilter;

                Expression<Func<T, bool>> expression = null;
                Func<Expression<Func<T, bool>>, Expression<Func<T, bool>>, Expression<Func<T, bool>>> expressionMerger;
                if (kcf.Logic == KendoFilterLogic.And)
                {
                    expressionMerger = (first, second) => first.AndAlso(second);
                }
                else
                {
                    expressionMerger = (first, second) => first.OrElse(second);
                }

                foreach (IKendoFilter item in kcf.Filters)
                {
                    if (expression == null)
                    {
                        expression = item.ToExpression<T>();
                    }
                    else
                    {
                        expression = expressionMerger(expression, item.ToExpression<T>());
                    }   
                }

                filterExpression = expression;

            }

            return filterExpression;
        }
    }
}