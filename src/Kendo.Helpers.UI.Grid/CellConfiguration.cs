using Kendo.Helpers.Core;
using Newtonsoft.Json.Schema;
using System.Runtime.Serialization;
using static Newtonsoft.Json.JsonConvert;

namespace Kendo.Helpers.UI.Grid
{
    [DataContract]
    public class CellConfiguration : IKendoObject
    {
        public const string EnabledPropertyName = "enabled";

        public static JSchema Schema => new JSchema
        {
            Type = JSchemaType.Object,
            MinimumProperties = 1
        };

        [DataMember(Name = EnabledPropertyName, EmitDefaultValue = false)]
        public bool? Enabled { get; set; }

        public string ToJson() => SerializeObject(this);
    }
}
