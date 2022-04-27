using System;
using System.Collections.Generic;

namespace models
{
    public class Company
    {
        public Guid CompanyId { get; set; }
        public string CompanyName { get; set; }

        public List<Employee> Employees { get; }

    }
}
