using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Newtonsoft.Json.JsonConvert;

namespace Kendo.Helpers.Grid
{

    public class KendoLocalDataSource : KendoLocalDataSource<object>
    {
        public KendoLocalDataSource(IEnumerable<object> objects) : base(objects)
        {

        }
    }

    public class KendoLocalDataSource<T> : KendoDataSource 
    {
        public IEnumerable<T> Data { get; set; } = Enumerable.Empty<T>();

        public KendoLocalDataSource(IEnumerable<T> objects)
        {
            Data = objects ?? Enumerable.Empty<T>();
        }

        public override string ToString() => SerializeObject(new JObject(Data));

        
    }

}
