using System;
using System.Collections.Generic;
using Api.CostRules;
using Api.Models;
using Api.Services.Concretes;
using Api.Services.Contracts;
using FluentAssertions;
using FluentAssertions.Primitives;
using Xunit;

namespace ApiTests.UnitTests
{
    public class CostRulesTests
    {

        [Fact]
        public void BaseRuleShouldAdd12KToTheEmployeeCost()
        {
            var expected = 12000m;
            Employee emp = new();
            var rule = new SalaryBaseRule();
            var result = rule.Calculate(emp);
            result.Should().Be(expected);
        }

        [Fact]
        public void WhenTheEmployeeHasDependentsOlderThan50ItShouldCalculateTheCost()
        {
            var expected = 2400;
            Employee emp = new()
            {
                Dependents = new List<Dependent>()
                {
                    new Dependent
                    {
                        DateOfBirth = new DateTime(1960, 1, 1)
                    },
                    new Dependent
                    {
                        DateOfBirth = DateTime.Now.AddYears(-5)
                    }
                }
            };
            var rule = new DependentOlderThanFifty();
            var result = rule.Calculate(emp);
            result.Should().Be(expected);
        }

        [Fact]
        public void WhenTheEmployeeHasDependentsEachShouldCost600Monthly()
        {
            var expected = 2 * 600m * 12;
            Employee emp = new()
            {
                Dependents = new List<Dependent>()
                {
                    new Dependent
                    {
                        DateOfBirth = new DateTime(1960, 1, 1)
                    },
                    new Dependent
                    {
                        DateOfBirth = DateTime.Now.AddYears(-5)
                    }
                }
            };
            var rule = new DependentRule();
            var result = rule.Calculate(emp);
            result.Should().Be(expected);
        }

        [Fact]
        public void WhenTheEmployeeHasMoreThan80kYearlySalaryItShouldCalculate2Percent()
        {
            var salary = 120000m;
            var expected = salary * .02m;
            Employee emp = new()
            {
                Salary = salary
            };
            var rule = new HighSalaryRule();
            var result = rule.Calculate(emp);
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData(120000)]
        [InlineData(75000)]
        public void WhenMultipleRulesApplyCalculationShouldBeGood(decimal salary)
        {
            // In this case the rules for the employees to be applied are the following
            /*
             * Base cost. It should apply always
             * Dependent. It should add $600 x3 x12
             * High salary. It should add 120k *0.02
             * Older rule. 2 dependents are older than 50
             */
            Employee emp = new()
            {
                Salary = salary,
                DateOfBirth = new DateTime(1970, 1, 1),
                Dependents = new List<Dependent>()
                {
                    // This one is the spouse and have more than 50 years
                    new Dependent()
                    {
                        Relationship = Relationship.Spouse,
                        DateOfBirth = new DateTime(1960,1,1)
                    },
                    // This one is a child
                    new Dependent()
                    {
                        Relationship = Relationship.Child,
                        DateOfBirth = DateTime.Now.AddYears(-12)
                    },
                    // This one is the father
                    new Dependent()
                    {
                        Relationship = Relationship.None,
                        DateOfBirth = new DateTime(1940,1,1)
                    }
                }
            };
            (string,decimal) expectedBase = new("base", 12 * 1000m);
            (string, decimal) expectedDependents = new("dependents", 600m * 3 * 12);
            (string, decimal) salaryHigherThan80K = new("higherthan80k", emp.Salary * .02m);
            (string, decimal) oldDepdent = new("older", 200m * 12 * 2);
            if (salary < 80000)
                salaryHigherThan80K.Item2 = 0;
            var expected = expectedBase.Item2+expectedDependents.Item2+salaryHigherThan80K.Item2+oldDepdent.Item2;
            var rules = new List<ICostRule>()
            {
                new SalaryBaseRule(),
                new DependentOlderThanFifty(),
                new DependentRule(),
                new HighSalaryRule()
            };
            var ruleCalculator = new CostCalculatorService(rules);
            var result = ruleCalculator.Calculate(emp);
            result.Should().Be(expected);
        }


        [Theory]
        [InlineData(1999,12,31,0)]
        [InlineData(2001, 1, 2, 2400)]
        public void WhenTheAgeInTheDateOfTheCalculationIsLessThan50ItShouldNotApplyTheOldRule(int year,int month,int day,decimal expected)
        {
            var birthDate = new DateTime(1950,1,1);
            var calculationDate = new DateTime(year, month, day);

            Employee emp = new()
            {
                Dependents = new List<Dependent>()
                {
                    new Dependent
                    {
                        DateOfBirth = birthDate
                    }
                }
            };
            var rule = new DependentOlderThanFifty {CalculationDate = calculationDate};
            var result = rule.Calculate(emp);
            result.Should().Be(expected);
        }
    }
}