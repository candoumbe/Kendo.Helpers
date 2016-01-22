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

namespace Kendo.Helpers.Data.Tests
{
    public class KendoModelTests
    {
        private readonly ITestOutputHelper _output;

        public static IEnumerable<object[]> SchemaCases
        {
            get
            {
                yield return new object[]
                {
                    new KendoModel(), false
                };


                yield return new object[]
                {
                    new KendoModel()
                    {
                        Id = "Id",
                        Fields = new KendoFieldBase[]
                        {
                            new KendoStringField("Firstname")
                        }
                    },
                    true
                };

                yield return new object[]
                {
                    new KendoModel()
                    {
                        Id = "Id",
                        Fields = new KendoFieldBase[]
                        {
                            new KendoDateField("BirthDate"),
                        }
                    },
                    true
                };

                yield return new object[]
                {
                    new KendoModel()
                    {
                        Id = "Id",
                        Fields = Enumerable.Empty<KendoFieldBase>()
                    },
                    true
                };

                yield return new object[]
                {
                    new KendoModel()
                    {
                        Id = "Id",
                        Fields = new KendoFieldBase[]
                        {
                            new KendoDateField("BirthDate") {
                                DefaultValue = new DateTime(1983, 6, 23).ToString("dd/MM/yyyy")
                            },

                        }
                    },
                    true
                };

                yield return new object[]
                {
                   new KendoModel
                   {
                        Id = "item.id",
                        Fields = new KendoFieldBase[]
                        {
                            new KendoStringField("item.firstname") { DefaultValue = string.Empty, Editable=true, Nullable= false },
                            new KendoStringField("item.lastname") { DefaultValue = string.Empty, Editable=true, Nullable= false },
                            new KendoDateField("item.birth_date") { Editable=true, Nullable= false },
                            new KendoStringField("item.birth_place") { DefaultValue = string.Empty, Editable=true, Nullable= false }
                        }
                    },
                    true
                };
            }
        }

        public static IEnumerable<object[]> ToJsonCases
        {
            get
            {
                yield return new object[]
                {
                    new KendoModel()
                    {
                        Id = "Id",
                        Fields = new KendoFieldBase[]
                        {
                            new KendoStringField("Firstname")
                        }
                    },
                    ((Expression<Func<string, bool>>)(json =>
                        "Id".Equals((string) JObject.Parse(json)[KendoModel.IdPropertyName]) 
                        && JObject.Parse(json)[KendoModel.FieldsPropertyName]["Firstname"].IsValid(KendoFieldBase.Schema(FieldType.String))
                    ))
                };

                yield return new object[]
                {
                    new KendoModel()
                    {
                        Id = "Id",
                        Fields = new KendoFieldBase[]
                        {
                            new KendoDateField("BirthDate"),
                        }
                    },
                    ((Expression<Func<string, bool>>)(json =>
                        "Id".Equals((string) JObject.Parse(json)[KendoModel.IdPropertyName])
                        && JObject.Parse(json)[KendoModel.FieldsPropertyName]["BirthDate"].IsValid(KendoFieldBase.Schema(FieldType.Date))
                    ))
                };

                yield return new object[]
                {
                    new KendoModel()
                    {
                        Id = "Id",
                        Fields = new KendoFieldBase[]
                        {
                            new KendoDateField("BirthDate") {
                                DefaultValue = new DateTime(1983, 6, 23).ToString("dd/MM/yyyy")
                            },

                        }
                    },
                    ((Expression<Func<string, bool>>)(json => 
                        "Id".Equals((string) JObject.Parse(json)[KendoModel.IdPropertyName])
                        && JObject.Parse(json)[KendoModel.FieldsPropertyName]["BirthDate"].IsValid(KendoFieldBase.Schema(FieldType.Date))
                    ))
                };


                yield return new object[]
                {
                    new KendoModel {
                        Id = "item.id",
                        Fields = new KendoFieldBase[]
                        {
                            new KendoStringField("item.firstname") { DefaultValue = string.Empty, Editable=true, Nullable= false },
                            new KendoStringField("item.lastname") { DefaultValue = string.Empty, Editable=true, Nullable= false },
                            new KendoDateField("item.birth_date") { Editable=true, Nullable= false },
                            new KendoStringField("item.birth_place") { DefaultValue = string.Empty, Editable=true, Nullable= false }
                        }
                    },

                    ((Expression<Func<string, bool>>)(json =>
                        "item.id".Equals((string) JObject.Parse(json)[KendoModel.IdPropertyName])
                        
                        && JObject.Parse(json)[KendoModel.FieldsPropertyName]["item.firstname"].IsValid(KendoFieldBase.Schema(FieldType.String)) 
                        && nameof(FieldType.String).ToLower().Equals(JObject.Parse(json)[KendoModel.FieldsPropertyName]["item.firstname"][KendoFieldBase.TypePropertyName].Value<string>())
                        
                        && JObject.Parse(json)[KendoModel.FieldsPropertyName]["item.lastname"].IsValid(KendoFieldBase.Schema(FieldType.String))
                        && nameof(FieldType.String).ToLower().Equals(JObject.Parse(json)[KendoModel.FieldsPropertyName]["item.lastname"][KendoFieldBase.TypePropertyName].Value<string>())
                        
                        && JObject.Parse(json)[KendoModel.FieldsPropertyName]["item.birth_date"].IsValid(KendoFieldBase.Schema(FieldType.Date))
                        && nameof(FieldType.Date).ToLower().Equals(JObject.Parse(json)[KendoModel.FieldsPropertyName]["item.birth_date"][KendoFieldBase.TypePropertyName].Value<string>())
                        
                        && JObject.Parse(json)[KendoModel.FieldsPropertyName]["item.birth_place"].IsValid(KendoFieldBase.Schema(FieldType.String))
                        && nameof(FieldType.String).ToLower().Equals(JObject.Parse(json)[KendoModel.FieldsPropertyName]["item.birth_place"][KendoFieldBase.TypePropertyName].Value<string>())

                    ))
                };
            }
        }


        public KendoModelTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Theory]
        [MemberData(nameof(ToJsonCases))]
        public void ToJson(KendoModel schema, Expression<Func<string, bool>> jsonMatcher)
        {
            _output.WriteLine($"Testing {schema} against {jsonMatcher}");
            schema.ToJson().Should().Match(jsonMatcher);
        }


        [Theory]
        [MemberData(nameof(SchemaCases))]
        public void Schema(KendoModel schema, bool expectedValidity)
        {

            _output.WriteLine($"Validating {schema} {Environment.NewLine} against {Environment.NewLine} {KendoModel.Schema}");

            JObject.Parse(schema.ToJson()).IsValid(KendoModel.Schema)
              .Should().Be(expectedValidity);
        }
    }
}
