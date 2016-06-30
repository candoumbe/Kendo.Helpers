using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Kendo.Helpers.Data;
using Xunit;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;
using Newtonsoft.Json.Linq;
using Xunit.Abstractions;
using static Kendo.Helpers.Data.KendoFilterOperator;
using static Kendo.Helpers.Data.KendoFilterLogic;
using Newtonsoft.Json;

namespace Kendo.Helpers.Data.Tests
{

    public class KendoExpressionBuilderTests
    {
        private readonly ITestOutputHelper _output;

        public class Person
        {
            public string Firstname { get; set; }

            public string Lastname { get; set; }

            public DateTime BirthDate { get; set; }

        }

        public class SuperHero : Person
        {
            public string Nickname { get; set; }

            public int Height { get; set; }

            public Henchman Henchman { get; set; }
        }

        public class Henchman : SuperHero
        {

        }

        public KendoExpressionBuilderTests(ITestOutputHelper output)
        {
            _output = output;
        }


        public static IEnumerable<object> EqualToTestCases
        {
            get
            {
                yield return new object[]
                {
                    Enumerable.Empty<SuperHero>(),
                    new KendoFilter { Field = nameof(SuperHero.Firstname), Operator = EqualTo, Value  = "Bruce"  },
                    ((Expression<Func<SuperHero, bool>>)(item => item.Firstname == "Bruce"))
                };


                yield return new object[]
                {
                    new[] {new SuperHero { Firstname = "Bruce", Lastname = "Wayne", Height = 190, Nickname = "Batman" }},
                    new KendoFilter { Field = nameof(SuperHero.Firstname), Operator = EqualTo, Value  = "Bruce"  },
                    ((Expression<Func<SuperHero, bool>>)(item => item.Firstname == "Bruce"))
                };

                yield return new object[]
                {
                    new[] {new SuperHero { Firstname = "Clark", Lastname = "Kent", Height = 190, Nickname = "Superman" }},
                    new KendoFilter { Field = nameof(SuperHero.Firstname), Operator = EqualTo, Value  = "Bruce"  },
                    ((Expression<Func<SuperHero, bool>>)(item => item.Firstname == "Bruce"))
                };


                yield return new object[]
                {
                    new[] {new SuperHero { Firstname = "Bruce", Lastname = "Wayne", Height = 190 }},
                    new KendoFilter { Field = nameof(SuperHero.Height), Operator = EqualTo, Value  = 190  },
                    ((Expression<Func<SuperHero, bool>>)(item => item.Height == 190 ))
                };

                yield return new object[]
                {
                    new[] {
                        new SuperHero { Firstname = "Bruce", Lastname = "Wayne", Height = 190, Nickname = "Batman" },
                        new SuperHero { Firstname = "Clark", Lastname = "Kent", Height = 190, Nickname = "Superman" }
                    },
                    new KendoCompositeFilter {
                        Logic = Or,
                        Filters = new [] {
                            new KendoFilter { Field = nameof(SuperHero.Nickname), Operator = EqualTo, Value  = "Batman" },
                            new KendoFilter { Field = nameof(SuperHero.Nickname), Operator = EqualTo, Value  = "Superman" }
                        }
                    },
                    ((Expression<Func<SuperHero, bool>>)(item => item.Nickname == "Batman" || item.Nickname == "Superman"))
                };

                
                yield return new object[]
                {
                    new[] {
                        new SuperHero { Firstname = "Bruce", Lastname = "Wayne", Height = 190, Nickname = "Batman" },
                        new SuperHero { Firstname = "Clark", Lastname = "Kent", Height = 190, Nickname = "Superman" },
                        new SuperHero { Firstname = "Barry", Lastname = "Allen", Height = 190, Nickname = "Flash" }

                    },
                    new KendoCompositeFilter {
                        Logic = And,
                        Filters = new IKendoFilter [] {
                            new KendoFilter { Field = nameof(SuperHero.Firstname), Operator = Contains, Value  = "a" },
                            new KendoCompositeFilter
                            {
                                Logic = Or,
                                Filters = new [] {
                                    new KendoFilter { Field = nameof(SuperHero.Nickname), Operator = EqualTo, Value  = "Batman" },
                                    new KendoFilter { Field = nameof(SuperHero.Nickname), Operator = EqualTo, Value  = "Superman" }
                                }
                            }
                        }
                    },
                    ((Expression<Func<SuperHero, bool>>)(item => item.Firstname.Contains("a") && (item.Nickname == "Batman" || item.Nickname == "Superman")))
                };

            }

        }

        public static IEnumerable<object> NotEqualToTestCases
        {
            get
            {
                yield return new object[]
                {
                    Enumerable.Empty<SuperHero>(),
                    new KendoFilter { Field = nameof(SuperHero.Lastname), Operator = NotEqualTo, Value  = "Kent"  },
                    ((Expression<Func<SuperHero, bool>>)(item => item.Lastname != "Kent"))
                };
                yield return new object[]
                {
                    new[] {
                        new SuperHero { Firstname = "Clark", Lastname = "Kent", Height = 190, Nickname = "Superman" }
                    },
                    new KendoFilter { Field = nameof(SuperHero.Lastname), Operator = NotEqualTo, Value  = "Kent"  },
                    ((Expression<Func<SuperHero, bool>>)(item => item.Lastname != "Kent"))
                };

                yield return new object[]
                {
                    new[] {
                        new SuperHero { Firstname = "Bruce", Lastname = "Wayne", Height = 190, Nickname = "Batman" },
                        new SuperHero { Firstname = "Clark", Lastname = "Kent", Height = 190, Nickname = "Superman" }
                    },
                    new KendoFilter { Field = nameof(SuperHero.Lastname), Operator = NotEqualTo, Value  = "Kent"  },
                    ((Expression<Func<SuperHero, bool>>)(item => item.Lastname != "Kent"))
                };


                yield return new object[]
                {
                    new[] {
                        new SuperHero {
                            Firstname = "Bruce",
                            Lastname = "Wayne",
                            Height = 190,
                            Nickname = "Batman",
                            Henchman = new Henchman
                            {
                                Firstname = "Dick",
                                Lastname = "Grayson"
                            }
                        }
                    },
                    new KendoFilter { Field = $"{nameof(SuperHero.Henchman)}.{nameof(Henchman.Firstname)}", Operator = NotEqualTo, Value  = "Dick"  },
                    ((Expression<Func<SuperHero, bool>>)(item => item.Henchman.Firstname != "Dick"))
                };

            }
        }

        public static IEnumerable<object> IsEmptyTestCases
        {
            get
            {
                yield return new object[]
                {
                    Enumerable.Empty<SuperHero>(),
                    new KendoFilter { Field = nameof(SuperHero.Lastname), Operator = IsEmpty  },
                    ((Expression<Func<SuperHero, bool>>)(item => item.Lastname == string.Empty))
                };


                yield return new object[]
                {
                    new[] {
                        new SuperHero { Firstname = "Clark", Lastname = "Kent", Height = 190, Nickname = "Superman" },
                        new SuperHero { Firstname = "", Lastname = "", Height = 178, Nickname = "Sinestro" }

                    },
                    new KendoFilter { Field = nameof(SuperHero.Lastname), Operator = IsEmpty  },
                    ((Expression<Func<SuperHero, bool>>)(item => item.Lastname == string.Empty)),

                };

                yield return new object[]
                {
                    new[] {
                        new SuperHero {
                            Firstname = "Bruce", Lastname = "Wayne", Height = 190, Nickname = "Batman",
                            Henchman = new Henchman
                            {
                                Firstname = "Dick", Lastname = "Grayson", Nickname = "Robin"
                            }
                        },
                        new SuperHero { Firstname = "Clark", Lastname = "Kent", Height = 190, Nickname = "Superman",
                           Henchman = new Henchman { Nickname = "Krypto" } }
                    },
                    new KendoFilter { Field = $"{nameof(SuperHero.Henchman)}.{nameof(Henchman.Firstname)}", Operator = IsEmpty   },
                    ((Expression<Func<SuperHero, bool>>)(item => item.Henchman.Lastname == string.Empty))
                };


                yield return new object[]
                {
                    new[] {
                        new SuperHero {
                            Firstname = "Bruce",
                            Lastname = "Wayne",
                            Height = 190,
                            Nickname = "Batman",
                            Henchman = new Henchman
                            {
                                Firstname = "Dick",
                                Lastname = "Grayson"
                            }
                        }
                    },
                    new KendoFilter { Field = $"{nameof(SuperHero.Henchman)}.{nameof(Henchman.Firstname)}", Operator = NotEqualTo, Value  = "Dick"  },
                    ((Expression<Func<SuperHero, bool>>)(item => item.Henchman.Firstname != "Dick"))
                };

            }
        }

        public static IEnumerable<object> IsNullTestCases
        {
            get
            {
                yield return new object[]
                {
                    Enumerable.Empty<SuperHero>(),
                    new KendoFilter { Field = nameof(SuperHero.Firstname), Operator = IsNull  },
                    ((Expression<Func<SuperHero, bool>>)(item => item.Firstname == null))
                };

                yield return new object[]
                {
                    new[] {
                        new SuperHero { Firstname = "Clark", Lastname = "Kent", Height = 190, Nickname = "Superman" },
                        new SuperHero { Firstname = null, Lastname = "", Height = 178, Nickname = "Sinestro" }

                    },
                    new KendoFilter { Field = nameof(SuperHero.Firstname), Operator = IsNull },
                    ((Expression<Func<SuperHero, bool>>)(item => item.Firstname == null)),
                };
            }
        }

        public static IEnumerable<object> LessThanTestCases
        {
            get
            {
                yield return new object[]
                {
                    new[] {
                        new SuperHero { Firstname = "Clark", Lastname = "Kent", Height = 190, Nickname = "Superman" },
                        new SuperHero { Firstname = null, Lastname = "", Height = 178, Nickname = "Sinestro" }

                    },
                    new KendoFilter { Field = nameof(SuperHero.Height), Operator = LessThan,  Value = 150 },
                    ((Expression<Func<SuperHero, bool>>)(item => item.Height <  150)),
                };


            }
        }



        [Theory]
        [MemberData(nameof(EqualToTestCases))]
        public void BuildEqual(IEnumerable<SuperHero> superheroes, IKendoFilter filter, Expression<Func<SuperHero, bool>> expression)
            => Build(superheroes, filter, expression);

        [Theory]
        [MemberData(nameof(IsNullTestCases))]
        public void BuildIsNull(IEnumerable<SuperHero> superheroes, IKendoFilter filter, Expression<Func<SuperHero, bool>> expression)
            => Build(superheroes, filter, expression);


        [Theory]
        [MemberData(nameof(IsEmptyTestCases))]
        public void BuildIsEmpty(IEnumerable<SuperHero> superheroes, IKendoFilter filter, Expression<Func<SuperHero, bool>> expression)
            => Build(superheroes, filter, expression);

        [Theory]
        [MemberData(nameof(LessThanTestCases))]
        public void BuildLessThan(IEnumerable<SuperHero> superheroes, IKendoFilter filter, Expression<Func<SuperHero, bool>> expression)
            => Build(superheroes, filter, expression);


        [Theory]
        [MemberData(nameof(NotEqualToTestCases))]
        public void BuildNotEqual(IEnumerable<SuperHero> superheroes, IKendoFilter filter, Expression<Func<SuperHero, bool>> expression)
            => Build(superheroes, filter, expression);


        /// <summary>
        /// Tests various filters
        /// </summary>
        /// <param name="superheroes">Collections of </param>
        /// <param name="filter">filter under test</param>
        /// <param name="expression">Expression the filter should match once built</param>
        private void Build(IEnumerable<SuperHero> superheroes, IKendoFilter filter, Expression<Func<SuperHero, bool>> expression)
        {
            _output.WriteLine($"Filtering {JsonConvert.SerializeObject(superheroes)}");
            _output.WriteLine($"Filter under test : {filter}");
            _output.WriteLine($"Reference expression : {expression.Body.ToString()}");

            Expression<Func<SuperHero, bool>> buildResult = filter.ToExpression<SuperHero>();
            IEnumerable<SuperHero> filteredResult = superheroes
                .Where(buildResult.Compile())
                .ToList();

            filteredResult.Should()
                .NotBeNull()
                .And.BeEquivalentTo(superheroes?.Where(expression.Compile()));

        }

    }
}
