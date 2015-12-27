using System.Runtime.Serialization;
using static Newtonsoft.Json.JsonConvert;

namespace Kendo.Helpers.Data
{
    [DataContract]
    public class KendoTransportOperation : IKendoObject
    {
        [DataMember(Name = "url", EmitDefaultValue = false, IsRequired = true, Order = 1)]
        public string Url { get; set; }

        [DataMember(Name = "type", EmitDefaultValue = false, Order = 2)]
        public string Type { get; set; }

        [DataMember(Name = "data", EmitDefaultValue = false, Order = 3)]
        public object Data { get; set; }

        [DataMember(Name = "cache", EmitDefaultValue = false, Order = 5)]
        public bool? Cache { get; set; }

        [DataMember(Name = "contentType", EmitDefaultValue = false, Order = 6)]
        public string ContentType { get; set; }
        

        public override string ToString()
            => ToJson();

        public virtual string ToJson()
            => SerializeObject(this);
    }
}