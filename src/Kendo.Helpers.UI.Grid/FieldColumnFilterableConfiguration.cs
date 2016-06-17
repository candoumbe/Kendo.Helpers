using Kendo.Helpers.Core;
using Newtonsoft.Json.Schema;
using static Newtonsoft.Json.JsonConvert;
using Kendo.Helpers.Data;
using Newtonsoft.Json;

namespace Kendo.Helpers.UI.Grid
{
    [JsonObject]
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

        [JsonProperty(PropertyName = DataSourcePropertyName, DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public IKendoDataSource DataSource { get; set; }

        [JsonProperty(PropertyName = MultiPropertyName, DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public bool? Multi { get; set; }


#if DEBUG
        public override string ToString() => ToJson();
#endif

        public virtual string ToJson()
#if DEBUG
            => SerializeObject(this, Formatting.Indented);
#else
            => SerializeObject(this);
#endif

    }
}
