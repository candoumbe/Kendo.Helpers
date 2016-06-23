﻿using Kendo.Helpers.Data.Converters;
using Newtonsoft.Json;

namespace Kendo.Helpers.Data
{
    /// <summary>
    /// Operator that can be applied in <see cref="KendoFilter"/>
    /// </summary>
    public enum KendoFilterOperator
    {
        EqualTo,
        NotEqualTo,
        IsNull,
        IsNotNull,
        LessThan,
        GreaterThan,
        GreaterThanOrEqual,
        /// <summary>
        /// Applies only to string
        /// </summary>
        StartsWith,
        /// <summary>
        /// 
        /// <remarks>Applies only to string</remarks>
        /// </summary>
        EndsWith,
        Contains,
        IsEmpty,
        IsNotEmpty,
        LessThanOrEqualTo
    }
}
