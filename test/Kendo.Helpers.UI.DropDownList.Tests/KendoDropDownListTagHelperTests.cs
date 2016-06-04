using FluentAssertions;
using Kendo.Helpers.UI.Dropdownlist;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Kendo.Helpers.UI.Dropdownlist.Tests
{
    public class KendoDropdownlistTagHelperTests
    {

        private readonly ITestOutputHelper _output;

        public KendoDropdownlistTagHelperTests(ITestOutputHelper output)
        {
            _output = output;
        
        }

        public static IEnumerable<object[]> ProcessCases
        {
            get
            {
                yield return new object[]
                {
                    new KendoDropdownlistTagHelper() { },
                    new TagHelperContext( new TagHelperAttributeList(), new Dictionary<object, object>(), Guid.NewGuid().ToString("n")),
                    new TagHelperOutput("kendoDropDownList", new TagHelperAttributeList(),  (param1, param2) => Task.FromResult((TagHelperContent)null)),
                    (Expression<Func<TagHelperOutput, bool>>) (outputTag =>
                        outputTag.TagName == "input" &&
                        outputTag.TagMode == TagMode.SelfClosing &&
                        outputTag.Attributes.Count() == 2

                        && outputTag.Attributes.ContainsName(KendoDropdownlistTagHelper.RoleAttributeName)
                        && outputTag.Attributes[KendoDropdownlistTagHelper.RoleAttributeName].Value is string
                        && ((string)outputTag.Attributes[KendoDropdownlistTagHelper.RoleAttributeName].Value) == KendoDropdownlistTagHelper.WidgetName
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
