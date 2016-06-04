using FluentAssertions;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using Newtonsoft.Json.Schema;
using System.Linq.Expressions;
using Xunit;
using Xunit.Abstractions;
using static Kendo.Helpers.Data.KendoFilterLogic;
using static Kendo.Helpers.Data.KendoFilterOperator;
using System.Linq;

namespace Kendo.Helpers.Data.Tests
{
    public class KendoFilterTests
    {
        private readonly ITestOutputHelper _output;

        public static IEnumerable<object[]> KendoFilterToJsonCases
        {
            get
            {
                yield return new object[]
                {
                    new KendoFilter { Field = "Firstname", Operator = EqualTo,  Value = "Batman"},
                    ((Expression<Func<string, bool>>)(json =>
                        "Firstname".Equals((string) JObject.Parse(json)[KendoFilter.FieldJsonPropertyName]) &&
                        "eq".Equals((string) JObject.Parse(json)[KendoFilter.OperatorJsonPropertyName]) &&
                        "Batman".Equals((string) JObject.Parse(json)[KendoFilter.ValueJsonPropertyName])
                    ))
                };

            }
        }

        public static IEnumerable<object[]> KendoCompositeFilterToJsonCases
        {
            get
            {
                yield return new object[]
                {
                    new KendoCompositeFilter  {
                        Logic = Or,
                        Filters = new [] {
                            new KendoFilter { Field = "Nickname", Operator = EqualTo,  Value = "Batman" },
                            new KendoFilter { Field = "Nickname", Operator = EqualTo,  Value = "Robin" },

                        }
                    },
                    ((Expression<Func<string, bool>>)(json =>
                        JObject.Parse(json).Properties().Count() == 2 &&
                        
                        "or".Equals((string) JObject.Parse(json)[KendoCompositeFilter.LogicJsonPropertyName]) &&

                        "Nickname".Equals((string)JObject.Parse(json)[KendoCompositeFilter.FiltersJsonPropertyName][0][KendoFilter.FieldJsonPropertyName]) &&
                        "eq".Equals((string)JObject.Parse(json)[KendoCompositeFilter.FiltersJsonPropertyName][0][KendoFilter.OperatorJsonPropertyName]) &&
                        "Batman".Equals((string)JObject.Parse(json)[KendoCompositeFilter.FiltersJsonPropertyName][0][KendoFilter.ValueJsonPropertyName])
                               &&
                        "Nickname".Equals((string)JObject.Parse(json)[KendoCompositeFilter.FiltersJsonPropertyName][1][KendoFilter.FieldJsonPropertyName]) &&
                        "eq".Equals((string)JObject.Parse(json)[KendoCompositeFilter.FiltersJsonPropertyName][1][KendoFilter.OperatorJsonPropertyName]) &&
                        "Robin".Equals((string)JObject.Parse(json)[KendoCompositeFilter.FiltersJsonPropertyName][1][KendoFilter.ValueJsonPropertyName])

                    ))
                };

            }
        }


        public static IEnumerable<object> KendoFilterSchemaTestCases
        {
            get
            {
                yield return new object[]
                {
                    new KendoFilter { Field = "Firstname", Operator = EqualTo, Value = "Bruce" },
                    true
                };

                yield return new object[]
                {
                    new KendoFilter { Field = "Firstname", Operator = EqualTo, Value = null },
                    true
                };

                yield return new object[]
                {
                    new KendoFilter { Field = "Firstname", Operator = EqualTo },
                    true
                };

                yield return new object[]
                {
                    new KendoFilter { Field = "Firstname", Operator = Contains, Value = "Br"},
                    true
                };

                yield return new object[]
                {
                    new KendoFilter { Field = "Firstname", Operator = Contains, Value = 6},
                    false
                };
            }
        }


        public static IEnumerable<object> KendoCompositeFilterSchemaTestCases
        {
            get
            {
                yield return new object[]
                {
                    new KendoCompositeFilter  {
                        Logic = Or,
                        Filters = new [] {
                            new KendoFilter { Field = "Nickname", Operator = EqualTo,  Value = "Batman" },
                            new KendoFilter { Field = "Nickname", Operator = EqualTo,  Value = "Robin" },

                        }
                    },
                    true
                };

                yield return new object[]
                {
                    new KendoCompositeFilter  {
                        Filters = new [] {
                            new KendoFilter { Field = "Nickname", Operator = EqualTo,  Value = "Batman" },
                            new KendoFilter { Field = "Nickname", Operator = EqualTo,  Value = "Robin" },
                        }
                    },
                    true
                };

                yield return new object[]
                {
                    new KendoCompositeFilter  {
                        Logic = Or,
                        Filters = new [] {
                            new KendoFilter { Field = "Nickname", Operator = EqualTo,  Value = "Robin" },
                        }
                    },
                    false
                };
            }
        }



        public KendoFilterTests(ITestOutputHelper output)
        {
            _output = output;
        }


        [Theory]
        [MemberData(nameof(KendoFilterToJsonCases))]
        public void KendoFilterToJson(KendoFilter filter, Expression<Func<string, bool>> jsonMatcher)
            => ToJson(filter, jsonMatcher);


        [Theory]
        [MemberData(nameof(KendoCompositeFilterToJsonCases))]
        public void KendoCompositeFilterToJson(KendoCompositeFilter filter, Expression<Func<string, bool>> jsonMatcher)
            => ToJson(filter, jsonMatcher);


        private void ToJson(IKendoFilter filter, Expression<Func<string, bool>> jsonMatcher)
        {
            _output.WriteLine($"Testing : {filter}{Environment.NewLine} against {Environment.NewLine} {jsonMatcher} ");
            filter.ToJson().Should().Match(jsonMatcher);
        }


        [Theory]
        [MemberData(nameof(KendoFilterSchemaTestCases))]
        public void KendoFilterSchema(KendoFilter filter, bool expectedValidity)
            => Schema(filter, expectedValidity);

        [Theory]
        [MemberData(nameof(KendoCompositeFilterSchemaTestCases))]
        public void KendoCompositeFilterSchema(KendoCompositeFilter filter, bool expectedValidity)
            => Schema(filter, expectedValidity);


        private void Schema(IKendoFilter filter, bool expectedValidity)
        {
            JSchema schema = filter is KendoFilter
                ? KendoFilter.Schema(filter as KendoFilter)
                : KendoCompositeFilter.Schema;

            _output.WriteLine($"Testing :{filter} {Environment.NewLine} against {Environment.NewLine} {schema}");

            JObject.Parse(filter.ToJson())
                  .IsValid(schema)
                  .Should().Be(expectedValidity);
        }

    }
}
