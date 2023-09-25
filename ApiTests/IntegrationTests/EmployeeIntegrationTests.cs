using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Models;
using Microsoft.Extensions.FileProviders;
using Xunit;

namespace ApiTests.IntegrationTests;

public class EmployeeIntegrationTests : IntegrationTest
{
    [Fact]
    public async Task WhenAskedForAllEmployees_ShouldReturnAllEmployees()
    {
        var response = await HttpClient.GetAsync("/api/v1/employees");
        var employees = new List<GetEmployeeDto>
        {
            new()
            {
                Id = 1,
                FirstName = "LeBron",
                LastName = "James",
                Salary = 75420.99m,
                DateOfBirth = new DateTime(1984, 12, 30)
            },
            new()
            {
                Id = 2,
                FirstName = "Ja",
                LastName = "Morant",
                Salary = 92365.22m,
                DateOfBirth = new DateTime(1999, 8, 10),
                Dependents = new List<GetDependentDto>
                {
                    new()
                    {
                        Id = 1,
                        FirstName = "Spouse",
                        LastName = "Morant",
                        Relationship = Relationship.Spouse,
                        DateOfBirth = new DateTime(1998, 3, 3)
                    },
                    new()
                    {
                        Id = 2,
                        FirstName = "Child1",
                        LastName = "Morant",
                        Relationship = Relationship.Child,
                        DateOfBirth = new DateTime(2020, 6, 23)
                    },
                    new()
                    {
                        Id = 3,
                        FirstName = "Child2",
                        LastName = "Morant",
                        Relationship = Relationship.Child,
                        DateOfBirth = new DateTime(2021, 5, 18)
                    }
                }
            },
            new()
            {
                Id = 3,
                FirstName = "Michael",
                LastName = "Jordan",
                Salary = 143211.12m,
                DateOfBirth = new DateTime(1963, 2, 17),
                Dependents = new List<GetDependentDto>
                {
                    new()
                    {
                        Id = 4,
                        FirstName = "DP",
                        LastName = "Jordan",
                        Relationship = Relationship.DomesticPartner,
                        DateOfBirth = new DateTime(1974, 1, 2)
                    }
                }
            }
        };
        await response.ShouldReturn(HttpStatusCode.OK, employees);
    }

    [Fact]
    //task: make test pass
    public async Task WhenAskedForAnEmployee_ShouldReturnCorrectEmployee()
    {
        var response = await HttpClient.GetAsync("/api/v1/employees/1");
        var employee = new GetEmployeeDto
        {
            Id = 1,
            FirstName = "LeBron",
            LastName = "James",
            Salary = 75420.99m,
            DateOfBirth = new DateTime(1984, 12, 30)
        };
        await response.ShouldReturn(HttpStatusCode.OK, employee);
    }

    [Fact]
    //task: make test pass
    public async Task WhenAskedForANonexistentEmployee_ShouldReturn404()
    {
        var response = await HttpClient.GetAsync($"/api/v1/employees/{int.MinValue}");
        await response.ShouldReturn(HttpStatusCode.NotFound);
    }

    [Theory]
    [InlineData(1, null, HttpStatusCode.BadRequest)]
    [InlineData(1, "Margot", HttpStatusCode.OK)]
    [InlineData(2, null, HttpStatusCode.BadRequest)]
    [InlineData(2, "Margot", HttpStatusCode.BadRequest)]

    public async Task WhenAskedToAddNewPartnerItShouldValidateIfTheEmployeeHaveAlready(int employeeId, string? firstName, HttpStatusCode expectedResult)
    {
        var data = new PostDependentDto()
        {
            Relationship = Relationship.Spouse,
            FirstName = firstName,
            LastName = "Spouse",
            DateOfBirth = new DateTime(1980, 1, 1),
        };
        var response = await HttpClient.PostAsJsonAsync($"/api/v1/employees/{employeeId}/dependents", data);
        await response.ShouldReturn(expectedResult);
    }

    [Fact]
    public async Task WhenAksForDependentsOfOneEmployeeWhoDoesNotHaveShouldReturnOkAndEmptyList()
    {
        var response = await HttpClient.GetAsync($"/api/v1/employees/1/Dependents");
        await response.ShouldReturn<List<GetDependentDto>>(HttpStatusCode.OK, new List<GetDependentDto>());
    }

    [Fact]
    public async Task WhenAskForDependentsOfOneEmployeeShouldReturnThem()
    {
        var response = await HttpClient.GetAsync($"/api/v1/employees/2/Dependents");
        await response.ShouldReturn<List<GetDependentDto>>(HttpStatusCode.OK, new List<GetDependentDto>()
        {
            new()
            {
                Id = 1,
                FirstName = "Spouse",
                LastName = "Morant",
                Relationship = Relationship.Spouse,
                DateOfBirth = new DateTime(1998, 3, 3)
            },
            new()
            {
                Id = 2,
                FirstName = "Child1",
                LastName = "Morant",
                Relationship = Relationship.Child,
                DateOfBirth = new DateTime(2020, 6, 23)
            },
            new()
            {
                Id = 3,
                FirstName = "Child2",
                LastName = "Morant",
                Relationship = Relationship.Child,
                DateOfBirth = new DateTime(2021, 5, 18)
            }
        });
    }

    [Fact]
    public async Task WhenAskForDependentsOfOneEmployeeWhichDoesNotExistsShouldReturn404()
    {
        var response = await HttpClient.GetAsync($"/api/v1/employees/500/Dependents");
        await response.ShouldReturn(HttpStatusCode.NotFound);
    }
}

