using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using Newtonsoft.Json.Schema;
using Xunit;
using FluentAssertions;
using System.Linq.Expressions;
using System;
using System.Linq;

namespace Kendo.Helpers.UI.Grid.Tests
{
    public class CellConfigurationTests
    {
        public static IEnumerable<object[]> SchemaCases
        {
            get
            {
                yield return new object[]
                {
                    new CellConfiguration(), false
                };

                yield return new object[]
                {
                    new CellConfiguration { Enabled = true }, true
                };

                yield return new object[]
                {
                    new CellConfiguration { Enabled = false }, true
                };

            }
        }
        
        public static IEnumerable<object[]> ToJsonCases
        {
            get
            {
                yield return new object[]
                {
                    new CellConfiguration() { Enabled = false },
                    ((Expression<Func<string, bool>>) (json => 
                        !(bool)JObject.Parse(json)[CellConfiguration.EnabledPropertyName] &&
                        JObject.Parse(json).Properties().Count() == 1
                    ))
                };

                yield return new object[]
                {
                    new CellConfiguration() { Enabled = true },
                    ((Expression<Func<string, bool>>) (json =>
                        (bool)JObject.Parse(json)[CellConfiguration.EnabledPropertyName] &&
                        JObject.Parse(json).Properties().Count() == 1
                    ))
                };
            }
        }

        [Theory]
        [MemberData(nameof(ToJsonCases))]
        public void ToJson(CellConfiguration configuration, Expression<Func<string, bool>> jsonMatcher)
            => configuration.ToJson().Should().Match(jsonMatcher);

        [Theory]
        [MemberData(nameof(SchemaCases))]
        public void Schema(CellConfiguration configuration, bool expectedValidity)
            => JObject.Parse(configuration.ToJson()).IsValid(CellConfiguration.Schema)
            .Should().Be(expectedValidity);
    }
}
