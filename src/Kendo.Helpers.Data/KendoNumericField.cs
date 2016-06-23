using Kendo.Helpers.Data.Converters;
using Newtonsoft.Json;

namespace Kendo.Helpers.Data
{
    [JsonObject]
    [JsonConverter(typeof(KendoFieldConverter))]
    public class KendoNumericField : KendoFieldBase
    {

        public KendoNumericField(string fieldName) : base(fieldName, FieldType.Number)
        { 
        }
    }
}
