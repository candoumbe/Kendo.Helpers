using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using static Newtonsoft.Json.JsonConvert;


namespace Kendo.Helpers.Grid
{
    [DataContract]
    public class KendoTransport
    {
        [DataMember(Name = "create", EmitDefaultValue = false, Order = 1)]
        public KendoTransportOperation Create { get; set; }

        [DataMember(Name = "read", EmitDefaultValue = false, Order = 2)]
        public KendoTransportOperation Read { get; set; }

        [DataMember(Name = "update", EmitDefaultValue = false, Order = 3)]
        public KendoTransportOperation Update { get; set; }

        [DataMember(Name = "destroy", EmitDefaultValue = false, Order = 4)]
        public KendoTransportOperation Destroy { get; set; }


        
        public override string ToString()
            => SerializeObject(this);
    }
}
