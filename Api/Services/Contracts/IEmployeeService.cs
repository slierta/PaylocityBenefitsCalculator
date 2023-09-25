using Api.Dtos.Employee;
using Api.Models;

namespace Api.Services.Contracts
{
    /// <summary>
    /// It provides access to the employee services
    /// </summary>
    public interface IEmployeeService
    {
        IReadOnlyList<Employee> Employees { get; }


        /// <summary>
        /// Check if the current employee contains any dependent which is an spouse or partner
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> HasPartner(int id);

        /// <summary>
        /// Retrieves the employee data with the possibility to include the dependents in the response
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Employee?> Get(int id);

    }
}