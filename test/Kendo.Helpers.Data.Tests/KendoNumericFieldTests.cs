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
    public class KendoNumericFieldTests
    {
        private readonly ITestOutputHelper _output;

        

        public static IEnumerable<object[]> KendoNumericFieldToJsonCases
        {
            get
            {
                yield return new object[]
                {
                    new KendoNumericField("id"),
                    ((Expression<Func<string, bool>>)(json =>
                        JObject.Parse(json).IsValid(KendoFieldBase.Schema("id", FieldType.Number))
                        && JObject.Parse(json).Properties().Count() == 1
                        && nameof(FieldType.Number).ToLower().Equals((string) JObject.Parse(json)["id"][KendoFieldBase.TypePropertyName])
                    ))
                };
            }
        }

        
        public static IEnumerable<object[]> KendoNumericFieldSchemaCases
        {
            get
            {

                yield return new object[]
                {
                    new KendoNumericField("id"), true
                };
            }
        }




        public KendoNumericFieldTests(ITestOutputHelper output)
        {
            _output = output;
        }

        
        [Theory]
        [MemberData(nameof(KendoNumericFieldToJsonCases))]
        public void ToJson(KendoFieldBase kf, Expression<Func<string, bool>> resultMatcher)
        {
            _output.WriteLine($"Testing : {kf}{Environment.NewLine} against {Environment.NewLine} {resultMatcher} ");
            kf.ToJson().Should().Match(resultMatcher);
        }

        [Theory]
        [MemberData(nameof(KendoNumericFieldSchemaCases))]
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
