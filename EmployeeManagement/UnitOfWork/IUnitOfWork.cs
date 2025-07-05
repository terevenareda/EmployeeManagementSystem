using EmployeeManagement.Repositories;

namespace EmployeeManagement.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IEmployeeRepository Employees { get; }
        IDepartmentRepository Departments { get; }
        ILogHistoryRepository Logs { get; }
        Task<int> CompleteAsync(); 
    }

}
