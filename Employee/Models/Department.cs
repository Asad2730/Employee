﻿namespace Employee.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<User> Employees { get; set; }
    }
}