using FluentAssertions;
using Microsoft.AspNet.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Kendo.Helpers.Data;
using Xunit;

namespace Kendo.Helpers.Data.Tests
{
    public class KendoDataSourceTests
    {

        public KendoDataSourceTests()
        {}

        public static IEnumerable<object> KendoLocalDataSourceCases
        {
            get
            {
                yield return new object[] {
                    new KendoLocalDataSource(Enumerable.Empty<object>()),
                    "[]"
                };

                yield return new object[]
                {
                    new KendoLocalDataSource(new dynamic[] { new { firstname = "bruce" } }),
                    @"[{""firstname"":""bruce""}]"
                };


                yield return new object[] {
                    new KendoLocalDataSource(new dynamic[] {
                            new { firstname = "bruce", lastname="wayne", city="Gotham" },
                            new { firstname = "clark", lastname="kent", city="Metropolis" },
                        }),
                    @"[{""firstname"":""bruce"",""lastname"":""wayne"",""city"":""Gotham""},{""firstname"":""clark"",""lastname"":""kent"",""city"":""Metropolis""}]"
                };                  
            }
        }


        public static IEnumerable<object> KendoRemoteDataSourceCases
        {
            get
            {
                yield return new object[] {
                    new KendoRemoteDataSource(),
                    "{}"
                };
                yield return new object[] {
                    new KendoRemoteDataSource() {
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
                    @"{""transport"":{""create"":{""url"":""api/resources/create""},""destroy"":{""url"":""api/resources/delete""}}}"
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
                        Schema = new KendoSchema
                        {
                            Model = new KendoModel()
                            {
                                Id = "Id"
                            }
                        }
                    },
                    @"{""transport"":{""create"":{""url"":""api/resources/create""}},""schema"":{""model"":{""id"":""Id""}}}"
                };
            }
        }

        [Theory]
        [MemberData(nameof(KendoRemoteDataSourceCases))]
        public void ToJson(KendoRemoteDataSource dataSource, string expectedResult)
            => ToJson((KendoDataSource)dataSource, expectedResult);

        [Theory]
        [MemberData(nameof(KendoLocalDataSourceCases))]
        public void ToJson(KendoLocalDataSource dataSource, string expectedResult)
            => ToJson((KendoDataSource) dataSource, expectedResult);

        private void ToJson(KendoDataSource dataSource, string expectedResult)
            => dataSource.ToJson().Should().Be(expectedResult);


    }
}
