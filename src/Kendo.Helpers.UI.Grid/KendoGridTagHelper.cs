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
        /// <summary>
        /// Name of the property that holds the dataSource configuration
        /// </summary>
        public const string DataSourceAttributeName = "asp-data-source";
        /// <summary>
        /// Name of the ASP HTML attribute that holds the configuration of the columns
        /// </summary>
        public const string ColumnsAttributeName = "asp-data-columns";
        /// <summary>
        /// Name of the ASP HTML attribute that defines how columns can be reordered
        /// </summary>
        public const string ReorderableAttributeName = "asp-data-reoderable";

        public const string RoleAttributeName = "data-role";

        /// <summary>
        /// Name of the ASP HTML attribute that defines the pageable configuration of the grid
        /// </summary>
        public const string PageableAttributeName = "asp-data-pageable";

        
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

        /// <summary>
        /// Pageable configuration of the grid
        /// </summary>
        [HtmlAttributeName(PageableAttributeName)]
        public PageableConfiguration Pageable { get; set; }

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
                output.Attributes[ColumnsAttributeName.Substring(4)] = Columns.ToJson();
            }
           

            if (Reorderable.HasValue)
            {
                output.Attributes.Add(ReorderableAttributeName.Substring(4), Reorderable.Value);
            }

            if (Pageable != null)
            {
                output.Attributes[PageableAttributeName.Substring(4)] = Pageable.ToJson();
            }
        }
    }
}
