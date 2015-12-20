using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Kendo.Helpers.Grid.Tests
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
            }
        }


        [Theory]
        [MemberData(nameof(Cases))]
        public void ToString(KendoModel schema, string expectedString)
            => schema.ToString().Should().Be(expectedString);
    }
}
