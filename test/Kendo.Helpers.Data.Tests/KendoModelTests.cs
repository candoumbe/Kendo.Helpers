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
        public static IEnumerable<object[]> Cases
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
                        JObject.Parse(json).IsValid(KendoModel.Schema) &&
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
                        JObject.Parse(json).IsValid(KendoModel.Schema) &&
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
                        JObject.Parse(json).IsValid(KendoModel.Schema) &&
                        "Id".Equals((string) JObject.Parse(json)[KendoModel.IdPropertyName])
                    ))
                };
            }
        }


        [Theory]
        [MemberData(nameof(Cases))]
        public void ToJson(KendoModel schema, Expression<Func<string, bool>> jsonMatcher)
            => schema.ToJson().Should().Match(jsonMatcher);
    }
}
