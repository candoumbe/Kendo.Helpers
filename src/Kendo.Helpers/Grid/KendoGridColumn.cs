using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Kendo.Helpers.Grid
{

    public class KendoGridColumn
    {
        /// <summary>
        /// Gets/sets the field the column will represents
        /// </summary>
        [DataMember(Name="field", EmitDefaultValue = false, Order = 1)]
        public string Field { get; set; }
        [DataMember(Name = "title", EmitDefaultValue = false, Order = 2)]
        public string Title{ get; set; }

        public IEnumerable<string> Attributes { get; set; }

        /// <summary>
        /// Gets/Sets if the column is editable
        /// </summary>
        public bool? Editable { get; set; }


        public IEnumerable<KendoGridColumn> Columns { get; set; }
    }
}