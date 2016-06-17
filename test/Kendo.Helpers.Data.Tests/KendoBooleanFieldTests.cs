using FluentAssertions;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using Newtonsoft.Json.Schema;
using System.Linq.Expressions;
using Xunit;
using Xunit.Abstractions;
using Kendo.Helpers.Data;
using System.Linq;

namespace Kendo.Helpers.Data.Tests
{
    public class KendoBooleanFieldTests
    {
        private readonly ITestOutputHelper _output;

        
        public static IEnumerable<object[]> KendoBooleanFieldToJsonCases
        {
            get
            {
                yield return new object[]
               {
                    new KendoBooleanField("active"),
                    ((Expression<Func<string, bool>>)(json =>
                        JObject.Parse(json).IsValid(KendoFieldBase.Schema("active", FieldType.Boolean))
                        && JObject.Parse(json).Properties().Count() == 1
                        && nameof(FieldType.Boolean).ToLower().Equals((string) JObject.Parse(json)["active"][KendoFieldBase.TypePropertyName])
                    ))
               };
            }
        }
        
        public static IEnumerable<object[]> KendoBooleanFieldSchemaCases
        {
            get
            {

                yield return new object[]
                {
                    new KendoBooleanField("active"), true
                };
            }
        }
        
        public KendoBooleanFieldTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Theory]
        [MemberData(nameof(KendoBooleanFieldToJsonCases))]
        
        public void ToJson(KendoFieldBase kf, Expression<Func<string, bool>> resultMatcher)
        {
            _output.WriteLine($"Testing : {kf}{Environment.NewLine} against {Environment.NewLine} {resultMatcher} ");
            kf.ToJson().Should().Match(resultMatcher);
        }


        [Theory]
        [MemberData(nameof(KendoBooleanFieldSchemaCases))]
        public void Schema(KendoFieldBase kf, bool expectedValidity)
        {
            JSchema schema = KendoFieldBase.Schema(kf.Name, kf.Type);

            _output.WriteLine($"Validating:{kf.ToJson()}");
            _output.WriteLine($"Schema: {schema}");

            JObject.Parse(kf.ToJson())
                  .IsValid(schema)
                  .Should().Be(expectedValidity);
        }

    }
}
