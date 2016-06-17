using Kendo.Helpers.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kendo.Helpers.Data.Converters
{
    /// <summary>
    /// <see cref="JsonConverter"/> that support converting from/to <see cref="KendoFilter"/>
    /// </summary>
    public class KendoFilterOperatorConverter : JsonConverter
    {
        /// <summary>
        /// Defines matches between <see cref="KendoFilterOperator"/> and strings
        /// </summary>
        private static IEnumerable<Tuple<KendoFilterOperator, string>> AllowedOperators => new []
        {
            new Tuple<KendoFilterOperator, string> (KendoFilterOperator.Contains, "contains"),
            new Tuple<KendoFilterOperator, string> (KendoFilterOperator.EndsWith, "endswith"),
            new Tuple<KendoFilterOperator, string> (KendoFilterOperator.EqualTo, "eq"),
            new Tuple<KendoFilterOperator, string> (KendoFilterOperator.GreaterThan, "gt"),
            new Tuple<KendoFilterOperator, string> (KendoFilterOperator.GreaterThanOrEqual, "gte"),
            new Tuple<KendoFilterOperator, string> (KendoFilterOperator.IsEmpty, "isempty"),
            new Tuple<KendoFilterOperator, string> (KendoFilterOperator.IsNotEmpty, "isnotempty"),
            new Tuple<KendoFilterOperator, string> (KendoFilterOperator.IsNotNull, "isnotnull"),
            new Tuple<KendoFilterOperator, string> (KendoFilterOperator.IsNull, "isnull"),
            new Tuple<KendoFilterOperator, string> (KendoFilterOperator.LessThan, "lt"),
            new Tuple<KendoFilterOperator, string> (KendoFilterOperator.LessThanOrEqualTo, "lte"),
            new Tuple<KendoFilterOperator, string> (KendoFilterOperator.NotEqualTo, "neq"),
            new Tuple<KendoFilterOperator, string> (KendoFilterOperator.StartsWith, "startswith")
        };


        


        public override bool CanRead => true;
        
        /// <inheritdoc />
        public override bool CanWrite => true;

        /// <inheritdoc />
        public override bool CanConvert(Type type) => type == typeof(KendoFilterOperator);

        /// <inheritdoc />
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            string value = reader.Value as string;

            return ConvertFromStringToEnum(value);

        }

        /// <summary>
        /// Function that can convert a string to its <see cref="KendoFilterOperator"/>
        /// </summary>
        private static Func<string, KendoFilterOperator> ConvertFromStringToEnum => stringValue =>
        {
            Tuple<KendoFilterOperator, string> tuple = AllowedOperators.SingleOrDefault(item => item.Item2 == stringValue);
            return tuple?.Item1 ?? KendoFilterOperator.EqualTo;
        };

        /// <summary>
        /// Function that can convert a <see cref="KendoFilterOperator"/> to its string representation
        /// </summary>
        private static Func<KendoFilterOperator, string> ConvertFromEnumToString => enumValue =>
        {
            Tuple<KendoFilterOperator, string> tuple = AllowedOperators.SingleOrDefault(item => item.Item1 == enumValue);
            return tuple?.Item2 ?? "eq";
        };

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            KendoFilterOperator @operator = (KendoFilterOperator) value;

            //writer.WritePropertyName(KendoFilter.OperatorJsonPropertyName, true);
            writer.WriteValue($"{ConvertFromEnumToString(@operator)}");          
        }
    }
}
