using Kendo.Helpers.Data.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

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
