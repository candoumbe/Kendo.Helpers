using FluentAssertions;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using Xunit;
using Newtonsoft.Json.Schema;
using System.Linq.Expressions;
using System;
using Xunit.Abstractions;
using System.Linq;

namespace Kendo.Helpers.Data.Tests
{
    public class KendoTransportTests : IDisposable
    {
        private ITestOutputHelper _output;

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
                        JToken.Parse(json).Type == JTokenType.Object
                        && JObject.Parse(json).Properties().Count() == 1
                        && JObject.Parse(json)[KendoTransport.CreatePropertyName].IsValid(KendoTransportOperation.Schema)
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
                        JToken.Parse(json).Type == JTokenType.Object
                        && JObject.Parse(json).Properties().Count() == 1
                        && JObject.Parse(json)[KendoTransport.ReadPropertyName].IsValid(KendoTransportOperation.Schema)
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
                        JToken.Parse(json).Type == JTokenType.Object
                        && JObject.Parse(json).Properties().Count() == 1
                        && JObject.Parse(json)[KendoTransport.CreatePropertyName].IsValid(KendoTransportOperation.Schema)
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
                        JToken.Parse(json).Type == JTokenType.Object
                        && JObject.Parse(json).Properties().Count() == 2
                        && JObject.Parse(json)[KendoTransport.CreatePropertyName].IsValid(KendoTransportOperation.Schema)
                        && JObject.Parse(json)[KendoTransport.ReadPropertyName].IsValid(KendoTransportOperation.Schema)

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
                        JToken.Parse(json).Type == JTokenType.Object
                        && JObject.Parse(json).Properties().Count() == 4
                        && JObject.Parse(json)[KendoTransport.CreatePropertyName].IsValid(KendoTransportOperation.Schema)
                        && JObject.Parse(json)[KendoTransport.ReadPropertyName].IsValid(KendoTransportOperation.Schema)
                        && JObject.Parse(json)[KendoTransport.UpdatePropertyName].IsValid(KendoTransportOperation.Schema)
                        && JObject.Parse(json)[KendoTransport.DeletePropertyName].IsValid(KendoTransportOperation.Schema)
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
            _output.WriteLine($"Matcher {jsonMatcher}");

            transport.ToJson().Should().Match(jsonMatcher);
        }

        [Theory]
        [MemberData(nameof(SchemaCases))]
        public void Schema(KendoTransport transport, bool expectedValidity)
        {

            JSchema schema = KendoTransport.Schema;

            _output.WriteLine($"Validating {transport}");
           

            JObject.Parse(transport.ToJson()).IsValid(schema)
                .Should().Be(expectedValidity);
        }


        public void Dispose()
        {
            _output = null;
        }
    }
}
