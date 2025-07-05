using EmployeeManagement.Repositories;
using System;
using EmployeeManagement.Data;
namespace EmployeeManagement.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IEmployeeRepository Employees { get; private set; }
        public IDepartmentRepository Departments { get; private set; }
        public ILogHistoryRepository Logs { get; private set; }

        public UnitOfWork(ApplicationDbContext context,
                          IEmployeeRepository employeeRepo,
                          IDepartmentRepository departmentRepo,
                          ILogHistoryRepository logRepo)
        {
            _context = context;
            Employees = employeeRepo;
            Departments = departmentRepo;
            Logs = logRepo;
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
