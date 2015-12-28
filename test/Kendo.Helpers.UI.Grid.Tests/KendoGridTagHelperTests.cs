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
                    (Expression<Func<TagHelperOutput, bool>>) (input => input.TagName == "div" && !input.Attributes.Any()),
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
                        input.Attributes.Count() == 1 &&
                        input.Attributes.First().Name == "data-columns" &&
                        input.Attributes.First().Value is string &&
                        input.Attributes.First().Value.ToString() == @"""columns"":[{""field"":""Firstname""},{""field"":""Lastname""},{""command"":[{""name"":""edit""}]}]")

                };


                yield return new object[]
                {
                    new KendoGridTagHelper()
                    {
                        Columns = new KendoGridColumnBase[]
                        {
                            new KendoGridFieldColumn() { Field = "Firstname", Editable = false },
                            new KendoGridFieldColumn() { Field = "Lastname" },
                            new KendoGridCommandColumn() { Name = "edit" }
                        }
                    },
                    new TagHelperContext(Enumerable.Empty<IReadOnlyTagHelperAttribute>(), new Dictionary<object, object>(), Guid.NewGuid().ToString("n")),
                    new TagHelperOutput("kendoGrid", new TagHelperAttributeList(),  input => Task.FromResult((TagHelperContent)null)),
                    (Expression<Func<TagHelperOutput, bool>>) (input => input.TagName == "div" &&
                        input.Attributes.Count() == 1 &&
                        input.Attributes.First().Name == "data-columns" &&
                        input.Attributes.First().Value is string &&
                        input.Attributes.First().Value.ToString() == @"""columns"":[{""field"":""Firstname""},{""field"":""Lastname""},{""command"":[{""name"":""edit""}]}]")

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
