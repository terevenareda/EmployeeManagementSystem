﻿using EmployeeManagement.Data;
using EmployeeManagement.DTOs;
using EmployeeManagement.Entities;
using Microsoft.EntityFrameworkCore;
namespace EmployeeManagement.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;
        public EmployeeRepository(ApplicationDbContext context) => _context = context;
        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _context.Employees
                .Include(e => e.Department)
                .ToListAsync();
        }
        public async Task<(List<Employee> Data, int TotalCount)> GetPagedAsync(int pageNumber, int pageSize)
        {
            var query = _context.Employees
                .Include(e => e.Department) 
                .AsQueryable();

            var totalCount = await query.CountAsync();

            var data = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (data, totalCount);
        }

        public async Task<Employee?> GetByIdAsync(int id)
        {
            return await _context.Employees
                .Include(e => e.Department)
                .FirstOrDefaultAsync(e => e.ID == id);
        }
        public async Task<Employee> AddAsync(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return employee;
        }
        public async Task UpdateAsync(Employee employee)
        {
            _context.Entry(employee).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var emp = await _context.Employees.FindAsync(id);
            if (emp != null)
            {
                _context.Employees.Remove(emp);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<Employee?> GetByEmailAsync(string email)
        {
            return await _context.Employees.FirstOrDefaultAsync(e => e.Email == email);
        }
        public async Task<IEnumerable<Employee>> GetFilteredAsync(EmployeeFilterDto filter)
        {
            var query = _context.Employees
                .Include(e => e.Department)
                .AsQueryable();

            // Filtering
            if (!string.IsNullOrWhiteSpace(filter.Name))
                query = query.Where(e => e.Name.Contains(filter.Name));

            if (filter.DepartmentId.HasValue)
                query = query.Where(e => e.DepartmentId == filter.DepartmentId.Value);

            if (filter.Status.HasValue)
                query = query.Where(e => e.Status == filter.Status.Value);

            if (filter.HireDateFrom.HasValue)
                query = query.Where(e => e.HireDate >= filter.HireDateFrom.Value);

            if (filter.HireDateTo.HasValue)
                query = query.Where(e => e.HireDate <= filter.HireDateTo.Value);

            // Sorting
            query = (filter.SortBy?.ToLower(), filter.SortOrder?.ToLower()) switch
            {
                ("hiredate", "desc") => query.OrderByDescending(e => e.HireDate),
                ("hiredate", _) => query.OrderBy(e => e.HireDate),
                ("name", "desc") => query.OrderByDescending(e => e.Name),
                _ => query.OrderBy(e => e.Name) // default sort
            };

            return await query.ToListAsync();
        }
    }
}
