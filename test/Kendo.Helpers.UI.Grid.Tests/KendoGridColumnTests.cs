using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Kendo.Helpers.UI.Grid.Tests
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    public class KendoGridColumnTests
    {


        public static IEnumerable<object[]> FieldColumnsCases
        {
            get
            {
                yield return new object[]
                {
                    new KendoGridFieldColumn
                    {
                        Field = "Firstname"
                    },
                    @"{""field"":""Firstname""}"
                };

                yield return new object[]
                {
                    new KendoGridFieldColumn
                    {
                        Field = "Firstname",
                        Title = "Firstname"
                    },
                    @"{""field"":""Firstname"",""title"":""Firstname""}"
                };

                yield return new object[]
                {
                    new KendoGridFieldColumn
                    {
                        Field = "Firstname",
                        Editable = false
                    },
                    @"{""field"":""Firstname"",""editable"":false}"
                };

                yield return new object[]
                {
                    new KendoGridFieldColumn
                    {
                        Field = "Birthdate",
                        Title = "Birth date",
                    },
                    @"{""field"":""Birthdate"",""title"":""Birth date""}"
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
                    @"{""field"":""Birthdate"",""title"":""Birth date"",""attributes"":{""class"":""td-class"",""font-size"":""14px""}}"
                };
            }
        }

        
        [Theory]
        [MemberData(nameof(FieldColumnsCases))]
        public void ToJson(KendoGridFieldColumn column, string expectedString)
            => ToJson((KendoGridColumnBase)column, expectedString);

        private void ToJson(KendoGridColumnBase column, string expectedString)
            => column.ToJson().Should().Be(expectedString);
    }
}
