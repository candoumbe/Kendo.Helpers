using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using Newtonsoft.Json.Schema;
using Xunit;
using FluentAssertions;
using System.Linq.Expressions;
using System;
using Xunit.Abstractions;

namespace Kendo.Helpers.UI.Grid.Tests
{
    public class GridFilterableConfigurationTests
    {
        private readonly ITestOutputHelper _output;

        public static IEnumerable<object> ToJsonCases
        {
            get
            {
                yield return new object[]
                {
                    new GridFilterableConfiguration 
                        {
                            Mode = GridFilterableMode.Menu
                        },
                    ((Expression<Func<string, bool>>) (json => 
                        "menu".Equals((string)JObject.Parse(json)[GridFilterableConfiguration.ModePropertyName])
                    ))
                };

                yield return new object[]
                {
                    new GridFilterableConfiguration
                        {
                            Mode = GridFilterableMode.Row
                        },
                    ((Expression<Func<string, bool>>) (json =>
                        "row".Equals((string)JObject.Parse(json)[GridFilterableConfiguration.ModePropertyName])
                    ))
                };

                yield return new object[]
                {
                    new GridFilterableConfiguration
                        {
                            Mode = GridFilterableMode.RowAndMenu
                        },
                    ((Expression<Func<string, bool>>) (json =>
                        "menu, row".Equals((string)JObject.Parse(json)[GridFilterableConfiguration.ModePropertyName])
                    ))
                };
                yield return new object[] 
                {
                    new GridFilterableConfiguration
                        {
                            Mode = GridFilterableMode.Row | GridFilterableMode.Menu
                        },
                    ((Expression<Func<string, bool>>) (json =>
                        "menu, row".Equals((string)JObject.Parse(json)[GridFilterableConfiguration.ModePropertyName])
                    ))
                };
            }
        }

        public static IEnumerable<object> SchemaCases
        {
            get
            {
                yield return new object[]
                {
                    new GridFilterableConfiguration() , false,
                };
                yield return new object[] 
                {
                    new GridFilterableConfiguration { Mode = GridFilterableMode.Menu }, true
                };
                yield return new object[]
                {
                    new GridFilterableConfiguration { Mode = GridFilterableMode.Row }, true
                };

                yield return new object[]
                {
                    new GridFilterableConfiguration { Mode = GridFilterableMode.Row | GridFilterableMode.Menu }, true
                };

                yield return new object[]
                {
                    new GridFilterableConfiguration { Mode = GridFilterableMode.Row & GridFilterableMode.Menu }, false
                };

                yield return new object[]
                {
                    new GridFilterableConfiguration { Extra = false }, true
                };

                yield return new object[]
                {
                    new GridFilterableConfiguration { Extra = null }, false
                };

                yield return new object[]
                {
                    new GridFilterableConfiguration { Extra = true }, true
                };


            }
        }

        public GridFilterableConfigurationTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Theory]
        [MemberData(nameof(SchemaCases))]
        public void Schema(GridFilterableConfiguration configuration, bool expectedValidity)
        {
            _output.WriteLine($"Testing : {configuration}");
            JObject.Parse(configuration.ToJson()).IsValid(GridFilterableConfiguration.Schema).Should().Be(expectedValidity);
        }

        [Theory]
        [MemberData(nameof(ToJsonCases))]
        public void ToJson(GridFilterableConfiguration configuration, Expression<Func<string, bool>> jsonMatcher)
        {
            _output.WriteLine($"Testing : {configuration}");
            configuration.ToJson().Should().Match(jsonMatcher);
        }
    }
}
