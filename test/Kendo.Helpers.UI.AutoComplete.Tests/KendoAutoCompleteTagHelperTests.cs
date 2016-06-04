using Kendo.Helpers.Data;
using Microsoft.AspNetCore.Razor.TagHelpers;
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
using static Kendo.Helpers.UI.Autocomplete.AutocompleteFilterType;

namespace Kendo.Helpers.UI.Autocomplete.Tests
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    public class KendoAutocompleteTagHelperTests : IDisposable
    {
        private ITestOutputHelper _output;


        public class Person
        {
            public string Firstname { get; set; }

            public string Lastname { get; set; }

            public DateTime BirthDate { get; set; }

        }

        public class SuperHero : Person
        {
            public string Nickname { get; set; }

            public int Height { get; set; }

            public Henchman Henchman { get; set; }
        }

        public class Henchman : SuperHero
        {

        }


        public static IEnumerable<object[]> ProcessCases
        {
            get
            {
                yield return new object[]
                {
                    new KendoAutocompleteTagHelper() {
                        DataSource = new KendoLocalDataSource<dynamic> {
                            Data = new dynamic[] {
                                new { firstname = "bruce", lastname="wayne", city="Gotham" },
                                new { firstname = "clark", lastname="kent", city="Metropolis" },
                            }
                        }
                    },
                    new TagHelperContext(new TagHelperAttributeList(), new Dictionary<object, object>(), Guid.NewGuid().ToString("n")),
                    new TagHelperOutput("kendo-autocomplete", new TagHelperAttributeList(),  (param1, htmlEncoder) => Task.FromResult((TagHelperContent) null)),
                    (Expression<Func<TagHelperOutput, bool>>) (outputTag =>
                        outputTag.TagName == "input" &&
                        outputTag.TagMode == TagMode.SelfClosing &&
                        outputTag.Attributes.Count() == 3 
                        
                        && outputTag.Attributes.ContainsName(KendoAutocompleteTagHelper.RoleAttributeName) 
                        && outputTag.Attributes[KendoAutocompleteTagHelper.RoleAttributeName].Value is string
                        && ((string)outputTag.Attributes[KendoAutocompleteTagHelper.RoleAttributeName].Value) == KendoAutocompleteTagHelper.WidgetName

                        && outputTag.Attributes.ContainsName(KendoAutocompleteTagHelper.TypeAttributeName)
                        && outputTag.Attributes[KendoAutocompleteTagHelper.TypeAttributeName].Value is string
                        && "text".Equals(outputTag.Attributes[KendoAutocompleteTagHelper.TypeAttributeName].Value)
                        
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
                    new KendoAutocompleteTagHelper() {
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
                    new TagHelperContext(new TagHelperAttributeList(), new Dictionary<object, object>(), Guid.NewGuid().ToString("n")),
                    new TagHelperOutput("kendo-autocomplete", new TagHelperAttributeList(),  (param1, param2) => Task.FromResult((TagHelperContent)null)),
                    (Expression<Func<TagHelperOutput, bool>>) (outputTag =>
                        outputTag.TagName == "input" &&
                        outputTag.TagMode == TagMode.SelfClosing &&
                        outputTag.Attributes.Count() == 3
                        
                        && outputTag.Attributes.ContainsName(KendoAutocompleteTagHelper.TypeAttributeName) 
                        && outputTag.Attributes[KendoAutocompleteTagHelper.TypeAttributeName].Value is string 
                        && "text".Equals(outputTag.Attributes[KendoAutocompleteTagHelper.TypeAttributeName].Value)
                       
                        && ((string)outputTag.Attributes[KendoAutocompleteTagHelper.RoleAttributeName].Value) == KendoAutocompleteTagHelper.WidgetName 
                        
                        && outputTag.Attributes.ContainsName("data-source") && outputTag.Attributes["data-source"].Value is string &&
                            JObject.Parse((string)outputTag.Attributes["data-source"].Value).IsValid(KendoRemoteDataSource.Schema)

                    )
                };

                yield return new object[]
                {
                    new KendoAutocompleteTagHelper() {
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
                    new TagHelperContext(new TagHelperAttributeList(), new Dictionary<object, object>(), Guid.NewGuid().ToString("n")),
                    new TagHelperOutput("kendo-autocomplete", new TagHelperAttributeList(),  (param1, param2) => Task.FromResult((TagHelperContent)null)),
                    (Expression<Func<TagHelperOutput, bool>>) (outputTag =>
                        outputTag.TagName == "input" &&
                        outputTag.TagMode == TagMode.SelfClosing &&
                        outputTag.Attributes.Count() == 3

                        && outputTag.Attributes.ContainsName(KendoAutocompleteTagHelper.TypeAttributeName)
                        && outputTag.Attributes[KendoAutocompleteTagHelper.TypeAttributeName].Value is string
                        && "text".Equals(outputTag.Attributes[KendoAutocompleteTagHelper.TypeAttributeName].Value)

                        && ((string)outputTag.Attributes[KendoAutocompleteTagHelper.RoleAttributeName].Value) == KendoAutocompleteTagHelper.WidgetName

                        && outputTag.Attributes.ContainsName("data-source") && outputTag.Attributes["data-source"].Value is string &&
                            JObject.Parse((string)outputTag.Attributes["data-source"].Value).IsValid(KendoRemoteDataSource.Schema)

                    )
                };

                yield return new object[]
                {
                    new KendoAutocompleteTagHelper() {
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
                    new TagHelperContext(new TagHelperAttributeList(), new Dictionary<object, object>(), Guid.NewGuid().ToString("n")),
                    new TagHelperOutput("kendo-autocomplete", new TagHelperAttributeList(),  (param1, param2) => Task.FromResult((TagHelperContent)null)),
                    (Expression<Func<TagHelperOutput, bool>>) (outputTag =>
                        outputTag.TagName == "input" &&
                        outputTag.TagMode == TagMode.SelfClosing &&
                        outputTag.Attributes.Count() == 4

                        && outputTag.Attributes.ContainsName(KendoAutocompleteTagHelper.TypeAttributeName)
                        && outputTag.Attributes[KendoAutocompleteTagHelper.TypeAttributeName].Value is string
                        && "text".Equals(outputTag.Attributes[KendoAutocompleteTagHelper.TypeAttributeName].Value)

                        && ((string)outputTag.Attributes[KendoAutocompleteTagHelper.RoleAttributeName].Value) == KendoAutocompleteTagHelper.WidgetName

                        && outputTag.Attributes.ContainsName("data-source") && outputTag.Attributes["data-source"].Value is string &&
                            JObject.Parse((string)outputTag.Attributes["data-source"].Value).IsValid(KendoRemoteDataSource.Schema)

                        && outputTag.Attributes.ContainsName(KendoAutocompleteTagHelper.IgnoreCaseAttributeName) 
                            && outputTag.Attributes[KendoAutocompleteTagHelper.IgnoreCaseAttributeName].Value is bool
                            && !(bool)outputTag.Attributes[KendoAutocompleteTagHelper.IgnoreCaseAttributeName].Value

                    )
                };


                yield return new object[]
                {
                    new KendoAutocompleteTagHelper() {
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
                    new TagHelperContext(new TagHelperAttributeList(), new Dictionary<object, object>(), Guid.NewGuid().ToString("n")),
                    new TagHelperOutput("kendo-autocomplete", new TagHelperAttributeList(),  (param1, param2) => Task.FromResult((TagHelperContent)null)),
                    (Expression<Func<TagHelperOutput, bool>>) (outputTag =>
                        outputTag.TagName == "input" &&
                        outputTag.TagMode == TagMode.SelfClosing &&
                        outputTag.Attributes.Count() == 4

                        && outputTag.Attributes.ContainsName(KendoAutocompleteTagHelper.TypeAttributeName)
                        && outputTag.Attributes[KendoAutocompleteTagHelper.TypeAttributeName].Value is string
                        && "text".Equals(outputTag.Attributes[KendoAutocompleteTagHelper.TypeAttributeName].Value)

                        && ((string)outputTag.Attributes[KendoAutocompleteTagHelper.RoleAttributeName].Value) == KendoAutocompleteTagHelper.WidgetName

                        && outputTag.Attributes.ContainsName("data-source") && outputTag.Attributes["data-source"].Value is string &&
                            JObject.Parse((string)outputTag.Attributes["data-source"].Value).IsValid(KendoRemoteDataSource.Schema)

                        && outputTag.Attributes.ContainsName(KendoAutocompleteTagHelper.IgnoreCaseAttributeName)
                            && outputTag.Attributes[KendoAutocompleteTagHelper.IgnoreCaseAttributeName].Value is bool
                            && !(bool)outputTag.Attributes[KendoAutocompleteTagHelper.IgnoreCaseAttributeName].Value

                    )
                };


                yield return new object[]
                {
                    new KendoAutocompleteTagHelper() {
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
                    new TagHelperContext(new TagHelperAttributeList(), new Dictionary<object, object>(), Guid.NewGuid().ToString("n")),
                    new TagHelperOutput("kendo-autocomplete", new TagHelperAttributeList(),  (param1, param2) => Task.FromResult((TagHelperContent)null)),
                    (Expression<Func<TagHelperOutput, bool>>) (outputTag =>
                        outputTag.TagName == "input" &&
                        outputTag.TagMode == TagMode.SelfClosing &&
                        outputTag.Attributes.Count() == 4

                        && outputTag.Attributes.ContainsName(KendoAutocompleteTagHelper.TypeAttributeName)
                        && outputTag.Attributes[KendoAutocompleteTagHelper.TypeAttributeName].Value is string
                        && "text".Equals(outputTag.Attributes[KendoAutocompleteTagHelper.TypeAttributeName].Value)

                        && ((string)outputTag.Attributes[KendoAutocompleteTagHelper.RoleAttributeName].Value) == KendoAutocompleteTagHelper.WidgetName

                        && outputTag.Attributes.ContainsName("data-source") && outputTag.Attributes["data-source"].Value is string &&
                            JObject.Parse((string)outputTag.Attributes["data-source"].Value).IsValid(KendoRemoteDataSource.Schema)

                        && outputTag.Attributes.ContainsName(KendoAutocompleteTagHelper.IgnoreCaseAttributeName)
                            && outputTag.Attributes[KendoAutocompleteTagHelper.IgnoreCaseAttributeName].Value is bool
                            && (bool)outputTag.Attributes[KendoAutocompleteTagHelper.IgnoreCaseAttributeName].Value

                    )
                };

                yield return new object[]
                {
                    new KendoAutocompleteTagHelper() {
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
                    new TagHelperContext(new TagHelperAttributeList(), new Dictionary<object, object>(), Guid.NewGuid().ToString("n")),
                    new TagHelperOutput("kendo-autocomplete", new TagHelperAttributeList(),  (param1, param2) => Task.FromResult((TagHelperContent)null)),
                    (Expression<Func<TagHelperOutput, bool>>) (outputTag =>
                        outputTag.TagName == "input" &&
                        outputTag.TagMode == TagMode.SelfClosing &&
                        outputTag.Attributes.Count() == 4

                        && outputTag.Attributes.ContainsName(KendoAutocompleteTagHelper.TypeAttributeName)
                        && outputTag.Attributes[KendoAutocompleteTagHelper.TypeAttributeName].Value is string
                        && "text".Equals(outputTag.Attributes[KendoAutocompleteTagHelper.TypeAttributeName].Value)

                        && ((string)outputTag.Attributes[KendoAutocompleteTagHelper.RoleAttributeName].Value) == KendoAutocompleteTagHelper.WidgetName

                        && outputTag.Attributes.ContainsName("data-source") && outputTag.Attributes["data-source"].Value is string &&
                            JObject.Parse((string)outputTag.Attributes["data-source"].Value).IsValid(KendoRemoteDataSource.Schema)

                        && outputTag.Attributes.ContainsName(KendoAutocompleteTagHelper.MinLengthAttributeName)
                            && outputTag.Attributes[KendoAutocompleteTagHelper.MinLengthAttributeName].Value is uint
                            && 3 == (uint)outputTag.Attributes[KendoAutocompleteTagHelper.MinLengthAttributeName].Value

                    )
                };


                yield return new object[]
                {
                    new KendoAutocompleteTagHelper() {
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
                    new TagHelperContext(new TagHelperAttributeList(), new Dictionary<object, object>(), Guid.NewGuid().ToString("n")),
                    new TagHelperOutput("kendo-autocomplete", new TagHelperAttributeList(),  (param1, param2) => Task.FromResult((TagHelperContent)null)),
                    (Expression<Func<TagHelperOutput, bool>>) (outputTag =>
                        outputTag.TagName == "input" &&
                        outputTag.TagMode == TagMode.SelfClosing &&
                        outputTag.Attributes.Count() == 3

                        && outputTag.Attributes.ContainsName(KendoAutocompleteTagHelper.TypeAttributeName)
                        && outputTag.Attributes[KendoAutocompleteTagHelper.TypeAttributeName].Value is string
                        && "text".Equals(outputTag.Attributes[KendoAutocompleteTagHelper.TypeAttributeName].Value)

                        && ((string)outputTag.Attributes[KendoAutocompleteTagHelper.RoleAttributeName].Value) == KendoAutocompleteTagHelper.WidgetName

                        && outputTag.Attributes.ContainsName("data-source") && outputTag.Attributes["data-source"].Value is string &&
                            JObject.Parse((string)outputTag.Attributes["data-source"].Value).IsValid(KendoRemoteDataSource.Schema)
                    )
                };

                yield return new object[]
                {
                    new KendoAutocompleteTagHelper() {
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
                    new TagHelperContext(new TagHelperAttributeList(), new Dictionary<object, object>(), Guid.NewGuid().ToString("n")),
                    new TagHelperOutput("kendo-autocomplete", new TagHelperAttributeList(),  (param1, param2) => Task.FromResult((TagHelperContent)null)),
                    (Expression<Func<TagHelperOutput, bool>>) (outputTag =>
                        outputTag.TagName == "input" &&
                        outputTag.TagMode == TagMode.SelfClosing &&
                        outputTag.Attributes.Count() == 3

                        && outputTag.Attributes.ContainsName(KendoAutocompleteTagHelper.TypeAttributeName)
                        && outputTag.Attributes[KendoAutocompleteTagHelper.TypeAttributeName].Value is string
                        && "text".Equals(outputTag.Attributes[KendoAutocompleteTagHelper.TypeAttributeName].Value)

                        && ((string)outputTag.Attributes[KendoAutocompleteTagHelper.RoleAttributeName].Value) == KendoAutocompleteTagHelper.WidgetName

                        && outputTag.Attributes.ContainsName("data-source") && outputTag.Attributes["data-source"].Value is string &&
                            JObject.Parse((string)outputTag.Attributes["data-source"].Value).IsValid(KendoRemoteDataSource.Schema)
                    )
                };


                yield return new object[]
                {
                    new KendoAutocompleteTagHelper() {
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
                    new TagHelperContext(new TagHelperAttributeList(), new Dictionary<object, object>(), Guid.NewGuid().ToString("n")),
                    new TagHelperOutput("kendo-autocomplete", new TagHelperAttributeList(),  (param1, param2) => Task.FromResult((TagHelperContent)null)),
                    (Expression<Func<TagHelperOutput, bool>>) (outputTag =>
                        outputTag.TagName == "input" &&
                        outputTag.TagMode == TagMode.SelfClosing &&
                        outputTag.Attributes.Count() == 3

                        && outputTag.Attributes.ContainsName(KendoAutocompleteTagHelper.TypeAttributeName)
                        && outputTag.Attributes[KendoAutocompleteTagHelper.TypeAttributeName].Value is string
                        && "text".Equals(outputTag.Attributes[KendoAutocompleteTagHelper.TypeAttributeName].Value)

                        && ((string)outputTag.Attributes[KendoAutocompleteTagHelper.RoleAttributeName].Value) == KendoAutocompleteTagHelper.WidgetName

                        && outputTag.Attributes.ContainsName("data-source") && outputTag.Attributes["data-source"].Value is string &&
                            JObject.Parse((string)outputTag.Attributes["data-source"].Value).IsValid(KendoRemoteDataSource.Schema)
                    )
                };

                yield return new object[]
                {
                    new KendoAutocompleteTagHelper() {
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
                    new TagHelperContext(new TagHelperAttributeList(), new Dictionary<object, object>(), Guid.NewGuid().ToString("n")),
                    new TagHelperOutput("kendo-autocomplete", new TagHelperAttributeList(),  (param1, param2) => Task.FromResult((TagHelperContent)null)),
                    (Expression<Func<TagHelperOutput, bool>>) (outputTag =>
                        outputTag.TagName == "input" &&
                        outputTag.TagMode == TagMode.SelfClosing &&
                        outputTag.Attributes.Count() == 4

                        && outputTag.Attributes.ContainsName(KendoAutocompleteTagHelper.TypeAttributeName)
                        && outputTag.Attributes[KendoAutocompleteTagHelper.TypeAttributeName].Value is string
                        && "text".Equals(outputTag.Attributes[KendoAutocompleteTagHelper.TypeAttributeName].Value)

                        && ((string)outputTag.Attributes[KendoAutocompleteTagHelper.RoleAttributeName].Value) == KendoAutocompleteTagHelper.WidgetName

                        && outputTag.Attributes.ContainsName("data-source") && outputTag.Attributes["data-source"].Value is string &&
                            JObject.Parse((string)outputTag.Attributes["data-source"].Value).IsValid(KendoRemoteDataSource.Schema)

                        && outputTag.Attributes.ContainsName(KendoAutocompleteTagHelper.PlaceholderAttributeName)
                            && outputTag.Attributes[KendoAutocompleteTagHelper.PlaceholderAttributeName].Value is string
                            && "Enter a city name".Equals(outputTag.Attributes[KendoAutocompleteTagHelper.PlaceholderAttributeName].Value)

                    )
                };

                yield return new object[]
                {
                    new KendoAutocompleteTagHelper() {
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
                        Filter = StartsWith
                    },
                    new TagHelperContext(new TagHelperAttributeList(), new Dictionary<object, object>(), Guid.NewGuid().ToString("n")),
                    new TagHelperOutput("kendo-autocomplete", new TagHelperAttributeList(),  (param1, param2) => Task.FromResult((TagHelperContent)null)),
                    (Expression<Func<TagHelperOutput, bool>>) (outputTag =>
                        outputTag.TagName == "input" &&
                        outputTag.TagMode == TagMode.SelfClosing &&
                        outputTag.Attributes.Count() == 4
                        
                        && outputTag.Attributes.ContainsName(KendoAutocompleteTagHelper.RoleAttributeName) 
                        && outputTag.Attributes[KendoAutocompleteTagHelper.RoleAttributeName].Value is string 
                        && ((string)outputTag.Attributes[KendoAutocompleteTagHelper.RoleAttributeName].Value) == KendoAutocompleteTagHelper.WidgetName

                        && outputTag.Attributes.ContainsName(KendoAutocompleteTagHelper.TypeAttributeName)
                        && outputTag.Attributes[KendoAutocompleteTagHelper.TypeAttributeName].Value is string
                        && "text".Equals(outputTag.Attributes[KendoAutocompleteTagHelper.TypeAttributeName].Value)

                        && outputTag.Attributes.ContainsName("data-source") && outputTag.Attributes["data-source"].Value is string &&
                            JObject.Parse((string)outputTag.Attributes["data-source"].Value).IsValid(KendoRemoteDataSource.Schema)

                        && outputTag.Attributes.ContainsName(KendoAutocompleteTagHelper.PlaceholderAttributeName)
                            && outputTag.Attributes[KendoAutocompleteTagHelper.PlaceholderAttributeName].Value is string
                            && "Enter a city name".Equals(outputTag.Attributes[KendoAutocompleteTagHelper.PlaceholderAttributeName].Value)

                        && outputTag.Attributes.ContainsName(KendoAutocompleteTagHelper.FilterAttributeName) && outputTag.Attributes[KendoAutocompleteTagHelper.FilterAttributeName].Value is string &&
                            nameof(StartsWith).ToLower().Equals((string)outputTag.Attributes[KendoAutocompleteTagHelper.FilterAttributeName].Value)

                    )
                };

            }
        }

        public KendoAutocompleteTagHelperTests(ITestOutputHelper output)
        {
            _output = output;

        }
        
        [Theory]
        [MemberData(nameof(ProcessCases))]
        public void Process(KendoAutocompleteTagHelper tagHelper, TagHelperContext context, TagHelperOutput output, Expression<Func<TagHelperOutput, bool>> outputMatcher)
        {
            _output.WriteLine(SerializeObject(tagHelper, Indented));

            tagHelper.Process(context, output);
            output.Should().Match(outputMatcher);
        }


        public void Dispose ()
        {
            _output = null;
        }
    }
}
