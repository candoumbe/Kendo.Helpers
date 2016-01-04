using FluentAssertions;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace Kendo.Helpers.Data.Tests
{
    public class KendoModelTests
    {

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
                        Fields = new KendoFieldBase[]
                        {
                            new KendoDateField("BirthDate") {
                                DefaultValue = new DateTime(1983, 6, 23).ToString("dd/MM/yyyy")
                            },

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
                    ))
                };
            }
        }


        [Theory]
        [MemberData(nameof(ToJsonCases))]
        public void ToJson(KendoModel schema, Expression<Func<string, bool>> jsonMatcher)
            => schema.ToJson().Should().Match(jsonMatcher);


        [Theory]
        [MemberData(nameof(SchemaCases))]
        public void Schema(KendoModel schema, bool expectedValidity)
            => JObject.Parse(schema.ToJson()).IsValid(KendoModel.Schema)
            .Should().Be(expectedValidity);
    }
}
