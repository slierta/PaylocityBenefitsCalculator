using System;
using System.Linq;
using System.Threading.Tasks;
using Api.Services.Contracts;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace ApiTests.IntegrationTests
{
    public class PaycheckCalculatorIntegrationTests:IClassFixture<PaycheckCalculatorFixture>
    {
        private readonly PaycheckCalculatorFixture fixture;

        public PaycheckCalculatorIntegrationTests(PaycheckCalculatorFixture fixture)
        {
            this.fixture=fixture;
        }
        [Fact]
        public async Task WhenTheEmployee_DoesNotHaveAnyDependent_CostShouldBeSalaryDividedByPayChecks()
        {
            var employee = fixture.Employees.Single(s => s.Id == 1);
            var salary = employee.Salary;
            var costs = fixture.ServiceProvider.GetRequiredService<ICostCalculatorService>().Calculate(employee);
            var expected = (salary - costs) / fixture.PaycheckCalculatorService.NumberOfPayChecks;
            var result = await fixture.PaycheckCalculatorService.Calculate(employee, DateTime.Now);
            result.Should().Be(expected);
        }

        [Fact]
        public async Task WhenEmployeeHaveDependentsCalculationShouldBeRight()
        {
            var employee = fixture.Employees.Single(s => s.Id == 2);
            var salary = employee.Salary;
            var costs = fixture.ServiceProvider.GetRequiredService<ICostCalculatorService>().Calculate(employee);
            var expected = (salary - costs) / fixture.PaycheckCalculatorService.NumberOfPayChecks;
            var result = await fixture.PaycheckCalculatorService.Calculate(employee, DateTime.Now);
            result.Should().Be(expected);
        }
    }
}