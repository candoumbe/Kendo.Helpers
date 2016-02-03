using FluentAssertions;
using Kendo.Helpers.Data;
using Microsoft.AspNet.Razor.TagHelpers;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using static Newtonsoft.Json.JsonConvert;
using static Newtonsoft.Json.Formatting;

namespace Kendo.Helpers.UI.Grid.Tests
{
    public class KendoGridTagHelperTests
    {

        private static readonly JSchema localDataSourceSchema = new JSchemaGenerator().Generate(typeof(KendoLocalDataSource));
        private readonly ITestOutputHelper _testOutputHelper;

        public static IEnumerable<object[]> KendoGridTagHelperCases
        {
            get
            {
                yield return new object[]
                {
                    new KendoGridTagHelper(),
                    new TagHelperContext(Enumerable.Empty<IReadOnlyTagHelperAttribute>(), new Dictionary<object, object>(), Guid.NewGuid().ToString("n")),
                    new TagHelperOutput("kendoGrid", new TagHelperAttributeList(),  outputTag => Task.FromResult((TagHelperContent)null)),
                    (Expression<Func<TagHelperOutput, bool>>) (outputTag => 
                        outputTag.TagName == "div" && 
                        outputTag.TagMode == TagMode.StartTagAndEndTag &&
                        outputTag.Attributes.Count() == 1 &&
                        outputTag.Attributes.ContainsName(KendoGridTagHelper.RoleAttributeName) &&
                        outputTag.Attributes[KendoGridTagHelper.RoleAttributeName].Value is string && 
                        ((string)outputTag.Attributes[KendoGridTagHelper.RoleAttributeName].Value) == "grid")
                };

                yield return new object[]
                {
                    new KendoGridTagHelper() {
                        DataSource = new KendoLocalDataSource<dynamic> {
                            Data = new dynamic[] {
                                new { firstname = "bruce", lastname="wayne", city="Gotham" },
                                new { firstname = "clark", lastname="kent", city="Metropolis" },
                            }
                        }
                    },
                    new TagHelperContext(Enumerable.Empty<IReadOnlyTagHelperAttribute>(), new Dictionary<object, object>(), Guid.NewGuid().ToString("n")),
                    new TagHelperOutput("kendoGrid", new TagHelperAttributeList(),  outputTag => Task.FromResult((TagHelperContent)null)),
                    (Expression<Func<TagHelperOutput, bool>>) (outputTag =>
                        outputTag.TagName == "div" &&
                        outputTag.TagMode == TagMode.StartTagAndEndTag &&
                        outputTag.Attributes.Count() == 2 &&
                        outputTag.Attributes.ContainsName(KendoGridTagHelper.RoleAttributeName) && outputTag.Attributes[KendoGridTagHelper.RoleAttributeName].Value is string && ((string)outputTag.Attributes[KendoGridTagHelper.RoleAttributeName].Value) == "grid" &&
                        outputTag.Attributes.ContainsName("data-source") && outputTag.Attributes["data-source"].Value is string &&
                            JObject.Parse((string)outputTag.Attributes["data-source"].Value).IsValid(localDataSourceSchema) &&
                            "bruce".Equals((string)JObject.Parse((string)outputTag.Attributes["data-source"].Value)[KendoLocalDataSource.DataPropertyName][0]["firstname"]) &&
                            "wayne".Equals((string)JObject.Parse((string)outputTag.Attributes["data-source"].Value)[KendoLocalDataSource.DataPropertyName][0]["lastname"]) &&
                            "Gotham".Equals((string)JObject.Parse((string)outputTag.Attributes["data-source"].Value)[KendoLocalDataSource.DataPropertyName][0]["city"]) &&
                            "clark".Equals((string)JObject.Parse((string)outputTag.Attributes["data-source"].Value)[KendoLocalDataSource.DataPropertyName][1]["firstname"]) &&
                            "kent".Equals((string)JObject.Parse((string)outputTag.Attributes["data-source"].Value)[KendoLocalDataSource.DataPropertyName][1]["lastname"]) &&
                            "Metropolis".Equals((string)JObject.Parse((string)outputTag.Attributes["data-source"].Value)[KendoLocalDataSource.DataPropertyName][1]["city"])
                    )
                };

                yield return new object[]
                {
                    new KendoGridTagHelper() {
                        DataSource = new KendoLocalDataSource<dynamic> {
                            Data = new dynamic[] {
                                new { firstname = "bruce", lastname="wayne", city="Gotham" },
                                new { firstname = "clark", lastname="kent", city="Metropolis" },
                            }
                        }
                    },
                    new TagHelperContext(new [] { new TagHelperAttribute("class", "row")}, new Dictionary<object, object>(), Guid.NewGuid().ToString("n")),
                    new TagHelperOutput("kendoGrid", new TagHelperAttributeList(),  outputTag => Task.FromResult((TagHelperContent)null)),
                    (Expression<Func<TagHelperOutput, bool>>) (outputTag =>
                        outputTag.TagName == "div" &&
                        outputTag.TagMode == TagMode.StartTagAndEndTag &&
                        outputTag.Attributes.Count() == 2 &&
                        outputTag.Attributes.ContainsName(KendoGridTagHelper.RoleAttributeName) && outputTag.Attributes[KendoGridTagHelper.RoleAttributeName].Value is string && ((string)outputTag.Attributes[KendoGridTagHelper.RoleAttributeName].Value) == "grid" &&
                        outputTag.Attributes.ContainsName("data-source") && outputTag.Attributes["data-source"].Value is string &&
                            JObject.Parse((string)outputTag.Attributes["data-source"].Value).IsValid(localDataSourceSchema) &&
                            "bruce".Equals((string)JObject.Parse((string)outputTag.Attributes["data-source"].Value)[KendoLocalDataSource.DataPropertyName][0]["firstname"]) &&
                            "wayne".Equals((string)JObject.Parse((string)outputTag.Attributes["data-source"].Value)[KendoLocalDataSource.DataPropertyName][0]["lastname"]) &&
                            "Gotham".Equals((string)JObject.Parse((string)outputTag.Attributes["data-source"].Value)[KendoLocalDataSource.DataPropertyName][0]["city"]) &&
                            "clark".Equals((string)JObject.Parse((string)outputTag.Attributes["data-source"].Value)[KendoLocalDataSource.DataPropertyName][1]["firstname"]) &&
                            "kent".Equals((string)JObject.Parse((string)outputTag.Attributes["data-source"].Value)[KendoLocalDataSource.DataPropertyName][1]["lastname"]) &&
                            "Metropolis".Equals((string)JObject.Parse((string)outputTag.Attributes["data-source"].Value)[KendoLocalDataSource.DataPropertyName][1]["city"])
                    )
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
                        outputTag.Attributes.ContainsName(KendoGridTagHelper.RoleAttributeName) && outputTag.Attributes[KendoGridTagHelper.RoleAttributeName].Value is string && ((string)outputTag.Attributes[KendoGridTagHelper.RoleAttributeName].Value) == "grid" &&
                        outputTag.Attributes.ContainsName("data-columns") && outputTag.Attributes["data-columns"].Value is string
                        &&
                            "Firstname".Equals((string)JArray.Parse((string)outputTag.Attributes["data-columns"].Value)[0][KendoGridFieldColumn.FieldPropertyName]) 
                            && "Lastname".Equals((string)JArray.Parse((string)outputTag.Attributes["data-columns"].Value)[1][KendoGridFieldColumn.FieldPropertyName])
                            && JArray.Parse((string)outputTag.Attributes["data-columns"].Value)[2] is JObject
                            && JArray.Parse((string)outputTag.Attributes["data-columns"].Value)[2].IsValid(KendoGridCommandColumn.Schema)
                            && JArray.Parse((string)outputTag.Attributes["data-columns"].Value)[2]["command"] is JArray
                            && "edit".Equals((string)JArray.Parse((string)outputTag.Attributes["data-columns"].Value)[2]["command"][0][KendoGridCommandColumn.NamePropertyName])
                    )
                };

                yield return new object[]
                {
                    new KendoGridTagHelper()
                    {
                        Columns = new KendoGridColumnBase[]
                        {
                            new KendoGridFieldColumn() { Field = "Firstname" },
                            new KendoGridTemplateColumn() { Template = "<button>Click me</button>" }
                        }
                    },
                    new TagHelperContext(Enumerable.Empty<IReadOnlyTagHelperAttribute>(), new Dictionary<object, object>(), Guid.NewGuid().ToString("n")),
                    new TagHelperOutput("kendoGrid", new TagHelperAttributeList(),  outputTag => Task.FromResult((TagHelperContent)null)),
                    (Expression<Func<TagHelperOutput, bool>>) (outputTag =>
                        outputTag.TagName == "div" &&
                        outputTag.TagMode == TagMode.StartTagAndEndTag &&
                        outputTag.Attributes.Count() == 2 &&
                        outputTag.Attributes.ContainsName(KendoGridTagHelper.RoleAttributeName) && outputTag.Attributes[KendoGridTagHelper.RoleAttributeName].Value is string && ((string)outputTag.Attributes[KendoGridTagHelper.RoleAttributeName].Value) == "grid" &&
                        outputTag.Attributes.ContainsName("data-columns") && outputTag.Attributes["data-columns"].Value is string
                        
                        && "Firstname".Equals((string)JArray.Parse((string)outputTag.Attributes["data-columns"].Value)[0][KendoGridFieldColumn.FieldPropertyName])
                        && "<button>Click me</button>".Equals((string)JArray.Parse((string)outputTag.Attributes["data-columns"].Value)[1][KendoGridTemplateColumn.TemplatePropertyName])


                    )
                };

                yield return new object[]
                {
                    new KendoGridTagHelper()
                    {
                        Columns = new KendoGridColumnBase[]
                        {
                            new KendoGridFieldColumn() { Field = "Firstname" },
                            new KendoGridTemplateColumn() { Template = "<button>Click me</button>" }
                        }
                    },
                    new TagHelperContext(Enumerable.Empty<IReadOnlyTagHelperAttribute>(), new Dictionary<object, object>(), Guid.NewGuid().ToString("n")),
                    new TagHelperOutput("kendoGrid", new TagHelperAttributeList(),  outputTag => Task.FromResult((TagHelperContent)null)),
                    (Expression<Func<TagHelperOutput, bool>>) (outputTag =>
                        outputTag.TagName == "div" &&
                        outputTag.TagMode == TagMode.StartTagAndEndTag &&
                        outputTag.Attributes.Count() == 2 &&
                        outputTag.Attributes.ContainsName(KendoGridTagHelper.RoleAttributeName) && outputTag.Attributes[KendoGridTagHelper.RoleAttributeName].Value is string && ((string)outputTag.Attributes[KendoGridTagHelper.RoleAttributeName].Value) == "grid" &&
                        outputTag.Attributes.ContainsName("data-columns") && outputTag.Attributes["data-columns"].Value is string

                        && "Firstname".Equals((string)JArray.Parse((string)outputTag.Attributes["data-columns"].Value)[0][KendoGridFieldColumn.FieldPropertyName])
                        && "<button>Click me</button>".Equals((string)JArray.Parse((string)outputTag.Attributes["data-columns"].Value)[1][KendoGridTemplateColumn.TemplatePropertyName])


                    )
                };


                yield return new object[]
                {
                    new KendoGridTagHelper()
                    {
                        Pageable = new PageableConfiguration
                        {
                            ButtonCount = 10
                        }
                    },
                    new TagHelperContext(Enumerable.Empty<IReadOnlyTagHelperAttribute>(), new Dictionary<object, object>(), Guid.NewGuid().ToString("n")),
                    new TagHelperOutput("kendoGrid", new TagHelperAttributeList(),  outputTag => Task.FromResult((TagHelperContent)null)),
                    (Expression<Func<TagHelperOutput, bool>>) (outputTag =>
                        outputTag.TagName == "div" &&
                        outputTag.TagMode == TagMode.StartTagAndEndTag &&
                        outputTag.Attributes.Count() == 2 &&
                        outputTag.Attributes.ContainsName(KendoGridTagHelper.RoleAttributeName) && outputTag.Attributes[KendoGridTagHelper.RoleAttributeName].Value is string && ((string)outputTag.Attributes[KendoGridTagHelper.RoleAttributeName].Value) == "grid" &&
                        outputTag.Attributes.ContainsName("data-pageable") && outputTag.Attributes["data-pageable"].Value is string &&
                            ((int)(JObject.Parse((string)outputTag.Attributes["data-pageable"].Value)[PageableConfiguration.ButtonCountPropertyName]) == 10
                        ))
                };
            }
        }

        public KendoGridTagHelperTests(ITestOutputHelper outputHelper)
        {
            _testOutputHelper = outputHelper;
        }

        [Theory]
        [MemberData(nameof(KendoGridTagHelperCases))]
        public void Process(KendoGridTagHelper tagHelper, TagHelperContext context, TagHelperOutput output, Expression<Func<TagHelperOutput, bool>> outputMatcher)
        {
            _testOutputHelper.WriteLine(SerializeObject(tagHelper, Indented));

            tagHelper.Process(context, output);
            output.Should().Match(outputMatcher);
        }
    }
}
