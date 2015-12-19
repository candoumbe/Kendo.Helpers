using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Kendo.Helpers.Grid
{
    [DataContract]
    public class KendoTransport
    {
        [DataMember(Name = "create", EmitDefaultValue = false)]
        public KendoTransportOperation Create { get; set; }

        [DataMember(Name = "read", EmitDefaultValue = false)]
        public KendoTransportOperation Read { get; set; }

        [DataMember(Name = "update", EmitDefaultValue = false)]
        public KendoTransportOperation Update { get; set; }

        [DataMember(Name = "destroy", EmitDefaultValue = false)]
        public KendoTransportOperation Destroy { get; set; }
    }
}
