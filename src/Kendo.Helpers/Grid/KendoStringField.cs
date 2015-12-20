using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using static Newtonsoft.Json.JsonConvert;
namespace Kendo.Helpers.Grid
{
    [DataContract]
    public class KendoStringField : KendoFieldBase
    {
        public KendoStringField(string name) : base(name, FieldType.String)
        {
        }

        
        public override string ToString()
            => SerializeObject(this);
    }

}
