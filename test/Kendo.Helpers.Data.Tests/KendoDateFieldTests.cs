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
using static Newtonsoft.Json.JsonConvert;

namespace Kendo.Helpers.Data.Tests
{
    public class KendoDateFieldTests : IDisposable
    {
        private ITestOutputHelper _output;

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

        public static IEnumerable<object[]> DeserializeCases
        {
            get
            {
                yield return new object[]
                {
                    "{'BirthDate':{ type : 'date'}}",
                    ((Expression<Func<KendoDateField, bool>>) ((kf) => kf.Name == "BirthDate" && kf.DefaultValue == null 
                        && kf.Editable == null
                        && kf.From == null))
                };

                yield return new object[]
                {
                    "{'BirthDate':{ type : 'date', editable : false}}",
                    ((Expression<Func<KendoDateField, bool>>) ((kf) => 
                        kf.Name == "BirthDate" 
                        && kf.DefaultValue == null
                        && kf.Editable.HasValue && !kf.Editable.Value
                        && kf.From == null))
                };

                yield return new object[]
                {
                    "{'BirthDate':{ type : 'date', editable : false, defaultValue : '1983-06-23'}}",
                    ((Expression<Func<KendoDateField, bool>>) ((kf) => 
                        kf.Name == "BirthDate"
                        &&  kf.DefaultValue is string && Convert.ToString(kf.DefaultValue) == "1983-06-23"
                        && kf.Editable.HasValue && !kf.Editable.Value
                        && kf.From == null))
                };

                yield return new object[]
                {
                    "{'BirthDate':{ type : 'date', editable : true, defaultValue : '1983-06-23'}}",
                    ((Expression<Func<KendoDateField, bool>>) ((kf) => 
                        kf.Name == "BirthDate"
                        && kf.DefaultValue is string && Convert.ToString(kf.DefaultValue) == "1983-06-23"
                        && kf.Editable.HasValue && kf.Editable.Value
                        && kf.From == null))
                };
            }
        }


        public static IEnumerable<object[]> DeserializeToObjectCases
        {
            get
            {
                yield return new object[]
                {
                    "{'BirthDate':{ type : 'date'}}",
                    typeof(KendoDateField),
                    ((Expression<Func<object, bool>>) ((kf) => kf is KendoDateField 
                        && ((KendoDateField)kf).Name == "BirthDate"
                        && ((KendoDateField)kf).DefaultValue == null
                        && ((KendoDateField)kf).Editable == null
                        && ((KendoDateField)kf).From == null))
                };

                

                yield return new object[]
                {
                    "{'BirthDate':{ type : 'date', editable : false}}",
                    typeof(KendoDateField),
                    ((Expression<Func<object, bool>>) ((kf) => kf is KendoDateField
                        && ((KendoDateField)kf).Name == "BirthDate" 
                        && ((KendoDateField)kf).DefaultValue == null
                        && ((KendoDateField)kf).Editable.HasValue && !((KendoDateField)kf).Editable.Value
                        && ((KendoDateField)kf).From == null))
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

            string json = kf.ToJson();
            _output.WriteLine($"Result : {json}");
            json.Should().Match(resultMatcher);
        }
        
        [Theory]
        [MemberData(nameof(DeserializeCases))]
        public void Deserialize(string json, Expression<Func<KendoDateField, bool>> deserializationExpectation)
        {
            _output.WriteLine($"Deserializing {json}");
            _output.WriteLine($"Expectation : {deserializationExpectation}");

            KendoDateField kdf = DeserializeObject<KendoDateField>(json);
            _output.WriteLine($"Result : {kdf}");

            kdf.Should().Match(deserializationExpectation);
        }

        [Theory]
        [MemberData(nameof(DeserializeToObjectCases))]
        public void DeserializeToObject(string json, Type type, Expression<Func<object, bool>> deserializationExpectation)
        {
            _output.WriteLine($"Deserializing {json}");
            _output.WriteLine($"Expectation : {deserializationExpectation}");

            object kdf = DeserializeObject(json, type);

            _output.WriteLine($"Result : {kdf}");

            kdf.Should().Match(deserializationExpectation);
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


        public void Dispose()
        {
            _output = null;
        }

    }
}
