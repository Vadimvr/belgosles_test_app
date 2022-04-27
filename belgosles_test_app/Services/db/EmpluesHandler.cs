using db;
using models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using belgosles_test_app.Models;

namespace belgosles_test_app.Services.db
{
    public static class EmpluesHandler
    {
        public static List<Employee> Get(Company company)
        {
            List<Employee> res = new List<Employee>();
            if (company != null)
            {
                using (ApplicationDBContext db = new ApplicationDBContext(PathToDb.Path))
                {
                    res = db.Employees.ToArray().Where(x => x.CompanyId == company.CompanyId).ToList();
                }
            }
            return res;
        }

        internal static void Set(Employee employee, Company company)
        {
            using (ApplicationDBContext db = new ApplicationDBContext(PathToDb.Path))
            {
                Company company1 = db.Companies.FirstOrDefault(x => x.CompanyId == company.CompanyId);
                if (company1 != null)
                {
                    employee.Company = company1;
                    db.Employees.Add(employee);
                    db.SaveChanges();
                }
                //db.Employees.Add(employee);
            }
        }

        internal static void Del(Employee selectEmployee)
        {
            if (selectEmployee != null)
            {
                using (ApplicationDBContext db = new ApplicationDBContext(PathToDb.Path))
                {
                    Employee employee = db.Employees.FirstOrDefault(x => x.EmployeeId == selectEmployee.EmployeeId);
                    if (employee != null)
                    {
                        db.Employees.Remove(employee);
                        db.SaveChanges();
                    }
                }
            }
        }
        internal static void Update(Employee oldEmployee, Employee newEmplooye)
        {
            if (newEmplooye != null)
            {
                using (ApplicationDBContext db = new ApplicationDBContext(PathToDb.Path))
                {
                    Employee employee = db.Employees.FirstOrDefault(x => x.EmployeeId == oldEmployee.EmployeeId);
                    if (employee != null)
                    {
                        
                        employee.FirstName = newEmplooye.FirstName;
                        employee.LastName = newEmplooye.LastName;
                        employee.MiddleName = newEmplooye.MiddleName;
                        employee.Phone = newEmplooye.Phone;
                        employee.Department = newEmplooye.Department;
                        db.Employees.Update(employee);
                        db.SaveChanges();
                    }
                }
            }
        }
    }
}
