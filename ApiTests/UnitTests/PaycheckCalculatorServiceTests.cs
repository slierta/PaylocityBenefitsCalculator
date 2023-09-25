using Api.Models;
using Api.Services.Concretes;
using Api.Services.Contracts;
using AutoMapper;
using Moq;
using System;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace ApiTests.UnitTests
{
    public class PaycheckCalculatorServiceTests
    {
        private IPaycheckCalculatorService _service;
        private Mock<IEmployeeService> _employeeService;
        private Mock<ICostCalculatorService> _costCalculatorService;

        private readonly Employee _emp = new()
        {
            Id = 1,
            FirstName = "LeBron",
            LastName = "James",
            Salary = 75420.99m,
            DateOfBirth = new DateTime(1984, 12, 30)
        };

        public PaycheckCalculatorServiceTests()
        {
            _employeeService=new Mock<IEmployeeService>();
            _costCalculatorService =new Mock<ICostCalculatorService>();
            _service = new PaycheckCalculatorService(_employeeService.Object, _costCalculatorService.Object);
        }

        [Fact]
        public async Task WhenEmployeeHasNoDependentsPayCheckShouldBeJustSalary()
        {
            var expected =Math.Round(_emp.Salary / _service.NumberOfPayChecks,2);
            var result = Math.Round(await _service.Calculate(_emp, DateTime.Now),2);
            result.Should().Be(expected);
        }

        [Fact]
        public async Task WhenEmployeeHasCostsItShouldSubstractTheAmount()
        {
            var yearDeductions = 260m ;
            var expected = Math.Round(((_emp.Salary - yearDeductions) / _service.NumberOfPayChecks), 2);
            _costCalculatorService.Setup(s => s.Calculate(It.IsAny<Employee>())).Returns(260m);
            var result = Math.Round(await _service.Calculate(_emp, DateTime.Now),2);
            result.Should().Be(expected);
        }
    }
}