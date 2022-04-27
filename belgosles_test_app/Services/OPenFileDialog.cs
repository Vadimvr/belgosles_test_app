using Microsoft.Win32;
using System;
using System.Windows;

namespace belgosles_test_app.Services
{
    public static class OPenFileDialog
    {
        public static Tuple<bool,string> Open()
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;

            OpenFileDialog openFileDialog = new OpenFileDialog();


                openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Filter = "Файл базы данных SQLite (*.db)|*.db";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == true)
            {
                //Get the path of specified file
                filePath = openFileDialog.FileName;
                return Tuple.Create(true, filePath);
            }
            else
            {
                return new Tuple<bool, string>(false, filePath);
            }
        }
    }
}
