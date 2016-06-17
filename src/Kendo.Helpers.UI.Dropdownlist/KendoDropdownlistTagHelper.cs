using Microsoft.AspNetCore.Razor.TagHelpers;
using Kendo.Helpers.Data;

namespace Kendo.Helpers.UI.Dropdownlist
{
    public class KendoDropdownlistTagHelper : TagHelper
    {
        /// <summary>
        /// Name of the data-attribute that defines the role of the widget
        /// </summary>
        public const string RoleAttributeName = "data-role";

        /// <summary>
        /// Name of the widget the current helper builds
        /// </summary>
        public const string WidgetName = "dropdownlist";

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
            output.TagMode = TagMode.SelfClosing;

            output.TagName = "input";

            output.Attributes.Add(RoleAttributeName, WidgetName);

            output.Attributes.Add(DataSourceAttributeName.Substring(4), DataSource?.ToJson());
        }

    }
}
