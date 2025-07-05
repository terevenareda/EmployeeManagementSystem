namespace EmployeeManagement.Entities
{
    public class LogHistory
    {
        public int ID { get; set; }

        public string Action { get; set; }

        public int EmployeeId { get; set; }

        public string EmployeeName { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
