using Api.Models;
using Api.Services.Contracts;

namespace Api.CostRules
{
    /// <summary>
    /// Rule to add costs for each dependent
    /// </summary>
    public class DependentRule : BaseRule, ICostRule
    {
        /// <summary>
        /// Costly month to be applied
        /// </summary>
        public override decimal MonthlyCost { get; } = 600m;

        /// <summary>
        /// Description of the rule. It can be used for logs or reports
        /// </summary>
        public override string Description => "Each dependent has a 600 $ cost per month";
        /// <summary>
        /// For each of the dependents of the employee it will add $600 monthly
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public override decimal Calculate(Employee employee) => employee.Dependents.Count * MonthlyCost * Months;
    }
}