using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System.Runtime.Serialization;
using static Newtonsoft.Json.JsonConvert;


namespace Kendo.Helpers.Data
{
    [DataContract]
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

        public static JSchema Schema => new JSchema
        {
            Type = JSchemaType.Object,
            Properties =
            {
                [TransportPropertyName] = KendoTransport.Schema,
                [PagePropertyName] = new JSchema { Type = JSchemaType.Number, Minimum = 1 },
                [PageSizePropertyName] = new JSchema { Type = JSchemaType.Number},
                [SchemaPropertyName] = KendoSchema.Schema
            },
            Required = {TransportPropertyName},
            AllowAdditionalProperties = false
        };
       
        /// <summary>
        /// Gets/sets the transport configuration
        /// </summary>
        [DataMember(Name = TransportPropertyName, EmitDefaultValue = false)]
        public KendoTransport Transport { get; set; }

        /// <summary>
        /// Gets/sets the schema configuration
        /// </summary>
        [DataMember(Name = SchemaPropertyName, EmitDefaultValue = false)]
        public KendoSchema DataSchema { get; set; }

        /// <summary>
        /// Gets/sets the "page" configura
        /// </summary>
        [DataMember(Name = PagePropertyName, EmitDefaultValue = false)]
        public int? Page { get; set; }

        /// <summary>
        /// Gets/sets the "pageSize" configuration
        /// </summary>
        [DataMember(Name = PageSizePropertyName, EmitDefaultValue = false)]
        public int? PageSize { get; set; }

        public string ToJson()
        {
            JObject obj = new JObject();


            if (Transport != null)
            {
                obj.Add(TransportPropertyName, JToken.FromObject(Transport));
            }

            if (Page.HasValue)
            {
                obj.Add(PagePropertyName, Page.Value);
            }

            if (PageSize.HasValue)
            {
                obj.Add(PageSizePropertyName, PageSize.Value);
            }

            if (DataSchema != null)
            {
                obj.Add(SchemaPropertyName, JObject.Parse(DataSchema.ToJson()));
            }


            return obj.ToString();
        }

        public override string ToString() => ToJson();
    }
}
