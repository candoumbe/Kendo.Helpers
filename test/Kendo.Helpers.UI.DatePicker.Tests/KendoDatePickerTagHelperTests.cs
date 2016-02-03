using FluentAssertions;
using Microsoft.AspNet.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Kendo.Helpers.UI.DatePicker.Tests
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    public class KendoDatePickerTagHelperTests
    {
        private readonly ITestOutputHelper _output;

        public KendoDatePickerTagHelperTests(ITestOutputHelper output)
        {
            _output = output;
        }


        public static IEnumerable<object[]> ProcessCases {
            get
            {
                yield return new object[]
                {
                    new KendoDatePickerTagHelper() { },
                    new TagHelperContext(Enumerable.Empty<IReadOnlyTagHelperAttribute>(), new Dictionary<object, object>(), Guid.NewGuid().ToString("n")),
                    new TagHelperOutput("kendoDatePicker", new TagHelperAttributeList(),  outputTag => Task.FromResult((TagHelperContent)null)),
                    (Expression<Func<TagHelperOutput, bool>>) (outputTag =>
                        outputTag.TagName == "input" &&
                        outputTag.TagMode == TagMode.SelfClosing &&
                        outputTag.Attributes.Count() == 2

                        && outputTag.Attributes.ContainsName(KendoDatePickerTagHelper.TypeAttributeName)
                        && outputTag.Attributes[KendoDatePickerTagHelper.TypeAttributeName].Value is string
                        && "date".Equals(outputTag.Attributes[KendoDatePickerTagHelper.TypeAttributeName].Value)
                        
                        && outputTag.Attributes.ContainsName(KendoDatePickerTagHelper.RoleAttributeName) 
                        && outputTag.Attributes[KendoDatePickerTagHelper.RoleAttributeName].Value is string 
                        && ((string)outputTag.Attributes[KendoDatePickerTagHelper.RoleAttributeName].Value) == KendoDatePickerTagHelper.WidgetName
                    )
                };

                yield return new object[]
                {
                    new KendoDatePickerTagHelper() {
                        Min = new DateTime(1983, 06, 23)
                    },
                    new TagHelperContext(Enumerable.Empty<IReadOnlyTagHelperAttribute>(), new Dictionary<object, object>(), Guid.NewGuid().ToString("n")),
                    new TagHelperOutput("kendoDatePicker", new TagHelperAttributeList(),  outputTag => Task.FromResult((TagHelperContent)null)),
                    (Expression<Func<TagHelperOutput, bool>>) (outputTag =>
                        outputTag.TagName == "input" &&
                        outputTag.TagMode == TagMode.SelfClosing &&
                        outputTag.Attributes.Count() == 3

                        && outputTag.Attributes.ContainsName(KendoDatePickerTagHelper.TypeAttributeName)
                        && outputTag.Attributes[KendoDatePickerTagHelper.TypeAttributeName].Value is string
                        && "date".Equals(outputTag.Attributes[KendoDatePickerTagHelper.TypeAttributeName].Value)

                        && outputTag.Attributes.ContainsName(KendoDatePickerTagHelper.RoleAttributeName)
                        && outputTag.Attributes[KendoDatePickerTagHelper.RoleAttributeName].Value is string
                        && ((string)outputTag.Attributes[KendoDatePickerTagHelper.RoleAttributeName].Value) == KendoDatePickerTagHelper.WidgetName
                        
                        && outputTag.Attributes.ContainsName(KendoDatePickerTagHelper.MinAttributeName)
                        && outputTag.Attributes[KendoDatePickerTagHelper.MinAttributeName].Value is string
                        && "1983-06-23".Equals((string) outputTag.Attributes[KendoDatePickerTagHelper.MinAttributeName].Value)
                    )
                };


                yield return new object[]
                {
                    new KendoDatePickerTagHelper() {
                        Min = new DateTime(1983, 06, 23),
                        Format = "dd/MM/yyyy"
                    },
                    new TagHelperContext(Enumerable.Empty<IReadOnlyTagHelperAttribute>(), new Dictionary<object, object>(), Guid.NewGuid().ToString("n")),
                    new TagHelperOutput("kendoDatePicker", new TagHelperAttributeList(),  outputTag => Task.FromResult((TagHelperContent)null)),
                    (Expression<Func<TagHelperOutput, bool>>) (outputTag =>
                        outputTag.TagName == "input" &&
                        outputTag.TagMode == TagMode.SelfClosing &&
                        outputTag.Attributes.Count() == 4


                        && outputTag.Attributes.ContainsName(KendoDatePickerTagHelper.TypeAttributeName)
                        && outputTag.Attributes[KendoDatePickerTagHelper.TypeAttributeName].Value is string
                        && "date".Equals(outputTag.Attributes[KendoDatePickerTagHelper.TypeAttributeName].Value)

                        && outputTag.Attributes.ContainsName(KendoDatePickerTagHelper.RoleAttributeName)
                        && outputTag.Attributes[KendoDatePickerTagHelper.RoleAttributeName].Value is string
                        && ((string)outputTag.Attributes[KendoDatePickerTagHelper.RoleAttributeName].Value) == KendoDatePickerTagHelper.WidgetName

                        && outputTag.Attributes.ContainsName(KendoDatePickerTagHelper.MinAttributeName)
                        && outputTag.Attributes[KendoDatePickerTagHelper.MinAttributeName].Value is string
                        && "1983-06-23".Equals((string) outputTag.Attributes[KendoDatePickerTagHelper.MinAttributeName].Value)

                        && outputTag.Attributes.ContainsName(KendoDatePickerTagHelper.FormatAttributeName)
                        && outputTag.Attributes[KendoDatePickerTagHelper.FormatAttributeName].Value is string
                        && "dd/MM/yyyy".Equals((string) outputTag.Attributes[KendoDatePickerTagHelper.FormatAttributeName].Value)


                    )
                };


                yield return new object[]
                {
                    new KendoDatePickerTagHelper() {
                        Value = new DateTime(1983, 06, 23)
                    },
                    new TagHelperContext(Enumerable.Empty<IReadOnlyTagHelperAttribute>(), new Dictionary<object, object>(), Guid.NewGuid().ToString("n")),
                    new TagHelperOutput("kendoDatePicker", new TagHelperAttributeList(),  outputTag => Task.FromResult((TagHelperContent)null)),
                    (Expression<Func<TagHelperOutput, bool>>) (outputTag =>
                        outputTag.TagName == "input" &&
                        outputTag.TagMode == TagMode.SelfClosing &&
                        outputTag.Attributes.Count() == 3

                        && outputTag.Attributes.ContainsName(KendoDatePickerTagHelper.TypeAttributeName)
                        && outputTag.Attributes[KendoDatePickerTagHelper.TypeAttributeName].Value is string
                        && "date".Equals(outputTag.Attributes[KendoDatePickerTagHelper.TypeAttributeName].Value)
                        
                        && outputTag.Attributes.ContainsName(KendoDatePickerTagHelper.RoleAttributeName)
                        && outputTag.Attributes[KendoDatePickerTagHelper.RoleAttributeName].Value is string
                        && ((string)outputTag.Attributes[KendoDatePickerTagHelper.RoleAttributeName].Value) == KendoDatePickerTagHelper.WidgetName
                        
                        && outputTag.Attributes.ContainsName(KendoDatePickerTagHelper.ValueAttributeName)
                        && outputTag.Attributes[KendoDatePickerTagHelper.ValueAttributeName].Value is string
                        && "1983-06-23".Equals((string) outputTag.Attributes[KendoDatePickerTagHelper.ValueAttributeName].Value)
                    )
                };

                yield return new object[]
                {
                    new KendoDatePickerTagHelper() {
                        Max = new DateTime(1983, 06, 23)
                    },
                    new TagHelperContext(Enumerable.Empty<IReadOnlyTagHelperAttribute>(), new Dictionary<object, object>(), Guid.NewGuid().ToString("n")),
                    new TagHelperOutput("kendoDatePicker", new TagHelperAttributeList(),  outputTag => Task.FromResult((TagHelperContent)null)),
                    (Expression<Func<TagHelperOutput, bool>>) (outputTag =>
                        outputTag.TagName == "input" 
                        && outputTag.TagMode == TagMode.SelfClosing 
                        && outputTag.Attributes.Count() == 3

                        && outputTag.Attributes.ContainsName(KendoDatePickerTagHelper.TypeAttributeName)
                        && outputTag.Attributes[KendoDatePickerTagHelper.TypeAttributeName].Value is string
                        && "date".Equals(outputTag.Attributes[KendoDatePickerTagHelper.TypeAttributeName].Value)

                        && outputTag.Attributes.ContainsName(KendoDatePickerTagHelper.RoleAttributeName)
                        && outputTag.Attributes[KendoDatePickerTagHelper.RoleAttributeName].Value is string
                        && ((string)outputTag.Attributes[KendoDatePickerTagHelper.RoleAttributeName].Value) == KendoDatePickerTagHelper.WidgetName
                        
                        && outputTag.Attributes.ContainsName(KendoDatePickerTagHelper.MaxAttributeName)
                        && outputTag.Attributes[KendoDatePickerTagHelper.MaxAttributeName].Value is string
                        && "1983-06-23".Equals((string) outputTag.Attributes[KendoDatePickerTagHelper.MaxAttributeName].Value)
                    )
                };

                yield return new object[]
                {
                    new KendoDatePickerTagHelper() {
                        Max = new DateTime(1983, 06, 23),
                        Min = new DateTime(1980, 6, 23)
                    },
                    new TagHelperContext(Enumerable.Empty<IReadOnlyTagHelperAttribute>(), new Dictionary<object, object>(), Guid.NewGuid().ToString("n")),
                    new TagHelperOutput("kendoDatePicker", new TagHelperAttributeList(),  outputTag => Task.FromResult((TagHelperContent)null)),
                    (Expression<Func<TagHelperOutput, bool>>) (outputTag =>
                        outputTag.TagName == "input" &&
                        outputTag.TagMode == TagMode.SelfClosing &&
                        outputTag.Attributes.Count() == 4

                        && outputTag.Attributes.ContainsName(KendoDatePickerTagHelper.TypeAttributeName)
                        && outputTag.Attributes[KendoDatePickerTagHelper.TypeAttributeName].Value is string
                        && "date".Equals(outputTag.Attributes[KendoDatePickerTagHelper.TypeAttributeName].Value)

                        && outputTag.Attributes.ContainsName(KendoDatePickerTagHelper.RoleAttributeName)
                        && outputTag.Attributes[KendoDatePickerTagHelper.RoleAttributeName].Value is string
                        && ((string)outputTag.Attributes[KendoDatePickerTagHelper.RoleAttributeName].Value) == KendoDatePickerTagHelper.WidgetName
                        
                        && outputTag.Attributes.ContainsName(KendoDatePickerTagHelper.MaxAttributeName)
                        && outputTag.Attributes[KendoDatePickerTagHelper.MaxAttributeName].Value is string
                        && "1983-06-23".Equals((string) outputTag.Attributes[KendoDatePickerTagHelper.MaxAttributeName].Value)

                        && outputTag.Attributes.ContainsName(KendoDatePickerTagHelper.MinAttributeName)
                        && outputTag.Attributes[KendoDatePickerTagHelper.MinAttributeName].Value is string
                        && "1980-06-23".Equals((string) outputTag.Attributes[KendoDatePickerTagHelper.MinAttributeName].Value)
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
