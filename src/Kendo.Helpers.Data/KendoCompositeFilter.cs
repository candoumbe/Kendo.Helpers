using Newtonsoft.Json.Schema;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using Kendo.Helpers.Data.Converters;
using static Newtonsoft.Json.DefaultValueHandling;
using static Newtonsoft.Json.Required;
using static Newtonsoft.Json.JsonConvert;

namespace Kendo.Helpers.Data
{
    /// <summary>
    /// An instance of this class holds combination of <see cref="IKendoFilter"/>
    /// </summary>
    [JsonObject]
    [JsonConverter(typeof(KendoCompositeFilterConverter))]
    public class KendoCompositeFilter : IKendoFilter
    {
        /// <summary>
        /// Name of the json property that holds filter's filters collection.
        /// </summary>
        public const string FiltersJsonPropertyName = "filters";

        /// <summary>
        /// Name of the json property that holds the composite filter's logic
        /// </summary>
        public const string LogicJsonPropertyName = "logic";

        public static JSchema Schema => new JSchema
        {
            Type = JSchemaType.Object,
            Properties =
            {
                [FiltersJsonPropertyName] = new JSchema { Type = JSchemaType.Array, MinimumItems = 2 },
                [LogicJsonPropertyName] = new JSchema { Type = JSchemaType.String, Default = "and"}
            },
            Required = {FiltersJsonPropertyName}
        };

        /// <summary>
        /// Collections of filters
        /// </summary>
        [JsonProperty(PropertyName = FiltersJsonPropertyName, Required = Always)]
        public IEnumerable<IKendoFilter> Filters { get; set; } = Enumerable.Empty<IKendoFilter>();

        /// <summary>
        /// Operator to apply between <see cref="Filters"/>
        /// </summary>
        [JsonProperty(PropertyName = LogicJsonPropertyName, DefaultValueHandling = IgnoreAndPopulate)]
        [JsonConverter(typeof(CamelCaseEnumTypeConverter))]
        public KendoFilterLogic Logic { get; set; } = KendoFilterLogic.And;

        public virtual string ToJson()
#if DEBUG
        => SerializeObject(this, Formatting.Indented);
#else
            => SerializeObject(this);
#endif

#if DEBUG
        public override string ToString() => ToJson();
#endif



    }
}
