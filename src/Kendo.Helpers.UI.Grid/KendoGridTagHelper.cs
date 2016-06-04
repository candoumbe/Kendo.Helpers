using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Kendo.Helpers.Data;

namespace Kendo.Helpers.UI.Grid
{
    /// <summary>
    /// Tag helper suitable to build 
    /// </summary>
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
            output.Attributes.Add(RoleAttributeName, "grid");
            if (DataSource != null)
            {
                output.Attributes.Add(DataSourceAttributeName.Substring(4), DataSource.ToJson());
            }

            if (Columns?.Any() ?? false)
            {
                output.Attributes.Add(ColumnsAttributeName.Substring(4), Columns.ToJson());
            }
           

            if (Reorderable.HasValue)
            {
                output.Attributes.Add(ReorderableAttributeName.Substring(4), Reorderable.Value);
            }

            if (Pageable != null)
            {
                output.Attributes.Add(PageableAttributeName.Substring(4), Pageable.ToJson());
            }
        }
    }
}
