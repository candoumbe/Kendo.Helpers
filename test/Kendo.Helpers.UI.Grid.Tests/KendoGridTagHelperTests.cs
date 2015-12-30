using FluentAssertions;
using Kendo.Helpers.Data;
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
                //Empty kendoGrid with no
                yield return new object[]
                {
                    new KendoGridTagHelper(),
                    new TagHelperContext(Enumerable.Empty<IReadOnlyTagHelperAttribute>(), new Dictionary<object, object>(), Guid.NewGuid().ToString("n")),
                    new TagHelperOutput("kendoGrid", new TagHelperAttributeList(),  outputTag => Task.FromResult((TagHelperContent)null)),
                    (Expression<Func<TagHelperOutput, bool>>) (outputTag => 
                        outputTag.TagName == "div" && 
                        outputTag.TagMode == TagMode.StartTagAndEndTag &&
                        outputTag.Attributes.Count() == 1 &&
                        outputTag.Attributes.ContainsName("data-role") && outputTag.Attributes["data-role"].Value is string && ((string)outputTag.Attributes["data-role"].Value) == "grid")
                };

                yield return new object[]
                {
                    new KendoGridTagHelper() {
                        DataSource = new KendoLocalDataSource(new dynamic[] {
                            new { firstname = "bruce", lastname="wayne", city="Gotham" },
                            new { firstname = "clark", lastname="kent", city="Metropolis" },
                        })

                    },
                    new TagHelperContext(Enumerable.Empty<IReadOnlyTagHelperAttribute>(), new Dictionary<object, object>(), Guid.NewGuid().ToString("n")),
                    new TagHelperOutput("kendoGrid", new TagHelperAttributeList(),  outputTag => Task.FromResult((TagHelperContent)null)),
                    (Expression<Func<TagHelperOutput, bool>>) (outputTag =>
                        outputTag.TagName == "div" &&
                        outputTag.TagMode == TagMode.StartTagAndEndTag &&
                        outputTag.Attributes.Count() == 2 &&
                        outputTag.Attributes.ContainsName("data-role") && outputTag.Attributes["data-role"].Value is string && ((string)outputTag.Attributes["data-role"].Value) == "grid" &&
                        outputTag.Attributes.ContainsName("data-source") && outputTag.Attributes["data-source"].Value is string && ((string)outputTag.Attributes["data-source"].Value == @"[{""firstname"":""bruce"",""lastname"":""wayne"",""city"":""Gotham""},{""firstname"":""clark"",""lastname"":""kent"",""city"":""Metropolis""}]"))
                };

                yield return new object[]
                {
                    new KendoGridTagHelper() {
                        DataSource = new KendoLocalDataSource(new dynamic[] {
                            new { firstname = "bruce", lastname="wayne", city="Gotham" },
                            new { firstname = "clark", lastname="kent", city="Metropolis" },
                        })

                    },
                    new TagHelperContext(new [] { new TagHelperAttribute("class", "row")}, new Dictionary<object, object>(), Guid.NewGuid().ToString("n")),
                    new TagHelperOutput("kendoGrid", new TagHelperAttributeList(),  outputTag => Task.FromResult((TagHelperContent)null)),
                    (Expression<Func<TagHelperOutput, bool>>) (outputTag =>
                        outputTag.TagName == "div" &&
                        outputTag.TagMode == TagMode.StartTagAndEndTag &&
                        outputTag.Attributes.Count() == 2 &&
                        outputTag.Attributes.ContainsName("data-role") && outputTag.Attributes["data-role"].Value is string && ((string)outputTag.Attributes["data-role"].Value) == "grid" &&
                        outputTag.Attributes.ContainsName("data-source") && outputTag.Attributes["data-source"].Value is string && ((string)outputTag.Attributes["data-source"].Value == @"[{""firstname"":""bruce"",""lastname"":""wayne"",""city"":""Gotham""},{""firstname"":""clark"",""lastname"":""kent"",""city"":""Metropolis""}]"))
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
                    new TagHelperOutput("kendoGrid", new TagHelperAttributeList(),  outputTag => Task.FromResult((TagHelperContent)null)),
                    (Expression<Func<TagHelperOutput, bool>>) (outputTag => 
                        outputTag.TagName == "div" &&
                        outputTag.TagMode == TagMode.StartTagAndEndTag &&
                        outputTag.Attributes.Count() == 2 &&
                        outputTag.Attributes.ContainsName("data-role") && outputTag.Attributes["data-role"].Value is string && ((string)outputTag.Attributes["data-role"].Value) == "grid" &&
                        outputTag.Attributes.ContainsName("data-columns") && outputTag.Attributes["data-columns"].Value is string && ((string)outputTag.Attributes["data-columns"].Value) == @"""columns"":[{""field"":""Firstname""},{""field"":""Lastname""},{""command"":[{""name"":""edit""}]}]")

                };

                yield return new object[]
                {
                    new KendoGridTagHelper()
                    {
                        Columns = new KendoGridColumnBase[]
                        {
                            new KendoGridFieldColumn() { Field = "Firstname" }
                        }
                    },
                    new TagHelperContext(Enumerable.Empty<IReadOnlyTagHelperAttribute>(), new Dictionary<object, object>(), Guid.NewGuid().ToString("n")),
                    new TagHelperOutput("kendoGrid", new TagHelperAttributeList(),  outputTag => Task.FromResult((TagHelperContent)null)),
                    (Expression<Func<TagHelperOutput, bool>>) (outputTag => 
                        outputTag.TagName == "div" &&
                        outputTag.Attributes.Count() == 2 &&
                        outputTag.TagMode == TagMode.StartTagAndEndTag &&
                        outputTag.Attributes.ContainsName("data-role") && outputTag.Attributes["data-role"].Value is string && ((string)outputTag.Attributes["data-role"].Value) == "grid" &&
                        outputTag.Attributes.ContainsName("data-columns") && outputTag.Attributes["data-columns"].Value is string && ((string)outputTag.Attributes["data-columns"].Value) == @"""columns"":[{""field"":""Firstname""}]")

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
                    new TagHelperOutput("kendoGrid", new TagHelperAttributeList(),  outputTag => Task.FromResult((TagHelperContent)null)),
                    (Expression<Func<TagHelperOutput, bool>>) (outputTag =>
                        outputTag.TagName == "div" &&
                        outputTag.TagMode == TagMode.StartTagAndEndTag &&
                        outputTag.Attributes.Count() == 2 &&
                        outputTag.Attributes.ContainsName("data-role") && outputTag.Attributes["data-role"].Value is string && ((string)outputTag.Attributes["data-role"].Value) == "grid" &&
                        outputTag.Attributes.ContainsName("data-columns") && outputTag.Attributes["data-columns"].Value is string && ((string)outputTag.Attributes["data-columns"].Value) == @"""columns"":[{""field"":""Firstname""},{""field"":""Lastname""},{""command"":[{""name"":""edit""},{""name"":""destroy""}]}]")

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
