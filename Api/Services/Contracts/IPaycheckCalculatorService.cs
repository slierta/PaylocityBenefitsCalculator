using Api.Models;

namespace Api.Services.Contracts
{
    /// <summary>
    /// Service that calculates the paycheck
    /// </summary>
    public interface IPaycheckCalculatorService
    {
        /// <summary>
        /// Number of yearly paychecks 
        /// </summary>
        int NumberOfPayChecks { get; set; }

        /// <summary>
        /// Calculates the paycheck for the current day and employee id
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        Task<decimal> Calculate(int employeeId);
        /// <summary>
        /// Calculate the paycheck for a specified date and employee id
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="calculationDate"></param>
        /// <returns></returns>
        Task<decimal> Calculate(int employeeId, DateTime calculationDate);
        /// <summary>
        /// Calculate the paycheck for one employee for a specified date
        /// </summary>
        /// <param name="employee"></param>
        /// <param name="calculationDate"></param>
        /// <returns></returns>
        Task<decimal> Calculate(Employee employee, DateTime calculationDate);
    }
}