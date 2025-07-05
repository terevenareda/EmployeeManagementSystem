using EmployeeManagement.Entities;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.DTOs
{
    public class CreateEmployeeDto
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Department is required")]
        [Range(1, int.MaxValue, ErrorMessage = "DepartmentId must be greater than 0")]
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "Hire date is required")]
        public DateTime HireDate { get; set; }

        [Required(ErrorMessage = "Status is required")]
        [EnumDataType(typeof(EmployeeStatus), ErrorMessage = "Invalid status")]
        public EmployeeStatus Status { get; set; }
    }

}

