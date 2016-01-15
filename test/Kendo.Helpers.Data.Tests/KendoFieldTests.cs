using FluentAssertions;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using Newtonsoft.Json.Schema;
using System.Linq.Expressions;
using Xunit;
using Xunit.Abstractions;

namespace Kendo.Helpers.Data.Tests
{
    public class KendoFieldTests
    {
        private readonly ITestOutputHelper _output;

        public static IEnumerable<object[]> ToJsonCases
        {
            get
            {
                yield return new object[]
                {
                    new KendoStringField("Firstname"),
                    ((Expression<Func<string, bool>>)(json => 
                        JObject.Parse(json).IsValid(KendoFieldBase.Schema("Firstname", FieldType.String)) &&
                        nameof(FieldType.String).ToLower().Equals((string) JObject.Parse(json)[KendoFieldBase.TypePropertyName])
                    ))
                };

                yield return new object[]
                {
                    new KendoStringField("Firstname") { DefaultValue = null },
                    ((Expression<Func<string, bool>>)(json =>
                        JObject.Parse(json).IsValid(KendoFieldBase.Schema("Firstname", FieldType.String)) &&
                        nameof(FieldType.String).ToLower().Equals((string) JObject.Parse(json)[KendoFieldBase.TypePropertyName])
                    ))
                };
            }
        }

        public static IEnumerable<object[]> KendoStringFieldSchemaCases
        {
            get
            {

                yield return new object[]
                {
                    new KendoStringField("Firstname"), true
                };
            }
        }

        
        public KendoFieldTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Theory]
        [MemberData(nameof(ToJsonCases))]
        public void ToJson(KendoFieldBase kf, Expression<Func<string, bool>> jsonMatcher)
        {
            _output.WriteLine(kf.ToJson());
            kf.ToJson().Should().Match(jsonMatcher);
        }

        

        [Theory]
        [MemberData(nameof(KendoStringFieldSchemaCases))]
        public void Schema(KendoFieldBase kf, bool expectedValidity)
        {
            _output.WriteLine(kf.ToJson());
            JObject.Parse(kf.ToJson())
                  .IsValid(KendoFieldBase.Schema(kf.Name, kf.Type))
                  .Should().Be(expectedValidity);
        }

    }
}
