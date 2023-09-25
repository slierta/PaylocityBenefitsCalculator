using Api.Models;
using Api.Services.Contracts;

namespace Api.CostRules
{
    /// <summary>
    /// Common base rule
    /// </summary>
    public abstract class BaseRule:ICostRule
    {
        /// <summary>
        /// Contains the date where the calculation is done
        /// </summary>
        public virtual DateTime CalculationDate { get; set; }=DateTime.Now;

        /// <summary>
        /// Costly month to be applied
        /// </summary>
        public virtual decimal MonthlyCost { get; } = 0m;

        /// <summary>
        /// Months to be used in the calculation, by default is the full year
        /// </summary>
        public virtual int Months { get;  } = 12;

        /// <summary>
        /// Description of the rule. It can be used for logs or reports
        /// </summary>
        public abstract string Description { get; }

        /// <summary>
        /// Returns the cost of the employee for a year period
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public abstract decimal Calculate(Employee employee) ;
    }
}