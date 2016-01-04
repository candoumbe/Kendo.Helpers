using Kendo.Helpers.Core;
using Newtonsoft.Json.Schema;
using System.Runtime.Serialization;
using static Newtonsoft.Json.JsonConvert;
using Kendo.Helpers.Data;

namespace Kendo.Helpers.UI.Grid
{
    [DataContract]
    public class FieldColumnFilterableConfiguration : IKendoObject
    {

        public const string MultiPropertyName = "multi";
        public const string DataSourcePropertyName = "dataSource";
        

        public static JSchema Schema => new JSchema
        {
            Type = JSchemaType.Object,
            AllowAdditionalProperties = false,
            MinimumProperties = 1,
            Properties  =
            {
                [MultiPropertyName] = new JSchema { Type = JSchemaType.Boolean, Default = false },
                [DataSourcePropertyName] = new JSchema { Type = JSchemaType.Object }

            }
        };

        [DataMember(Name = DataSourcePropertyName, EmitDefaultValue = false)]
        public IKendoDataSource DataSource { get; set; }

        [DataMember(Name = MultiPropertyName, EmitDefaultValue = false)]
        public bool? Multi { get; set; }


        public string ToJson() => SerializeObject(this);
    }
}
