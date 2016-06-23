
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Xunit;
using Xunit.Abstractions;

namespace Kendo.Helpers.Tools.Tests
{
    public class ObjectExtensions : IDisposable
    {
        private ITestOutputHelper _outputHelper;

        private class Person
        {
            public string Firstname { get; set; }

            public string Lastname { get; set; }

        }

        private class SuperHero : Person
        {
            public string Nickname { get; set; }
        }

        public static IEnumerable<object[]> AsCases
        {
            get
            {
                yield return new object[]
                {
                    new SuperHero() { Firstname = "Bruce", Lastname = "Wayne", Nickname = "Batman" },
                    typeof(Person),
                    ((Expression<Func<object, bool>>) (person =>
                        person is Person
                        && "Bruce".Equals(((Person) person).Firstname)
                        && "Wayne".Equals(((Person) person).Lastname)))
                };
            }
        }


        public ObjectExtensions(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        [Theory]
        [MemberData(nameof(AsCases))]
        public void As(object source, Type targetType, Expression<Func<object, bool>> resultExpectation)
        {
            _outputHelper.WriteLine($"Converting {source} to {targetType}");
            source.As(targetType)
                .Should().Match(resultExpectation);
        }

        public void Dispose()
        {
            _outputHelper = null;    
        }
    }
}
