using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Models;
using AutoMapper;

namespace Api.Mappers
{
    public class EmployeeProfile:Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, GetEmployeeDto>();
        }
    }

    public class DepdendentProfile : Profile
    {
        public DepdendentProfile()
        {
            CreateMap<Dependent, GetDependentDto>();
        }
    }
}