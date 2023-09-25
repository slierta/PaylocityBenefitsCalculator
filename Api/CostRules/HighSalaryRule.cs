using Api.Models;
using Api.Services.Contracts;

namespace Api.CostRules
{
    /// <inheritdoc />
    public class HighSalaryRule : BaseRule, ICostRule
    {
        
        /// <summary>
        /// Description of the rule. It can be used for logs or reports
        /// </summary>
        public override string Description => "Each employee who earn most than 80k $ per year has a 2% of salary additional cost";

        /// <summary>
        /// When the employee earns more than 80k per year, it adds an additional 2% of cost
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public override decimal Calculate(Employee employee) => employee.Salary > 80000 ? (employee.Salary * .02m) : 0m;
    }
}