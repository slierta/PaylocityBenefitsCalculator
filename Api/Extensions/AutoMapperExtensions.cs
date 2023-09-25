using System.Diagnostics.CodeAnalysis;
using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Models;
using AutoMapper;

namespace Api.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class AutoMapperExtensions
    {
        public static MapperConfiguration BuildMapperConfiguration()
        {
            return  new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Employee, GetEmployeeDto>();
                cfg.CreateMap<Dependent, GetDependentDto>();
            });
        }
    }
}