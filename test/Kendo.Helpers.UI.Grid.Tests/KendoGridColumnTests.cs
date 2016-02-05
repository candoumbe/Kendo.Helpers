using FluentAssertions;
using GenFu;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Xunit;
using Xunit.Abstractions;

namespace Kendo.Helpers.UI.Grid.Tests
{
    public class KendoGridColumnTests
    {
        private readonly ITestOutputHelper _output;

        public static IEnumerable<object[]> FieldColumnsToJsonCases
        {
            get
            {
                yield return new object[]
                {
                    new KendoGridFieldColumn {
                        Field = "Firstname"
                    },
                    ((Expression<Func<string, bool>>) (json => 
                        "Firstname".Equals((string)JObject.Parse(json)[KendoGridFieldColumnBase.FieldPropertyName])
                    ))
                };

                yield return new object[]
                {
                    new KendoGridFieldColumn {
                        Field = "Firstname",
                        Encoded = false
                    },
                    ((Expression<Func<string, bool>>) (json =>
                        "Firstname".Equals((string)JObject.Parse(json)[KendoGridFieldColumnBase.FieldPropertyName]) &&
                        !(bool)JObject.Parse(json)[KendoGridFieldColumnBase.EncodedPropertyName]
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
                        "Firstname".Equals((string)JObject.Parse(json)[KendoGridFieldColumnBase.FieldPropertyName]) &&
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
                        "Firstname".Equals((string)JObject.Parse(json)[KendoGridFieldColumnBase.FieldPropertyName]) &&
                        "Firstname".Equals((string)JObject.Parse(json)[KendoGridFieldColumnBase.TitlePropertyName])
                    ))
                };

                
                yield return new object[]
                {
                    new KendoGridFieldColumn
                    {
                        Field = "Birthdate",
                        Title = "Birth date"
                    },
                    ((Expression<Func<string, bool>>) (json =>
                        "Birthdate".Equals((string)JObject.Parse(json)[KendoGridFieldColumnBase.FieldPropertyName]) &&
                        "Birth date".Equals((string)JObject.Parse(json)[KendoGridFieldColumnBase.TitlePropertyName])
                    ))
                };

                yield return new object[]
                {
                    new KendoGridFieldColumn
                    {
                        Field = "Birthdate",
                        Title = "Birth date",
                        Format = "dd/MM/yyyy"
                    },
                    ((Expression<Func<string, bool>>) (json =>
                        "Birthdate".Equals((string)JObject.Parse(json)[KendoGridFieldColumnBase.FieldPropertyName]) 
                        && "Birth date".Equals((string)JObject.Parse(json)[KendoGridFieldColumnBase.TitlePropertyName])
                        && "dd/MM/yyyy".Equals((string) JObject.Parse(json)[KendoGridFieldColumnBase.FormatPropertyName])
                    ))
                };

                yield return new object[]
                {
                    new KendoGridFieldColumn()
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
                        "Birthdate".Equals((string)JObject.Parse(json)[KendoGridFieldColumnBase.FieldPropertyName]) &&
                        "Birth date".Equals((string)JObject.Parse(json)[KendoGridFieldColumnBase.TitlePropertyName]) &&
                        "td-class".Equals((string)JObject.Parse(json)[KendoGridFieldColumnBase.AttributesPropertyName]["class"]) &&
                        "14px".Equals((string)JObject.Parse(json)[KendoGridFieldColumnBase.AttributesPropertyName]["font-size"])
                    ))
                };

                yield return new object[]
                {
                    new KendoGridFieldColumn
                    {
                        Field = "Firstname",
                        Template = "<strong>#: Firstname # </strong>"
                    },
                    ((Expression<Func<string, bool>>)(json =>
                       "Firstname".Equals((string)JObject.Parse(json)[KendoGridFieldColumnBase.FieldPropertyName])
                       && "<strong>#: Firstname # </strong>".Equals((string)JObject.Parse(json)[KendoGridFieldColumnBase.TemplatePropertyName])
                    ))
                };

                yield return new object[]
                {
                    new KendoGridFieldColumn
                    {
                        Field = "Firstname",
                        Groupable = false
                    },
                    ((Expression<Func<string, bool>>)(json =>
                       "Firstname".Equals((string)JObject.Parse(json)[KendoGridFieldColumnBase.FieldPropertyName]) 
                       && !(bool)JObject.Parse(json)[KendoGridFieldColumn.GroupablePropertyName]
                    ))
                };

                yield return new object[]
                {
                    new KendoGridFieldColumn
                    {
                        Field = "Firstname",
                        Groupable = true
                    },
                    ((Expression<Func<string, bool>>)(json =>
                       "Firstname".Equals((string)JObject.Parse(json)[KendoGridFieldColumnBase.FieldPropertyName])
                       && (bool)JObject.Parse(json)[KendoGridFieldColumn.GroupablePropertyName]
                    ))
                };

                yield return new object[]
                {
                    new KendoGridFieldColumn
                    {
                        Field = "Firstname",
                        Hidden = false
                    },
                    ((Expression<Func<string, bool>>)(json =>
                       "Firstname".Equals((string)JObject.Parse(json)[KendoGridFieldColumnBase.FieldPropertyName])
                       && !(bool)JObject.Parse(json)[KendoGridFieldColumn.HiddenPropertyName]
                    ))
                };

                yield return new object[]
                {
                    new KendoGridFieldColumn
                    {
                        Field = "Firstname",
                        Hidden = true
                    },
                    ((Expression<Func<string, bool>>)(json =>
                       "Firstname".Equals((string)JObject.Parse(json)[KendoGridFieldColumnBase.FieldPropertyName])
                       && (bool)JObject.Parse(json)[KendoGridFieldColumn.HiddenPropertyName]
                    ))
                };

                yield return new object[]
                {
                    new KendoGridFieldColumn
                    {
                        Field = "Firstname",
                        Locked = false
                    },
                    ((Expression<Func<string, bool>>)(json =>
                       "Firstname".Equals((string)JObject.Parse(json)[KendoGridFieldColumnBase.FieldPropertyName])
                       && !(bool)JObject.Parse(json)[KendoGridFieldColumnBase.LockedPropertyName]
                    ))
                };

                yield return new object[]
                {
                    new KendoGridFieldColumn
                    {
                        Field = "Firstname",
                        Locked = true
                    },
                    ((Expression<Func<string, bool>>)(json =>
                       "Firstname".Equals((string)JObject.Parse(json)[KendoGridFieldColumnBase.FieldPropertyName])
                       && (bool)JObject.Parse(json)[KendoGridFieldColumnBase.LockedPropertyName]
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
                    new KendoGridFieldColumn { Field = "Firstname"},
                   true
                };

                yield return new object[]
                {
                    new KendoGridFieldColumn
                    {
                        Field = "Borthdate",
                        Title = "Birth date",
                        Attributes = new Dictionary<string, object>
                        {
                            ["class"] = "td-class",
                            ["font-size"] = "14px"
                        },
                        Format = "dd/MM/yyyy"
                    },
                    true
                };

                yield return new object[]
                {
                    new KendoGridFieldColumn
                    {
                        Field = "Firstname",
                        Groupable = true
                    },
                    true
                };

                yield return new object[]
                {
                    new KendoGridFieldColumn
                    {
                        Field = "Firstname",
                        Groupable = false
                    },
                    true
                };

                yield return new object[]
                {
                    new KendoGridFieldColumn
                    {
                        Field = "Firstname",
                        Groupable = null
                    },
                    true
                };

                yield return new object[]
                {
                    new KendoGridFieldColumn
                    {
                        Field = "Firstname",
                        Hidden = true
                    },
                    true
                };

                yield return new object[]
                {
                    new KendoGridFieldColumn
                    {
                        Field = "Firstname",
                        Hidden = false
                    },
                    true
                };

                yield return new object[]
                {
                    new KendoGridFieldColumn
                    {
                        Field = "Firstname",
                        Hidden = null
                    },
                    true
                };

                yield return new object[]
                {
                    new KendoGridFieldColumn
                    {
                        Field = "Firstname",
                        Locked = true
                    },
                    true
                };

                yield return new object[]
                {
                    new KendoGridFieldColumn
                    {
                        Field = "Firstname",
                        Locked = false
                    },
                    true
                };

                yield return new object[]
                {
                    new KendoGridFieldColumn
                    {
                        Field = "Firstname",
                        Locked = null
                    },
                    true
                };

                yield return new object[]
                {
                    new KendoGridFieldColumn
                    {
                        Field = "Firstname",
                        Lockable = true
                    },
                    true
                };

                yield return new object[]
                {
                    new KendoGridFieldColumn
                    {
                        Field = "Firstname",
                        Lockable = false
                    },
                    true
                };

                yield return new object[]
                {
                    new KendoGridFieldColumn()
                    {
                        Field = "Firstname",
                        Lockable = null
                    },
                    true
                };

                yield return new object[]
                {
                    new KendoGridFieldColumn()
                    {
                        Field = "Firstname",
                        MinScreenWidth = 750
                    },
                    true
                };

                yield return new object[]
                {
                    new KendoGridFieldColumn()
                    {
                        Field = "Firstname",
                        MinScreenWidth = null
                    },
                    true
                };


                yield return new object[]
                {
                    new KendoGridFieldColumn()
                    {
                        Field = "Firstname",
                        Encoded = true
                    },
                   true
                };

                yield return new object[]
                {
                    new KendoGridFieldColumn()
                    {
                        Field = "Firstname",
                        Title = "Firstname"
                    },
                    true
                };

                yield return new object[]
                {
                    new KendoGridFieldColumn()
                    {
                        Field = "Firstname",
                        Template = null
                    },
                    true
                };

                yield return new object[]
                {
                    new KendoGridFieldColumn
                    {
                        Field = "Firstname",
                        Template = "<strong>#: Firstname # </strong>"
                    },
                    true
                };

                yield return new object[]
                {
                    new KendoGridFieldColumn
                    {
                        Template = "<strong>#: Firstname # </strong>"
                    },
                    true
                };

                yield return new object[]
                {
                     new KendoGridFieldColumn()
                    {
                        Title = "Fullname" ,
                        Columns = new [] {
                            new KendoGridFieldColumn{ Field = "firstname" },
                            new KendoGridFieldColumn() { Field = "lastname" }
                        }
                    },
                    true
                };
            }
        }


        public static IEnumerable<object[]> StronglyTypedFieldColumnsToJsonCases
        {
            get
            {
                yield return new object[]
                {
                    new KendoGridFieldColumn<KendoGridFieldColumn, string>(column => column.Field) { },
                    ((Expression<Func<string, bool>>) (json =>
                        "Field".Equals((string)JObject.Parse(json)[KendoGridFieldColumnBase.FieldPropertyName])
                    ))
                };
            }
        }
        
        public static IEnumerable<object[]> TemplateColumnsSchemaCases
        {
            get
            {
                yield return new object[]
                {
                    new KendoGridTemplateColumn(), true
                };
                yield return new object[]
                            {
                    new KendoGridTemplateColumn
                    {
                        Template = ""
                    },
                   true
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


        public KendoGridColumnTests(ITestOutputHelper output)
        {
            _output = output;
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
        {
            _output.WriteLine($"{column}{Environment.NewLine}{jsonMatcher}");
            column.ToJson().Should().Match(jsonMatcher);
        }

        [Theory]
        [MemberData(nameof(CommandColumnsSchemaCases))]
        public void CommandColumnSchema(KendoGridCommandColumn command, bool expectedValidity)
            => Schema(command, KendoGridCommandColumn.Schema, expectedValidity);

        [Theory]
        [MemberData(nameof(FieldColumnsSchemaCases))]
        public void FieldColumnSchema(KendoGridFieldColumn column, bool expectedValidity)
            => Schema(column, KendoGridFieldColumn.Schema, expectedValidity);


        [Theory]
        [MemberData(nameof(TemplateColumnsSchemaCases))]
        public void FieldColumnSchema(KendoGridTemplateColumn column, bool expectedValidity)
            => Schema(column, KendoGridTemplateColumn.Schema, expectedValidity);


        private void Schema(KendoGridColumnBase baseColumn, JSchema schema, bool expectedValidity)
        {
            _output.WriteLine($"Validating {baseColumn}{Environment.NewLine}against{Environment.NewLine}{schema}");

            JObject.Parse(baseColumn.ToJson()).IsValid(schema).Should().Be(expectedValidity);
        }
    }
}
