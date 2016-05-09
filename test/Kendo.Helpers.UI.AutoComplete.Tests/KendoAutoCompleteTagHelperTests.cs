using Kendo.Helpers.Data;
using Microsoft.AspNet.Razor.TagHelpers;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;
using Newtonsoft.Json.Schema;
using Xunit.Abstractions;
using static Newtonsoft.Json.JsonConvert;
using static Newtonsoft.Json.Formatting;
using FluentAssertions;

namespace Kendo.Helpers.UI.AutoComplete.Tests
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    public class KendoAutoCompleteTagHelperTests
    {
        private readonly ITestOutputHelper _output;


        public static IEnumerable<object[]> ProcessCases
        {
            get
            {
                yield return new object[]
                {
                    new KendoAutoCompleteTagHelper() {
                        DataSource = new KendoLocalDataSource<dynamic> {
                            Data = new dynamic[] {
                                new { firstname = "bruce", lastname="wayne", city="Gotham" },
                                new { firstname = "clark", lastname="kent", city="Metropolis" },
                            }
                        }
                    },
                    new TagHelperContext(Enumerable.Empty<IReadOnlyTagHelperAttribute>(), new Dictionary<object, object>(), Guid.NewGuid().ToString("n")),
                    new TagHelperOutput("kendoAutoComplete", new TagHelperAttributeList(),  outputTag => Task.FromResult((TagHelperContent)null)),
                    (Expression<Func<TagHelperOutput, bool>>) (outputTag =>
                        outputTag.TagName == "input" &&
                        outputTag.TagMode == TagMode.SelfClosing &&
                        outputTag.Attributes.Count() == 3 
                        
                        && outputTag.Attributes.ContainsName(KendoAutoCompleteTagHelper.RoleAttributeName) 
                        && outputTag.Attributes[KendoAutoCompleteTagHelper.RoleAttributeName].Value is string
                        && ((string)outputTag.Attributes[KendoAutoCompleteTagHelper.RoleAttributeName].Value) == KendoAutoCompleteTagHelper.WidgetName

                        && outputTag.Attributes.ContainsName(KendoAutoCompleteTagHelper.TypeAttributeName)
                        && outputTag.Attributes[KendoAutoCompleteTagHelper.TypeAttributeName].Value is string
                        && "text".Equals(outputTag.Attributes[KendoAutoCompleteTagHelper.TypeAttributeName].Value)
                        
                        && outputTag.Attributes.ContainsName("data-source") && outputTag.Attributes["data-source"].Value is string &&
                            JObject.Parse((string)outputTag.Attributes["data-source"].Value).IsValid(KendoLocalDataSource.Schema) &&
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
                    new KendoAutoCompleteTagHelper() {
                        DataSource = new KendoRemoteDataSource
                        {
                            Transport = new KendoTransport
                            {
                                Read = new KendoTransportOperation
                                {
                                    Cache = false,
                                    Url = "api/cities/",
                                    Type = "GET"
                                }
                            }
                        }
                    },
                    new TagHelperContext(Enumerable.Empty<IReadOnlyTagHelperAttribute>(), new Dictionary<object, object>(), Guid.NewGuid().ToString("n")),
                    new TagHelperOutput("kendoAutoComplete", new TagHelperAttributeList(),  outputTag => Task.FromResult((TagHelperContent)null)),
                    (Expression<Func<TagHelperOutput, bool>>) (outputTag =>
                        outputTag.TagName == "input" &&
                        outputTag.TagMode == TagMode.SelfClosing &&
                        outputTag.Attributes.Count() == 3
                        
                        && outputTag.Attributes.ContainsName(KendoAutoCompleteTagHelper.TypeAttributeName) 
                        && outputTag.Attributes[KendoAutoCompleteTagHelper.TypeAttributeName].Value is string 
                        && "text".Equals(outputTag.Attributes[KendoAutoCompleteTagHelper.TypeAttributeName].Value)
                       
                        && ((string)outputTag.Attributes[KendoAutoCompleteTagHelper.RoleAttributeName].Value) == KendoAutoCompleteTagHelper.WidgetName 
                        
                        && outputTag.Attributes.ContainsName("data-source") && outputTag.Attributes["data-source"].Value is string &&
                            JObject.Parse((string)outputTag.Attributes["data-source"].Value).IsValid(KendoRemoteDataSource.Schema)

                    )
                };

                yield return new object[]
                {
                    new KendoAutoCompleteTagHelper() {
                        DataSource = new KendoRemoteDataSource
                        {
                            Transport = new KendoTransport
                            {
                                Read = new KendoTransportOperation
                                {
                                    Cache = false,
                                    Url = "api/cities/",
                                    Type = "GET"
                                }
                            }
                        },
                        IgnoreCase = null,
                    },
                    new TagHelperContext(Enumerable.Empty<IReadOnlyTagHelperAttribute>(), new Dictionary<object, object>(), Guid.NewGuid().ToString("n")),
                    new TagHelperOutput("kendoAutoComplete", new TagHelperAttributeList(),  outputTag => Task.FromResult((TagHelperContent)null)),
                    (Expression<Func<TagHelperOutput, bool>>) (outputTag =>
                        outputTag.TagName == "input" &&
                        outputTag.TagMode == TagMode.SelfClosing &&
                        outputTag.Attributes.Count() == 3

                        && outputTag.Attributes.ContainsName(KendoAutoCompleteTagHelper.TypeAttributeName)
                        && outputTag.Attributes[KendoAutoCompleteTagHelper.TypeAttributeName].Value is string
                        && "text".Equals(outputTag.Attributes[KendoAutoCompleteTagHelper.TypeAttributeName].Value)

                        && ((string)outputTag.Attributes[KendoAutoCompleteTagHelper.RoleAttributeName].Value) == KendoAutoCompleteTagHelper.WidgetName

                        && outputTag.Attributes.ContainsName("data-source") && outputTag.Attributes["data-source"].Value is string &&
                            JObject.Parse((string)outputTag.Attributes["data-source"].Value).IsValid(KendoRemoteDataSource.Schema)

                    )
                };

                yield return new object[]
                {
                    new KendoAutoCompleteTagHelper() {
                        DataSource = new KendoRemoteDataSource
                        {
                            Transport = new KendoTransport
                            {
                                Read = new KendoTransportOperation
                                {
                                    Cache = false,
                                    Url = "api/cities/",
                                    Type = "GET"
                                }
                            }
                        },
                        IgnoreCase = false,
                    },
                    new TagHelperContext(Enumerable.Empty<IReadOnlyTagHelperAttribute>(), new Dictionary<object, object>(), Guid.NewGuid().ToString("n")),
                    new TagHelperOutput("kendoAutoComplete", new TagHelperAttributeList(),  outputTag => Task.FromResult((TagHelperContent)null)),
                    (Expression<Func<TagHelperOutput, bool>>) (outputTag =>
                        outputTag.TagName == "input" &&
                        outputTag.TagMode == TagMode.SelfClosing &&
                        outputTag.Attributes.Count() == 4

                        && outputTag.Attributes.ContainsName(KendoAutoCompleteTagHelper.TypeAttributeName)
                        && outputTag.Attributes[KendoAutoCompleteTagHelper.TypeAttributeName].Value is string
                        && "text".Equals(outputTag.Attributes[KendoAutoCompleteTagHelper.TypeAttributeName].Value)

                        && ((string)outputTag.Attributes[KendoAutoCompleteTagHelper.RoleAttributeName].Value) == KendoAutoCompleteTagHelper.WidgetName

                        && outputTag.Attributes.ContainsName("data-source") && outputTag.Attributes["data-source"].Value is string &&
                            JObject.Parse((string)outputTag.Attributes["data-source"].Value).IsValid(KendoRemoteDataSource.Schema)

                        && outputTag.Attributes.ContainsName(KendoAutoCompleteTagHelper.IgnoreCaseAttributeName) 
                            && outputTag.Attributes[KendoAutoCompleteTagHelper.IgnoreCaseAttributeName].Value is bool
                            && !(bool)outputTag.Attributes[KendoAutoCompleteTagHelper.IgnoreCaseAttributeName].Value

                    )
                };

                yield return new object[]
                {
                    new KendoAutoCompleteTagHelper() {
                        DataSource = new KendoRemoteDataSource
                        {
                            Transport = new KendoTransport
                            {
                                Read = new KendoTransportOperation
                                {
                                    Cache = false,
                                    Url = "api/cities/",
                                    Type = "GET"
                                }
                            }
                        },
                        IgnoreCase = true,
                    },
                    new TagHelperContext(Enumerable.Empty<IReadOnlyTagHelperAttribute>(), new Dictionary<object, object>(), Guid.NewGuid().ToString("n")),
                    new TagHelperOutput("kendoAutoComplete", new TagHelperAttributeList(),  outputTag => Task.FromResult((TagHelperContent)null)),
                    (Expression<Func<TagHelperOutput, bool>>) (outputTag =>
                        outputTag.TagName == "input" &&
                        outputTag.TagMode == TagMode.SelfClosing &&
                        outputTag.Attributes.Count() == 4

                        && outputTag.Attributes.ContainsName(KendoAutoCompleteTagHelper.TypeAttributeName)
                        && outputTag.Attributes[KendoAutoCompleteTagHelper.TypeAttributeName].Value is string
                        && "text".Equals(outputTag.Attributes[KendoAutoCompleteTagHelper.TypeAttributeName].Value)

                        && ((string)outputTag.Attributes[KendoAutoCompleteTagHelper.RoleAttributeName].Value) == KendoAutoCompleteTagHelper.WidgetName

                        && outputTag.Attributes.ContainsName("data-source") && outputTag.Attributes["data-source"].Value is string &&
                            JObject.Parse((string)outputTag.Attributes["data-source"].Value).IsValid(KendoRemoteDataSource.Schema)

                        && outputTag.Attributes.ContainsName(KendoAutoCompleteTagHelper.IgnoreCaseAttributeName)
                            && outputTag.Attributes[KendoAutoCompleteTagHelper.IgnoreCaseAttributeName].Value is bool
                            && (bool)outputTag.Attributes[KendoAutoCompleteTagHelper.IgnoreCaseAttributeName].Value

                    )
                };

                yield return new object[]
                {
                    new KendoAutoCompleteTagHelper() {
                        DataSource = new KendoRemoteDataSource
                        {
                            Transport = new KendoTransport
                            {
                                Read = new KendoTransportOperation
                                {
                                    Cache = false,
                                    Url = "api/cities/",
                                    Type = "GET"
                                }
                            }
                        },
                        MinLength = 3,
                    },
                    new TagHelperContext(Enumerable.Empty<IReadOnlyTagHelperAttribute>(), new Dictionary<object, object>(), Guid.NewGuid().ToString("n")),
                    new TagHelperOutput("kendoAutoComplete", new TagHelperAttributeList(),  outputTag => Task.FromResult((TagHelperContent)null)),
                    (Expression<Func<TagHelperOutput, bool>>) (outputTag =>
                        outputTag.TagName == "input" &&
                        outputTag.TagMode == TagMode.SelfClosing &&
                        outputTag.Attributes.Count() == 4

                        && outputTag.Attributes.ContainsName(KendoAutoCompleteTagHelper.TypeAttributeName)
                        && outputTag.Attributes[KendoAutoCompleteTagHelper.TypeAttributeName].Value is string
                        && "text".Equals(outputTag.Attributes[KendoAutoCompleteTagHelper.TypeAttributeName].Value)

                        && ((string)outputTag.Attributes[KendoAutoCompleteTagHelper.RoleAttributeName].Value) == KendoAutoCompleteTagHelper.WidgetName

                        && outputTag.Attributes.ContainsName("data-source") && outputTag.Attributes["data-source"].Value is string &&
                            JObject.Parse((string)outputTag.Attributes["data-source"].Value).IsValid(KendoRemoteDataSource.Schema)

                        && outputTag.Attributes.ContainsName(KendoAutoCompleteTagHelper.MinLengthAttributeName)
                            && outputTag.Attributes[KendoAutoCompleteTagHelper.MinLengthAttributeName].Value is uint
                            && 3 == (uint)outputTag.Attributes[KendoAutoCompleteTagHelper.MinLengthAttributeName].Value

                    )
                };


                yield return new object[]
                {
                    new KendoAutoCompleteTagHelper() {
                        DataSource = new KendoRemoteDataSource
                        {
                            Transport = new KendoTransport
                            {
                                Read = new KendoTransportOperation
                                {
                                    Cache = false,
                                    Url = "api/cities/",
                                    Type = "GET"
                                }
                            }
                        },
                        Placeholder = "",
                    },
                    new TagHelperContext(Enumerable.Empty<IReadOnlyTagHelperAttribute>(), new Dictionary<object, object>(), Guid.NewGuid().ToString("n")),
                    new TagHelperOutput("kendoAutoComplete", new TagHelperAttributeList(),  outputTag => Task.FromResult((TagHelperContent)null)),
                    (Expression<Func<TagHelperOutput, bool>>) (outputTag =>
                        outputTag.TagName == "input" &&
                        outputTag.TagMode == TagMode.SelfClosing &&
                        outputTag.Attributes.Count() == 3

                        && outputTag.Attributes.ContainsName(KendoAutoCompleteTagHelper.TypeAttributeName)
                        && outputTag.Attributes[KendoAutoCompleteTagHelper.TypeAttributeName].Value is string
                        && "text".Equals(outputTag.Attributes[KendoAutoCompleteTagHelper.TypeAttributeName].Value)

                        && ((string)outputTag.Attributes[KendoAutoCompleteTagHelper.RoleAttributeName].Value) == KendoAutoCompleteTagHelper.WidgetName

                        && outputTag.Attributes.ContainsName("data-source") && outputTag.Attributes["data-source"].Value is string &&
                            JObject.Parse((string)outputTag.Attributes["data-source"].Value).IsValid(KendoRemoteDataSource.Schema)
                    )
                };

                yield return new object[]
                {
                    new KendoAutoCompleteTagHelper() {
                        DataSource = new KendoRemoteDataSource
                        {
                            Transport = new KendoTransport
                            {
                                Read = new KendoTransportOperation
                                {
                                    Cache = false,
                                    Url = "api/cities/",
                                    Type = "GET"
                                }
                            }
                        },
                        Placeholder = null,
                    },
                    new TagHelperContext(Enumerable.Empty<IReadOnlyTagHelperAttribute>(), new Dictionary<object, object>(), Guid.NewGuid().ToString("n")),
                    new TagHelperOutput("kendoAutoComplete", new TagHelperAttributeList(),  outputTag => Task.FromResult((TagHelperContent)null)),
                    (Expression<Func<TagHelperOutput, bool>>) (outputTag =>
                        outputTag.TagName == "input" &&
                        outputTag.TagMode == TagMode.SelfClosing &&
                        outputTag.Attributes.Count() == 3

                        && outputTag.Attributes.ContainsName(KendoAutoCompleteTagHelper.TypeAttributeName)
                        && outputTag.Attributes[KendoAutoCompleteTagHelper.TypeAttributeName].Value is string
                        && "text".Equals(outputTag.Attributes[KendoAutoCompleteTagHelper.TypeAttributeName].Value)

                        && ((string)outputTag.Attributes[KendoAutoCompleteTagHelper.RoleAttributeName].Value) == KendoAutoCompleteTagHelper.WidgetName

                        && outputTag.Attributes.ContainsName("data-source") && outputTag.Attributes["data-source"].Value is string &&
                            JObject.Parse((string)outputTag.Attributes["data-source"].Value).IsValid(KendoRemoteDataSource.Schema)
                    )
                };


                yield return new object[]
                {
                    new KendoAutoCompleteTagHelper() {
                        DataSource = new KendoRemoteDataSource
                        {
                            Transport = new KendoTransport
                            {
                                Read = new KendoTransportOperation
                                {
                                    Cache = false,
                                    Url = "api/cities/",
                                    Type = "GET"
                                }
                            }
                        },
                        Placeholder = "  ",
                    },
                    new TagHelperContext(Enumerable.Empty<IReadOnlyTagHelperAttribute>(), new Dictionary<object, object>(), Guid.NewGuid().ToString("n")),
                    new TagHelperOutput("kendoAutoComplete", new TagHelperAttributeList(),  outputTag => Task.FromResult((TagHelperContent)null)),
                    (Expression<Func<TagHelperOutput, bool>>) (outputTag =>
                        outputTag.TagName == "input" &&
                        outputTag.TagMode == TagMode.SelfClosing &&
                        outputTag.Attributes.Count() == 3

                        && outputTag.Attributes.ContainsName(KendoAutoCompleteTagHelper.TypeAttributeName)
                        && outputTag.Attributes[KendoAutoCompleteTagHelper.TypeAttributeName].Value is string
                        && "text".Equals(outputTag.Attributes[KendoAutoCompleteTagHelper.TypeAttributeName].Value)

                        && ((string)outputTag.Attributes[KendoAutoCompleteTagHelper.RoleAttributeName].Value) == KendoAutoCompleteTagHelper.WidgetName

                        && outputTag.Attributes.ContainsName("data-source") && outputTag.Attributes["data-source"].Value is string &&
                            JObject.Parse((string)outputTag.Attributes["data-source"].Value).IsValid(KendoRemoteDataSource.Schema)
                    )
                };

                yield return new object[]
                {
                    new KendoAutoCompleteTagHelper() {
                        DataSource = new KendoRemoteDataSource
                        {
                            Transport = new KendoTransport
                            {
                                Read = new KendoTransportOperation
                                {
                                    Cache = false,
                                    Url = "api/cities/",
                                    Type = "GET"
                                }
                            }
                        },
                        Placeholder = "Enter a city name",
                    },
                    new TagHelperContext(Enumerable.Empty<IReadOnlyTagHelperAttribute>(), new Dictionary<object, object>(), Guid.NewGuid().ToString("n")),
                    new TagHelperOutput("kendoAutoComplete", new TagHelperAttributeList(),  outputTag => Task.FromResult((TagHelperContent)null)),
                    (Expression<Func<TagHelperOutput, bool>>) (outputTag =>
                        outputTag.TagName == "input" &&
                        outputTag.TagMode == TagMode.SelfClosing &&
                        outputTag.Attributes.Count() == 4

                        && outputTag.Attributes.ContainsName(KendoAutoCompleteTagHelper.TypeAttributeName)
                        && outputTag.Attributes[KendoAutoCompleteTagHelper.TypeAttributeName].Value is string
                        && "text".Equals(outputTag.Attributes[KendoAutoCompleteTagHelper.TypeAttributeName].Value)

                        && ((string)outputTag.Attributes[KendoAutoCompleteTagHelper.RoleAttributeName].Value) == KendoAutoCompleteTagHelper.WidgetName

                        && outputTag.Attributes.ContainsName("data-source") && outputTag.Attributes["data-source"].Value is string &&
                            JObject.Parse((string)outputTag.Attributes["data-source"].Value).IsValid(KendoRemoteDataSource.Schema)

                        && outputTag.Attributes.ContainsName(KendoAutoCompleteTagHelper.PlaceholderAttributeName)
                            && outputTag.Attributes[KendoAutoCompleteTagHelper.PlaceholderAttributeName].Value is string
                            && "Enter a city name".Equals(outputTag.Attributes[KendoAutoCompleteTagHelper.PlaceholderAttributeName].Value)

                    )
                };

            }
        }

        public KendoAutoCompleteTagHelperTests(ITestOutputHelper output)
        {
            _output = output;

        }


        [Theory]
        [MemberData(nameof(ProcessCases))]
        public void Process(KendoAutoCompleteTagHelper tagHelper, TagHelperContext context, TagHelperOutput output, Expression<Func<TagHelperOutput, bool>> outputMatcher)
        {
            _output.WriteLine(SerializeObject(tagHelper, Indented));

            tagHelper.Process(context, output);
            output.Should().Match(outputMatcher);
        }
    }
}
