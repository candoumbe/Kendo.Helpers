using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Razor.TagHelpers;

namespace Kendo.Helpers.UI.DatePicker
{
    /// <summary>
    /// <para>
    /// Tag helper to generate a kendo date picker.
    /// </para>
    /// 
    /// <para>
    /// Usage :
    ///     <code>
    ///         <kendoDatePicker max="new DateTime(2012, 12, 25)" >
    ///             
    ///        </kendoDatePicker>
    ///     </code>
    /// </para>
    /// </summary>
    public class KendoDatePickerTagHelper : TagHelper
    {
        /// <summary>
        /// Name of the data-attribute that defines the role of the widget
        /// </summary>
        public const string RoleAttributeName = "data-role";

        /// <summary>
        /// Name of the widget the current helper builds
        /// </summary>
        public const string WidgetName = "datepicker";


        public const string MinAttributeName = "data-min";

        /// <summary>
        /// Name of the data-attribute that holds the max selectable date
        /// </summary>
        public const string MaxAttributeName = "data-max";

        /// <summary>
        /// Name of the data-attribute that holds the value of the date picker
        /// </summary>
        public const string ValueAttributeName = "data-value";

        /// <summary>
        /// Format used to output date in the json
        /// </summary>
        private const string DateOutputFormat = "yyyy-MM-dd";

        /// <summary>
        /// Name of the HTML attribute that holds the type of the input
        /// </summary>
        public const string TypeAttributeName = "type";

        /// <summary>
        /// Name of the data-attribute that holds the format used to display the selected date
        /// </summary>
        public const string FormatAttributeName = "data-format";
        /// <summary>
        /// Specifies the minimum selectable date.
        /// </summary>
        public DateTime? Min { get; set; }

        /// <summary>
        /// Specifies the maximum selectable date.
        /// </summary>
        public DateTime? Max { get; set; }

        /// <summary>
        /// Specifies the current selected date.
        /// </summary>
        public DateTime? Value { get; set; }

        /// <summary>
        /// Specifies the format used to display the date
        /// </summary>
        public string Format { get; set; }




        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "input";
            output.TagMode = TagMode.SelfClosing;

            output.Attributes[RoleAttributeName] = WidgetName;
            output.Attributes[TypeAttributeName] = "date";
            if (Min.HasValue)
            {
                output.Attributes[MinAttributeName] = Min?.ToString(DateOutputFormat);
            }

            if (Max.HasValue)
            {
                output.Attributes[MaxAttributeName] = Max?.ToString(DateOutputFormat);
            }

            if (Value.HasValue)
            {
                output.Attributes[ValueAttributeName] = Value?.ToString(DateOutputFormat);
            }
            if (Format != null)
            {
                output.Attributes[FormatAttributeName] = Format;
            }

            //output.Content.SetContent(string.Empty);
            base.Process(context, output);
        }


        


    }
}
