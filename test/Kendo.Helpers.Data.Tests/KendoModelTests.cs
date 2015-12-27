using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
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
                    @"{""id"":""Id"",""fields"":{""Firstname"":{}}}"
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
                    @"{""id"":""Id"",""fields"":{""BirthDate"":{""type"":""date""}}}"
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
                    @"{""id"":""Id"",""fields"":{""BirthDate"":{""type"":""date"",""defaultValue"":""23/06/1983""}}}"
                };
            }
        }


        [Theory]
        [MemberData(nameof(Cases))]
        public void ToJson(KendoModel schema, string expectedString)
            => schema.ToJson().Should().Be(expectedString);
    }
}
