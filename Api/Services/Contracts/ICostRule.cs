using Api.Models;

namespace Api.Services.Contracts
{
    /// <summary>
    /// Contains the description of a cost rule to calculate costs
    /// </summary>
    public interface ICostRule
    {
        /// <summary>
        /// Contains the date where the calculation is done
        /// </summary>
        public DateTime CalculationDate { get; set; }


        /// <summary>
        /// Costly month to be applied
        /// </summary>
        public decimal MonthlyCost { get; }
        /// <summary>
        /// Months to be used in the calculation, by default is the full year
        /// </summary>
        public int Months { get; }
        /// <summary>
        /// Description of the rule. It can be used for logs or reports
        /// </summary>
        string Description { get; }
        /// <summary>
        /// Returns the cost of the employee for a year period
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        decimal Calculate(Employee employee);
    }
}