using AutoMapper;
using EmployeeManagement.DTOs;
using EmployeeManagement.Entities;
using EmployeeManagement.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    [ApiController]
    [Route("api/employee")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogHistoryRepository _log;
        public EmployeesController(IEmployeeRepository repository, ILogHistoryRepository log, IMapper mapper)
        {
            _repository = repository;
            _log = log;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployees()
        {
            var employees = await _repository.GetAllAsync();
            var result = _mapper.Map<IEnumerable<EmployeeDto>>(employees);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDto>> GetEmployee(int id)
        {
            var emp = await _repository.GetByIdAsync(id);
            if (emp == null) return NotFound();
            return Ok(_mapper.Map<EmployeeDto>(emp));
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] CreateEmployeeDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var existingEmployee = await _repository.GetByEmailAsync(dto.Email);
            if (existingEmployee != null)
                return Conflict(new { message = "Email already exists" });

            var employee = _mapper.Map<Employee>(dto);
            var savedEmployee = await _repository.AddAsync(employee);
            await _log.AddAsync(new LogHistory
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
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return NotFound();

            existing.Name = dto.Name;
            existing.Email = dto.Email;
            existing.DepartmentId = dto.DepartmentId;
            existing.HireDate = dto.HireDate;
            existing.Status = dto.Status;

            await _repository.UpdateAsync(existing);
            await _log.AddAsync(new LogHistory
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
            var emp = await _repository.GetByIdAsync(id);
            if (emp == null) return NotFound();

            await _repository.DeleteAsync(id);
            await _log.AddAsync(new LogHistory
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
            var result = await _repository.GetFilteredAsync(filter);
            return Ok(result);
        }
    }

}
