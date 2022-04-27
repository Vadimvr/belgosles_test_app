using models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace belgosles_test_app.Services.db
{
    internal static class СheckingEmployeeData
    {
        public static  Tuple<bool,Employee, string>  Сheck(string firstname, string middleName, string lastName, string addrees, string phone, string departament)
        {
            string error = string.Empty;
            if(string.IsNullOrEmpty(firstname)|| firstname.Length<2)
            {
                return new Tuple<bool, Employee, string>(false, null, "Имя не заполнено (минимальная длина в 3 символа).");
            }

            if (string.IsNullOrEmpty(middleName) || middleName.Length < 2)
            {
                return new Tuple<bool, Employee, string>(false, null, "Отчество не заполнено (минимальная длина в 3 символа).");
            }

            if (string.IsNullOrEmpty(lastName) || lastName.Length < 2)
            {
                return new Tuple<bool, Employee, string>(false, null, "Фамилия не заполнена (минимальная длина в 3 символа).");
            }

            if (string.IsNullOrEmpty(addrees) || addrees.Length < 10)
            {
                return new Tuple<bool, Employee, string>(false, null, "Адрес не заполнен (минимальная длина в 10 символов).");
            }

            if (string.IsNullOrEmpty(phone) || phone.Length < 13)
            {
                return new Tuple<bool, Employee, string>(false, null, "Номер телефона не заполнено (минимальная длина в 13 символов).");
            }

            if (string.IsNullOrEmpty(departament) || departament.Length < 2)
            {
                return new Tuple<bool, Employee, string>(false, null, "Отдел не заполнен (минимальная длина в 3 символа).");
            }

            return new Tuple<bool, Employee, string>(true, new Employee()
            {
                FirstName = firstname, MiddleName= middleName,LastName = lastName, Address = addrees, Department = departament, Phone = phone,
            }, string.Empty);
        }
    }
}
