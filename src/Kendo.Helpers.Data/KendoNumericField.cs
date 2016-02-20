using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Kendo.Helpers.Data
{
    [DataContract]
    public class KendoNumericField : KendoFieldBase
    {

        public KendoNumericField(string fieldName) : base(fieldName, FieldType.Number)
        { 
        }
    }
}
