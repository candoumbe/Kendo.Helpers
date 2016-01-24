using Microsoft.AspNet.Razor.TagHelpers;
using Kendo.Helpers.Data;

namespace Kendo.Helpers.UI.AutoComplete
{
    [HtmlTargetElement("kendoAutoComplete", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class KendoAutoCompleteTagHelper : TagHelper
    {
        /// <summary>
        /// The "data-role" value for the component
        /// </summary>
        public const string RoleAttributeName = "data-role";

        /// <summary>
        /// Name of the attribute to use to enable tag helper assistance to configure data-source
        /// </summary>
        public const string DataSourceAttributeName = "asp-data-source";

        /// <summary>
        /// Gets/sets the <see cref="IKendoDataSource"/>
        /// </summary>
        [HtmlAttributeName(DataSourceAttributeName)]
        public IKendoDataSource DataSource { get; set; }


        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes[RoleAttributeName] = "autocomplete";

            output.Attributes[DataSourceAttributeName.Substring(4)] = DataSource?.ToJson();
        }

    }
}
