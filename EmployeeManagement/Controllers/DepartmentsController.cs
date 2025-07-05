using EmployeeManagement.Data;
using EmployeeManagement.DTOs;
using EmployeeManagement.Entities;
using EmployeeManagement.Repositories;
using EmployeeManagement.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Controllers
{
    [ApiController]
    [Route("api/department")]
    public class DepartmentsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
       
   

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Department>>> GetDepartments() =>
            Ok(await _unitOfWork.Departments.GetAllAsync());


        [HttpGet("{id}")]
        public async Task<ActionResult<Department>> GetDepartment(int id)
        {
            var department = await _unitOfWork.Departments.GetByIdAsync(id);
            if (department == null)
                return NotFound();

            return Ok(department);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDepartment([FromBody] DepartmentDto dto)
        {
            var department = new Department
            {
                Name = dto.Name
            };

            await _unitOfWork.Departments.AddAsync(department);
            await _unitOfWork.CompleteAsync();

            return CreatedAtAction(nameof(GetDepartment), new { id = department.ID }, department);
        }
    }
}
