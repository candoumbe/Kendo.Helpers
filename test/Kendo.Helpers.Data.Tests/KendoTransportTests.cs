using FluentAssertions;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using Xunit;
using Newtonsoft.Json.Schema;
using System.Linq.Expressions;
using System;

namespace Kendo.Helpers.Data.Tests
{
    public class KendoTransportTests
    {


        public static IEnumerable<object[]> ToJsonCases
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
                    ((Expression<Func<string,bool>>)(json =>
                        "url/to/ressources/create".Equals((string)JObject.Parse(json)[KendoTransport.CreatePropertyName][KendoTransportOperation.UrlPropertyName])
                    ))
                };

                yield return new object[]
                {
                    new KendoTransport {
                        Read = new KendoTransportOperation()
                        {
                            Url = "url/to/ressources/read"
                        }
                    },
                    ((Expression<Func<string,bool>>)(json =>
                        "url/to/ressources/read".Equals((string)JObject.Parse(json)[KendoTransport.ReadPropertyName][KendoTransportOperation.UrlPropertyName])
                    ))
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
                    ((Expression<Func<string,bool>>)(json =>
                        "url/to/ressources/create".Equals((string)JObject.Parse(json)[KendoTransport.CreatePropertyName][KendoTransportOperation.UrlPropertyName]) &&
                        "POST".Equals((string)JObject.Parse(json)[KendoTransport.CreatePropertyName][KendoTransportOperation.TypePropertyName])  
                    ))
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
                    ((Expression<Func<string,bool>>)(json =>
                        "url/to/ressources/create".Equals((string)JObject.Parse(json)[KendoTransport.CreatePropertyName][KendoTransportOperation.UrlPropertyName]) &&
                        "POST".Equals((string)JObject.Parse(json)[KendoTransport.CreatePropertyName][KendoTransportOperation.TypePropertyName]) &&
                        "url/to/ressources/read".Equals((string)JObject.Parse(json)[KendoTransport.ReadPropertyName][KendoTransportOperation.UrlPropertyName])
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
                    new KendoTransport (),
                    false
               };

                yield return new object[]
                {
                    new KendoTransport {
                        Create = new KendoTransportOperation()
                        {
                            Url = "url/to/ressources/create"
                        }
                    },
                    true
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
                    true
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
                    true
                };


            }
        }

        [Theory]
        [MemberData(nameof(ToJsonCases))]
        public void ToJson(KendoTransport transport, Expression<Func<string, bool>> jsonMatcher)
            => transport.ToJson().Should().Match(jsonMatcher);

        [Theory]
        [MemberData(nameof(SchemaCases))]
        public void Schema(KendoTransport transport, bool expectedValidity)
            => JObject.Parse(transport.ToJson()).IsValid(KendoTransport.Schema)
            .Should().Be(expectedValidity);
    }
}
