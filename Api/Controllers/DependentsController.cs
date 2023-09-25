using Api.Dtos.Dependent;
using Api.Models;
using Api.Services.Contracts;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class DependentsController : ControllerBase
{
    private readonly IEmployeeService _employeeService;
    private readonly IMapper _mapper;

    public DependentsController(IEmployeeService employeeService,IMapper mapper)
    {
        _employeeService = employeeService;
        _mapper = mapper;
    }

    [SwaggerOperation(Summary = "Get dependent by id")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<GetDependentDto>>> Get(int id)
    {
        var employee = _employeeService.Employees.SingleOrDefault(s => s.Dependents.Any(t => t.Id == id));
        if (employee == null)
            return NotFound();
        var dependant=employee.Dependents.Single(s=>s.Id==id);
        var result = new ApiResponse<GetDependentDto>()
        {
            Success = true,
            Data = _mapper.Map<GetDependentDto>(dependant)
        };
        return result;
    }

    [SwaggerOperation(Summary = "Get all dependents")]
    [HttpGet("")]
    public async Task<ActionResult<ApiResponse<List<GetDependentDto>>>> GetAll()
    {
        var result = new ApiResponse<List<GetDependentDto>>
        {
            Data = _mapper.Map<List<GetDependentDto>>(_employeeService.Employees.SelectMany(s => s.Dependents)),
            Success = true
        };
        return result;
    }
   
}
