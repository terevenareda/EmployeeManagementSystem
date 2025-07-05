using EmployeeManagement.DTOs;
using EmployeeManagement.Entities;

namespace EmployeeManagement.Repositories
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllAsync();
        Task<(List<Employee> Data, int TotalCount)> GetPagedAsync(int pageNumber, int pageSize);
        Task<Employee?> GetByIdAsync(int id);
        Task<Employee> AddAsync(Employee employee);
        Task UpdateAsync(Employee employee);
        Task DeleteAsync(int id);
        Task<Employee?> GetByEmailAsync(string email);
        Task<IEnumerable<Employee>> GetFilteredAsync(EmployeeFilterDto filter);
    }
}
