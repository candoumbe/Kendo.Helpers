using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Kendo.Helpers.Data.Tests
{
    public class KendoTransportOperationTests
    {


        public static IEnumerable<object[]> Cases
        {
            get
            {
                yield return new object[]
                {
                    new KendoTransportOperation()
                    {
                        Url = "url/to/ressources/create"
                    },
                    @"{""url"":""url/to/ressources/create""}"
                };

                yield return new object[]
                {
                    new KendoTransportOperation()
                    {
                        Url = "url/to/ressources/delete",
                        Type ="POST"
                    },
                    @"{""url"":""url/to/ressources/delete"",""type"":""POST""}"
                };

                yield return new object[]
                {
                    new KendoTransportOperation()
                    {
                        Url = "url/to/ressources/delete",
                        ContentType = "json"
                    },
                    @"{""url"":""url/to/ressources/delete"",""contentType"":""json""}"
                };
            }
        }

       
        [Theory]
        [MemberData(nameof(Cases))]
        public void ToJson(KendoTransportOperation operation, string expectedResult)
            => operation.ToJson().Should().Be(expectedResult);
    }
}
