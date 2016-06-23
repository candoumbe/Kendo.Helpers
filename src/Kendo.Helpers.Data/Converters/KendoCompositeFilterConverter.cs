using Kendo.Helpers.Tools;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;


namespace Kendo.Helpers.Data.Converters
{
    /// <summary>
    /// <see cref="JsonConverter"/> implementation that allow to convert json string from/to <see cref="KendoCompositeFilter"/>
    /// </summary>
    public class KendoCompositeFilterConverter : JsonConverter
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
            ["startswith"] = KendoFilterOperator.StartsWith,

        }.ToImmutableDictionary();

        public override bool CanConvert(Type objectType) => objectType == typeof(KendoCompositeFilter);


        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            KendoCompositeFilter kcf = null;

            JToken token = JToken.ReadFrom(reader);
            if (objectType == typeof(KendoCompositeFilter))
            {
                if (token.Type == JTokenType.Object)
                {
                    IEnumerable<JProperty> properties = ((JObject)token).Properties();

                    JProperty logicProperty = properties
                        .SingleOrDefault(prop => prop.Name == KendoCompositeFilter.LogicJsonPropertyName);

                    if (logicProperty != null)
                    {
                        JProperty filtersProperty = properties.SingleOrDefault(prop => prop.Name == KendoCompositeFilter.FiltersJsonPropertyName);
                        if (filtersProperty != null
                            && filtersProperty.Type == JTokenType.Array)
                        {
                            JArray filtersArray = (JArray)token[KendoCompositeFilter.FiltersJsonPropertyName];
                            int nbFilters = filtersArray.Count();
                            if (nbFilters > 2)
                            {
                                IList<IKendoFilter> filters = new List<IKendoFilter>(nbFilters);
                                foreach (var item in filtersArray)
                                {
                                    IKendoFilter kf = (IKendoFilter)item.ToObject<KendoFilter>() ?? item.ToObject<KendoCompositeFilter>();

                                    if (kf != null)
                                    {
                                        filters.Add(kf);
                                    }
                                }


                                if (filters.Count() >= 2)
                                {
                                    kcf = new KendoCompositeFilter
                                    {
                                        Logic = Logics[token[KendoCompositeFilter.LogicJsonPropertyName].Value<string>()],
                                        Filters = filters
                                    };
                                }
                            }
                            
                        }
                    }
                }
            }



            return kcf?.As(objectType);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            KendoCompositeFilter kcf = (KendoCompositeFilter)value;

            writer.WriteStartObject();

            // TODO Maybe can we rely on the serializer to handle the logic serialization ?
            writer.WritePropertyName(KendoCompositeFilter.LogicJsonPropertyName);
            writer.WriteValue(kcf.Logic.ToString().ToLower());

            writer.WritePropertyName(KendoCompositeFilter.FiltersJsonPropertyName);
            writer.WriteStartArray();
            foreach (IKendoFilter filter in kcf.Filters)
            {
                serializer.Serialize(writer, filter);
            }
            writer.WriteEndArray();

            writer.WriteEndObject();
        }
    }
}
