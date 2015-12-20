using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Kendo.Helpers.Grid.Tests
{
    public class KendoSchemaTests
    {
        public static IEnumerable<object[]> Cases
        {
            get
            {
                yield return new object[]
                {
                    new KendoSchema {
                        Model  = new KendoModel()
                        {
                            Id = "Id",
                            Fields = new KendoFieldBase[]
                            {
                                new KendoStringField("Firstname")
                            }
                        }
                    },
                    @"{""model"":{""id"":""Id"",""fields"":{""Firstname"":{}}}}"
                };


                yield return new object[]
                {
                    new KendoSchema {
                        Model  = new KendoModel()
                        {
                            Id = "Id",
                            Fields = new KendoFieldBase[]
                            {
                                new KendoStringField("Firstname")
                            }
                        }
                    },
                    @"{""model"":{""id"":""Id"",""fields"":{""Firstname"":{}}}}"
                };
            }
        }


        [Theory]
        [MemberData(nameof(Cases))]
        public void ToString(KendoSchema schema, string expectedString)
            => schema.ToString().Should().Be(expectedString);
    }
}
