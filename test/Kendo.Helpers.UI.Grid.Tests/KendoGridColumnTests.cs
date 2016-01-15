using FluentAssertions;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace Kendo.Helpers.UI.Grid.Tests
{
    public class KendoGridColumnTests
    {


        public static IEnumerable<object[]> FieldColumnsToJsonCases
        {
            get
            {
                yield return new object[]
                {
                    new KendoGridFieldColumn
                    {
                        Field = "Firstname"
                    },
                    ((Expression<Func<string, bool>>) (json => 
                        "Firstname".Equals((string)JObject.Parse(json)[KendoGridFieldColumn.FieldPropertyName])
                    ))
                };

                yield return new object[]
                {
                    new KendoGridFieldColumn
                    {
                        Field = "Firstname",
                        Encoded = false
                    },
                    ((Expression<Func<string, bool>>) (json =>
                        "Firstname".Equals((string)JObject.Parse(json)[KendoGridFieldColumn.FieldPropertyName]) &&
                        !(bool)JObject.Parse(json)[KendoGridFieldColumn.EncodedPropertyName]
                    ))
                };

                yield return new object[]
                {
                    new KendoGridFieldColumn
                    {
                        Field = "Firstname",
                        Encoded = true
                    },
                    ((Expression<Func<string, bool>>) (json =>
                        "Firstname".Equals((string)JObject.Parse(json)[KendoGridFieldColumn.FieldPropertyName]) &&
                        (bool)JObject.Parse(json)[KendoGridFieldColumn.EncodedPropertyName]
                    ))
                };

                yield return new object[]
                {
                    new KendoGridFieldColumn
                    {
                        Field = "Firstname",
                        Title = "Firstname"
                    },
                    ((Expression<Func<string, bool>>) (json =>
                        "Firstname".Equals((string)JObject.Parse(json)[KendoGridFieldColumn.FieldPropertyName]) &&
                        "Firstname".Equals((string)JObject.Parse(json)[KendoGridFieldColumn.TitlePropertyName])
                    ))
                };

                
                yield return new object[]
                {
                    new KendoGridFieldColumn
                    {
                        Field = "Birthdate",
                        Title = "Birth date",
                    },
                    ((Expression<Func<string, bool>>) (json =>
                        "Birthdate".Equals((string)JObject.Parse(json)[KendoGridFieldColumn.FieldPropertyName]) &&
                        "Birth date".Equals((string)JObject.Parse(json)[KendoGridFieldColumn.TitlePropertyName])
                    ))
                };

                yield return new object[]
                {
                    new KendoGridFieldColumn
                    {
                        Field = "Birthdate",
                        Title = "Birth date",
                        Attributes = new Dictionary<string, object>
                        {
                            ["class"] = "td-class",
                            ["font-size"] = "14px"
                        }
                    },
                    ((Expression<Func<string, bool>>) (json =>
                        "Birthdate".Equals((string)JObject.Parse(json)[KendoGridFieldColumn.FieldPropertyName]) &&
                        "Birth date".Equals((string)JObject.Parse(json)[KendoGridFieldColumn.TitlePropertyName]) &&
                        "td-class".Equals((string)JObject.Parse(json)[KendoGridFieldColumn.AttributesPropertyName]["class"]) &&
                        "14px".Equals((string)JObject.Parse(json)[KendoGridFieldColumn.AttributesPropertyName]["font-size"])
                    ))
                };
            }
        }

        public static IEnumerable<object[]> FieldColumnsSchemaCases
        {
            get
            {
                yield return new object[]
                {
                    new KendoGridFieldColumn(), false
                };
                yield return new object[]
                {
                    new KendoGridFieldColumn
                    {
                        Field = "Firstname"
                    },
                   true
                };

                yield return new object[]
                {
                    new KendoGridFieldColumn
                    {
                        Field = "Firstname",
                        Encoded = true
                    },
                   true
                };

                yield return new object[]
                {
                    new KendoGridFieldColumn
                    {
                        Title = "Firstname"
                    },
                    false
                };
            }
        }



        public static IEnumerable<object[]> CommandColumnsSchemaCases
        {
            get
            {
                yield return new object[]
                {
                    new KendoGridCommandColumn(), false
                };
                yield return new object[]
                {
                    new KendoGridCommandColumn
                    {
                        Name = "edit"
                    },
                   true
                };

                yield return new object[]
                {
                    new KendoGridCommandColumn
                    {
                        Name = "delete"
                    },
                   true
                };

                yield return new object[]
                {
                    new KendoGridCommandColumn
                    {
                        Text = "Firstname"
                    },
                    true
                };
            }
        }

        public static IEnumerable<object[]> CommandColumnsToJsonCases
        {
            get
            {
                yield return new object[]
                {
                    new KendoGridCommandColumn
                    {
                        Name = "edit"
                    },
                   ((Expression<Func<string, bool>>) (json =>
                        "edit".Equals((string)JObject.Parse(json)[KendoGridCommandColumn.NamePropertyName])
                    ))
                };
            }
        }

        [Theory]
        [MemberData(nameof(CommandColumnsToJsonCases))]
        public void ToJson(KendoGridCommandColumn column, Expression<Func<string, bool>> jsonMatcher)
            => ToJson((KendoGridColumnBase)column, jsonMatcher);



        [Theory]
        [MemberData(nameof(FieldColumnsToJsonCases))]
        public void ToJson(KendoGridFieldColumn column, Expression<Func<string, bool>> jsonMatcher)
            => ToJson((KendoGridColumnBase)column, jsonMatcher);


        private void ToJson(KendoGridColumnBase column, Expression<Func<string, bool>> jsonMatcher)
            => column.ToJson().Should().Match(jsonMatcher);

        [Theory]
        [MemberData(nameof(CommandColumnsSchemaCases))]
        public void CommandColumnSchema(KendoGridCommandColumn command, bool expectedValidity)
            => Schema(command, KendoGridCommandColumn.Schema, expectedValidity);

        [Theory]
        [MemberData(nameof(FieldColumnsSchemaCases))]
        public void FieldColumnSchema(KendoGridFieldColumn column, bool expectedValidity)
            => Schema(column, KendoGridFieldColumn.Schema, expectedValidity);


        private void Schema(KendoGridColumnBase baseColumn, JSchema schema, bool expectedValidity)
            => JObject.Parse(baseColumn.ToJson()).IsValid(schema).Should().Be(expectedValidity);
    }
}
