using FluentAssertions;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;
using Newtonsoft.Json.Schema;

namespace Kendo.Helpers.Data.Tests
{
    public class KendoTransportOperationTests
    {


        public static IEnumerable<object[]> ToJsonCases
        {
            get
            {
                yield return new object[]
                {
                    new KendoTransportOperation()
                    {
                        Url = "url/to/ressources/create"
                    },
                    ((Expression<Func<string,bool>>)(json =>
                        "url/to/ressources/create".Equals((string)JObject.Parse(json)[KendoTransportOperation.UrlPropertyName])
                    ))
                };

                yield return new object[]
                {
                    new KendoTransportOperation()
                    {
                        Url = "url/to/ressources/delete",
                        Type ="POST"
                    },
                    ((Expression<Func<string,bool>>)(json =>
                        "url/to/ressources/delete".Equals((string)JObject.Parse(json)[KendoTransportOperation.UrlPropertyName]) &&
                        "POST".Equals((string)JObject.Parse(json)[KendoTransportOperation.TypePropertyName])
                    ))
                };

                yield return new object[]
                {
                    new KendoTransportOperation()
                    {
                        Url = "url/to/ressources/delete",
                        ContentType = "json"
                    },
                    ((Expression<Func<string,bool>>)(json =>
                        "url/to/ressources/delete".Equals((string)JObject.Parse(json)[KendoTransportOperation.UrlPropertyName]) &&
                        "json".Equals((string)JObject.Parse(json)[KendoTransportOperation.ContentTypePropertyName])
                    ))
                };
            }
        }


        public static IEnumerable<object[]> SchemaCases
        {
            get
            {
                
                yield return new object[]
                {
                    new KendoTransportOperation()
                    {
                        Url = "url/to/ressources/create"
                    },
                    true
                };

                yield return new object[]
                {
                    new KendoTransportOperation()
                    {
                        Cache = true,
                        Url = "url/to/ressources/read"
                    },
                    true
                };

                yield return new object[]
                {
                    new KendoTransportOperation()
                    {
                        Url = "url/to/ressources/delete",
                        Type ="POST"
                    },
                    true
                };

                yield return new object[]
                {
                    new KendoTransportOperation()
                    {
                        Url = "url/to/ressources/delete",
                        ContentType = "json"
                    },
                    true
                };
            }
        }

        [Theory]
        [MemberData(nameof(SchemaCases))]
        public void Schema(KendoTransportOperation configuration, bool expectedValidity)
            => JObject.Parse(configuration.ToJson()).IsValid(KendoTransportOperation.Schema).Should().Be(expectedValidity);

        [Theory]
        [MemberData(nameof(ToJsonCases))]
        public void ToJson(KendoTransportOperation operation, Expression<Func<string, bool>> jsonMatcher)
            => operation.ToJson().Should().Match(jsonMatcher);
    }
}
