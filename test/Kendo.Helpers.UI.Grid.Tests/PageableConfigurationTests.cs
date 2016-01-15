using FluentAssertions;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Kendo.Helpers.UI.Grid.Tests
{
    public class PageableConfigurationTests
    {
        private readonly ITestOutputHelper _output;

        public static IEnumerable<object[]> SchemaCases
        {
            get
            {
                yield return new object[]
                {
                    new PageableConfiguration(), false
                };

                yield return new object[]
                {
                    new PageableConfiguration { Numeric = true }, true
                };

                yield return new object[]
                {
                    new PageableConfiguration { Numeric = false }, true
                };

                yield return new object[]
                {
                    new PageableConfiguration { Numeric = null }, false
                };

                yield return new object[]
                {
                    new PageableConfiguration { PageSize = 1 }, true
                };

                yield return new object[]
                {
                    new PageableConfiguration { PageSize = null }, false
                };

                yield return new object[]
                {
                    new PageableConfiguration { ButtonCount = 10 }, true
                };

                yield return new object[]
                {
                    new PageableConfiguration { ButtonCount = null }, false
                };

                yield return new object[]
                {
                    new PageableConfiguration {PreviousNext = null }, false
                };

                yield return new object[]
                {
                    new PageableConfiguration {PreviousNext = false }, true
                };

                yield return new object[]
                {
                    new PageableConfiguration {PreviousNext = true }, true
                };

                yield return new object[]
                {
                    new PageableConfiguration { Input = null }, false,
                };

                yield return new object[]
                {
                    new PageableConfiguration { Input = true }, true,
                };

                yield return new object[]
                {
                    new PageableConfiguration { Input = false}, true,
                };


                yield return new object[]
                {
                    new PageableConfiguration { Refresh = null }, false,
                };

                yield return new object[]
                {
                    new PageableConfiguration { Refresh = true }, true,
                };

                yield return new object[]
                {
                    new PageableConfiguration { Refresh = false}, true,
                };


                yield return new object[]
                {
                    new PageableConfiguration { PageSizes = false}, true,
                };

                yield return new object[]
                {
                    new PageableConfiguration { PageSizes = null}, false,
                };


                yield return new object[]
                {
                    new PageableConfiguration { PageSizes = true }, true,
                };


                yield return new object[]
                {
                    new PageableConfiguration { Info = false}, true,
                };

                yield return new object[]
                {
                    new PageableConfiguration { Info = null}, false,
                };


                yield return new object[]
                {
                    new PageableConfiguration { Info = true }, true,
                };


                yield return new object[]
                {
                    new PageableConfiguration { Messages = new PageableMessagesConfiguration() }, false,
                };

                yield return new object[]
                {
                    new PageableConfiguration {
                        Messages = new PageableMessagesConfiguration()
                        {
                            Display = string.Empty
                        }
                    }, true,
                };



            }
        }


        public static IEnumerable<object[]> ToJsonCases
        {
            get
            {
                yield return new object[]
                {
                    new PageableConfiguration { PageSize = 2 },
                    ((Expression<Func<string, bool>>) (json => 
                        JObject.Parse(json).Properties().Count() == 1 &&
                        2 == (int)JObject.Parse(json)[PageableConfiguration.PageSizePropertyName]
                    ))
                };


                yield return new object[]
                {
                    new PageableConfiguration { PageSizes = false },
                    ((Expression<Func<string, bool>>) (json =>
                        JObject.Parse(json).Properties().Count() == 1
                        && !(bool)JObject.Parse(json)[PageableConfiguration.PageSizesPropertyName]
                    ))
                };

                yield return new object[]
                {
                    new PageableConfiguration { PageSizes = true },
                    ((Expression<Func<string, bool>>) (json =>
                        JObject.Parse(json).Properties().Count() == 1
                        && (bool)JObject.Parse(json)[PageableConfiguration.PageSizesPropertyName]
                    ))
                };


                yield return new object[]
                {
                    new PageableConfiguration { Info = false },
                    ((Expression<Func<string, bool>>) (json =>
                        JObject.Parse(json).Properties().Count() == 1
                        && !(bool)JObject.Parse(json)[PageableConfiguration.InfoPropertyName]
                    ))
                };

                yield return new object[]
                {
                    new PageableConfiguration { Info = true },
                    ((Expression<Func<string, bool>>) (json =>
                        JObject.Parse(json).Properties().Count() == 1
                        && (bool)JObject.Parse(json)[PageableConfiguration.InfoPropertyName]
                    ))
                };


                yield return new object[]
                {
                    new PageableConfiguration { PreviousNext = false },
                    ((Expression<Func<string, bool>>) (json =>
                        JObject.Parse(json).Properties().Count() == 1 &&
                        !(bool)JObject.Parse(json)[PageableConfiguration.PreviousNextPropertyName]
                    ))
                };

                yield return new object[]
                {
                    new PageableConfiguration { Refresh = true },
                    ((Expression<Func<string, bool>>) (json =>
                        JObject.Parse(json).Properties().Count() == 1 &&
                        (bool)JObject.Parse(json)[PageableConfiguration.RefreshPropertyName]
                    ))
                };


                yield return new object[]
                {
                    new PageableConfiguration { Refresh = false },
                    ((Expression<Func<string, bool>>) (json =>
                        JObject.Parse(json).Properties().Count() == 1 &&
                        !(bool)JObject.Parse(json)[PageableConfiguration.RefreshPropertyName]
                    ))
                };

                yield return new object[]
                {
                    new PageableConfiguration { PreviousNext = true },
                    ((Expression<Func<string, bool>>) (json =>
                        JObject.Parse(json).Properties().Count() == 1 &&
                        (bool)JObject.Parse(json)[PageableConfiguration.PreviousNextPropertyName]
                    ))
                };


                yield return new object[]
                {
                    new PageableConfiguration { Input = true },
                    ((Expression<Func<string, bool>>) (json =>
                        JObject.Parse(json).Properties().Count() == 1 &&
                        (bool)JObject.Parse(json)[PageableConfiguration.InputPropertyName]
                    ))
                };


                yield return new object[]
                {
                    new PageableConfiguration { Input = false },
                    ((Expression<Func<string, bool>>) (json =>
                        JObject.Parse(json).Properties().Count() == 1 &&
                        !(bool)JObject.Parse(json)[PageableConfiguration.InputPropertyName]
                    ))
                };

                yield return new object[]
                {
                    new PageableConfiguration { ButtonCount = 2 },
                    ((Expression<Func<string, bool>>) (json =>
                        JObject.Parse(json).Properties().Count() == 1 &&
                        2 == (int)JObject.Parse(json)[PageableConfiguration.ButtonCountPropertyName]
                    ))
                };

                yield return new object[]
                {
                    new PageableConfiguration { PageSize = 0 },
                    ((Expression<Func<string, bool>>) (json => 
                        JObject.Parse(json).Properties().Count() == 1 &&
                        0 == (int)JObject.Parse(json)[PageableConfiguration.PageSizePropertyName]
                    ))
                };

                yield return new object[]
                {
                    new PageableConfiguration { },
                    ((Expression<Func<string, bool>>) (json =>
                        !JObject.Parse(json).Properties().Any()
                    ))
                };


                yield return new object[]
                {
                    new PageableConfiguration
                    {
                        Numeric = true
                    },
                    ((Expression<Func<string, bool>>) (json =>
                        JObject.Parse(json).Properties().Count() == 1 
                        && (bool)JObject.Parse(json)[PageableConfiguration.NumericPropertyName]
                    ))
                };

                yield return new object[]
                {
                    new PageableConfiguration
                    {
                        Numeric = false
                    },
                    ((Expression<Func<string, bool>>) (json =>
                        JObject.Parse(json).Properties().Count() == 1
                        && !(bool)JObject.Parse(json)[PageableConfiguration.NumericPropertyName]
                    ))
                };
            }
        }


        public PageableConfigurationTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Theory]
        [MemberData(nameof(ToJsonCases))]
        public void ToJson(PageableConfiguration configuration, Expression<Func<string, bool>> jsonMatcher)
        {
            _output.WriteLine($"Testing {configuration}");
            configuration.ToJson().Should().Match(jsonMatcher);
        }

        [Theory]
        [MemberData(nameof(SchemaCases))]
        public void Schema(PageableConfiguration configuration, bool expectedValidity)
        {
            _output.WriteLine($"Testing {configuration}");

            JObject.Parse(configuration.ToJson())
              .IsValid(PageableConfiguration.Schema)
              .Should().Be(expectedValidity);
        }
    }
}
