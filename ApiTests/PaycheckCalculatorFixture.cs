using System;
using System.Collections.Generic;
using Api.Extensions;
using Api.Models;
using Api.Services.Concretes;
using Api.Services.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace ApiTests
{
    /// <inheritdoc />
    public class PaycheckCalculatorFixture : IDisposable
    {
        public IServiceProvider ServiceProvider { get; private set; }
        public IPaycheckCalculatorService PaycheckCalculatorService { get; private set;}
        IServiceCollection _services;
        public IReadOnlyList<Employee> Employees { get; private set; }
        /// <summary>
        /// Default constructor
        /// </summary>
        public PaycheckCalculatorFixture()
        {
            _services = new ServiceCollection();
            _services.AddTransient<IEmployeeService, EmployeeService>();
            _services.AddTransient<ICostCalculatorService, CostCalculatorService>();
            _services.AddTransient<IPaycheckCalculatorService, PaycheckCalculatorService>();
            _services.RegisterCostRules();
            _services.AddAutoMapper(typeof(Program).Assembly);
            ServiceProvider = _services.BuildServiceProvider();
            // Populates the employee list
            Employees = ServiceProvider.GetRequiredService<IEmployeeService>().Employees;
            PaycheckCalculatorService = ServiceProvider.GetRequiredService<IPaycheckCalculatorService>();
        }
        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {
            _services.Clear();
            ServiceProvider = null;
        }
    }
}