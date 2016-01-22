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
using Xunit.Abstractions;

namespace Kendo.Helpers.Data.Tests
{
    public class KendoDataSourceTests
    {
        private readonly ITestOutputHelper _output;


        public static IEnumerable<object> LocalDataSourceToJsonCases
        {
            get
            {


                yield return new object[]
                {
                    new KendoLocalDataSource { Data = new dynamic[] { new { firstname = "bruce" } } },
                    ((Expression<Func<string, bool>>)(json =>
                        "bruce".Equals((string) JObject.Parse(json)[KendoLocalDataSource.DataPropertyName][0]["firstname"])
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
                        "bruce".Equals((string) JObject.Parse(json)[KendoLocalDataSource.DataPropertyName][0]["firstname"]) &&
                        "wayne".Equals((string) JObject.Parse(json)[KendoLocalDataSource.DataPropertyName][0]["lastname"]) &&
                        "Gotham".Equals((string) JObject.Parse(json)[KendoLocalDataSource.DataPropertyName][0]["city"]) &&

                        "clark".Equals((string) JObject.Parse(json)[KendoLocalDataSource.DataPropertyName][1]["firstname"]) &&
                        "kent".Equals((string) JObject.Parse(json)[KendoLocalDataSource.DataPropertyName][1]["lastname"]) &&
                        "Metropolis".Equals((string) JObject.Parse(json)[KendoLocalDataSource.DataPropertyName][1]["city"])

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
                        4.Equals((int) JObject.Parse(json)[KendoLocalDataSource.PagePropertyName]) &&
                        45.Equals((int) JObject.Parse(json)[KendoLocalDataSource.PageSizePropertyName]) &&
                        "bruce".Equals((string) JObject.Parse(json)[KendoLocalDataSource.DataPropertyName][0]["firstname"]) &&
                        "wayne".Equals((string) JObject.Parse(json)[KendoLocalDataSource.DataPropertyName][0]["lastname"]) &&
                        "Gotham".Equals((string) JObject.Parse(json)[KendoLocalDataSource.DataPropertyName][0]["city"]) &&

                        "clark".Equals((string) JObject.Parse(json)[KendoLocalDataSource.DataPropertyName][1]["firstname"]) &&
                        "kent".Equals((string) JObject.Parse(json)[KendoLocalDataSource.DataPropertyName][1]["lastname"]) &&
                        "Metropolis".Equals((string) JObject.Parse(json)[KendoLocalDataSource.DataPropertyName][1]["city"])

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

        public static IEnumerable<object> RemoteDataSourceToJsonCases
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
                        1.Equals((int) JObject.Parse(json)[KendoRemoteDataSource.PagePropertyName]) &&
                        20.Equals((int) JObject.Parse(json)[KendoRemoteDataSource.PageSizePropertyName]) &&
                        "api/resources/create".Equals((string) JObject.Parse(json)[KendoRemoteDataSource.TransportPropertyName][KendoTransport.CreatePropertyName][KendoTransportOperation.UrlPropertyName]) &&
                        "api/resources/delete".Equals((string) JObject.Parse(json)[KendoRemoteDataSource.TransportPropertyName][KendoTransport.DeletePropertyName][KendoTransportOperation.UrlPropertyName])
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
                        "api/resources/create".Equals((string) JObject.Parse(json)[KendoRemoteDataSource.TransportPropertyName][KendoTransport.CreatePropertyName][KendoTransportOperation.UrlPropertyName]) &&
                        "Id".Equals((string) JObject.Parse(json)[KendoRemoteDataSource.SchemaPropertyName][KendoSchema.ModelPropertyName][KendoModel.IdPropertyName])
                     ))
                };


                yield return new object[]
                {
                    new KendoRemoteDataSource
                    {
                        Transport = new KendoTransport
                        {
                            Read = new KendoTransportOperation
                            {
                                Cache = false,
                                Url = "api/patients/",
                                Type = "GET"
                            },
                            Create = new KendoTransportOperation
                            {
                                Url = "api/patients/create",
                                Type = "POST"
                            },
                            Update = new KendoTransportOperation
                            {
                                Url = "api/patients/patch/1",
                                Type = "PATCH"
                            },
                            Destroy = new KendoTransportOperation
                            {
                                Url = "api/patients/delete/1",
                                Type = "DELETE"
                            }

                        },
                        DataSchema = new KendoSchema
                        {
                            Model = new KendoModel
                            {
                                Id = "id",
                                Fields = new KendoFieldBase[]
                                {
                                    new KendoStringField("firstname") { DefaultValue = string.Empty, Editable=true, Nullable= false },
                                    new KendoStringField("lastname") { DefaultValue = string.Empty, Editable=true, Nullable= false },
                                    new KendoDateField("birth_date") { Editable=true, Nullable= false },
                                    new KendoStringField("birth_place") { DefaultValue = string.Empty, Editable=true, Nullable= false }
                                }
                            },
                            Data = "items",
                            Total = "count",

                        }
                    },
                    ((Expression<Func<string, bool>>)(json =>

                        JObject.Parse(json).IsValid(KendoRemoteDataSource.Schema)
                        &&JObject.Parse(json).Properties().Count() == 2 
                        && JObject.Parse(json).Properties().Count(prop => prop.Name ==  KendoRemoteDataSource.TransportPropertyName) == 1
                        && JObject.Parse(json).Properties().Count(prop => prop.Name ==  KendoRemoteDataSource.SchemaPropertyName) == 1

                        // checking 
                        && "api/patients/".Equals((string)JObject.Parse(json)[KendoRemoteDataSource.TransportPropertyName][KendoTransport.ReadPropertyName][KendoTransportOperation.UrlPropertyName])
                        && "GET".Equals((string)JObject.Parse(json)[KendoRemoteDataSource.TransportPropertyName][KendoTransport.ReadPropertyName][KendoTransportOperation.TypePropertyName])
                        && (!(bool)JObject.Parse(json)[KendoRemoteDataSource.TransportPropertyName][KendoTransport.ReadPropertyName][KendoTransportOperation.CachePropertyName])

                        && "api/patients/create".Equals((string)JObject.Parse(json)[KendoRemoteDataSource.TransportPropertyName][KendoTransport.CreatePropertyName][KendoTransportOperation.UrlPropertyName])
                        && "POST".Equals((string)JObject.Parse(json)[KendoRemoteDataSource.TransportPropertyName][KendoTransport.CreatePropertyName][KendoTransportOperation.TypePropertyName])

                        && "api/patients/patch/1".Equals((string)JObject.Parse(json)[KendoRemoteDataSource.TransportPropertyName][KendoTransport.UpdatePropertyName][KendoTransportOperation.UrlPropertyName])
                        && "PATCH".Equals((string)JObject.Parse(json)[KendoRemoteDataSource.TransportPropertyName][KendoTransport.UpdatePropertyName][KendoTransportOperation.TypePropertyName])

                    ))
                };
            }
        }


        public KendoDataSourceTests(ITestOutputHelper output)
        {
            _output = output;
        }


        [Theory]
        [MemberData(nameof(RemoteDataSourceToJsonCases))]
        [MemberData(nameof(LocalDataSourceToJsonCases))]
        public void ToJson(IKendoDataSource dataSource, Expression<Func<string, bool>> jsonMatcher)
        {
            _output.WriteLine($"Testing {Environment.NewLine} {dataSource} {Environment.NewLine} against {Environment.NewLine} {jsonMatcher}");
            dataSource.ToJson().Should().Match(jsonMatcher);
        }

        [Theory]
        [MemberData(nameof(LocalDataSourceSchemaCases))]
        public void LocalDataSourceSchema(IKendoDataSource dataSource, bool expectedValidity)
        {
            _output.WriteLine($"Validating {dataSource} against {KendoLocalDataSource.Schema}");
            JObject.Parse(dataSource.ToJson()).IsValid(KendoLocalDataSource.Schema)
              .Should().Be(expectedValidity);
        }

        [Theory]
        [MemberData(nameof(RemoteDataSourceSchemaCases))]
        public void RemoteDataSourceSchema(KendoRemoteDataSource dataSource, bool expectedValidity)
        {
            _output.WriteLine($"Validating {dataSource}");

            JObject.Parse(dataSource.ToJson()).IsValid(KendoRemoteDataSource.Schema)
              .Should().Be(expectedValidity);
        }

    }
}
