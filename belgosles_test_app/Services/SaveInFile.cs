using belgosles_test_app.Services.db;
using models;
using NPOI.XWPF.UserModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;

namespace belgosles_test_app.Services
{
    internal class SaveInFile
    {
        public void Componies()
        {
            var newFile2 = @"D:\newbook.core.docx";
            using (var fs = new FileStream(newFile2, FileMode.Create, FileAccess.Write))
            {
                XWPFDocument doc = new XWPFDocument();
                var p0 = doc.CreateParagraph();
                p0.Alignment = ParagraphAlignment.CENTER;
                XWPFRun r0 = p0.CreateRun();
                r0.FontFamily = "microsoft yahei";
                r0.FontSize = 18;
                r0.IsBold = true;
                r0.SetText("This is title");

                var p1 = doc.CreateParagraph();
                p1.Alignment = ParagraphAlignment.LEFT;
                p1.IndentationFirstLine = 500;
                XWPFRun r1 = p1.CreateRun();
                r1.FontFamily = "·ÂËÎ";
                r1.FontSize = 12;
                r1.IsBold = true;
                r1.SetText("This is content, content content content content content content content content content");

                doc.Write(fs);
            }
        }

        internal void Componies(ObservableCollection<Company> companies, string title)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss"); // Default file name
            dlg.DefaultExt = ".docx"; // Default file extension
            dlg.Filter = "Word (.docx)|*.docx"; // Filter files by extension

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                using (var fs = new FileStream(dlg.FileName, FileMode.Create, FileAccess.Write))
                {
                    XWPFDocument doc = new XWPFDocument();
                    var p0 = doc.CreateParagraph();
                    p0.Alignment = ParagraphAlignment.CENTER;
                    XWPFRun r0 = p0.CreateRun();
                    r0.FontFamily = "microsoft yahei";
                    r0.FontSize = 18;
                    r0.IsBold = true;
                    r0.SetText(title);

                    var p1 = doc.CreateParagraph();
                    p1.Alignment = ParagraphAlignment.LEFT;
                    p1.IndentationFirstLine = 500;
                    XWPFRun r1 = p1.CreateRun();
                    r1.FontFamily = "·ÂËÎ";
                    r1.FontSize = 12;
                    r1.IsBold = true;
                    StringBuilder sb = new StringBuilder();
                    foreach (var item in companies)
                    {
                        sb.AppendLine(item.CompanyName);
                    }

                    r1.SetText(sb.ToString());

                    doc.Write(fs);
                }
            }
        }

        internal void CompanyEmployees(ObservableCollection<Employee> employees, string companyName)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = companyName; // Default file name
            dlg.DefaultExt = ".docx"; // Default file extension
            dlg.Filter = "Word (.docx)|*.docx"; // Filter files by extension

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                using (var fs = new FileStream(dlg.FileName, FileMode.Create, FileAccess.Write))
                {
                    XWPFDocument doc = new XWPFDocument();
                    var p0 = doc.CreateParagraph();
                    p0.Alignment = ParagraphAlignment.CENTER;
                    XWPFRun r0 = p0.CreateRun();
                    r0.FontFamily = "microsoft yahei";
                    r0.FontSize = 18;
                    r0.IsBold = true;
                    r0.SetText(companyName);

                    var p1 = doc.CreateParagraph();
                    p1.Alignment = ParagraphAlignment.LEFT;
                    p1.IndentationFirstLine = 500;
                    XWPFRun r1 = p1.CreateRun();
                    r1.FontFamily = "·ÂËÎ";
                    r1.FontSize = 12;
                    r1.IsBold = true;
                    StringBuilder sb = new StringBuilder();
                    foreach (var item in employees)
                    {
                        sb.AppendLine(item.ToWordFile());
                    }

                    r1.SetText(sb.ToString());

                    doc.Write(fs);
                }
            }
        }
        internal void CompanyEmployeesSortForDepartament(ObservableCollection<Employee> employees, string companyName)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = companyName; // Default file name
            dlg.DefaultExt = ".docx"; // Default file extension
            dlg.Filter = "Word (.docx)|*.docx"; // Filter files by extension

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                using (var fs = new FileStream(dlg.FileName, FileMode.Create, FileAccess.Write))
                {
                    XWPFDocument doc = new XWPFDocument();
                    var p0 = doc.CreateParagraph();
                    p0.Alignment = ParagraphAlignment.CENTER;
                    XWPFRun r0 = p0.CreateRun();
                    r0.FontFamily = "microsoft yahei";
                    r0.FontSize = 18;
                    r0.IsBold = true;
                    r0.SetText(companyName);

                    var p1 = doc.CreateParagraph();
                    p1.Alignment = ParagraphAlignment.LEFT;
                    p1.IndentationFirstLine = 500;
                    XWPFRun r1 = p1.CreateRun();
                    r1.FontFamily = "·ÂËÎ";
                    r1.FontSize = 12;
                    r1.IsBold = true;
                    StringBuilder sb = new StringBuilder();
                    if (employees != null && employees.Count > 0)
                    {
                        var sortEmployees = employees.OrderByDescending(x => x.Department).ToList();
                        string department = string.Empty;
                        bool adddepartment = true;
                        foreach (var item in sortEmployees)
                        {
                            if(item.Department != department)
                            {
                                sb.AppendLine();
                                adddepartment = true;
                            }

                            if (adddepartment)
                            {
                                department = item.Department;
                                sb.AppendLine(item.Department);
                                adddepartment = false;
                            }

                            sb.AppendLine(item.ToWordFileWithoutDepartment());
                        }
                    }

                    r1.SetText(sb.ToString());

                    doc.Write(fs);
                }
            }
        }

        internal void CompaniesAndEmployees(ObservableCollection<Company> companies)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss"); // Default file name
            dlg.DefaultExt = ".docx"; // Default file extension
            dlg.Filter = "Word (.docx)|*.docx"; // Filter files by extension

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                using (var fs = new FileStream(dlg.FileName, FileMode.Create, FileAccess.Write))
                {
                    XWPFDocument doc = new XWPFDocument();
                    foreach (var item in companies)
                    {
                        var p0 = doc.CreateParagraph();
                        p0.Alignment = ParagraphAlignment.CENTER;
                        XWPFRun r0 = p0.CreateRun();
                        r0.FontFamily = "microsoft yahei";
                        r0.FontSize = 18;
                        r0.IsBold = true;
                        r0.SetText(item.CompanyName);

                        var p1 = doc.CreateParagraph();
                        p1.Alignment = ParagraphAlignment.LEFT;
                        p1.IndentationFirstLine = 500;
                        XWPFRun r1 = p1.CreateRun();
                        r1.FontFamily = "·ÂËÎ";
                        r1.FontSize = 12;
                        r1.IsBold = true;
                        StringBuilder sb = new StringBuilder();
                        List<Employee> employees = EmpluesHandler.Get(item);

                        if (employees != null && employees.Count > 0)
                        {
                            foreach (var employee in employees)
                            {
                                sb.AppendLine(employee.ToWordFile());
                            }
                        }
                        else
                        {
                            sb.AppendLine("Нет работников");
                        }



                        r1.SetText(sb.ToString());

                    }
                    doc.Write(fs);
                }
            }
        }
    }
}
