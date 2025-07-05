using EmployeeManagement.Entities;

namespace EmployeeManagement.Repositories
{
    public interface ILogHistoryRepository
    {
        Task AddAsync(LogHistory log);
        Task<List<LogHistory>> GetAllAsync();
    }
}
