using Api.Models;
using Api.Services.Contracts;

namespace Api.CostRules
{
    /// <summary>
    /// Base rule for all the employees
    /// </summary>
    public class SalaryBaseRule : BaseRule, ICostRule
    {
       
        /// <summary>
        /// Costly month to be applied
        /// </summary>
        public decimal MonthlyCost { get; set; } = 1000m;
      
        /// <summary>
        /// Description of the rule. It can be used for logs or reports
        /// </summary>
        public override string Description => "Each employee has a base cost of $1000 monthly";

        /// <summary>
        /// Returns the cost of the employee for a year period
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public override decimal Calculate(Employee employee) => MonthlyCost*Months;
    }
}