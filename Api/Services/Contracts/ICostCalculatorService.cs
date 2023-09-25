using Api.Models;

namespace Api.Services.Contracts
{
    /// <summary>
    /// Service to calculate the associated costs to one employee
    /// </summary>
    public interface ICostCalculatorService
    {
        /// <summary>
        /// Calculate the costs for the current date
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        decimal Calculate(Employee employee);
        /// <summary>
        /// Calculate the costs for one specified date
        /// </summary>
        /// <param name="employee"></param>
        /// <param name="calculationDate"></param>
        /// <returns></returns>
        decimal Calculate(Employee employee, DateTime calculationDate);
    }
}