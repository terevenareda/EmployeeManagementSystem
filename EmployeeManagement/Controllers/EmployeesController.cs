using AutoMapper;
using EmployeeManagement.DTOs;
using EmployeeManagement.Entities;
using EmployeeManagement.Repositories;
using EmployeeManagement.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    [ApiController]
    [Route("api/employee")]
    public class EmployeesController : ControllerBase
    {
        
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public EmployeesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployees()
        {
            var employees = await _unitOfWork.Employees.GetAllAsync();
            var result = _mapper.Map<IEnumerable<EmployeeDto>>(employees);
            return Ok(result);
        }
        [HttpGet("paged")]
        public async Task<IActionResult> GetPagedEmployees(int pageNumber = 1, int pageSize = 5)
        {
            var (employees, totalCount) = await _unitOfWork.Employees.GetPagedAsync(pageNumber, pageSize);

            var employeeDtos = _mapper.Map<List<EmployeeDto>>(employees);

            var response = new
            {
                totalCount,
                pageNumber,
                pageSize,
                data = employeeDtos
            };

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDto>> GetEmployee(int id)
        {
            var emp = await _unitOfWork.Employees.GetByIdAsync(id);
            if (emp == null) return NotFound();
            return Ok(_mapper.Map<EmployeeDto>(emp));
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] CreateEmployeeDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var existingEmployee = await _unitOfWork.Employees.GetByEmailAsync(dto.Email);
            if (existingEmployee != null)
                return Conflict(new { message = "Email already exists" });

            var employee = _mapper.Map<Employee>(dto);
            var savedEmployee = await _unitOfWork.Employees.AddAsync(employee);
            await _unitOfWork.Logs.AddAsync(new LogHistory
            {
                Action = "Created",
                Timestamp = DateTime.UtcNow,
                EmployeeId = savedEmployee.ID,
                EmployeeName = employee.Name
            });
            return CreatedAtAction(nameof(GetEmployee), new { id = employee.ID }, employee);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateEmployee(int id, [FromBody] CreateEmployeeDto dto)
        {
            var existing = await _unitOfWork.Employees.GetByIdAsync(id);
            if (existing == null) return NotFound();

            existing.Name = dto.Name;
            existing.Email = dto.Email;
            existing.DepartmentId = dto.DepartmentId;
            existing.HireDate = dto.HireDate;
            existing.Status = dto.Status;

            await _unitOfWork.Employees.UpdateAsync(existing);
            await _unitOfWork.Logs.AddAsync(new LogHistory
            {
                Action = "Updated",
                Timestamp = DateTime.UtcNow,
                EmployeeId = existing.ID,
                EmployeeName = existing.Name
            });
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEmployee(int id)
        {
            var emp = await _unitOfWork.Employees.GetByIdAsync(id);
            if (emp == null) return NotFound();

            await _unitOfWork.Employees.DeleteAsync(id);
            await _unitOfWork.Logs.AddAsync(new LogHistory
            {
                Action = "Deleted",
                Timestamp = DateTime.UtcNow,
                EmployeeId = emp.ID,
                EmployeeName = emp.Name
            });
            return NoContent();
        }

        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<Employee>>> FilterEmployees([FromQuery] EmployeeFilterDto filter)
        {
            var result = await _unitOfWork.Employees.GetFilteredAsync(filter);
            return Ok(result);
        }
    }

}
