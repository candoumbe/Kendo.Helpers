using FluentAssertions;
using Microsoft.AspNet.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace Kendo.Helpers.UI.Grid.Tests
{
    public class KendoGridTagHelperTests
    {

        public static IEnumerable<object[]> KendoGridTagHelperCases
        {
            get
            {

                yield return new object[]
                {
                    new KendoGridTagHelper(),
                    new TagHelperContext(Enumerable.Empty<IReadOnlyTagHelperAttribute>(), new Dictionary<object, object>(), Guid.NewGuid().ToString("n")),
                    new TagHelperOutput("kendoGrid", new TagHelperAttributeList(),  input => Task.FromResult((TagHelperContent)null)),
                    (Expression<Func<TagHelperOutput, bool>>) (input => 
                        input.TagName == "div" && 
                        input.Attributes.Count() == 1 &&
                        input.Attributes.ContainsName("data-role") && input.Attributes["data-role"].Value is string && ((string)input.Attributes["data-role"].Value) == "grid")
                };

                yield return new object[]
                {
                    new KendoGridTagHelper()
                    {
                        Columns = new KendoGridColumnBase[]
                        {
                            new KendoGridFieldColumn() { Field = "Firstname" },
                            new KendoGridFieldColumn() { Field = "Lastname" },
                            new KendoGridCommandColumn() { Name = "edit" }
                        }
                    },
                    new TagHelperContext(Enumerable.Empty<IReadOnlyTagHelperAttribute>(), new Dictionary<object, object>(), Guid.NewGuid().ToString("n")),
                    new TagHelperOutput("kendoGrid", new TagHelperAttributeList(),  input => Task.FromResult((TagHelperContent)null)),
                    (Expression<Func<TagHelperOutput, bool>>) (input => input.TagName == "div" && 
                        input.Attributes.Count() == 2 &&
                        input.Attributes.ContainsName("data-role") && input.Attributes["data-role"].Value is string && ((string)input.Attributes["data-role"].Value) == "grid" &&
                        input.Attributes.ContainsName("data-columns") && input.Attributes["data-columns"].Value is string && ((string)input.Attributes["data-columns"].Value) == @"""columns"":[{""field"":""Firstname""},{""field"":""Lastname""},{""command"":[{""name"":""edit""}]}]")

                };


                yield return new object[]
                {
                    new KendoGridTagHelper()
                    {
                        Columns = new KendoGridColumnBase[]
                        {
                            new KendoGridFieldColumn() { Field = "Firstname"},
                            new KendoGridFieldColumn() { Field = "Lastname" },
                            new KendoGridCommandColumn() { Name = "edit" },
                            new KendoGridCommandColumn() { Name = "destroy" }
                        }
                    },
                    new TagHelperContext(Enumerable.Empty<IReadOnlyTagHelperAttribute>(), new Dictionary<object, object>(), Guid.NewGuid().ToString("n")),
                    new TagHelperOutput("kendoGrid", new TagHelperAttributeList(),  input => Task.FromResult((TagHelperContent)null)),
                    (Expression<Func<TagHelperOutput, bool>>) (input => input.TagName == "div" &&
                        input.Attributes.Count() == 2 &&
                        input.Attributes.ContainsName("data-role") && input.Attributes["data-role"].Value is string && ((string)input.Attributes["data-role"].Value) == "grid" &&
                        input.Attributes.ContainsName("data-columns") && input.Attributes["data-columns"].Value is string && ((string)input.Attributes["data-columns"].Value) == @"""columns"":[{""field"":""Firstname""},{""field"":""Lastname""},{""command"":[{""name"":""edit""},{""name"":""destroy""}]}]")

                };
            }
        }



        [Theory]
        [MemberData(nameof(KendoGridTagHelperCases))]
        public void Process(KendoGridTagHelper tagHelper, TagHelperContext context, TagHelperOutput output, Expression<Func<TagHelperOutput, bool>> outputMatcher)
        {
            tagHelper.Process(context, output);

            output.Should().Match(outputMatcher);
        }
    }
}
