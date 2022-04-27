using System;

namespace models
{
    public class Employee
    {
        public Guid EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Department { get; set; }
        public Guid CompanyId { get; set; }
        public Company Company { get; set; }

        public string ToWordFile()
        {
            return $"{LastName} {FirstName} {MiddleName} {Address} {Phone} {Department}";
        }
        public string ToWordFileWithoutDepartment()
        {
            return $"{LastName} {FirstName} {MiddleName} {Address} {Phone}";
        }
    }
}