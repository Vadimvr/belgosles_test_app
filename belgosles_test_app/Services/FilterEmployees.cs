using models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace belgosles_test_app.Services
{
    public static class FilterEmployees
    {
        internal static ObservableCollection<Employee> Filter(
            string findFirstName,
            string findLastName,
            string findMiddleName,
            string findAddress,
            string findDepartment,
            string findPhone,
            List<Employee> immutableListOfEmployees)
        {
            if (immutableListOfEmployees != null)
            {
                IEnumerable<Employee> res = new List<Employee>(immutableListOfEmployees);
                if (!string.IsNullOrEmpty(findFirstName))
                {
                    res = res.Where(p => p.FirstName.Contains(findFirstName, StringComparison.OrdinalIgnoreCase));
                }

                if (!string.IsNullOrEmpty(findLastName))
                {
                    res = res.Where(p => p.LastName.Contains(findLastName, StringComparison.OrdinalIgnoreCase));
                }


                if (!string.IsNullOrEmpty(findMiddleName))
                {
                    res = res.Where(p => p.MiddleName.Contains(findMiddleName, StringComparison.OrdinalIgnoreCase));
                }

                if (!string.IsNullOrEmpty(findAddress))
                {
                    res = res.Where(p => p.Address.Contains(findAddress, StringComparison.OrdinalIgnoreCase));
                }

                if (!string.IsNullOrEmpty(findDepartment))
                {
                    res = res.Where(p => p.Department.Contains(findDepartment, StringComparison.OrdinalIgnoreCase));
                }

                if (!string.IsNullOrEmpty(findPhone))
                {
                    res = res.Where(p => p.Phone.Contains(findPhone, StringComparison.OrdinalIgnoreCase));
                }
                return new ObservableCollection<Employee>(res);
            }
            else
            {
                return new ObservableCollection<Employee>();
            }

        }
    }
}
