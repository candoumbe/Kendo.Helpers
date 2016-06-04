using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kendo.Helpers.Data
{
    public class DataSourceRequest
    {
        public int Limit { get; set; }

        public int Offset { get; set; }

        public IKendoFilter Filter { get; set; }
    }
}
