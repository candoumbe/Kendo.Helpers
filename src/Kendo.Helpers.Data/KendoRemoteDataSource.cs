using Newtonsoft.Json.Schema;
using System.Runtime.Serialization;
using static Newtonsoft.Json.JsonConvert;


namespace Kendo.Helpers.Data
{
    [DataContract]
    public class KendoRemoteDataSource : IKendoDataSource
    {
        public const string TransportPropertyName = "transport";
        public const string SchemaPropertyName = "schema";
        public const string PagePropertyName = "page";
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
            Required = {TransportPropertyName}
        };
       

        [DataMember(Name = TransportPropertyName, EmitDefaultValue = false)]
        public KendoTransport Transport { get; set; }

        [DataMember(Name = SchemaPropertyName, EmitDefaultValue = false)]
        public KendoSchema DataSchema { get; set; }
        [DataMember(Name = PagePropertyName, EmitDefaultValue = false)]
        public int? Page { get; set; }

        [DataMember(Name = PageSizePropertyName, EmitDefaultValue = false)]
        public int? PageSize { get; set; }

        public string ToJson() => SerializeObject(this);

        public override string ToString() => ToJson();
    }
}
