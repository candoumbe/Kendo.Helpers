using Newtonsoft.Json;
using Newtonsoft.Json.Schema;
using static Newtonsoft.Json.JsonConvert;

namespace Kendo.Helpers.UI.Grid
{
    [JsonObject]
    public class KendoGridCommandColumn : KendoGridColumnBase
    {
        
        /// <summary>
        /// Name of the json property where the name of the command is stored
        /// </summary>
        public const string NamePropertyName = "name";

        /// <summary>
        /// Name of the json property where the text of the commmand is stored
        /// </summary>
        public const string TextPropertyName = "text";


        public static JSchema Schema => new JSchema
        {
            Title = "command",
            Type = JSchemaType.Object,

            Properties =
            {
                [NamePropertyName] = new JSchema { Type = JSchemaType.String },
                [TextPropertyName] = new JSchema { Type = JSchemaType.String }
            },
            MinimumProperties = 1

            
        };

        /// <summary>
        /// Gets/Sets the name of the command
        /// </summary>
        [JsonProperty(PropertyName = NamePropertyName, DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public string Name { get; set; }

        /// <summary>
        /// Gets/Sets the text associated to the command
        /// </summary>
        [JsonProperty(PropertyName = TextPropertyName, DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public string Text { get; set; }

#if DEBUG
        public override string ToString() => ToJson();
#endif

        public override string ToJson()
#if DEBUG
            => SerializeObject(this, Formatting.Indented);
#else
            => SerializeObject(this);
#endif

    }
}
