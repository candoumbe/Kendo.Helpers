using System.Runtime.Serialization;

namespace Kendo.Helpers.Grid
{
    [DataContract]
    public class KendoTransportOperation
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

        [DataMember(Name = "method", EmitDefaultValue = false, Order = 7)]
        public HttpVerb Method { get; set; }
    }
}