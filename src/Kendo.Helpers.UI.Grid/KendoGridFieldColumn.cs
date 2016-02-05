using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization;
using static Newtonsoft.Json.JsonConvert;

namespace Kendo.Helpers.UI.Grid
{
    [DataContract]
    public class KendoGridFieldColumn : KendoGridFieldColumnBase
    {}
    
    public class KendoGridFieldColumn<TElement, TProperty> : KendoGridFieldColumnBase
    {
        public KendoGridFieldColumn(Expression<Func<TElement, TProperty>> propertySelector)
        {
            if (propertySelector == null)
            {
                throw new ArgumentNullException(nameof(propertySelector));
            }

            MemberInfo mi = (propertySelector.Body as MemberExpression)?.Member;
            if (mi == null)
            {
                throw new ArgumentOutOfRangeException(nameof(propertySelector), $"{nameof(propertySelector)} must be a member expression");
            }
            Field = mi.Name;
        }
    }
}