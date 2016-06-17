using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using static Newtonsoft.Json.JsonConvert;

namespace Kendo.Helpers.Data
{
    [JsonObject]
    public class KendoLocalDataSource : KendoLocalDataSource<object>
    {
        
    }

    [JsonObject]
    public class KendoLocalDataSource<T> : IKendoDataSource 
    {
        /// <summary>
        /// Name of the property that holds the "page" configuration
        /// </summary>
        public const string PagePropertyName = "page";
        /// <summary>
        /// Name of the property that holds the "pageSize" configuration
        /// </summary>
        public const string PageSizePropertyName = "pageSize";

        /// <summary>
        /// Name of the property that holds the "data" property
        /// </summary>
        public const string DataPropertyName = "data";

        public static JSchema Schema => new JSchema
        {
            Type = JSchemaType.Object,
            Properties =
            {
                [PagePropertyName] = new JSchema { Type = JSchemaType.Number, Minimum = 1 },
                [PageSizePropertyName] = new JSchema { Type = JSchemaType.Number},
                [DataPropertyName] = new JSchema { Type = JSchemaType.Array }
            },
            Required = {DataPropertyName}
        };

        [JsonProperty(PropertyName = PageSizePropertyName, DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public int? PageSize { get; set; }

        [JsonProperty(PropertyName = PagePropertyName, DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public int? Page { get; set; }

        [JsonProperty(PropertyName = DataPropertyName, DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public IEnumerable<T> Data { get; set; }


        public string ToJson()
#if DEBUG
            => SerializeObject(this, Formatting.Indented);
#else
            => SerializeObject(this);
#endif

        public override string ToString() => ToJson();
    }

}
