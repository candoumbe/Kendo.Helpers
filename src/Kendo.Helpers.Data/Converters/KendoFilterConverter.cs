using Kendo.Helpers.Tools;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;


namespace Kendo.Helpers.Data.Converters
{
    public class KendoFilterConverter : JsonConverter
    {

        private static IImmutableDictionary<string, KendoFilterOperator> Operators = new Dictionary<string, KendoFilterOperator>
        {
            ["contains"] = KendoFilterOperator.Contains,
            ["endswith"] = KendoFilterOperator.EndsWith,
            ["eq"] = KendoFilterOperator.EqualTo,
            ["gt"] = KendoFilterOperator.GreaterThan,
            ["gte"] = KendoFilterOperator.GreaterThanOrEqual,
            ["isempty"] = KendoFilterOperator.IsEmpty,
            ["isnotempty"] = KendoFilterOperator.IsNotEmpty,
            ["isnotnull"] = KendoFilterOperator.IsNotNull,
            ["isnull"] = KendoFilterOperator.IsNull,
            ["lt"] = KendoFilterOperator.LessThan,
            ["lte"] = KendoFilterOperator.LessThanOrEqualTo,
            ["neq"] = KendoFilterOperator.NotEqualTo,
            ["startswith"] = KendoFilterOperator.StartsWith
        }.ToImmutableDictionary();

        public override bool CanConvert(Type objectType) => objectType == typeof(KendoFilter);


        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            KendoFilter filter = null;

            JToken token = JToken.ReadFrom(reader);
            if (objectType == typeof(KendoFilter))
            {
                if (token.Type == JTokenType.Object)
                {
                    IEnumerable<JProperty> properties = ((JObject)token).Properties();

                    if (properties.Any(prop => prop.Name == KendoFilter.FieldJsonPropertyName)
                         && properties.Any(prop => prop.Name == KendoFilter.OperatorJsonPropertyName))
                    {
                        filter = new KendoFilter()
                        {
                            Field = token[KendoFilter.FieldJsonPropertyName].Value<string>(),
                            Operator = Operators[token[KendoFilter.OperatorJsonPropertyName].Value<string>()],
                            Value = token[KendoFilter.ValueJsonPropertyName]?.Value<string>()
                        };
                    }
                }
            }

            return filter?.As(objectType);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            KendoFilter kf = (KendoFilter)value;

            writer.WriteStartObject();

            writer.WritePropertyName(KendoFilter.FieldJsonPropertyName);
            writer.WriteValue(kf.Field);

            writer.WritePropertyName(KendoFilter.OperatorJsonPropertyName);
            KeyValuePair<string, KendoFilterOperator> kv = Operators.SingleOrDefault(item => item.Value == kf.Operator);
            writer.WriteValue(Equals(default(KeyValuePair<string, KendoFilterOperator>), kv) 
                ? Operators.Single(item => item.Value == KendoFilterOperator.EqualTo).Key
                : kv.Key);


            writer.WritePropertyName(KendoFilter.ValueJsonPropertyName);
            if (kf.Value != null)
            {
                writer.WriteValue(kf.Value);
            }
            else
            {
                writer.WriteNull();
            }

            writer.WriteEnd();
        }
    }
}
