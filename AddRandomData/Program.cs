using db;
using models;
using System;
using System.Linq;

namespace AddRandomData
{
    internal class Program
    {
        static string s = "type does not contain a definition for name and no accessible extension method name accepting a first argument of";

        static string[] dep = new string[] { "продажи", "работники", "бухгалтерия", "транспорт" };

        static string[] names = new string[] {"Данилов","Владимир","Михайлович","Золотов","Михаил","Константинович","Игнатова","Вероника","Максимовна","Антонов","Демид","Владимирович",
            "Семенова","Алина","Андреевна","Никитин","Алексей","Олегович","Суворов","Артемий","Михайлович","Баранов","Максим","Дмитриевич","Фадеева"+
                                        "Анастасия","Константиновна","Михайлов","Кирилл","Тимофеевич","Кузнецов","Константин","Фёдорович","Царев","Григорий","Максимович"
                                        ,"Покровская","Ева","Александровна"};

        static void Main(string[] args)
        {
            Random rand = new Random();

            int x = rand.Next(10, 50);

            using (ApplicationDBContext db = new ApplicationDBContext())
            {
                for (int i = 0; i < x; i++)
                {
                    Company company = new Company() { CompanyName = RandomCompany() };
                    db.Companies.Add(company);
                    db.SaveChanges();
                    x = rand.Next(10, 50);
                    for (int j = 0; j < x; j++)
                    {

                        db.Employees.Add(new Employee()
                        {
                            FirstName = RandomEmployee(),
                            LastName = RandomEmployee(),
                            MiddleName = RandomEmployee(),
                            Address = RandomEmployee() + RandomEmployee() + RandomEmployee() + rand.Next(1, 200) + "/" + rand.Next(1, 55),
                            Phone = RandomPhone(),
                            Department = dep[rand.Next(0, 3)],
                            Company = company
                        });
                        db.SaveChanges();
                    }

                }
            }
        }

        static string RandomCompany()
        {
            Random rand = new Random();
            var arr = s.Split(' ');
            var res = arr[rand.Next(0, arr.Length)] + arr[rand.Next(0, arr.Length)];
            return res;
        }

        static string RandomEmployee()
        {
            Random rand = new Random();
            var res = names[rand.Next(0, names.Length)];
            return res;
        }

        static string RandomPhone()
        {
            Random rand = new Random();
            var temp = string.Empty;
            for (int h = 0; h < 13; h++)
            {
                temp += rand.Next(0, 9);
            }
            return temp;

        }
    }
}
