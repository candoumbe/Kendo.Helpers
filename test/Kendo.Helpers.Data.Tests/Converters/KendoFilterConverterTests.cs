using FluentAssertions;
using Kendo.Helpers.Data.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using Newtonsoft.Json;
using System.Linq.Expressions;
using Moq;
using static Kendo.Helpers.Data.KendoFilterOperator;
using Newtonsoft.Json.Linq;
using System.Collections.Immutable;

namespace Kendo.Helpers.Data.Tests.Converters
{
    public class KendoFilterConverterTests : IDisposable
    {
        private ITestOutputHelper _outputHelper;
        private Mock<JsonSerializer> _serializerMock;
        private object obj;

        private static IImmutableDictionary<string, KendoFilterOperator> Operators => new Dictionary<string, KendoFilterOperator>
        {
            ["eq"] =  EqualTo,
            ["neq"] =  NotEqualTo,
            ["lt"] = LessThan,
            ["gt"] = GreaterThan,
            ["lte"] =  LessThanOrEqualTo,
            ["gte"] =  GreaterThanOrEqual,
            ["contains"] =  Contains,
            ["isnull"] =  IsNull,
            ["isnotnull"] =  IsNotNull,
            ["isnotempty"] =  IsNotEmpty,
            ["isempty"] =  IsEmpty
        }.ToImmutableDictionary();

        public KendoFilterConverterTests(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }


        /// <summary>
        /// Deserialize tests cases
        /// </summary>
        public static IEnumerable<object[]> DeserializeCases
        {
            get
            {
                foreach (KeyValuePair<string, KendoFilterOperator> item in Operators)
                {
                    yield return new object[]{
                        $"{{field :'Firstname', operator :'{item.Key}', value : 'Bruce'}}",
                        typeof(KendoFilter),
                        ((Expression<Func<object, bool>>) ((result) => result is KendoFilter
                            && "Firstname" == ((KendoFilter)result).Field
                            && item.Value == ((KendoFilter)result).Operator
                            && ((KendoFilter)result).Value is string
                            && "Bruce".Equals((string)((KendoFilter)result).Value)))
                    };
                }

                yield return new object[]{
                        $"{{field :'Firstname', operator :'isnull', value : null}}",
                        typeof(KendoFilter),
                        ((Expression<Func<object, bool>>) ((result) => result is KendoFilter
                            && "Firstname" == ((KendoFilter)result).Field
                            && Operators["isnull"] == ((KendoFilter)result).Operator
                            && ((KendoFilter)result).Value == null))
                    };

                yield return new object[]{
                        $"{{field :'Firstname', operator :'isnull'}}",
                        typeof(KendoFilter),
                        ((Expression<Func<object, bool>>) ((result) => result is KendoFilter
                            && "Firstname" == ((KendoFilter)result).Field
                            && Operators["isnull"] == ((KendoFilter)result).Operator
                            && ((KendoFilter)result).Value == null))
                    };

                yield return new object[]{
                        $"{{field :'Firstname', operator :'isnotnull', value : null}}",
                        typeof(KendoFilter),
                        ((Expression<Func<object, bool>>) ((result) => result is KendoFilter
                            && "Firstname" == ((KendoFilter)result).Field
                            && Operators["isnotnull"] == ((KendoFilter)result).Operator
                            && ((KendoFilter)result).Value == null))
                    };

                yield return new object[]{
                        $"{{field :'Firstname', operator :'isnotnull'}}",
                        typeof(KendoFilter),
                        ((Expression<Func<object, bool>>) ((result) => result is KendoFilter
                            && "Firstname" == ((KendoFilter)result).Field
                            && Operators["isnotnull"] == ((KendoFilter)result).Operator
                            && ((KendoFilter)result).Value == null))
                    };



            }
        }

        /// <summary>
        /// Deserialize tests cases
        /// </summary>
        public static IEnumerable<object[]> SerializeCases
        {
            get
            {
                foreach (KeyValuePair<string, KendoFilterOperator> item in Operators)
                {
                    yield return new object[]{
                        new KendoFilter { Field = "Firstname", Operator = item.Value, Value = "Bruce" },
                        ((Expression<Func<string, bool>>) ((json) => json != null 
                            && JToken.Parse(json).Type == JTokenType.Object
                            && JObject.Parse(json).Properties().Count() == 3
                            && "Firstname".Equals(JObject.Parse(json)[KendoFilter.FieldJsonPropertyName].Value<string>())
                            && item.Key.Equals(JObject.Parse(json)[KendoFilter.OperatorJsonPropertyName].Value<string>())
                            && "Bruce".Equals(JObject.Parse(json)[KendoFilter.ValueJsonPropertyName].Value<string>())))


                    };
                }
            }
        }


        /// <summary>
        /// Tests the deserialization of the <paramref name="json"/> to an instance of the specified <paramref name="targetType"/> <br/>
        /// The deserialization is done using <c>JsonConvert.DeserializeObject</c>
        /// </summary>
        /// <param name="json">json to deserialize</param>
        /// <param name="targetType">type the json string will be deserialize into</param>
        /// <param name="expectation">Expectation that result of the deserialization should match</param>
        [Theory]
        [MemberData(nameof(DeserializeCases))]
        public void Deserialize(string json, Type targetType, Expression<Func<object, bool>> expectation)
        {
            _outputHelper.WriteLine($"Deserializing {json}");

            object result = JsonConvert.DeserializeObject(json, targetType);

            result.Should()
                .Match(expectation);
        }


        /// <summary>
        /// Tests the serialization of the <paramref name="obj"/> to its string representation
        /// The deserialization is done using <c>JsonConvert.DeserializeObject</c>
        /// </summary>
        /// <param name="filter">json to deserialize</param>
        /// <param name="expectation">Expectation that result of the deserialization should match</param>
        [Theory]
        [MemberData(nameof(SerializeCases))]
        public void Serialize(IKendoFilter filter, Expression<Func<string, bool>> expectation)
        {
            _outputHelper.WriteLine($"Serializing {filter}");

            string result = JsonConvert.SerializeObject(filter);

            result.Should()
                .Match(expectation);
        }


        public void Dispose()
        {
            _outputHelper = null;
        }

    }
}
