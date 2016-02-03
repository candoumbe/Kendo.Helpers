using Newtonsoft.Json.Schema;
using System.Collections.Generic;
using System.Runtime.Serialization;
using static Newtonsoft.Json.JsonConvert;

namespace Kendo.Helpers.UI.Grid
{

    [DataContract]
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
        [DataMember(Name = TitlePropertyName, EmitDefaultValue = false)]
        public string Title { get; set; }

        /// <summary>
        /// Gets/Sets the text associated to the command
        /// </summary>
        [DataMember(Name = TemplatePropertyName, EmitDefaultValue = false)]
        public string Template { get; set; }

        public override string ToJson() => SerializeObject(this);

        public override string ToString() => ToJson();
    }
}
