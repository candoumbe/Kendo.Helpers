using Newtonsoft.Json;
using Newtonsoft.Json.Schema;
using static Newtonsoft.Json.JsonConvert;

namespace Kendo.Helpers.Data
{
    public class KendoRemoteDataSource : IKendoDataSource
    {
        /// <summary>
        /// Name of the json property that holds "transport" configuration
        /// </summary>
        public const string TransportPropertyName = "transport";
        /// <summary>
        /// Name of the json property that holds "schema" configuration
        /// </summary>
        public const string SchemaPropertyName = "schema";
        /// <summary>
        /// Name of the json property that holds "page" configuration
        /// </summary>
        public const string PagePropertyName = "page";
        /// <summary>
        /// Name of the json property that holds "pageSize" configuration
        /// </summary>
        public const string PageSizePropertyName = "pageSize";

        /// <summary>
        /// Name of the json property that holds
        /// </summary>
        public const string ServerFilteringPropertyName = "serverFiltering";

        public static JSchema Schema => new JSchema
        {
            Type = JSchemaType.Object,
            Properties =
            {
                [TransportPropertyName] = KendoTransport.Schema,
                [PagePropertyName] = new JSchema { Type = JSchemaType.Number, Minimum = 1 },
                [PageSizePropertyName] = new JSchema { Type = JSchemaType.Number},
                [SchemaPropertyName] = KendoSchema.Schema,
                [ServerFilteringPropertyName] = new JSchema { Type = JSchemaType.Boolean, Default = false }
            },
            Required = {TransportPropertyName},
            AllowAdditionalProperties = false
        };
       
        /// <summary>
        /// Gets/sets the transport configuration
        /// </summary>
        [JsonProperty(TransportPropertyName, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public KendoTransport Transport { get; set; }

        /// <summary>
        /// Gets/sets the schema configuration
        /// </summary>
        [JsonProperty(SchemaPropertyName, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public KendoSchema DataSchema { get; set; }

        /// <summary>
        /// Gets/sets the "page" configura
        /// </summary>
        [JsonProperty(PagePropertyName, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int? Page { get; set; }

        [JsonProperty(ServerFilteringPropertyName, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool? ServerFiltering { get; set; }


        /// <summary>
        /// Gets/sets the "pageSize" configuration
        /// </summary>
        [JsonProperty(PageSizePropertyName, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int? PageSize { get; set; }

        public string ToJson() => SerializeObject(this);

        public override string ToString() => ToJson();
    }
}
