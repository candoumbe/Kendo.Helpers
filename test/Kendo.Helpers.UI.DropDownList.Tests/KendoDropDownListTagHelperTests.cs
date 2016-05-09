using FluentAssertions;
using Kendo.Helpers.UI.DropDown;
using Microsoft.AspNet.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Kendo.Helpers.UI.DropDownList.Tests
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    public class KendoDropDownListTagHelperTests
    {

        private readonly ITestOutputHelper _output;

        public KendoDropDownListTagHelperTests(ITestOutputHelper output)
        {
            _output = output;
        
        }

        public static IEnumerable<object[]> ProcessCases
        {
            get
            {
                yield return new object[]
                {
                    new KendoDropDownListTagHelper() { },
                    new TagHelperContext(Enumerable.Empty<IReadOnlyTagHelperAttribute>(), new Dictionary<object, object>(), Guid.NewGuid().ToString("n")),
                    new TagHelperOutput("kendoDropDownList", new TagHelperAttributeList(),  outputTag => Task.FromResult((TagHelperContent)null)),
                    (Expression<Func<TagHelperOutput, bool>>) (outputTag =>
                        outputTag.TagName == "input" &&
                        outputTag.TagMode == TagMode.SelfClosing &&
                        outputTag.Attributes.Count() == 2

                        && outputTag.Attributes.ContainsName(KendoDropDownListTagHelper.RoleAttributeName)
                        && outputTag.Attributes[KendoDropDownListTagHelper.RoleAttributeName].Value is string
                        && ((string)outputTag.Attributes[KendoDropDownListTagHelper.RoleAttributeName].Value) == KendoDropDownListTagHelper.WidgetName
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
