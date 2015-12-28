using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using static Newtonsoft.Json.JsonConvert;

namespace Kendo.Helpers.UI.Grid
{
    [DataContract]
    public class KendoGridFieldColumn : KendoGridColumnBase
    {
        /// <summary>
        /// Gets/sets the field the column will represents
        /// </summary>
        [DataMember(Name="field", EmitDefaultValue = false, Order = 1)]
        public string Field { get; set; }
        /// <summary>
        /// Gets/Sets the title of the column
        /// </summary>
        [DataMember(Name = "title", EmitDefaultValue = false, Order = 2)]
        public string Title{ get; set; }

        /// <summary>
        /// Additional CSS attributes for the columns
        /// </summary>
        [DataMember(Name = "attributes", EmitDefaultValue = false, Order = 4)]
        public IDictionary<string, object> Attributes { get; set; }

        

        public IEnumerable<KendoGridFieldColumn> Columns { get; set; }

        public override string ToJson()
            => SerializeObject(this);
    }
}