using belgosles_test_app.Models;
using db;
using models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace belgosles_test_app.Services.db
{
    public static class CompanyHandler
    {
        public static List<Company> Get()
        {
            List<Company> res = new List<Company>();
            using (ApplicationDBContext db = new ApplicationDBContext(PathToDb.Path))
            {
                res = db.Companies.ToList();
            }
            return res;
        }

        internal static void Set(string newCompanyName)
        {
            using (ApplicationDBContext db = new ApplicationDBContext(PathToDb.Path))
            {
                Company compony = db.Companies.FirstOrDefault(x => x.CompanyName == newCompanyName);
                if (compony == null)
                {
                    db.Companies.Add(new Company() { CompanyName = newCompanyName });
                    db.SaveChanges();
                }
            }
        }
        internal static void Delete(Company selectCompany)
        {

            using (ApplicationDBContext db = new ApplicationDBContext(PathToDb.Path))
            {
                if (selectCompany != null)
                {
                    db.Companies.Remove(selectCompany);
                    db.SaveChanges();
                }
            }
        }

        internal static void Rename(string newCompanyName, Company oldCompany)
        {
            if (oldCompany != null)
            {
                using (ApplicationDBContext db = new ApplicationDBContext(PathToDb.Path))
                {
                    Company compony = db.Companies.FirstOrDefault(x => x.CompanyId == oldCompany.CompanyId);
                    if (compony != null)
                    {
                        compony.CompanyName = newCompanyName;
                        db.Companies.Update(compony);
                        db.SaveChanges();
                    }
                }
            }
        }
    }
}
