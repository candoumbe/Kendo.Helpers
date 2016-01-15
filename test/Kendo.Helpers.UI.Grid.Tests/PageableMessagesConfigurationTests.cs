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
    public class PageableMessagesConfigurationTests
    {
        private readonly ITestOutputHelper _output;

        public static IEnumerable<object[]> SchemaCases
        {
            get
            {
                yield return new object[]
                {
                    new PageableMessagesConfiguration(), false
                };

                yield return new object[]
                {
                    new PageableMessagesConfiguration() { Display = null }, false
                };


                yield return new object[]
                {
                    new PageableMessagesConfiguration() {
                        Display = string.Empty
                    }
                    , true,
                };


                yield return new object[]
                {
                    new PageableMessagesConfiguration() { Empty = null }, false
                };


                yield return new object[]
                {
                    new PageableMessagesConfiguration() { Empty = string.Empty}, true,
                };


                yield return new object[]
                {
                    new PageableMessagesConfiguration() { Page = null }, false
                };


                yield return new object[]
                {
                    new PageableMessagesConfiguration() { Page = string.Empty }, true,
                };


                yield return new object[]
                {
                    new PageableMessagesConfiguration() { Of = null }, false
                };


                yield return new object[]
                {
                    new PageableMessagesConfiguration() { Of = string.Empty }, true,
                };


            }
        }


        public static IEnumerable<object[]> ToJsonCases
        {
            get
            {
                
                yield return new object[]
                {
                    new PageableMessagesConfiguration { Display = string.Empty },
                    ((Expression<Func<string, bool>>) (json =>
                        JObject.Parse(json).Properties().Count() == 1 &&
                        string.Empty.Equals((string)JObject.Parse(json)[PageableMessagesConfiguration.DisplayPropertyName])
                    ))
                };


                yield return new object[]
                {
                    new PageableMessagesConfiguration { Display = "Display" },
                    ((Expression<Func<string, bool>>) (json =>
                        JObject.Parse(json).Properties().Count() == 1 &&
                        "Display".Equals((string)JObject.Parse(json)[PageableMessagesConfiguration.DisplayPropertyName])
                    ))
                };

                yield return new object[]
                {
                    new PageableMessagesConfiguration { Display = null},
                    ((Expression<Func<string, bool>>) (json => !JObject.Parse(json).Properties().Any() ))
                };

                yield return new object[]
                {
                    new PageableMessagesConfiguration { Empty = string.Empty },
                    ((Expression<Func<string, bool>>) (json =>
                        JObject.Parse(json).Properties().Count() == 1 &&
                        string.Empty.Equals((string)JObject.Parse(json)[PageableMessagesConfiguration.EmptyPropertyName])
                    ))
                };

                yield return new object[]
                {
                    new PageableMessagesConfiguration { Empty = "No items to display" },
                    ((Expression<Func<string, bool>>) (json =>
                        JObject.Parse(json).Properties().Count() == 1 &&
                        "No items to display".Equals((string)JObject.Parse(json)[PageableMessagesConfiguration.EmptyPropertyName])
                    ))
                };

                yield return new object[]
                {
                    new PageableMessagesConfiguration { Empty = null},
                    ((Expression<Func<string, bool>>) (json => !JObject.Parse(json).Properties().Any() ))
                };


                yield return new object[]
                {
                    new PageableMessagesConfiguration { Page = string.Empty },
                    ((Expression<Func<string, bool>>) (json =>
                        JObject.Parse(json).Properties().Count() == 1 &&
                        string.Empty.Equals((string)JObject.Parse(json)[PageableMessagesConfiguration.PagePropertyName])
                    ))
                };

                yield return new object[]
                {
                    new PageableMessagesConfiguration { Page = "Page" },
                    ((Expression<Func<string, bool>>) (json =>
                        JObject.Parse(json).Properties().Count() == 1 &&
                        "Page".Equals((string)JObject.Parse(json)[PageableMessagesConfiguration.PagePropertyName])
                    ))
                };

                yield return new object[]
                {
                    new PageableMessagesConfiguration { Page = null},
                    ((Expression<Func<string, bool>>) (json => !JObject.Parse(json).Properties().Any() ))
                };


                yield return new object[]
                {
                    new PageableMessagesConfiguration { Of = string.Empty },
                    ((Expression<Func<string, bool>>) (json =>
                        JObject.Parse(json).Properties().Count() == 1 &&
                        string.Empty.Equals((string)JObject.Parse(json)[PageableMessagesConfiguration.OfPropertyName])
                    ))
                };

                yield return new object[]
                {
                    new PageableMessagesConfiguration { Of = "Of" },
                    ((Expression<Func<string, bool>>) (json =>
                        JObject.Parse(json).Properties().Count() == 1 &&
                        "Of".Equals((string)JObject.Parse(json)[PageableMessagesConfiguration.OfPropertyName])
                    ))
                };

                yield return new object[]
                {
                    new PageableMessagesConfiguration { Of = null},
                    ((Expression<Func<string, bool>>) (json => !JObject.Parse(json).Properties().Any() ))
                };


                yield return new object[]
                {
                    new PageableMessagesConfiguration { ItemsPerPage = string.Empty },
                    ((Expression<Func<string, bool>>) (json =>
                        JObject.Parse(json).Properties().Count() == 1 &&
                        string.Empty.Equals((string)JObject.Parse(json)[PageableMessagesConfiguration.ItemsPerPagePropertyName])
                    ))
                };

                yield return new object[]
                {
                    new PageableMessagesConfiguration { ItemsPerPage = "Items per page" },
                    ((Expression<Func<string, bool>>) (json =>
                        JObject.Parse(json).Properties().Count() == 1 &&
                        "Items per page".Equals((string)JObject.Parse(json)[PageableMessagesConfiguration.ItemsPerPagePropertyName])
                    ))
                };

                yield return new object[]
                {
                    new PageableMessagesConfiguration { ItemsPerPage = null},
                    ((Expression<Func<string, bool>>) (json => !JObject.Parse(json).Properties().Any() ))
                };

                yield return new object[]
                {
                    new PageableMessagesConfiguration { First = string.Empty },
                    ((Expression<Func<string, bool>>) (json =>
                        JObject.Parse(json).Properties().Count() == 1 &&
                        string.Empty.Equals((string)JObject.Parse(json)[PageableMessagesConfiguration.FirstPropertyName])
                    ))
                };

                yield return new object[]
                {
                    new PageableMessagesConfiguration { First = "First" },
                    ((Expression<Func<string, bool>>) (json =>
                        JObject.Parse(json).Properties().Count() == 1 &&
                        "First".Equals((string)JObject.Parse(json)[PageableMessagesConfiguration.FirstPropertyName])
                    ))
                };

                yield return new object[]
                {
                    new PageableMessagesConfiguration { First = null},
                    ((Expression<Func<string, bool>>) (json => !JObject.Parse(json).Properties().Any() ))
                };

                
                yield return new object[]
                {
                    new PageableMessagesConfiguration { Last = "Last" },
                    ((Expression<Func<string, bool>>) (json =>
                        JObject.Parse(json).Properties().Count() == 1 &&
                        "Last".Equals((string)JObject.Parse(json)[PageableMessagesConfiguration.LastPropertyName])
                    ))
                };

                yield return new object[]
                {
                    new PageableMessagesConfiguration { Last = null},
                    ((Expression<Func<string, bool>>) (json => !JObject.Parse(json).Properties().Any() ))
                };

                yield return new object[]
                {
                    new PageableMessagesConfiguration { Next = string.Empty },
                    ((Expression<Func<string, bool>>) (json =>
                        JObject.Parse(json).Properties().Count() == 1 &&
                        string.Empty.Equals((string)JObject.Parse(json)[PageableMessagesConfiguration.NextPropertyName])
                    ))
                };

                yield return new object[]
                {
                    new PageableMessagesConfiguration { Next = "Next" },
                    ((Expression<Func<string, bool>>) (json =>
                        JObject.Parse(json).Properties().Count() == 1 &&
                        "Next".Equals((string)JObject.Parse(json)[PageableMessagesConfiguration.NextPropertyName])
                    ))
                };

                yield return new object[]
                {
                    new PageableMessagesConfiguration { Next = null},
                    ((Expression<Func<string, bool>>) (json => !JObject.Parse(json).Properties().Any() ))
                };


                yield return new object[]
                {
                    new PageableMessagesConfiguration { Previous = string.Empty },
                    ((Expression<Func<string, bool>>) (json =>
                        JObject.Parse(json).Properties().Count() == 1 &&
                        string.Empty.Equals((string)JObject.Parse(json)[PageableMessagesConfiguration.PreviousPropertyName])
                    ))
                };

                yield return new object[]
                {
                    new PageableMessagesConfiguration { Previous = "Previous"},
                    ((Expression<Func<string, bool>>) (json =>
                        JObject.Parse(json).Properties().Count() == 1 &&
                        "Previous".Equals((string)JObject.Parse(json)[PageableMessagesConfiguration.PreviousPropertyName])
                    ))
                };

                yield return new object[]
                {
                    new PageableMessagesConfiguration { Previous = null},
                    ((Expression<Func<string, bool>>) (json => !JObject.Parse(json).Properties().Any() ))
                };

                yield return new object[]
                {
                    new PageableMessagesConfiguration { Refresh = string.Empty },
                    ((Expression<Func<string, bool>>) (json =>
                        JObject.Parse(json).Properties().Count() == 1 &&
                        string.Empty.Equals((string)JObject.Parse(json)[PageableMessagesConfiguration.RefreshPropertyName])
                    ))
                };

                yield return new object[]
                {
                    new PageableMessagesConfiguration { Refresh = "Refresh" },
                    ((Expression<Func<string, bool>>) (json =>
                        JObject.Parse(json).Properties().Count() == 1 &&
                        "Refresh".Equals((string)JObject.Parse(json)[PageableMessagesConfiguration.RefreshPropertyName])
                    ))
                };

                yield return new object[]
                {
                    new PageableMessagesConfiguration { Refresh = null},
                    ((Expression<Func<string, bool>>) (json => !JObject.Parse(json).Properties().Any() ))
                };

                yield return new object[]
                {
                    new PageableMessagesConfiguration { MorePages = string.Empty },
                    ((Expression<Func<string, bool>>) (json =>
                        JObject.Parse(json).Properties().Count() == 1 &&
                        string.Empty.Equals((string)JObject.Parse(json)[PageableMessagesConfiguration.MorePagesPropertyName])
                    ))
                };

                yield return new object[]
                {
                    new PageableMessagesConfiguration { MorePages = "More pages" },
                    ((Expression<Func<string, bool>>) (json =>
                        JObject.Parse(json).Properties().Count() == 1 &&
                        "More pages".Equals((string)JObject.Parse(json)[PageableMessagesConfiguration.MorePagesPropertyName])
                    ))
                };

                yield return new object[]
                {
                    new PageableMessagesConfiguration { MorePages = null},
                    ((Expression<Func<string, bool>>) (json => !JObject.Parse(json).Properties().Any() ))
                };
            }
        }


        public PageableMessagesConfigurationTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Theory]
        [MemberData(nameof(ToJsonCases))]
        public void ToJson(PageableMessagesConfiguration configuration, Expression<Func<string, bool>> jsonMatcher)
        {
            _output.WriteLine($"Testing {configuration}");
            configuration.ToJson().Should().Match(jsonMatcher);
        }

        [Theory]
        [MemberData(nameof(SchemaCases))]
        public void Schema(PageableMessagesConfiguration configuration, bool expectedValidity)
        {
            _output.WriteLine($"Testing {configuration}");

            JObject.Parse(configuration.ToJson())
              .IsValid(PageableMessagesConfiguration.Schema)
              .Should().Be(expectedValidity);
        }
    }
}
