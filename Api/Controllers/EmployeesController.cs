using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Models;
using Api.Services.Contracts;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeService _employeeService;
    private readonly IMapper _mapper;

    public EmployeesController(IEmployeeService employeeService,IMapper mapper)
    {
        _employeeService = employeeService;
        _mapper = mapper;
    }


    [SwaggerOperation(Summary = "Get employee by id")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<GetEmployeeDto>>> Get(int id)
    {
        var result = new ApiResponse<GetEmployeeDto>()
        {
            Data = _mapper.Map<GetEmployeeDto>(_employeeService.Employees.SingleOrDefault(s => s.Id == id))
        };
        result.Success = result.Data != null;
        if (!result.Success)
            return NotFound();
        return result;

    }
    [SwaggerOperation(Summary = "Get dependents of one employee by id")]
    [HttpGet("{id}/Dependents")]
    public async Task<ActionResult<ApiResponse<List<GetDependentDto>>>> GetDependents(int id)
    {
        var emp = await _employeeService.Get(id);
        if (emp == null)
            return NotFound();
        var result = new ApiResponse<List<GetDependentDto>>
        {
            Success = true,
            Data = _mapper.Map<List<GetDependentDto>>(emp.Dependents??Enumerable.Empty<Dependent>())
        };
        return result;

    }
  
    [SwaggerOperation(Summary = "Get all employees")]
    [HttpGet("")]
    public async Task<ActionResult<ApiResponse<List<GetEmployeeDto>>>> GetAll()
    {


        var result = new ApiResponse<List<GetEmployeeDto>>
        {
            Data = _mapper.Map<List<GetEmployeeDto>>(_employeeService.Employees.ToList()),
            Success = true
        };

        return result;
    }
    /// <summary>
    /// Add a new dependent to an existing employee
    /// </summary>
    /// <param name="id"></param>
    /// <param name="dependent"></param>
    /// <returns></returns>
    [SwaggerOperation(Summary = "Add one dependent")]
    [HttpPost("{id}/Dependents")]
    public async Task<ActionResult<ApiResponse<GetDependentDto>>> CreateDependent(int id, [FromBody] PostDependentDto dependent)
    {
        var employee=await _employeeService.Get(id);
        if (employee == null)
            return NotFound();
        var employeeHasPartner = await _employeeService.HasPartner(id);
        // TODO: When the employee does not have a spouse or partner, it should store the data into the db
        return employeeHasPartner ? BadRequest() : Ok();
    }
}
