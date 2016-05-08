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
        /// <see cref="IKendoDataSource"/>
        /// <seealso cref="KendoLocalDataSource"/>
        /// <seealso cref="KendoLocalDataSource{T}"/>
        /// <seealso cref="KendoRemoteDataSource"/>
        public const string DataSourceAttributeName = "asp-data-source";

        /// <summary>
        /// Name of the attribute to use to enable tag helper assistance to configure
        /// </summary>
        public const string MinLengthAttributeName = "min-length";

        /// <summary>
        /// Name of the attribute which defines "ignore-case" behaviour
        /// </summary>
        public const string IgnoreCaseAttributeName = "ignore-case";

        /// <summary>
        /// Name of the widget
        /// </summary>
        public const string WidgetName = "autocomplete";

        /// <summary>
        /// Name of the placeholder
        /// </summary>
        public const string PlaceholderAttributeName = "placeholder";

        /// <summary>
        /// Gets/sets the <see cref="IKendoDataSource"/>
        /// </summary>
        [HtmlAttributeName(DataSourceAttributeName)]
        public IKendoDataSource DataSource { get; set; }

        /// <summary>
        /// The minimum number of characters the user must type before a search is performed. 
        /// Set to higher value than 1 if the search could match a lot of items.
        /// </summary>
        [HtmlAttributeName(MinLengthAttributeName)]
        public uint? MinLength { get; set; }

        /// <summary>
        /// If set to false case-sensitive search will be performed to find suggestions. 
        /// The widget performs case-insensitive searching by default.
        /// </summary>
        [HtmlAttributeName(IgnoreCaseAttributeName)]
        public bool? IgnoreCase { get; set; }


        /// <summary>
        /// The hint displayed by the widget when it is empty. Not set by default.
        /// </summary>
        [HtmlAttributeName(PlaceholderAttributeName)]
        public string Placeholder { get; set; }



        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "input";
            output.Attributes[TypeAttributeName] = "text";
            output.TagMode = TagMode.SelfClosing;
            output.Attributes[RoleAttributeName] = WidgetName;

            output.Attributes[DataSourceAttributeName.Substring(4)] = DataSource?.ToJson();
            if (MinLength.HasValue)
            {
                output.Attributes.Add($"data-{MinLengthAttributeName}", MinLength.Value); 
            }
            if (IgnoreCase.HasValue)
            {
                output.Attributes.Add($"data-{IgnoreCaseAttributeName}", IgnoreCase.Value);
            }

            if (!string.IsNullOrWhiteSpace(Placeholder))
            {
                output.Attributes[$"data-{PlaceholderAttributeName}"] = Placeholder;
            }
            


        }

    }
}
