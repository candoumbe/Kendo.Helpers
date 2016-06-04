using FluentAssertions;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using Newtonsoft.Json.Schema;
using System.Linq.Expressions;
using Xunit;
using Xunit.Abstractions;
using Kendo.Helpers.Data;

namespace Kendo.Helpers.Data.Tests
{
    public class KendoFieldTests
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
                        JObject.Parse(json).IsValid(KendoFieldBase.Schema(FieldType.String)) &&
                        nameof(FieldType.String).ToLower().Equals((string) JObject.Parse(json)[KendoFieldBase.TypePropertyName])
                    ))
                };

            }
        }


        public static IEnumerable<object[]> KendoNumericFieldToJsonCases
        {
            get
            {
                yield return new object[]
                {
                    new KendoNumericField("id"),
                    ((Expression<Func<string, bool>>)(json =>
                        nameof(FieldType.Number).ToLower().Equals((string) JObject.Parse(json)[KendoFieldBase.TypePropertyName])
                    ))
                };
            }
        }

        public static IEnumerable<object[]> KendoBooleanFieldToJsonCases
        {
            get
            {
                yield return new object[]
               {
                    new KendoBooleanField("active"),
                    ((Expression<Func<string, bool>>)(json =>
                        nameof(FieldType.Boolean).ToLower().Equals((string) JObject.Parse(json)[KendoFieldBase.TypePropertyName])
                    ))
               };
            }
        }

        public static IEnumerable<object[]> KendoDateFieldToJsonCases
        {
            get
            {
                yield return new object[]
                {
                    new KendoDateField("BirthDate") {
                                DefaultValue = new DateTime(1983, 6, 23).ToString("dd/MM/yyyy")
                            },

                    ((Expression<Func<string, bool>>)(json =>
                        "23/06/1983".Equals((string) JObject.Parse(json)[KendoFieldBase.DefaultValuePropertyName])
                        && nameof(FieldType.Date).ToLower().Equals((string) JObject.Parse(json)[KendoFieldBase.TypePropertyName])
                    ))
                };


                yield return new object[]
                {
                    new KendoDateField("BirthDate"),

                    ((Expression<Func<string, bool>>)(json =>
                        JObject.Parse(json).IsValid(KendoFieldBase.Schema(FieldType.Date))
                        && nameof(FieldType.Date).ToLower().Equals((string) JObject.Parse(json)[KendoFieldBase.TypePropertyName])
                    ))
                };

                yield return new object[]
                {
                    new KendoDateField("birthdate") {
                                DefaultValue = new DateTime(1983, 6, 23).ToString("dd/MM/yyyy"),
                                From = "item.birth_date"
                            },

                    ((Expression<Func<string, bool>>)(json =>
                        "23/06/1983".Equals((string) JObject.Parse(json)[KendoFieldBase.DefaultValuePropertyName])
                        && nameof(FieldType.Date).ToLower().Equals((string) JObject.Parse(json)[KendoFieldBase.TypePropertyName])
                        && "item.birth_date".Equals((string) JObject.Parse(json)[KendoFieldBase.FromPropertyName])
                    ))
                };

                yield return new object[]
                {
                    new KendoStringField("firstname")
                    {
                        DefaultValue = string.Empty,
                        From = "item.firstname"
                    },
                    ((Expression<Func<string, bool>>)(json =>
                        string.Empty == ((string) JObject.Parse(json)[KendoFieldBase.DefaultValuePropertyName])
                        && nameof(FieldType.String).ToLower().Equals((string) JObject.Parse(json)[KendoFieldBase.TypePropertyName])
                        && "item.firstname".Equals((string) JObject.Parse(json)[KendoFieldBase.FromPropertyName])
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


        public static IEnumerable<object[]> KendoDateFieldSchemaCases
        {
            get
            {

                yield return new object[]
                {
                    new KendoDateField("BirthDate") {
                                DefaultValue = new DateTime(1983, 6, 23).ToString("dd/MM/yyyy")
                            }, true
                };
            }
        }


        public KendoFieldTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Theory]
        [MemberData(nameof(KendoStringFieldToJsonCases))]
        [MemberData(nameof(KendoDateFieldToJsonCases))]
        [MemberData(nameof(KendoNumericFieldToJsonCases))]
        [MemberData(nameof(KendoBooleanFieldToJsonCases))]
        public void ToJson(KendoFieldBase kf, Expression<Func<string, bool>> jsonMatcher)
        {
            _output.WriteLine($"Testing : {kf}{Environment.NewLine} against {Environment.NewLine} {jsonMatcher} ");
            kf.ToJson().Should().Match(jsonMatcher);
        }



        [Theory]
        [MemberData(nameof(KendoDateFieldSchemaCases))]
        [MemberData(nameof(KendoStringFieldSchemaCases))]
        [MemberData(nameof(KendoNumericFieldSchemaCases))]
        [MemberData(nameof(KendoBooleanFieldSchemaCases))]
        public void Schema(KendoFieldBase kf, bool expectedValidity)
        {
            JSchema schema = KendoFieldBase.Schema(kf.Type);

            _output.WriteLine($"Testing :{kf} {Environment.NewLine} against {Environment.NewLine} {schema}");

            JObject.Parse(kf.ToJson())
                  .IsValid(schema)
                  .Should().Be(expectedValidity);
        }

    }
}
