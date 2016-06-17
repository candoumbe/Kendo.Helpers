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
    public class KendoDateFieldTests
    {
        private readonly ITestOutputHelper _output;

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
                        "23/06/1983".Equals((string) JObject.Parse(json)["BirthDate"][KendoFieldBase.DefaultValuePropertyName])
                        && nameof(FieldType.Date).ToLower().Equals((string) JObject.Parse(json)["BirthDate"][KendoFieldBase.TypePropertyName])
                    ))
                };


                yield return new object[]
                {
                    new KendoDateField("BirthDate"),

                    ((Expression<Func<string, bool>>)(json =>
                        JObject.Parse(json).IsValid(KendoFieldBase.Schema("BirthDate", FieldType.Date))
                        && nameof(FieldType.Date).ToLower().Equals((string) JObject.Parse(json)["BirthDate"][KendoFieldBase.TypePropertyName])
                    ))
                };

                yield return new object[]
                {
                    new KendoDateField("BirthDate") {
                                DefaultValue = new DateTime(1983, 6, 23).ToString("dd/MM/yyyy"),
                                From = "item.birth_date"
                            },

                    ((Expression<Func<string, bool>>)(json =>
                        "23/06/1983".Equals((string) JObject.Parse(json)["BirthDate"][KendoFieldBase.DefaultValuePropertyName])
                        && nameof(FieldType.Date).ToLower().Equals((string) JObject.Parse(json)["BirthDate"][KendoFieldBase.TypePropertyName])
                        && "item.birth_date".Equals((string) JObject.Parse(json)["BirthDate"][KendoFieldBase.FromPropertyName])
                    ))
                };

                
            }
        }
        

        public static IEnumerable<object[]> KendoDateFieldSchemaCases
        {
            get
            {

                IEnumerable<KendoDateField> fieldsWithValidSchema = new[]
                {
                    new KendoDateField("BirthDate") { DefaultValue = new DateTime(1983, 6, 23).ToString("yyyy-MM-dd") },
                    new KendoDateField("BirthDate"),
                    new KendoDateField("BirthDate") {
                                DefaultValue = new DateTime(1983, 6, 23).ToString("dd/MM/yyyy"),
                                From = "item.birth_date"
                            }
                };

                foreach (var item in fieldsWithValidSchema)
                {
                    yield return new object[] { item, true };
                }

                
            }
        }


        public KendoDateFieldTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Theory]
        [MemberData(nameof(KendoDateFieldToJsonCases))]
        public void ToJson(KendoFieldBase kf, Expression<Func<string, bool>> resultMatcher)
        {
            _output.WriteLine($"Testing : {kf}{Environment.NewLine} against {Environment.NewLine} {resultMatcher} ");
            kf.ToJson().Should().Match(resultMatcher);
        }


        
        [Theory]
        [MemberData(nameof(KendoDateFieldSchemaCases))]
        public void Schema(KendoFieldBase kf, bool expectedValidity)
        {
            JSchema schema = KendoFieldBase.Schema(kf.Name, kf.Type);

            _output.WriteLine($"Validating:{kf}");
            _output.WriteLine($"Schema: {schema}");

            JObject.Parse(kf.ToJson())
                  .IsValid(schema)
                  .Should().Be(expectedValidity);
        }

    }
}
