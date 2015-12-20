using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Newtonsoft.Json.JsonConvert;
namespace Kendo.Helpers.Grid
{
    public class KendoDateField : KendoFieldBase
    {
        public KendoDateField(string name) : base(name, FieldType.Date)
        { }

    }
}
