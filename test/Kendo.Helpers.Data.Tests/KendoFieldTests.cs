using FluentAssertions;
using System.Collections.Generic;
using Xunit;

namespace Kendo.Helpers.Data.Tests
{
    public class KendoFieldTests
    {
        public static IEnumerable<object[]> Cases
        {
            get
            {
                yield return new object[]
                {
                    new KendoStringField("Firstname"),
                    "Firstname:{}"
                };
            }
        }

        [Theory]
        [MemberData(nameof(Cases))]
        public void ToJson(KendoFieldBase kf, string expectedString)
            => kf.ToJson().Should().Be(expectedString);


    }
}
