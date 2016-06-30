using Newtonsoft.Json;
using System.Collections.Generic;

namespace Kendo.Helpers.Data
{
    /// <summary>
    /// An instance of this class represents a kendo datasource request
    /// </summary>
    [JsonObject]
    public class DataSourceRequest
    {
       
        public int PageSize { get; set; }

        public int Page { get; set; }

        
        public IEnumerable<IKendoFilter> Filters { get; set; }
    }
}
