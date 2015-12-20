using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using static Newtonsoft.Json.JsonConvert;


namespace Kendo.Helpers.Grid
{
    [DataContract]
    public class KendoRemoteDataSource : KendoDataSource
    {
        [DataMember(Name = "transport", EmitDefaultValue = false, Order = 1)]
        public KendoTransport Transport { get; set; }

        [DataMember(Name = "schema", EmitDefaultValue = false, Order = 2)]
        public KendoSchema Schema { get; set; }
                
        public override string ToString() => SerializeObject(this);
    }
}
