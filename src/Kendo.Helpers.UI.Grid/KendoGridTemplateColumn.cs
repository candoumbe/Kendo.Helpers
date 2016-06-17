using Newtonsoft.Json.Schema;
using static Newtonsoft.Json.JsonConvert;
using static Newtonsoft.Json.DefaultValueHandling;
using Newtonsoft.Json;

namespace Kendo.Helpers.UI.Grid
{

    [JsonObject]
    public class KendoGridTemplateColumn : KendoGridColumnBase
    {
        
        /// <summary>
        /// Name of the json property where the template of the command is stored
        /// </summary>
        public const string TemplatePropertyName = "template";

        /// <summary>
        /// Name of the json property where the title of the commmand is stored
        /// </summary>
        public const string TitlePropertyName = "title";


        public static JSchema Schema => new JSchema
        {
            Title = "template",
            Type = JSchemaType.Object,

            Properties =
            {
                [TemplatePropertyName] = new JSchema { Type = JSchemaType.String },
                [TitlePropertyName] = new JSchema { Type = JSchemaType.String }
            },
            AllowAdditionalProperties = false

            
        };

        /// <summary>
        /// Gets/Sets the name of the command
        /// </summary>
        [JsonProperty(PropertyName = TitlePropertyName, DefaultValueHandling = IgnoreAndPopulate)]
        public string Title { get; set; }

        /// <summary>
        /// Gets/Sets the text associated to the command
        /// </summary>
        [JsonProperty(PropertyName = TemplatePropertyName, DefaultValueHandling = IgnoreAndPopulate)]
        public string Template { get; set; }

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
