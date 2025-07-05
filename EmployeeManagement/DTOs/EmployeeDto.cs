using EmployeeManagement.Entities;

namespace EmployeeManagement.DTOs
{
    public class EmployeeDto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string? DepartmentName { get; set; }  
        public DateTime HireDate { get; set; }
        public EmployeeStatus Status { get; set; }
    }

}
