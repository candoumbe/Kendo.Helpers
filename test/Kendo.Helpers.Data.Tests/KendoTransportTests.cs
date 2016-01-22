using FluentAssertions;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using Xunit;
using Newtonsoft.Json.Schema;
using System.Linq.Expressions;
using System;
using Xunit.Abstractions;

namespace Kendo.Helpers.Data.Tests
{
    public class KendoTransportTests
    {
        private readonly ITestOutputHelper _output;

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

                yield return new object[]
                {
                    new KendoTransport
                    {
                        Read = new KendoTransportOperation
                        {
                            Cache = false,
                            Url = "url/to/ressources/read"
                        },
                        Create = new KendoTransportOperation
                        {
                            Url = "url/to/ressources/create",
                            Type = "POST"
                        },
                        Update = new KendoTransportOperation
                        {
                            Url = "url/to/ressources/update",
                            Type = "PATCH"
                        },
                        Destroy = new KendoTransportOperation
                        {
                            Url = "url/to/ressources/delete",
                            Type = "DELETE"
                        }

                    },
                    ((Expression<Func<string,bool>>)(json =>

                        "url/to/ressources/read".Equals((string)JObject.Parse(json)[KendoTransport.ReadPropertyName][KendoTransportOperation.UrlPropertyName])
                        && !JObject.Parse(json)[KendoTransport.ReadPropertyName][KendoTransportOperation.CachePropertyName].Value<bool>() 
                        
                        && "url/to/ressources/create".Equals((string)JObject.Parse(json)[KendoTransport.CreatePropertyName][KendoTransportOperation.UrlPropertyName]) 
                        && "POST".Equals((string)JObject.Parse(json)[KendoTransport.CreatePropertyName][KendoTransportOperation.TypePropertyName])

                        && "url/to/ressources/update".Equals((string)JObject.Parse(json)[KendoTransport.UpdatePropertyName][KendoTransportOperation.UrlPropertyName])
                        && "PATCH".Equals((string)JObject.Parse(json)[KendoTransport.UpdatePropertyName][KendoTransportOperation.TypePropertyName])
                        
                        && "url/to/ressources/delete".Equals((string)JObject.Parse(json)[KendoTransport.DeletePropertyName][KendoTransportOperation.UrlPropertyName])
                        && "DELETE".Equals((string)JObject.Parse(json)[KendoTransport.DeletePropertyName][KendoTransportOperation.TypePropertyName])
                        
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


        public KendoTransportTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Theory]
        [MemberData(nameof(ToJsonCases))]
        public void ToJson(KendoTransport transport, Expression<Func<string, bool>> jsonMatcher)
        {
            _output.WriteLine($"Testing {transport}");
            transport.ToJson().Should().Match(jsonMatcher);
        }

        [Theory]
        [MemberData(nameof(SchemaCases))]
        public void Schema(KendoTransport transport, bool expectedValidity)
        {
            JSchema schema = KendoTransport.Schema;
            _output.WriteLine($"Validating {transport} against scheam");

            JObject.Parse(transport.ToJson()).IsValid(schema)
            .Should().Be(expectedValidity);
        }
    }
}
