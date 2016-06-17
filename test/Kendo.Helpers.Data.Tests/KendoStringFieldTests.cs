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
    public class KendoStringFieldTests
    {
        private readonly ITestOutputHelper _output;

        public static IEnumerable<object[]> KendoStringFieldToJsonCases
        {
            get
            {
                yield return new object[]
                {
                    new KendoStringField("Firstname"),
                    ((Expression<Func<string, bool>>)(json =>
                        JObject.Parse(json).IsValid(KendoFieldBase.Schema("Firstname", FieldType.String))
                        && JObject.Parse(json).Properties().Count() == 1
                        && nameof(FieldType.String).ToLower().Equals((string) JObject.Parse(json)["Firstname"][KendoFieldBase.TypePropertyName])
                    ))
                };

                yield return new object[] {
                    new KendoStringField("item.lastname") { From = null, Editable = true, DefaultValue = string.Empty },
                    ((Expression<Func<string, bool>>)(json =>
                        JObject.Parse(json).IsValid(KendoFieldBase.Schema("item.lastname", FieldType.String))
                        && JObject.Parse(json).Properties().Count() == 1
                        && nameof(FieldType.String).ToLower().Equals((string) JObject.Parse(json)["item.lastname"][KendoFieldBase.TypePropertyName])

                        && null == JObject.Parse(json)["item.lastname"][KendoFieldBase.FromPropertyName]
                        && string.Empty.Equals(JObject.Parse(json)["item.lastname"][KendoFieldBase.DefaultValuePropertyName].Value<string>())
                        && JObject.Parse(json)["item.lastname"][KendoFieldBase.EditablePropertyName].Value<bool>()
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

                yield return new object[]
                {
                    new KendoStringField("item.lastname") { From = null, Editable = true, DefaultValue = string.Empty }, true
                };
            }
        }



        public KendoStringFieldTests(ITestOutputHelper output)
        {
            _output = output;
        }


        [Theory]
        [MemberData(nameof(KendoStringFieldToJsonCases))]
        public void ToJson(KendoFieldBase kf, Expression<Func<string, bool>> resultMatcher)
        {
            _output.WriteLine($"Testing : {kf}{Environment.NewLine} against {Environment.NewLine} {resultMatcher} ");
            kf.ToJson().Should().Match(resultMatcher);
        }




        [Theory]
        [MemberData(nameof(KendoStringFieldSchemaCases))]
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
