using Api.Extensions;
using Api.Models;
using Api.Services.Contracts;

namespace Api.CostRules
{
    /// <summary>
    /// 
    /// </summary>
    public class DependentOlderThanFifty : BaseRule, ICostRule
    {
        /// <summary>
        /// Description of the rule. It can be used for logs or reports
        /// </summary>
        public override string Description=>"Each dependent that is older of 50 years old, have an additional $200 cost per month";

        /// <summary>
        /// Returns the cost of the employee for a year period
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public override decimal Calculate(Employee employee) => employee.Dependents.Count(d => d.DateOfBirth.Age(CalculationDate) > 50) * MonthlyCost * Months;

        /// <summary>
        /// Costly month to be applied
        /// </summary>
        public override decimal MonthlyCost { get; } = 200m;
    }
}