using Newtonsoft.Json.Converters;

namespace Kendo.Helpers.Data.Converters
{
    public class CamelCaseEnumTypeConverter : StringEnumConverter
    {
        public CamelCaseEnumTypeConverter()
        {
            CamelCaseText = true;
            
        }
    }
}
