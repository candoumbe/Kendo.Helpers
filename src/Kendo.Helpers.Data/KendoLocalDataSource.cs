using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using static Newtonsoft.Json.JsonConvert;

namespace Kendo.Helpers.Data
{
    [DataContract]
    public class KendoLocalDataSource : KendoLocalDataSource<object>
    {
        
    }

    [DataContract]
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

        [DataMember(Name = PageSizePropertyName, EmitDefaultValue = false)]
        public int? PageSize { get; set; }

        [DataMember(Name = PagePropertyName, EmitDefaultValue = false)]
        public int? Page { get; set; }

        [DataMember(Name = DataPropertyName, EmitDefaultValue = false)]
        public IEnumerable<T> Data { get; set; }


        public string ToJson() => SerializeObject(this);

        public override string ToString() => ToJson();
    }

}
