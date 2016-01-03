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
        public const string DataSourceAttributeName = "asp-data-source";
        public const string ColumnsAttributeName = "asp-data-columns";
        public const string ReorderableAttributeName = "asp-data-reoderable";
        public const string RoleAttributeName = "data-role";

        
        /// <summary>
        /// Gets 
        /// </summary>
        [HtmlAttributeName(DataSourceAttributeName)]
        public IKendoDataSource DataSource { get; set; }

        [HtmlAttributeName(ReorderableAttributeName)]
        public bool? Reorderable { get; set; }


        /// <summary>
        /// Columns to display in the grid
        /// </summary>
        [HtmlAttributeName(ColumnsAttributeName)]
        public IEnumerable<KendoGridColumnBase> Columns { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes[RoleAttributeName] = "grid";

            if (DataSource != null)
            {
                output.Attributes[DataSourceAttributeName.Substring(4)] = DataSource.ToJson();
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
                output.Attributes[ColumnsAttributeName.Substring(4)] = $"[{sbFields}{(sbFields.Length > 0 && sbCommands.Length > 0 ? "," : string.Empty)}{(sbCommands.Length > 0 ? $@"{{""command"":[{sbCommands}]}}" : string.Empty)}]";
            }
        }
    }
}
