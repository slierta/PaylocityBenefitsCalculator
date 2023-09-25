using Api.Services.Contracts;
using System.Diagnostics.CodeAnalysis;

namespace Api.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class CostRulesExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public static void RegisterCostRules(this IServiceCollection services)
        {
            services.AddTransient<ICostRule, CostRules.SalaryBaseRule>();
            services.AddTransient<ICostRule, CostRules.DependentOlderThanFifty>();
            services.AddTransient<ICostRule, CostRules.DependentRule>();
            services.AddTransient<ICostRule, CostRules.HighSalaryRule>();
        }
    }
}