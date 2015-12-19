using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kendo.Helpers.Grid
{

    public class KendoLocalDataSource<T> : KendoDataSource
        where T : IEnumerable<T>
    { }

}
