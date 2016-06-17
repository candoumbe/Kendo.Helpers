using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kendo.Helpers.Data.Converters
{
    public class SchemaTypeConverter : StringEnumConverter
    {
        public SchemaTypeConverter()
        {
            CamelCaseText = true;
        }
    }
}
