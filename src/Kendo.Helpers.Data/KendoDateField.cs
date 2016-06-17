using Kendo.Helpers.Data.Converters;
using Newtonsoft.Json;
namespace Kendo.Helpers.Data
{
    [JsonObject]
    [JsonConverter(typeof(KendoFieldConverter))]
    public class KendoDateField : KendoFieldBase
    {

        public KendoDateField(string name) : base(name, FieldType.Date)
        { }

    }
}
