using Microsoft.AspNetCore.Razor.TagHelpers;
using Kendo.Helpers.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using static Kendo.Helpers.UI.Autocomplete.AutocompleteFilterType;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Kendo.Helpers.UI.Autocomplete
{
    public class KendoAutocompleteTagHelper : TagHelper
    {
        /// <summary>
        /// The "data-role" value for the component
        /// </summary>
        public const string RoleAttributeName = "data-role";

        /// <summary>
        /// Name of the property that define
        /// </summary>
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
        /// Name of the attribute
        /// </summary>
        public const string ForAttributeName = "asp-for";

        /// <summary>
        /// Name of the attribute to use to enable tag helper assistance to configure
        /// </summary>
        public const string MinLengthAttributeName = "data-min-length";

        /// <summary>
        /// Name of the attribute which defines "ignore-case" behaviour
        /// </summary>
        public const string IgnoreCaseAttributeName = "data-ignore-case";

        /// <summary>
        /// Name of the widget
        /// </summary>
        public const string WidgetName = "autocomplete";

        /// <summary>
        /// Name of the placeholder attribute
        /// </summary>
        public const string PlaceholderAttributeName = "data-placeholder";

        /// <summary>
        /// Name of the filter attribute
        /// </summary>
        public const string FilterAttributeName = "data-filter";

        

        /// <summary>
        /// Gets/sets the <see cref="IKendoDataSource"/>
        /// </summary>
        [HtmlAttributeName(DataSourceAttributeName)]
        public IKendoDataSource DataSource { get; set; }

        /// <summary>
        /// The minimum number of characters the user must type before a search is performed. 
        /// Set to higher value than 1 if the search could match a lot of items.
        /// </summary>
        public uint? MinLength { get; set; }

        /// <summary>
        /// If set to false case-sensitive search will be performed to find suggestions. 
        /// The widget performs case-insensitive searching by default.
        /// </summary>
        public bool? IgnoreCase { get; set; }


        /// <summary>
        /// The hint displayed by the widget when it is empty. Not set by default.
        /// </summary>
        public string Placeholder { get; set; }

        /// <summary>
        /// The filtering method used to determine the suggestions for the current value. 
        /// The default filter is "startswith" - all data items which begin with the current widget value are displayed in the suggestion popup
        /// </summary>
        public AutocompleteFilterType? Filter { get; set; }


        [HtmlAttributeName("asp-for")]
        public ModelExpression For { get; set; }

        

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "input";
            output.Attributes.Add(TypeAttributeName, "text");
            output.TagMode = TagMode.SelfClosing;
            output.Attributes.Add(RoleAttributeName, WidgetName);
            if (For != null && !output.Attributes.ContainsName("id"))
            {
                output.Attributes.Add("id", For.Metadata.PropertyName);
            }
            
            output.Attributes.Add(DataSourceAttributeName.Substring(4), DataSource?.ToJson());

            if (MinLength.HasValue)
            {
                output.Attributes.Add(MinLengthAttributeName, MinLength.Value); 
            }

            if (IgnoreCase.HasValue)
            {
                output.Attributes.Add(IgnoreCaseAttributeName, IgnoreCase.Value);
            }

            if (!string.IsNullOrWhiteSpace(Placeholder))
            {
                output.Attributes.Add(PlaceholderAttributeName, Placeholder);
            }
            if (Filter.HasValue)
            {
                output.Attributes.Add(FilterAttributeName, Filter.ToString().ToLower());
            }
           
        }

    }
}
