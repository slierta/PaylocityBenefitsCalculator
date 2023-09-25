using Api.Models;
using Api.Services.Contracts;

namespace Api.Services.Concretes
{
    /// <inheritdoc />
    public class PaycheckCalculatorService : IPaycheckCalculatorService
    {
        private readonly IEmployeeService _employeeService;
        private readonly ICostCalculatorService _costCalculatorService;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="employeeService"></param>
        /// <param name="costCalculatorService"></param>
        public PaycheckCalculatorService(IEmployeeService employeeService, ICostCalculatorService costCalculatorService)
        {
            _employeeService = employeeService;
            _costCalculatorService = costCalculatorService;
        }
        /// <summary>
        /// Number of yearly paychecks 
        /// </summary>
        public int NumberOfPayChecks { get; set; } = 26;

        /// <summary>
        /// Calculates the paycheck for the current day and employee id
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public async Task<decimal> Calculate(int employeeId) => await Calculate(employeeId, DateTime.Now);

        /// <summary>
        /// Calculate the paycheck for a specified date and employee id
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="calculationDate"></param>
        /// <returns></returns>
        public async Task<decimal> Calculate(int employeeId, DateTime calculationDate)
        {
            var employee = await _employeeService.Get(employeeId);
            return await Calculate(employee, calculationDate);
        }


        /// <summary>
        /// Calculate the paycheck for one employee for a specified date
        /// </summary>
        /// <param name="employee"></param>
        /// <param name="calculationDate"></param>
        /// <returns></returns>
        public Task<decimal> Calculate(Employee employee, DateTime calculationDate)
        {
            var cost = _costCalculatorService.Calculate(employee) / NumberOfPayChecks;
            var salary = employee.Salary / NumberOfPayChecks;
            var result = salary - cost;
            return Task.FromResult(result);
        }
    }
}