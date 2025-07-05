using EmployeeManagement.Data;
using EmployeeManagement.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Repositories
{
    public class LogHistoryRepository : ILogHistoryRepository
    {
        private readonly ApplicationDbContext _context;

        public LogHistoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(LogHistory log)
        {
            _context.LogHistories.Add(log);
            await _context.SaveChangesAsync();
        }

        public async Task<List<LogHistory>> GetAllAsync()
        {
            return await _context.LogHistories.OrderByDescending(l => l.Timestamp).ToListAsync();
        }
    }
}
