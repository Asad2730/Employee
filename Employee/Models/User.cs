namespace Employee.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ManagerId { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}
