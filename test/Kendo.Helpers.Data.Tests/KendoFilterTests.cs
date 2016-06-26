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
using System.Collections.Immutable;
using static Newtonsoft.Json.JsonConvert;

namespace Kendo.Helpers.Data.Tests
{
    public class KendoFilterTests
    {
        private readonly ITestOutputHelper _output;
        private static IImmutableDictionary<string, KendoFilterOperator> Operators = new Dictionary<string, KendoFilterOperator>
        {
            ["contains"] = Contains,
            ["endswith"] = EndsWith,
            ["eq"] = EqualTo,
            ["gt"] = GreaterThan,
            ["gte"] = GreaterThanOrEqual,
            ["isempty"] = IsEmpty,
            ["isnotempty"] = IsNotEmpty,
            ["isnotnull"] = IsNotNull,
            ["isnull"] = IsNull,
            ["lt"] = LessThan,
            ["lte"] = LessThanOrEqualTo,
            ["neq"] = NotEqualTo,
            ["startswith"] = StartsWith
        }.ToImmutableDictionary();


        /// <summary>
        /// Serialization of instance of <see cref="KendoFilter"/> test cases
        /// </summary>
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

        /// <summary>
        /// Deserialization of various json representation into <see cref="KendoFilter"/>
        /// </summary>
        public static IEnumerable<object[]> KendoFilterDeserializeCases
        {
            get
            {
                foreach (var item in Operators)
                {
                    yield return new object[]
                    {

                        $"{{ field = 'Firstname', operator = '{item.Key}',  Value = 'Batman'}}",
                        ((Expression<Func<IKendoFilter, bool>>)(result => result is KendoFilter
                            && "Firstname".Equals(((KendoFilter) result).Field)
                            && item.Value.Equals(((KendoFilter) result).Operator) &&
                            "Batman".Equals(((KendoFilter) result).Value)
                        ))
                    };
                }
            }
        }


        public static IEnumerable<object[]> CollectionOfKendoFiltersCases
        {
            get
            {
                yield return new object[] {
                    new IKendoFilter[]
                    {
                        new KendoFilter { Field = "Firstname", Operator = EqualTo, Value = "Bruce" },
                        new KendoFilter { Field = "Lastname", Operator = EqualTo, Value = "Wayne" }
                    },
                    ((Expression<Func<string, bool>>)(json =>
                        JToken.Parse(json).Type == JTokenType.Array
                        && JArray.Parse(json).Count == 2
                        

                        && JArray.Parse(json)[0].Type == JTokenType.Object
                        && JArray.Parse(json)[1].IsValid(KendoFilter.Schema(EqualTo))
                        && JArray.Parse(json)[0][KendoFilter.FieldJsonPropertyName].Value<string>() == "Firstname"
                        && JArray.Parse(json)[0][KendoFilter.OperatorJsonPropertyName].Value<string>() == "eq"
                        && JArray.Parse(json)[0][KendoFilter.ValueJsonPropertyName].Value<string>() == "Bruce"

                        && JArray.Parse(json)[1].Type == JTokenType.Object
                        && JArray.Parse(json)[1].IsValid(KendoFilter.Schema(EqualTo))
                        && JArray.Parse(json)[1][KendoFilter.FieldJsonPropertyName].Value<string>() == "Firstname"
                        && JArray.Parse(json)[1][KendoFilter.OperatorJsonPropertyName].Value<string>() == "eq"
                        && JArray.Parse(json)[1][KendoFilter.ValueJsonPropertyName].Value<string>() == "Wayne"


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

                yield return new object[]
                {
                    new KendoCompositeFilter  {
                        Filters = new [] {
                            new KendoFilter { Field = "Nickname", Operator = EqualTo,  Value = "Batman" },
                            new KendoFilter { Field = "Nickname", Operator = EqualTo,  Value = "Robin" },

                        }
                    },
                    ((Expression<Func<string, bool>>)(json =>
                        JObject.Parse(json).Properties().Count() == 2 &&

                        "and".Equals((string) JObject.Parse(json)[KendoCompositeFilter.LogicJsonPropertyName]) &&

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
                    false
                };

                yield return new object[]
                {
                    new KendoFilter { Field = "Firstname", Operator = EqualTo },
                    false
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
                            new KendoFilter { Field = "Firstname", Operator = EqualTo,  Value = "Bruce" },
                            new KendoFilter { Field = "Lastname", Operator = EqualTo,  Value = "Wayne" },
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


        [Theory]
        [MemberData(nameof(CollectionOfKendoFiltersCases))]
        public void CollectionOfFiltersToJson(IEnumerable<IKendoFilter> filters, Expression<Func<string, bool>> jsonExpectation)
        {
            string json = SerializeObject(filters);
        }

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
                ? KendoFilter.Schema((filter as KendoFilter).Operator)
                : KendoCompositeFilter.Schema;

            _output.WriteLine($"Testing :{filter} {Environment.NewLine} against {Environment.NewLine} {schema}");

            JObject.Parse(filter.ToJson())
                  .IsValid(schema)
                  .Should().Be(expectedValidity);
        }

    }
}