using FluentAssertions;
using Kendo.Helpers.Data;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Xunit;

namespace Kendo.Helpers.UI.Grid.Tests
{
    public class FieldColumnFilterableConfigurationTests
    {
        public static IEnumerable<object[]> SchemaCases
        {
            get
            {
                yield return new object[]
                {
                    new FieldColumnFilterableConfiguration(), false
                };

                yield return new object[]
                {
                    new FieldColumnFilterableConfiguration {
                        Multi = false
                    },
                    true
                };

                yield return new object[]
                {
                    new FieldColumnFilterableConfiguration
                    {
                        Multi = false,
                        DataSource = new KendoLocalDataSource<string>()
                        {
                            Data = new []{ "FR", "EN", "ES" }
                        }
                    },
                    true
                };

                yield return new object[]
                {
                    new FieldColumnFilterableConfiguration
                    {
                        Multi = false,
                        DataSource = new KendoRemoteDataSource
                        {
                            Transport = new KendoTransport
                            {
                                Read = new KendoTransportOperation
                                {
                                    Url = "api/resources/"
                                }
                            }
                        }
                    },
                    true
                };
            }
        }

        public static IEnumerable<object[]> ToJsonCases
        {
            get
            {
                
                yield return new object[]
                {
                    new FieldColumnFilterableConfiguration {
                        Multi = false
                    },
                    ((Expression<Func<string, bool>>)(json =>
                        !(bool)JObject.Parse(json)[FieldColumnFilterableConfiguration.MultiPropertyName]
                    ))
                };

                yield return new object[]
                {
                    new FieldColumnFilterableConfiguration
                    {
                        Multi = false,
                        DataSource = new KendoLocalDataSource<string>()
                        {
                            Data = new []{ "FR", "EN", "ES" }
                        },
                    },
                    ((Expression<Func<string, bool>>)(json =>
                        !(bool)JObject.Parse(json)[FieldColumnFilterableConfiguration.MultiPropertyName] &&
                        JObject.Parse(JObject.Parse(json)[FieldColumnFilterableConfiguration.DataSourcePropertyName].ToString())
                            .IsValid(KendoLocalDataSource<string>.Schema)
                    ))
                };

                yield return new object[]
                {
                    new FieldColumnFilterableConfiguration
                    {
                        Multi = false,
                        DataSource = new KendoRemoteDataSource
                        {
                            Transport = new KendoTransport
                            {
                                Read = new KendoTransportOperation
                                {
                                    Url = "api/resources/"
                                }
                            }
                        }
                    },
                    ((Expression<Func<string, bool>>)(json =>
                        !(bool)JObject.Parse(json)[FieldColumnFilterableConfiguration.MultiPropertyName] &&
                         JObject.Parse(JObject.Parse(json)[FieldColumnFilterableConfiguration.DataSourcePropertyName].ToString())
                            .IsValid(KendoRemoteDataSource.Schema)
                    ))
                };
            }
        }

        [Theory]
        [MemberData(nameof(ToJsonCases))]
        public void ToJson(FieldColumnFilterableConfiguration configuration, Expression<Func<string, bool>> jsonMatcher)
            => configuration.ToJson().Should().Match(jsonMatcher);


        [Theory]
        [MemberData(nameof(SchemaCases))]
        public void Schema(FieldColumnFilterableConfiguration configuration, bool expectedValidity)
            => JObject.Parse(configuration.ToJson()).IsValid(FieldColumnFilterableConfiguration.Schema)
            .Should().Be(expectedValidity);
    }
}
