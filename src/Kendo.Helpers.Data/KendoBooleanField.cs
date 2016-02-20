using Newtonsoft.Json.Schema;
using System.Runtime.Serialization;
namespace Kendo.Helpers.Data
{
    [DataContract]
    public class KendoBooleanField : KendoFieldBase
    {

        public KendoBooleanField(string fieldName) : base(fieldName, FieldType.Boolean)
        { }

    }
}
