using Kendo.Helpers.Data.Converters;
using Newtonsoft.Json;

namespace Kendo.Helpers.Data
{
    [JsonObject]
    [JsonConverter(typeof(KendoFieldConverter))]
    public class KendoStringField : KendoFieldBase
    {
        public KendoStringField(string name) : base(name, FieldType.String)
        {
        }

    }

}
