using Api.Models;
using Api.Services.Contracts;

namespace Api.Services.Concretes
{
    /// <summary>
    /// Service used to calculate the cost for one employee
    /// </summary>
    public class CostCalculatorService : ICostCalculatorService
    {
        /// <summary>
        /// Full list of the rules that will be used
        /// </summary>
        private readonly IEnumerable<ICostRule> _rules;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="rules"></param>
        public CostCalculatorService(IEnumerable<ICostRule> rules)
        {
            _rules = rules;
        }

        /// <inheritdoc />
        public  decimal Calculate(Employee employee) => _rules.Sum(rule => rule.Calculate(employee));

        /// <summary>
        /// Calculate the costs for one specified date
        /// </summary>
        /// <param name="employee"></param>
        /// <param name="calculationDate"></param>
        /// <returns></returns>
        /// <remarks>
        /// This method is not tested, but because I wasn't sure it is needed thinking of the birthday of the employee
        /// </remarks>
        public decimal Calculate(Employee employee, DateTime calculationDate) => _rules.Sum(rule =>
        {
            rule.CalculationDate = calculationDate;
            return rule.Calculate(employee);
        });
    }
}