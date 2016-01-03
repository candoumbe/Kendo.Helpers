using FluentAssertions;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace Kendo.Helpers.Data.Tests
{
    public class KendoSchemaTests
    {
        

        public static IEnumerable<object[]> ToJsonCases
        {
            get
            {
                yield return new object[]
                {
                    new KendoSchema {
                        Model  = new KendoModel()
                        {
                            Id = "Id",
                            Fields = new KendoFieldBase[]
                            {
                                new KendoStringField("Firstname")
                            }
                        }
                    },
                    ((Expression<Func<string, bool>>)(json =>
                        "Id".Equals((string) JObject.Parse(json)[KendoSchema.ModelPropertyName][KendoModel.IdPropertyName]) &&
                        JObject.Parse(json)[KendoSchema.ModelPropertyName].Type == JTokenType.Object
                    ))
                };


                

                yield return new object[]
                {
                    new KendoSchema {
                        Data = "items"
                    },
                    ((Expression<Func<string, bool>>)(json =>
                        
                        "items".Equals((string) JObject.Parse(json)[KendoSchema.DataPropertyName])
                    ))
                };

                yield return new object[]
                {
                    new KendoSchema {
                        Data = "items",
                        Model  = new KendoModel()
                        {
                            Id = "Id",
                            Fields = new KendoFieldBase[]
                            {
                                new KendoStringField("Firstname")
                            }
                        }
                    },
                    ((Expression<Func<string, bool>>)(json =>
                        "items".Equals((string) JObject.Parse(json)[KendoSchema.DataPropertyName])
                    ))
                };

                yield return new object[]
                {
                    new KendoSchema {
                        Data = "items",
                        Total = "count",
                        Model  = new KendoModel()
                        {
                            Id = "Id",
                            Fields = new KendoFieldBase[]
                            {
                                new KendoStringField("Firstname")
                            }
                        }
                    },
                    ((Expression<Func<string, bool>>)(json =>
                        "count".Equals((string) JObject.Parse(json)[KendoSchema.TotalPropertyName]) &&
                        "items".Equals((string) JObject.Parse(json)[KendoSchema.DataPropertyName]) &&
                        "Id".Equals((string) JObject.Parse(json)[KendoSchema.ModelPropertyName][KendoModel.IdPropertyName])
                    ))
                };

                yield return new object[]
                {
                    new KendoSchema {
                        Data = "items",
                        Total = "count",
                        Type = SchemaType.Json,
                        Model  = new KendoModel()
                        {
                            Id = "Id",
                            Fields = new KendoFieldBase[]
                            {
                                new KendoStringField("Firstname")
                            }
                        }
                    },
                    ((Expression<Func<string, bool>>)(json =>
                        nameof(SchemaType.Json).ToLower().Equals((string) JObject.Parse(json)[KendoSchema.TypePropertyName]) &&
                        "count".Equals((string) JObject.Parse(json)[KendoSchema.TotalPropertyName]) &&
                        "items".Equals((string) JObject.Parse(json)[KendoSchema.DataPropertyName]) &&
                        "Id".Equals((string) JObject.Parse(json)[KendoSchema.ModelPropertyName][KendoModel.IdPropertyName])
                    ))
                };

                yield return new object[]
                {
                    new KendoSchema {
                        Data = "items",
                        Total = "count",
                        Type = SchemaType.Xml,
                        Model  = new KendoModel()
                        {
                            Id = "Id",
                            Fields = new KendoFieldBase[]
                            {
                                new KendoStringField("Firstname")
                            }
                        }
                    },
                    ((Expression<Func<string, bool>>)(json =>
                        nameof(SchemaType.Xml).ToLower().Equals((string) JObject.Parse(json)[KendoSchema.TypePropertyName]) &&
                        "count".Equals((string) JObject.Parse(json)[KendoSchema.TotalPropertyName]) &&
                        "items".Equals((string) JObject.Parse(json)[KendoSchema.DataPropertyName]) &&
                        "Id".Equals((string) JObject.Parse(json)[KendoSchema.ModelPropertyName][KendoModel.IdPropertyName])
                    ))
                };

                yield return new object[]
                {
                    new KendoSchema {
                        Data = "items",
                        Total = "count",
                        Type = null,
                        Model  = new KendoModel()
                        {
                            Id = "Id",
                            Fields = new KendoFieldBase[]
                            {
                                new KendoStringField("Firstname")
                            }
                        }
                    },
                    ((Expression<Func<string, bool>>)(json =>
                        ((string) JObject.Parse(json)[KendoSchema.TypePropertyName]) == null &&
                        "count".Equals((string) JObject.Parse(json)[KendoSchema.TotalPropertyName]) &&
                        "items".Equals((string) JObject.Parse(json)[KendoSchema.DataPropertyName]) &&
                        "Id".Equals((string) JObject.Parse(json)[KendoSchema.ModelPropertyName][KendoModel.IdPropertyName]) 
                    ))                };
            }
        }

        public static IEnumerable<object[]> SchemaCases
        {
            get
            {
                yield return new object[]
                {
                    new KendoSchema {
                        Model  = new KendoModel()
                        {
                            Id = "Id",
                            Fields = new KendoFieldBase[]
                            {
                                new KendoStringField("Firstname")
                            }
                        }
                    },
                    true
                };




                yield return new object[]
                {
                    new KendoSchema {
                        Data = "items"
                    },
                    true
                };

                yield return new object[]
                {
                    new KendoSchema {
                        Data = "items",
                        Model  = new KendoModel()
                        {
                            Id = "Id",
                            Fields = new KendoFieldBase[]
                            {
                                new KendoStringField("Firstname")
                            }
                        }
                    },
                   true
                };

                yield return new object[]
                {
                    new KendoSchema {
                        Data = "items",
                        Total = "count",
                        Model  = new KendoModel()
                        {
                            Id = "Id",
                            Fields = new KendoFieldBase[]
                            {
                                new KendoStringField("Firstname")
                            }
                        }
                    },
                    true
                };

                yield return new object[]
                {
                    new KendoSchema {
                        Data = "items",
                        Total = "count",
                        Type = SchemaType.Json,
                        Model  = new KendoModel()
                        {
                            Id = "Id",
                            Fields = new KendoFieldBase[]
                            {
                                new KendoStringField("Firstname")
                            }
                        }
                    },
                    true
                };

                yield return new object[]
                {
                    new KendoSchema {
                        Data = "items",
                        Total = "count",
                        Type = SchemaType.Xml,
                        Model  = new KendoModel()
                        {
                            Id = "Id",
                            Fields = new KendoFieldBase[]
                            {
                                new KendoStringField("Firstname")
                            }
                        }
                    },
                    true
                };

                yield return new object[]
                {
                    new KendoSchema {
                        Data = "items",
                        Total = "count",
                        Type = null,
                        Model  = new KendoModel()
                        {
                            Id = "Id",
                            Fields = new KendoFieldBase[]
                            {
                                new KendoStringField("Firstname")
                            }
                        }
                    },
                    true
                };
            }
        }



        [Theory]
        [MemberData(nameof(ToJsonCases))]
        public void ToJson(KendoSchema schema, Expression<Func<string, bool>> jsonMatch)
            => schema.ToJson().Should().Match(jsonMatch);

        [Theory]
        [MemberData(nameof(SchemaCases))]
        public void Schema(KendoSchema schema, bool expectedValidity)
            => JObject.Parse(schema.ToJson()).IsValid(KendoSchema.Schema)
                .Should()
                .Be(expectedValidity);
    }
}
