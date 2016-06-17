using Kendo.Helpers.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema;
using static Newtonsoft.Json.JsonConvert;

namespace Kendo.Helpers.UI.Grid
{
    [JsonObject]
    public class CellConfiguration : IKendoObject
    {
        public const string EnabledPropertyName = "enabled";

        public static JSchema Schema => new JSchema
        {
            Type = JSchemaType.Object,
            Properties =
            {
                [EnabledPropertyName] = new JSchema { Type = JSchemaType.Boolean }
            },
            MinimumProperties = 1
        };

        [JsonProperty(PropertyName = EnabledPropertyName, DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public bool? Enabled { get; set; }

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
