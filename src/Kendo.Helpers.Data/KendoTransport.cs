using System.Runtime.Serialization;
using static Newtonsoft.Json.JsonConvert;
using Kendo.Helpers.Core;

namespace Kendo.Helpers.Data
{
    [DataContract]
    public class KendoTransport : IKendoObject
    {
        [DataMember(Name = "create", EmitDefaultValue = false, Order = 1)]
        public KendoTransportOperation Create { get; set; }

        [DataMember(Name = "read", EmitDefaultValue = false, Order = 2)]
        public KendoTransportOperation Read { get; set; }

        [DataMember(Name = "update", EmitDefaultValue = false, Order = 3)]
        public KendoTransportOperation Update { get; set; }

        [DataMember(Name = "destroy", EmitDefaultValue = false, Order = 4)]
        public KendoTransportOperation Destroy { get; set; }


        public override string ToString() => ToJson();

        public virtual string ToJson() => SerializeObject(this);
    }
}
