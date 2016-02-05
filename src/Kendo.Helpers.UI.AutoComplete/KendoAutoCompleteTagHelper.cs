using Microsoft.AspNet.Razor.TagHelpers;
using Kendo.Helpers.Data;

namespace Kendo.Helpers.UI.AutoComplete
{
    public class KendoAutoCompleteTagHelper : TagHelper
    {
        /// <summary>
        /// The "data-role" value for the component
        /// </summary>
        public const string RoleAttributeName = "data-role";


        public const string TypeAttributeName = "type";

        /// <summary>
        /// Name of the attribute to use to enable tag helper assistance to configure data-source
        /// </summary>
        public const string DataSourceAttributeName = "asp-data-source";

        /// <summary>
        /// Name of the widget
        /// </summary>
        public const string WidgetName = "autocomplete";

        /// <summary>
        /// Gets/sets the <see cref="IKendoDataSource"/>
        /// </summary>
        [HtmlAttributeName(DataSourceAttributeName)]
        public IKendoDataSource DataSource { get; set; }


        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "input";
            output.Attributes[TypeAttributeName] = "text";
            output.TagMode = TagMode.SelfClosing;
            output.Attributes[RoleAttributeName] = WidgetName;

            output.Attributes[DataSourceAttributeName.Substring(4)] = DataSource?.ToJson();


        }

    }
}
