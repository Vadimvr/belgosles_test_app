using models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace belgosles_test_app.Models
{
    public class CompanyWrappper
    {

        public CompanyWrappper() { }
        public Guid CompanyId { get; set; }
        public string CompanyName { get; set; }

        public List<Employee> Employees { get; set; }
    }
}
