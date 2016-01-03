using System.Runtime.Serialization;
using static Newtonsoft.Json.JsonConvert;
using Kendo.Helpers.Core;
using Newtonsoft.Json.Schema;

namespace Kendo.Helpers.Data
{
    [DataContract]
    public class KendoTransport : IKendoObject
    {

        public const string CreatePropertyName = "create";
        public const string ReadPropertyName = "read";
        public const string UpdatePropertyName = "update";
        public const string DeletePropertyName = "destroy";

        public static JSchema Schema => new JSchema
        {
            Title = "transport",
            Type = JSchemaType.Object,
            Properties =
            {
                [CreatePropertyName] = KendoTransportOperation.Schema,
                [ReadPropertyName] = KendoTransportOperation.Schema,
                [UpdatePropertyName] = KendoTransportOperation.Schema,
                [DeletePropertyName] = KendoTransportOperation.Schema,
            },
            MinimumProperties = 1
        };



        [DataMember(Name = CreatePropertyName, EmitDefaultValue = false)]
        public KendoTransportOperation Create { get; set; }

        [DataMember(Name = ReadPropertyName, EmitDefaultValue = false)]
        public KendoTransportOperation Read { get; set; }

        [DataMember(Name = UpdatePropertyName, EmitDefaultValue = false)]
        public KendoTransportOperation Update { get; set; }

        [DataMember(Name = DeletePropertyName, EmitDefaultValue = false)]
        public KendoTransportOperation Destroy { get; set; }


        public override string ToString() => ToJson();

        public virtual string ToJson() => SerializeObject(this);
    }
}
