using EmployeeManagement.Entities;
using EmployeeManagement.Repositories;
using EmployeeManagement.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    [ApiController]
    [Route("api/logs")]
    public class LogController :ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public LogController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LogHistory>>> GetLogs()
        {
            var logs = await _unitOfWork.Logs.GetAllAsync();
            return Ok(logs);
        }
       
    }
    
}
