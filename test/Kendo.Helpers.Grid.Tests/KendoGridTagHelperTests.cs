using FluentAssertions;
using Microsoft.AspNet.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace Kendo.Helpers.Grid.Tests
{
    public class KendoGridTagHelperTests
    {
       
        private static IEnumerable<object> Cases
        {
            get
            {
                yield return new object[] {
                    new KendoGridRazorHelper(),
                    null,
                    new TagHelperOutput("div", new TagHelperAttributeList(Enumerable.Empty<TagHelperAttribute>()), null),
                    (Expression<Func<TagHelperAttributeList, bool>>) (attributes => attributes == null || !attributes.Any())                     };
            }
        }

        [Theory]
        [MemberData(nameof(Cases))]
        public void GenerateHtml(KendoGridRazorHelper config, TagHelperContext context, TagHelperOutput output, Expression<Func<TagHelperAttributeList, bool>> attributesExpectation)
        {
            config.Process(context, output);

            output.Attributes.Should().Match(attributesExpectation);
        }

        
    }
}
