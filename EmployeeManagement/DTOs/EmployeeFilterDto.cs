using EmployeeManagement.Entities;

namespace EmployeeManagement.DTOs
{
    public class EmployeeFilterDto
    {
        public string? Name { get; set; }
        public int? DepartmentId { get; set; }
        public EmployeeStatus? Status { get; set; }
        public DateTime? HireDateFrom { get; set; }
        public DateTime? HireDateTo { get; set; }
        public string? SortBy { get; set; } = "Name"; // default
        public string? SortOrder { get; set; } = "asc"; // "asc" or "desc"
    }
}
