using Kendo.Helpers.Tools;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;

namespace Kendo.Helpers.Data.Converters
{
    public class KendoFilterArrayConverter : JsonConverter
    {

        private static IImmutableDictionary<string, KendoFilterLogic> Logics = new Dictionary<string, KendoFilterLogic>
        {
            [KendoFilterLogic.And.ToString().ToLower()] = KendoFilterLogic.And,
            [KendoFilterLogic.Or.ToString().ToLower()] = KendoFilterLogic.Or
        }.ToImmutableDictionary();


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

        public override bool CanConvert(Type objectType) => objectType.GetTypeInfo().IsAssignableFrom(typeof(IEnumerable<IKendoFilter>));


        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            IList<IKendoFilter> filters = null;

            JToken token = JToken.ReadFrom(reader);

            if (token.Type == JTokenType.Array)
            {

                int childrenCount = token.Children().Count();
                int childrenObjectCount = token.Children<JObject>().Count();

                for (int i = 0; i < childrenObjectCount; i++)
                {

                }
               
                
            }

            return filters?.As(objectType);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException($"{nameof(KendoFilterArrayConverter)} is a readonly implementation");
        }
    }
}
