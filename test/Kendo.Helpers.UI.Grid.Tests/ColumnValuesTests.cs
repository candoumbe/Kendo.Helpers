using FluentAssertions;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Xunit;
using Xunit.Abstractions;


namespace Kendo.Helpers.UI.Grid.Tests
{
    public class ColumnValuesTests
    {
        private readonly ITestOutputHelper _output;

        public ColumnValuesTests(ITestOutputHelper output) 
        {
            _output = output;
        }


        public static IEnumerable<object[]> KendoColumnValuesSchemaCases {
            get {

                yield return new object[]
                {
                    new ColumnValues(1, "M."), true
                };

                yield return new object[]
                {
                    new ColumnValues(null, "M."), false
                };


            }
        }


        public void Schema(ColumnValues value, bool expectedValidity)
        {
            _output.WriteLine($"Validating {value} {Environment.NewLine} against {Environment.NewLine} {ColumnValues.Schema}");

            JObject.Parse(value.ToJson()).IsValid(ColumnValues.Schema)
              .Should().Be(expectedValidity);

        }
    }
}
