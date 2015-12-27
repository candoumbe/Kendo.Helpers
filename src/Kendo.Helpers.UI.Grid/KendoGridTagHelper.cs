using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Razor.Runtime.TagHelpers;
using Microsoft.AspNet.Razor.TagHelpers;
using static Newtonsoft.Json.JsonConvert;
using Newtonsoft.Json;
using Kendo.Helpers.Data;

namespace Kendo.Helpers.UI.Grid
{

    [HtmlTargetElement(TagStructure = TagStructure.NormalOrSelfClosing)]
    public class KendoGridTagHelper : TagHelper
    {

        public const string dataSourceAttributeName = "data-dataSource";
        public const string columnsAttributesName = "data-columns";
        
        /// <summary>
        /// Gets 
        /// </summary>
        [HtmlAttributeName(dataSourceAttributeName)]
        public KendoDataSource DataSource { get; set; }

        
        /// <summary>
        /// Columns to display in the grid
        /// </summary>
        [HtmlAttributeName(columnsAttributesName)]
        public IEnumerable<KendoGridFieldColumn> Columns { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            if (DataSource != null)
            {
                output.Attributes[dataSourceAttributeName] = DataSource.ToJson();
            }

            if (Columns?.Any() ?? false)
            {
                output.Attributes[columnsAttributesName] = SerializeObject(Columns);
            }
        }
    }
}
