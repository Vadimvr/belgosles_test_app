using db;
using models;
using System;
using System.Linq;

namespace AddRandomData
{
    internal class Program
    {
        static string s = "type does not contain a definition for name and no accessible extension method name accepting a first argument of";
        static string[] dep = new string[] { "Прораб", "Бригадир на производстве", "Технолог", "Инженер", "Контролер качества", "Мастер", "Механик", "Рабочий непищевого производства", "Слесарь", "Электрик", "Машинист строительной техники", "Автослесарь", "Монтажник", "Автоэлектрик", "Инженер по технике безопасности", "Машинист холодильных установок", "Шеф-повар", "Повар", "Кондитер", "Маркировщик", "Пекарь", "Рубщик мяса", "Фасовщик", "Рабочий пищевого производства" };
        static string[] firstnames = new string[] { "Орест", "Милан", "Оливер", "Борис", "Харитон", "Чеслав", "Люций", "Устин", "Вениамин", "Стефан", "Степан", "Никодим", "Ян", "Владлен", "Чарльз", "Зенон", "Эдуард", "Устин", "Зенон", "Нестор" };
        static string[] midleNames = new string[] { "Львович", "Леонидович", "Богданович", "Виталиевич", "Ярославович", "Петрович", "Львович", "Артёмович", "Эдуардович", "Виталиевич", "Виталиевич", "Фёдрович", "Валериевич", "Фёдорович", "Леонидович", "Ярославович", "Сергеевич", "Фёдорович", "Львович", "Анатолиевич", };
        static string[] lasnames = new string[] { "Городецкий", "Симоненко", "Захарченко", "Комиссаров", "Гребневский", "Дьячков", "Сирко", "Суворов", "Потапов", "Гелетей", "Потапов", "Алексеев", "Павлов", "Васильев", "Гущин", "Никонов", "Антонов", "Тягай", "Белоусов", "Романов", };
        static string[] sity = new string[] { "Ричмонд", "Каргополь", "Олонец", "Бор", "Петровск", "Пардубице", "Мосул", "Козьмодемьянск", "Колпашево" };
        static string[] streets = new string[] { "Лицкойная", "Фурштатская улица", "Жмоск", "Лазенска", "Карнаби Стрит", "Жмоск", "Набережная реки Фонтанки", "Гран Виа", "Шохтемура", "Гран Виа", "Хуалин Жаде", "Улица Нахимсона", "Жизнь", "Жмо", "Эбби роуд", "Балчуг" };

        static void Main(string[] args)
        {
            Random rand = new Random();
            Console.Write("Укажите путь куда сохранить базу данных: ");
            var path = Console.ReadLine();
            int x = rand.Next(10, 50);
            if (!string.IsNullOrEmpty(path))
            {

                using (ApplicationDBContext db = new ApplicationDBContext(path))
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
                                FirstName = RandomItem(firstnames),
                                LastName = RandomItem(lasnames),
                                MiddleName = RandomItem(midleNames),
                                Address = "г. " + RandomItem(sity) + " ул. " + RandomItem(streets) + " " + rand.Next(1, 200) + "/" + rand.Next(1, 55),
                                Phone = RandomPhone(),
                                Department = RandomItem(dep),
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

            static string RandomItem(string[] arr)
            {
                Random rand = new Random();
                var res = arr[rand.Next(0, arr.Length)];
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
}
