using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Razor.Runtime.TagHelpers;
using Microsoft.AspNet.Razor.TagHelpers;
using static Newtonsoft.Json.JsonConvert;
using Newtonsoft.Json;

namespace Kendo.Helpers.Grid
{

    [HtmlTargetElement(TagStructure = TagStructure.NormalOrSelfClosing)]
    public class KendoGridRazorHelper : TagHelper
    {

        public const string dataSourceAttributeName = "dataSource";
        public const string columnsAttributesName = "columns";
        /// <summary>
        /// Gets 
        /// </summary>
        [HtmlAttributeName(dataSourceAttributeName)]
        public KendoDataSource DataSource { get; set; }

        /// <summary>
        /// Columns to display in the grid
        /// </summary>
        [HtmlAttributeName(columnsAttributesName)]
        public IEnumerable<KendoGridColumn> Columns { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes[dataSourceAttributeName] = SerializeObject(DataSource);
            output.Attributes[columnsAttributesName] = SerializeObject(Columns);

        }
    }
}
