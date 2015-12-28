using System.Collections.Generic;
using System.Runtime.Serialization;
using static Newtonsoft.Json.JsonConvert;

namespace Kendo.Helpers.UI.Grid
{
    [DataContract]
    public class KendoGridCommandColumn : KendoGridColumnBase
    {
        /// <summary>
        /// Gets/Sets the name of the command
        /// </summary>
        [DataMember(Name = "name", EmitDefaultValue = false)]
        public string Name { get; set; }

        /// <summary>
        /// Gets/Sets the text associated to the command
        /// </summary>
        [DataMember(Name = "text", EmitDefaultValue = false)]
        public string Text { get; set; }

        public override string ToJson() => SerializeObject(this);
    }
}
