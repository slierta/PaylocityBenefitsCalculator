using System.Collections.ObjectModel;
using Api.Models;
using Api.Services.Contracts;
using AutoMapper;

namespace Api.Services.Concretes
{
    /// <summary>
    /// Contains all the methods from <see cref="IEmployeeService"/>
    /// </summary>
    public class EmployeeService : IEmployeeService
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="mapper"></param>
        public EmployeeService()
        {
        }
        private readonly IReadOnlyCollection<Employee> _employeeList = new List<Employee>()
        {
            new()
            {
                Id = 1,
                FirstName = "LeBron",
                LastName = "James",
                Salary = 75420.99m,
                DateOfBirth = new(1984, 12, 30)
            }, 
            new()
            {
                Id = 2,
                FirstName = "Ja",
                LastName = "Morant",
                Salary = 92365.22m,
                DateOfBirth = new(1999, 8, 10),
                Dependents = new List<Dependent>
                {
                    new()
                    {
                        Id = 1,
                        FirstName = "Spouse",
                        LastName = "Morant",
                        Relationship = Relationship.Spouse,
                        DateOfBirth = new(1998, 3, 3)
                    },
                    new()
                    {
                        Id = 2,
                        FirstName = "Child1",
                        LastName = "Morant",
                        Relationship = Relationship.Child,
                        DateOfBirth = new(2020, 6, 23)
                    },
                    new()
                    {
                        Id = 3,
                        FirstName = "Child2",
                        LastName = "Morant",
                        Relationship = Relationship.Child,
                        DateOfBirth = new(2021, 5, 18)
                    }
                }
            },
            new()
            {
                Id = 3,
                FirstName = "Michael",
                LastName = "Jordan",
                Salary = 143211.12m,
                DateOfBirth = new(1963, 2, 17),
                Dependents = new List<Dependent>
                {
                    new()
                    {
                        Id = 4,
                        FirstName = "DP",
                        LastName = "Jordan",
                        Relationship = Relationship.DomesticPartner,
                        DateOfBirth = new(1974, 1, 2)
                    }
                }
            }
        };

        /// <summary>
        /// Public access to the Employee
        /// </summary>
        public IReadOnlyList<Employee> Employees => new ReadOnlyCollection<Employee>((IList<Employee>)_employeeList) ;


        /// <summary>
        /// Check if the current employee contains any dependent which is an spouse or partner
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> HasPartner(int id) =>await Task.FromResult(_employeeList.SingleOrDefault(emp => emp.Id == id)?.Dependents.Any(d =>
            d.Relationship is Relationship.Spouse or Relationship.DomesticPartner)??false);


        /// <summary>
        /// Retrieves the employee data with the possibility to include the dependents in the response
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Employee?> Get(int id) =>await Task.FromResult(_employeeList.SingleOrDefault(s => s.Id == id));
    }
}