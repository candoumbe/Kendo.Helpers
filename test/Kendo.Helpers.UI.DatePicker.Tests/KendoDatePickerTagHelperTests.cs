using FluentAssertions;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Kendo.Helpers.UI.Datepicker.Tests
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    public class KendoDatepickerTagHelperTests
    {
        private readonly ITestOutputHelper _output;

        public KendoDatepickerTagHelperTests(ITestOutputHelper output)
        {
            _output = output;
        }


        public static IEnumerable<object[]> ProcessCases {
            get
            {
                yield return new object[]
                {
                    new KendoDatepickerTagHelper() { },
                    new TagHelperContext(new TagHelperAttributeList(), new Dictionary<object, object>(), Guid.NewGuid().ToString("n")),
                    new TagHelperOutput("kendoDatePicker", new TagHelperAttributeList(),  (param1, param2) => Task.FromResult((TagHelperContent)null)),
                    (Expression<Func<TagHelperOutput, bool>>) (outputTag =>
                        outputTag.TagName == "input" &&
                        outputTag.TagMode == TagMode.SelfClosing &&
                        outputTag.Attributes.Count() == 2

                        && outputTag.Attributes.ContainsName(KendoDatepickerTagHelper.TypeAttributeName)
                        && outputTag.Attributes[KendoDatepickerTagHelper.TypeAttributeName].Value is string
                        && "date".Equals(outputTag.Attributes[KendoDatepickerTagHelper.TypeAttributeName].Value)
                        
                        && outputTag.Attributes.ContainsName(KendoDatepickerTagHelper.RoleAttributeName) 
                        && outputTag.Attributes[KendoDatepickerTagHelper.RoleAttributeName].Value is string 
                        && ((string)outputTag.Attributes[KendoDatepickerTagHelper.RoleAttributeName].Value) == KendoDatepickerTagHelper.WidgetName
                    )
                };

                yield return new object[]
                {
                    new KendoDatepickerTagHelper() {
                        Min = new DateTime(1983, 06, 23)
                    },
                    new TagHelperContext(new TagHelperAttributeList(), new Dictionary<object, object>(), Guid.NewGuid().ToString("n")),
                    new TagHelperOutput("kendoDatePicker", new TagHelperAttributeList(),  (param1, param2) => Task.FromResult((TagHelperContent)null)),
                    (Expression<Func<TagHelperOutput, bool>>) (outputTag =>
                        outputTag.TagName == "input" &&
                        outputTag.TagMode == TagMode.SelfClosing &&
                        outputTag.Attributes.Count() == 3

                        && outputTag.Attributes.ContainsName(KendoDatepickerTagHelper.TypeAttributeName)
                        && outputTag.Attributes[KendoDatepickerTagHelper.TypeAttributeName].Value is string
                        && "date".Equals(outputTag.Attributes[KendoDatepickerTagHelper.TypeAttributeName].Value)

                        && outputTag.Attributes.ContainsName(KendoDatepickerTagHelper.RoleAttributeName)
                        && outputTag.Attributes[KendoDatepickerTagHelper.RoleAttributeName].Value is string
                        && ((string)outputTag.Attributes[KendoDatepickerTagHelper.RoleAttributeName].Value) == KendoDatepickerTagHelper.WidgetName
                        
                        && outputTag.Attributes.ContainsName(KendoDatepickerTagHelper.MinAttributeName)
                        && outputTag.Attributes[KendoDatepickerTagHelper.MinAttributeName].Value is string
                        && "1983-06-23".Equals((string) outputTag.Attributes[KendoDatepickerTagHelper.MinAttributeName].Value)
                    )
                };


                yield return new object[]
                {
                    new KendoDatepickerTagHelper() {
                        Min = new DateTime(1983, 06, 23),
                        Format = "dd/MM/yyyy"
                    },
                    new TagHelperContext(new TagHelperAttributeList(), new Dictionary<object, object>(), Guid.NewGuid().ToString("n")),
                    new TagHelperOutput("kendoDatePicker", new TagHelperAttributeList(),  (param1, param2) => Task.FromResult((TagHelperContent)null)),
                    (Expression<Func<TagHelperOutput, bool>>) (outputTag =>
                        outputTag.TagName == "input" &&
                        outputTag.TagMode == TagMode.SelfClosing &&
                        outputTag.Attributes.Count() == 4


                        && outputTag.Attributes.ContainsName(KendoDatepickerTagHelper.TypeAttributeName)
                        && outputTag.Attributes[KendoDatepickerTagHelper.TypeAttributeName].Value is string
                        && "date".Equals(outputTag.Attributes[KendoDatepickerTagHelper.TypeAttributeName].Value)

                        && outputTag.Attributes.ContainsName(KendoDatepickerTagHelper.RoleAttributeName)
                        && outputTag.Attributes[KendoDatepickerTagHelper.RoleAttributeName].Value is string
                        && ((string)outputTag.Attributes[KendoDatepickerTagHelper.RoleAttributeName].Value) == KendoDatepickerTagHelper.WidgetName

                        && outputTag.Attributes.ContainsName(KendoDatepickerTagHelper.MinAttributeName)
                        && outputTag.Attributes[KendoDatepickerTagHelper.MinAttributeName].Value is string
                        && "1983-06-23".Equals((string) outputTag.Attributes[KendoDatepickerTagHelper.MinAttributeName].Value)

                        && outputTag.Attributes.ContainsName(KendoDatepickerTagHelper.FormatAttributeName)
                        && outputTag.Attributes[KendoDatepickerTagHelper.FormatAttributeName].Value is string
                        && "dd/MM/yyyy".Equals((string) outputTag.Attributes[KendoDatepickerTagHelper.FormatAttributeName].Value)


                    )
                };


                yield return new object[]
                {
                    new KendoDatepickerTagHelper() {
                        Value = new DateTime(1983, 06, 23)
                    },
                    new TagHelperContext(new TagHelperAttributeList(), new Dictionary<object, object>(), Guid.NewGuid().ToString("n")),
                    new TagHelperOutput("kendoDatePicker", new TagHelperAttributeList(),  (param1, param2) => Task.FromResult((TagHelperContent)null)),
                    (Expression<Func<TagHelperOutput, bool>>) (outputTag =>
                        outputTag.TagName == "input" &&
                        outputTag.TagMode == TagMode.SelfClosing &&
                        outputTag.Attributes.Count() == 3

                        && outputTag.Attributes.ContainsName(KendoDatepickerTagHelper.TypeAttributeName)
                        && outputTag.Attributes[KendoDatepickerTagHelper.TypeAttributeName].Value is string
                        && "date".Equals(outputTag.Attributes[KendoDatepickerTagHelper.TypeAttributeName].Value)
                        
                        && outputTag.Attributes.ContainsName(KendoDatepickerTagHelper.RoleAttributeName)
                        && outputTag.Attributes[KendoDatepickerTagHelper.RoleAttributeName].Value is string
                        && ((string)outputTag.Attributes[KendoDatepickerTagHelper.RoleAttributeName].Value) == KendoDatepickerTagHelper.WidgetName
                        
                        && outputTag.Attributes.ContainsName(KendoDatepickerTagHelper.ValueAttributeName)
                        && outputTag.Attributes[KendoDatepickerTagHelper.ValueAttributeName].Value is string
                        && "1983-06-23".Equals((string) outputTag.Attributes[KendoDatepickerTagHelper.ValueAttributeName].Value)
                    )
                };

                yield return new object[]
                {
                    new KendoDatepickerTagHelper() {
                        Max = new DateTime(1983, 06, 23)
                    },
                    new TagHelperContext(new TagHelperAttributeList(), new Dictionary<object, object>(), Guid.NewGuid().ToString("n")),
                    new TagHelperOutput("kendoDatePicker", new TagHelperAttributeList(),  (param1, param2) => Task.FromResult((TagHelperContent)null)),
                    (Expression<Func<TagHelperOutput, bool>>) (outputTag =>
                        outputTag.TagName == "input" 
                        && outputTag.TagMode == TagMode.SelfClosing 
                        && outputTag.Attributes.Count() == 3

                        && outputTag.Attributes.ContainsName(KendoDatepickerTagHelper.TypeAttributeName)
                        && outputTag.Attributes[KendoDatepickerTagHelper.TypeAttributeName].Value is string
                        && "date".Equals(outputTag.Attributes[KendoDatepickerTagHelper.TypeAttributeName].Value)

                        && outputTag.Attributes.ContainsName(KendoDatepickerTagHelper.RoleAttributeName)
                        && outputTag.Attributes[KendoDatepickerTagHelper.RoleAttributeName].Value is string
                        && ((string)outputTag.Attributes[KendoDatepickerTagHelper.RoleAttributeName].Value) == KendoDatepickerTagHelper.WidgetName
                        
                        && outputTag.Attributes.ContainsName(KendoDatepickerTagHelper.MaxAttributeName)
                        && outputTag.Attributes[KendoDatepickerTagHelper.MaxAttributeName].Value is string
                        && "1983-06-23".Equals((string) outputTag.Attributes[KendoDatepickerTagHelper.MaxAttributeName].Value)
                    )
                };

                yield return new object[]
                {
                    new KendoDatepickerTagHelper() {
                        Max = new DateTime(1983, 06, 23),
                        Min = new DateTime(1980, 6, 23)
                    },
                    new TagHelperContext(new TagHelperAttributeList(), new Dictionary<object, object>(), Guid.NewGuid().ToString("n")),
                    new TagHelperOutput("kendoDatePicker", new TagHelperAttributeList(),  (param1, param2) => Task.FromResult((TagHelperContent)null)),
                    (Expression<Func<TagHelperOutput, bool>>) (outputTag =>
                        outputTag.TagName == "input" &&
                        outputTag.TagMode == TagMode.SelfClosing &&
                        outputTag.Attributes.Count() == 4

                        && outputTag.Attributes.ContainsName(KendoDatepickerTagHelper.TypeAttributeName)
                        && outputTag.Attributes[KendoDatepickerTagHelper.TypeAttributeName].Value is string
                        && "date".Equals(outputTag.Attributes[KendoDatepickerTagHelper.TypeAttributeName].Value)

                        && outputTag.Attributes.ContainsName(KendoDatepickerTagHelper.RoleAttributeName)
                        && outputTag.Attributes[KendoDatepickerTagHelper.RoleAttributeName].Value is string
                        && ((string)outputTag.Attributes[KendoDatepickerTagHelper.RoleAttributeName].Value) == KendoDatepickerTagHelper.WidgetName
                        
                        && outputTag.Attributes.ContainsName(KendoDatepickerTagHelper.MaxAttributeName)
                        && outputTag.Attributes[KendoDatepickerTagHelper.MaxAttributeName].Value is string
                        && "1983-06-23".Equals((string) outputTag.Attributes[KendoDatepickerTagHelper.MaxAttributeName].Value)

                        && outputTag.Attributes.ContainsName(KendoDatepickerTagHelper.MinAttributeName)
                        && outputTag.Attributes[KendoDatepickerTagHelper.MinAttributeName].Value is string
                        && "1980-06-23".Equals((string) outputTag.Attributes[KendoDatepickerTagHelper.MinAttributeName].Value)
                    )
                };
            }

        }

        [Theory]
        [MemberData(nameof(ProcessCases))]
        public void Process(TagHelper helper, TagHelperContext context, TagHelperOutput tagHelperOutput, Expression<Func<TagHelperOutput, bool>> outputMatcher)
        {
            _output.WriteLine($"Processing {helper}");

            helper.Process(context, tagHelperOutput);

            tagHelperOutput.Should().Match(outputMatcher);
        }
    }
}
