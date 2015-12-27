using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Kendo.Helpers.Data.Tests
{
    public class KendoTransportTests
    {


        public static IEnumerable<object[]> Cases
        {
            get
            {
                yield return new object[]
                {
                    new KendoTransport {
                        Create = new KendoTransportOperation()
                        {
                            Url = "url/to/ressources/create"
                        }
                    },
                    @"{""create"":{""url"":""url/to/ressources/create""}}"
                };

                yield return new object[]
                {
                    new KendoTransport {
                        Create = new KendoTransportOperation()
                        {
                            Url = "url/to/ressources/create",
                            Type = "POST"
                        }
                    },
                    @"{""create"":{""url"":""url/to/ressources/create"",""type"":""POST""}}"
                };

                yield return new object[]
                {
                    new KendoTransport {
                        Create = new KendoTransportOperation()
                        {
                            Url = "url/to/ressources/create",
                            Type = "POST"
                        },
                        Read = new KendoTransportOperation()
                        {
                            Url = "url/to/ressources/read",
                        }

                    },
                    @"{""create"":{""url"":""url/to/ressources/create"",""type"":""POST""},""read"":{""url"":""url/to/ressources/read""}}"
                };


            }
        }


        [Theory]
        [MemberData(nameof(Cases))]
        public void ToJson(KendoTransport transport, string expectedResult)
            => transport.ToJson().Should().Be(expectedResult);
    }
}
