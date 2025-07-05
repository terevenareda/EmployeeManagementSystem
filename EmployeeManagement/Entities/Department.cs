namespace EmployeeManagement.Entities
{
    public class Department
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public ICollection<Employee> Employees { get; set; }

    }
}
