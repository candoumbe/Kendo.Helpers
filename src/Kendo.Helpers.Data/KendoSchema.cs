using System.Runtime.Serialization;
using System.Text;
using Kendo.Helpers.Core;
using Newtonsoft.Json.Schema;

using static Newtonsoft.Json.JsonConvert;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Kendo.Helpers.Data.Converters;

namespace Kendo.Helpers.Data
{
    /// <summary>
    /// An instance of this class allows to customize the schema configuration of a <see cref="KendoRemoteDataSource"/>
    /// </summary>
    [JsonObject]
    public class KendoSchema : IKendoObject
    {
        /// <summary>
        /// Name of the json property that holds the "data" property name
        /// </summary>
        public const string DataPropertyName = "data";
        /// <summary>
        /// Name of the json property that holds the "total" property
        /// </summary>
        public const string TotalPropertyName = "total";
        /// <summary>
        /// Name of the json property that holds the "type" property
        /// </summary>
        public const string TypePropertyName = "type";
        /// <summary>
        /// Name of the json property that holds the "model" property
        /// </summary>
        public const string ModelPropertyName = "model";


        public static JSchema Schema => new JSchema
        {
            Type = JSchemaType.Object,
            Properties =
            {
                [DataPropertyName] = new JSchema { Type = JSchemaType.String, Default = "json"},
                [TotalPropertyName] = new JSchema { Type = JSchemaType.String, Default = "total"},
                [TypePropertyName] = new JSchema { Type = JSchemaType.String, Default = "json"},
                [ModelPropertyName] = KendoModel.Schema

            }
        };

        /// <summary>
        /// Gets/Sets the data configuration (see http://docs.telerik.com/kendo-ui/api/javascript/data/datasource#configuration-schema.data).
        /// </summary>
        [JsonProperty(PropertyName = DataPropertyName, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Data { get; set; }

        /// <summary>
        /// <para>
        /// Gets/Sets the field of the server which holds the total number of data items in the response.
        /// </para>
        /// </summary>
        [JsonProperty(PropertyName = TotalPropertyName, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Total { get; set; }

        /// <summary>
        /// <para>
        /// Gets/Sets the type of the server response. Can be either "json" or "xml"
        /// </para>
        /// </summary>
        [JsonProperty(PropertyName = TypePropertyName, DefaultValueHandling = DefaultValueHandling.Ignore)]
        [JsonConverter(typeof(SchemaTypeConverter))]
        public SchemaType? Type { get; set; }

        [JsonProperty(PropertyName = ModelPropertyName, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public KendoModel Model { get; set; }

        public string ToJson()
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
