using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using static Newtonsoft.Json.JsonConvert;

namespace Kendo.Helpers.Data
{
    [DataContract]
    public class KendoLocalDataSource : KendoLocalDataSource<object>
    {
        public KendoLocalDataSource(IEnumerable<object> objects) : base(objects)
        {

        }
    }

    [DataContract]
    public class KendoLocalDataSource<T> : KendoDataSource 
    {
        [DataMember]
        public IEnumerable<T> Data { get; set; } = Enumerable.Empty<T>();

        public KendoLocalDataSource(IEnumerable<T> objects)
        {
            Data = objects ?? Enumerable.Empty<T>();
        }

        public override string ToJson() => SerializeObject(Data);

        public override string ToString() => ToJson();
    }

}
