using FluentAssertions;
using Kendo.Helpers.Grid;
using Microsoft.AspNet.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace Kendo.Helpers.Grid.Tests
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
                    "{}"
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
        public void ToString(KendoRemoteDataSource dataSource, string expectedResult)
            => ToString((KendoDataSource)dataSource, expectedResult);

        [Theory]
        [MemberData(nameof(KendoLocalDataSourceCases))]
        public void ToString(KendoLocalDataSource dataSource, string expectedResult)
            => ToString((KendoDataSource) dataSource, expectedResult);

        private void ToString(KendoDataSource dataSource, string expectedResult)
            => dataSource.ToString().Should().Be(expectedResult);


    }
}
