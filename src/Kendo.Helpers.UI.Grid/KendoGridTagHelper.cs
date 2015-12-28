using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Razor.Runtime.TagHelpers;
using Microsoft.AspNet.Razor.TagHelpers;
using static Newtonsoft.Json.JsonConvert;
using Newtonsoft.Json;
using Kendo.Helpers.Data;
using System.Text;

namespace Kendo.Helpers.UI.Grid
{

    [HtmlTargetElement(TagStructure = TagStructure.NormalOrSelfClosing)]
    public class KendoGridTagHelper : TagHelper
    {

        public const string dataSourceAttributeName = "data-dataSource";
        public const string columnsAttributesName = "data-columns";
        public const string reorderableAttributeName = "data-reoderable";
        public const string roleAttributeName = "data-role";

        
        /// <summary>
        /// Gets 
        /// </summary>
        [HtmlAttributeName(dataSourceAttributeName)]
        public KendoDataSource DataSource { get; set; }

        [HtmlAttributeName(reorderableAttributeName)]
        public bool? Reorderable { get; set; }


        /// <summary>
        /// Columns to display in the grid
        /// </summary>
        [HtmlAttributeName(columnsAttributesName)]
        public IEnumerable<KendoGridColumnBase> Columns { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Attributes[roleAttributeName] = "grid";
            if (DataSource != null)
            {
                output.Attributes[dataSourceAttributeName] = DataSource.ToJson();
            }

            if (Columns?.Any() ?? false)
            {
                
                StringBuilder sbFields = new StringBuilder();
                StringBuilder sbCommands = new StringBuilder();
                foreach (KendoGridColumnBase item in Columns)
                {
                    if (item is KendoGridFieldColumn)
                    {
                        sbFields.Append($"{(sbFields.Length > 0 ? "," : string.Empty)}{item.ToJson()}");
                    }
                    else if (item is KendoGridCommandColumn)
                    {
                        sbCommands.Append($"{(sbCommands.Length > 0 ? "," : string.Empty)}{item.ToJson()}");
                    }    
                }
                string sbColumns = $"{(sbFields.Length > 0 || sbCommands.Length > 0)}";

                output.Attributes[columnsAttributesName] = $@"""columns"":[{sbFields}{(sbFields.Length > 0 && sbCommands.Length > 0 ? "," : string.Empty)}{{""command"":[{sbCommands}]}}]";
            }
        }
    }
}
