using Newtonsoft.Json.Schema;
using System.Runtime.Serialization;

namespace Kendo.Helpers.Data
{
    [DataContract]
    public class KendoStringField : KendoFieldBase
    {
        public KendoStringField(string name) : base(name, FieldType.String)
        {
        }

    }

}
