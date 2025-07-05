using EmployeeManagement.Entities;
using EmployeeManagement.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    [ApiController]
    [Route("api/logs")]
    public class LogController :ControllerBase
    {
        private readonly ILogHistoryRepository _repository;
        public LogController(ILogHistoryRepository repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LogHistory>>> GetLogs()
        {
            var logs = await _repository.GetAllAsync();
            return Ok(logs);
        }
       
    }
    
}
