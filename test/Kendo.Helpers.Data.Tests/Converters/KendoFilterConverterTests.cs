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

namespace Kendo.Helpers.Data.Tests.Converters
{
    public class KendoFilterConverterTests : IDisposable
    {
        private ITestOutputHelper _outputHelper;
        private Mock<JsonSerializer> _serializerMock;
        private object obj;

        private static IEnumerable<Tuple<string, KendoFilterOperator>> Operators => new[]
        {
            Tuple.Create("eq", EqualTo),
            Tuple.Create("neq", NotEqualTo),
            Tuple.Create("lt", LessThan),
            Tuple.Create("gt", GreaterThan),
            Tuple.Create("lte", LessThanOrEqualTo),
            Tuple.Create("gte", GreaterThanOrEqual),
            Tuple.Create("contains", Contains),
            Tuple.Create("isnull", IsNull),
            Tuple.Create("isnotnull", IsNotNull),
            Tuple.Create("isnotempty", IsNotEmpty),
            Tuple.Create("isempty", IsEmpty)
        };

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
                foreach (var item in Operators)
                {
                    yield return new object[]{
                        $"{{field : 'Firstname', operator : '{item.Item1}', value : 'Bruce'}}",
                        typeof(KendoFilter),
                        ((Expression<Func<object, bool>>) ((result) => result is KendoFilter
                            && "Firstname" == ((KendoFilter)result).Field
                            && item.Item2 == ((KendoFilter)result).Operator
                            && ((KendoFilter)result).Value is string
                            && "Bruce".Equals((string)((KendoFilter)result).Value)))
                    };
                }
            }
        }

        /// <summary>
        /// Deserialize tests cases
        /// </summary>
        public static IEnumerable<object[]> SerializeCases
        {
            get
            {
                foreach (var item in Operators)
                {
                    yield return new object[]{
                        new KendoFilter { Field = "Firstname", Operator = item.Item2, Value = "Bruce" },
                        ((Expression<Func<string, bool>>) ((json) => json != null 
                            && JObject.Parse(json).Properties().Count() == 3
                            && "Firstname".Equals(JObject.Parse(json)[KendoFilter.FieldJsonPropertyName].Value<string>())
                            && item.Item1.Equals(JObject.Parse(json)[KendoFilter.OperatorJsonPropertyName].Value<string>())
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
