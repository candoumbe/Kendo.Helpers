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
    [HtmlTargetElement("kendoGrid")]
    public class KendoGridTagHelper : TagHelper
    {
        public const string dataSourceAttributeName = "asp-data-source";
        public const string columnsAttributesName = "asp-data-columns";
        public const string reorderableAttributeName = "asp-data-reoderable";
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
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes[roleAttributeName] = "grid";

            if (DataSource != null)
            {
                output.Attributes[dataSourceAttributeName.Substring(4)] = DataSource.ToJson();
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
                output.Attributes[columnsAttributesName.Substring(4)] = $"[{sbFields}{(sbFields.Length > 0 && sbCommands.Length > 0 ? "," : string.Empty)}{(sbCommands.Length > 0 ? $@"{{""command"":[{sbCommands}]}}" : string.Empty)}]";
            }
        }
    }
}
