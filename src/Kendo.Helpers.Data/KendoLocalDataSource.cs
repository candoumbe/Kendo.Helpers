using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using static Newtonsoft.Json.JsonConvert;

namespace Kendo.Helpers.Data
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

        public override string ToJson() => ToString();
    }

}
