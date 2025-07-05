namespace EmployeeManagement.Entities
{
    public class Employee
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public DateTime HireDate { get; set; }
        public EmployeeStatus Status { get; set; }

    }

    public enum EmployeeStatus{
            Active,
            Suspended
    }


}
