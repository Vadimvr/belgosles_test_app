using belgosles_test_app.Models;
using models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace belgosles_test_app.Services
{
    internal static class SaveInJson
    {
        public static void Save(ObservableCollection<Employee> employee, Company  company)
        {

            Tuple<bool, string> res =  UserSaveDialog.Save(company.CompanyName, ".json", "JSON  (.json)|*.json");
            if (res.Item1)
            {
                var temp = new
                {
                    CompanyId = company.CompanyId,
                    Employees = employee.ToList(),
                    CompanyName = company.CompanyName
                };
                using(StreamWriter sw = new StreamWriter(res.Item2,false, System.Text.Encoding.UTF8))
                {
                    sw.Write(JsonConvert.SerializeObject(temp));
                }
            }
        }
    }

    internal static class SaveInXml
    {
        public static void Save(ObservableCollection<Employee> employee, Company company)
        {

            Tuple<bool, string> res = UserSaveDialog.Save(company.CompanyName, ".xml", "XML  (.xml)|*.xml");
            if (res.Item1)
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(CompanyWrappper));
                CompanyWrappper temp = new CompanyWrappper()
                {
                    CompanyId = company.CompanyId,
                    Employees = employee.ToList(),
                    CompanyName = company.CompanyName
                };

                using (FileStream fs = new FileStream(res.Item2, FileMode.OpenOrCreate))
                {
                    xmlSerializer.Serialize(fs, temp);
                }
            }
        }

     
    }

    internal static class UserSaveDialog
    {

        /// <summary>
        /// UserSaveDialog
        /// </summary>
        /// <param name="fileName"> DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss");</param>
        /// <param name="fileType"> ".docx"</param>
        /// <param name="filterFile"> "Word (.docx)|*.docx"</param>
        /// <returns> result dialog and path to file.If result == false path to file = string.Empty</returns>
        public static Tuple<bool,string> Save(string fileName, string fileType, string filterFile)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = fileName; // Default file name
            dlg.DefaultExt = fileType; // Default file extension
            dlg.Filter = filterFile; // Filter files by extension

            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                return  new Tuple<bool, string>(true, dlg.FileName);
            }
            else
            {
                return new Tuple<bool, string>(false, string.Empty);
            }
        }
    }
}
