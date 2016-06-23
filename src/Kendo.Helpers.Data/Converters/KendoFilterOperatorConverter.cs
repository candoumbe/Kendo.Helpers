using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

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
            Tuple.Create(KendoFilterOperator.Contains, "contains"),
            Tuple.Create(KendoFilterOperator.EndsWith, "endswith"),
            Tuple.Create(KendoFilterOperator.EqualTo, "eq"),
            Tuple.Create(KendoFilterOperator.GreaterThan, "gt"),
            Tuple.Create(KendoFilterOperator.GreaterThanOrEqual, "gte"),
            Tuple.Create(KendoFilterOperator.IsEmpty, "isempty"),
            Tuple.Create(KendoFilterOperator.IsNotEmpty, "isnotempty"),
            Tuple.Create(KendoFilterOperator.IsNotNull, "isnotnull"),
            Tuple.Create(KendoFilterOperator.IsNull, "isnull"),
            Tuple.Create(KendoFilterOperator.LessThan, "lt"),
            Tuple.Create(KendoFilterOperator.LessThanOrEqualTo, "lte"),
            Tuple.Create(KendoFilterOperator.NotEqualTo, "neq"),
            Tuple.Create(KendoFilterOperator.StartsWith, "startswith")
        };

        public override bool CanRead => true;
        
        /// <inheritdoc />
        public override bool CanWrite => true;

        /// <inheritdoc />
        public override bool CanConvert(Type type) => type == typeof(KendoFilterOperator);

        /// <inheritdoc />
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            KendoFilterOperator kfo = KendoFilterOperator.EqualTo;
            JToken token = JToken.ReadFrom(reader);
            if (token.Type == JTokenType.Property)
            {
                string value = token.Value<string>();
                kfo = ConvertFromStringToEnum(value);
            }

            return kfo;

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

            writer.WritePropertyName(KendoFilter.OperatorJsonPropertyName, true);
            writer.WriteValue($"{ConvertFromEnumToString(@operator)}");          
        }
    }
}
