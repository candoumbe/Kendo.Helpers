using FluentAssertions;
using Microsoft.AspNet.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Kendo.Helpers.Data;
using Xunit;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;
using Newtonsoft.Json.Linq;

namespace Kendo.Helpers.Data.Tests
{
    public class KendoDataSourceTests
    {

        public KendoDataSourceTests()
        { }

        public static IEnumerable<object> KendoLocalDataSourceCases
        {
            get
            {


                yield return new object[]
                {
                    new KendoLocalDataSource { Data = new dynamic[] { new { firstname = "bruce" } } },
                    ((Expression<Func<string, bool>>)(json =>
                        "bruce".Equals((string) JObject.Parse(json)["data"][0]["firstname"])
                    ))
                };


                yield return new object[] {
                    new KendoLocalDataSource
                    {  Data = new dynamic[] {
                            new { firstname = "bruce", lastname="wayne", city="Gotham" },
                            new { firstname = "clark", lastname="kent", city="Metropolis" },
                        }
                    },
                     ((Expression<Func<string, bool>>)(json =>
                        "bruce".Equals((string) JObject.Parse(json)["data"][0]["firstname"]) &&
                        "wayne".Equals((string) JObject.Parse(json)["data"][0]["lastname"]) &&
                        "Gotham".Equals((string) JObject.Parse(json)["data"][0]["city"]) &&

                        "clark".Equals((string) JObject.Parse(json)["data"][1]["firstname"]) &&
                        "kent".Equals((string) JObject.Parse(json)["data"][1]["lastname"]) &&
                        "Metropolis".Equals((string) JObject.Parse(json)["data"][1]["city"])

                    ))
                };

                yield return new object[] {
                    new KendoLocalDataSource
                    {
                        Data = new dynamic[] {
                            new { firstname = "bruce", lastname="wayne", city="Gotham" },
                            new { firstname = "clark", lastname="kent", city="Metropolis" },
                        },
                        PageSize = 45,
                        Page = 4
                    },
                     ((Expression<Func<string, bool>>)(json =>
                        4.Equals((int) JObject.Parse(json)["page"]) &&
                        45.Equals((int) JObject.Parse(json)["pageSize"]) &&
                        "bruce".Equals((string) JObject.Parse(json)["data"][0]["firstname"]) &&
                        "wayne".Equals((string) JObject.Parse(json)["data"][0]["lastname"]) &&
                        "Gotham".Equals((string) JObject.Parse(json)["data"][0]["city"]) &&

                        "clark".Equals((string) JObject.Parse(json)["data"][1]["firstname"]) &&
                        "kent".Equals((string) JObject.Parse(json)["data"][1]["lastname"]) &&
                        "Metropolis".Equals((string) JObject.Parse(json)["data"][1]["city"])

                    ))
                };
            }
        }


        public static IEnumerable<object[]> LocalDataSourceSchemaCases
        {
            get
            {
                yield return new object[] { new KendoLocalDataSource(), false };
                yield return new object[] { new KendoLocalDataSource<dynamic>(), false };
                yield return new object[] { new KendoLocalDataSource<object>(), false };
                yield return new object[] { new KendoLocalDataSource() { Data = Enumerable.Empty<object>() }, true };
                yield return new object[] { new KendoLocalDataSource() { Data = null }, false };

            }
        }

         public static IEnumerable<object[]> RemoteDataSourceSchemaCases
        {
            get
            {
                yield return new object[] { new KendoRemoteDataSource(), false };
                
            }
        }

        public static IEnumerable<object> KendoRemoteDataSourceToJsonCases
        {
            get
            {



                yield return new object[] {
                    new KendoRemoteDataSource() {

                        Page = 1,
                        PageSize = 20,
                        Transport = new KendoTransport()
                        {
                            Create = new KendoTransportOperation()
                            {
                                Url = "api/resources/create"
                            },
                            Destroy = new KendoTransportOperation()
                            {
                                Url = "api/resources/delete"
                            }
                        }
                    },
                    ((Expression<Func<string, bool>>)(json =>
                        1.Equals((int) JObject.Parse(json)["page"]) &&
                        20.Equals((int) JObject.Parse(json)["pageSize"]) &&
                        "api/resources/create".Equals((string) JObject.Parse(json)["transport"]["create"]["url"]) &&
                        "api/resources/delete".Equals((string) JObject.Parse(json)["transport"]["destroy"]["url"])
                     ))
                };

                yield return new object[] {
                    new KendoRemoteDataSource() {
                        Transport = new KendoTransport()
                        {
                            Create = new KendoTransportOperation()
                            {
                                Url = "api/resources/create"
                            },
                        },
                        DataSchema = new KendoSchema
                        {
                            Model = new KendoModel()
                            {
                                Id = "Id"
                            }
                        }
                    },
                    ((Expression<Func<string, bool>>)(json =>
                        "api/resources/create".Equals((string) JObject.Parse(json)["transport"]["create"]["url"]) &&
                        "Id".Equals((string) JObject.Parse(json)["schema"]["model"]["id"])
                     ))
                };
            }
        }


        [Theory]
        [MemberData(nameof(KendoRemoteDataSourceToJsonCases))]
        [MemberData(nameof(KendoLocalDataSourceCases))]
        public void ToJson(IKendoDataSource dataSource, Expression<Func<string, bool>> jsonMatcher)
            => dataSource.ToJson().Should().Match(jsonMatcher);

        [Theory]
        [MemberData(nameof(LocalDataSourceSchemaCases))]
        public void LocalDataSourceSchema(IKendoDataSource dataSource, bool expectedValidity)
            => JObject.Parse(dataSource.ToJson()).IsValid(KendoLocalDataSource.Schema)
            .Should().Be(expectedValidity);


        [Theory]
        [MemberData(nameof(RemoteDataSourceSchemaCases))]
        public void RemoteDataSourceSchema(KendoRemoteDataSource dataSource, bool expectedValidity)
            => JObject.Parse(dataSource.ToJson()).IsValid(KendoRemoteDataSource.Schema)
            .Should().Be(expectedValidity);

    }
}
