using Newtonsoft.Json.Schema;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Kendo.Helpers.Data
{
    /// <summary>
    /// An instance of this class holds combination of <see cref="IKendoFilter"/>
    /// </summary>
    [DataContract]
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
            Required = {FiltersJsonPropertyName, LogicJsonPropertyName}
        };

        /// <summary>
        /// Collections of filters
        /// </summary>
        [DataMember(Name = FiltersJsonPropertyName, IsRequired = true)]
        public IEnumerable<IKendoFilter> Filters { get; set; } = Enumerable.Empty<IKendoFilter>();

        /// <summary>
        /// Operator to apply between <see cref="Filters"/>
        /// </summary>
        [DataMember(Name = LogicJsonPropertyName, IsRequired = true)]
        public KendoFilterLogic Logic { get; set; } = KendoFilterLogic.And;

        public override string ToString() => ToJson();

        public string ToJson()
        {
            JObject jObj = new JObject();

            jObj.Add(LogicJsonPropertyName, Logic == KendoFilterLogic.And ? "and" : "or");
            JArray jFilters = new JArray();
            foreach (IKendoFilter item in Filters)
            {
                jFilters.Add(JObject.Parse(item.ToJson()));
            }
            jObj.Add(FiltersJsonPropertyName, jFilters);


            return jObj.ToString();
        }


    }
}
