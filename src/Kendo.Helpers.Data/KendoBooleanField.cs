using Kendo.Helpers.Data.Converters;
using Newtonsoft.Json;
namespace Kendo.Helpers.Data
{
    [JsonObject]
    [JsonConverter(typeof(KendoFieldConverter))]
    public class KendoBooleanField : KendoFieldBase
    {

        public KendoBooleanField(string fieldName) : base(fieldName, FieldType.Boolean)
        { }

    }
}
