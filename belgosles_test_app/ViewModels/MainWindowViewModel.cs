using belgosles_test_app.Infrastructure.Commands;
using belgosles_test_app.Models;
using belgosles_test_app.Services;
using belgosles_test_app.Services.db;
using belgosles_test_app.Services.Interfaces;
using belgosles_test_app.ViewModels.Base;
using models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace belgosles_test_app.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        static string dbPath = @$"D:\database\belgosles_test_db_sqlite.db";

        private readonly IUserDialog _UserDialog;
        private readonly IDataService _DataService;

        #region Title : string - Заголовок окна

        /// <summary>Заголовок окна</summary>
        private string _Title = "belgosles test app";

        /// <summary>Заголовок окна</summary>
        public string Title { get => _Title; set => Set(ref _Title, value); }

        #endregion

        #region Status : string - Статус

        /// <summary>Статус</summary>
        private string _Status = "Готов!";

        /// <summary>Статус</summary>
        public string Status { get => _Status; set => Set(ref _Status, value); }

        #endregion


        private ObservableCollection<Company> companies;

        public ObservableCollection<Company> Companies { get => companies; set => Set(ref companies, value); }
        public MainWindowViewModel(IUserDialog UserDialog, IDataService DataService)
        {

            _UserDialog = UserDialog;
            _DataService = DataService;
            RefreshComponyCommand = new LambdaCommand(OnRefreshComponyCommandExecuted, CanRefreshComponyCommandExecute);
            AddNewComponyCommand = new LambdaCommand(OnAddNewComponyCommandExecuted, CanAddNewComponyCommandExecute);
            RemoveCompanuCommand = new LambdaCommand(OnRemoveCompanuCommandExecuted, CanRemoveCompanuCommandExecute);
            AddNewEmployeeCommand = new LambdaCommand(OnAddNewEmployeeCommandExecuted, CanAddNewEmployeeCommandExecute);
            RemoveEmployeeCommand = new LambdaCommand(OnRemoveEmployeeCommandExecuted, CanRemoveEmployeeCommandExecute);
            EditEmployeeCommand = new LambdaCommand(OnEditEmployeeCommandExecuted, CanEditEmployeeCommandExecute);
            SaveEmployeeCommand = new LambdaCommand(OnSaveEmployeeCommandExecuted, CanSaveEmployeeCommandExecute);
            EditCompanyCommand = new LambdaCommand(OnEditCompanyCommandExecuted, CanEditCompanyCommandExecute);
            RenameCompanyCommand = new LambdaCommand(OnRenameCompanyCommandExecuted, CanRenameCompanyCommandExecute);
            SeveCompaniesCommand = new LambdaCommand(OnSeveCompaniesCommandExecuted, CanSeveCompaniesCommandExecute);
            SeveCompaniesAndEmployeesCommand = new LambdaCommand(OnSeveCompaniesAndEmployeesCommandExecuted, CanSeveCompaniesAndEmployeesCommandExecute);
            SeveCompanyEmployeesCommand = new LambdaCommand(OnSeveCompanyEmployeesCommandExecuted, CanSeveCompanyEmployeesCommandExecute);
            SaveEmployeesUnderDeportationCommand = new LambdaCommand(OnSaveEmployeesUnderDeportationCommandExecuted, CanSaveEmployeesUnderDeportationCommandExecute);
            ExportXMLCommand = new LambdaCommand(OnExportXMLCommandExecuted, CanExportXMLCommandExecute);
            ExportJsonCommand = new LambdaCommand(OnExportJsonCommandExecuted, CanExportJsonCommandExecute);
            OpenDataBaseCommand = new LambdaCommand(OnOpenDataBaseCommandExecuted, CanOpenDataBaseCommandExecute);


        }

        public List<Employee> ImmutableListOfEmployees { get; set; }
        public ICommand RefreshComponyCommand { get; }

        private bool CanRefreshComponyCommandExecute(object p) => !string.IsNullOrEmpty(PathToDb.Path);
        private void OnRefreshComponyCommandExecuted(object p)
        {
            Companies = new ObservableCollection<Company>(CompanyHandler.Get());

        }

        private string newCompanyName;

        public string NewCompanyName { get => newCompanyName; set => Set(ref newCompanyName, value); }

        public ICommand AddNewComponyCommand { get; }

        private bool CanAddNewComponyCommandExecute(object p) => Companies != null;
        private void OnAddNewComponyCommandExecuted(object p)
        {
            CompanyHandler.Set(newCompanyName);
            //обновляем список 
            OnRefreshComponyCommandExecuted(p);
        }

        public ICommand RemoveCompanuCommand { get; }

        private bool CanRemoveCompanuCommandExecute(object p) => SelectCompany != null;
        private void OnRemoveCompanuCommandExecuted(object p)
        {
            int tempIndex = Companies.IndexOf(SelectCompany);
            CompanyHandler.Delete(SelectCompany);
            Companies.Remove(SelectCompany);
            if (tempIndex < Companies.Count)
            {
                SelectCompany = Companies[tempIndex];
            }
        }

        private Company _SelectCompany;

        public Company SelectCompany
        {
            get => _SelectCompany; set
            {
                if (Set(ref _SelectCompany, value))
                {
                    ImmutableListOfEmployees = EmpluesHandler.Get(SelectCompany);
                    Employees = new ObservableCollection<Employee>(ImmutableListOfEmployees);
                    SelectEmployee = null;
                }
            }
        }

        private ObservableCollection<Employee> _Employees;

        public ObservableCollection<Employee> Employees { get => _Employees; set => Set(ref _Employees, value); }

        private string firstName;

        public string FirstName { get => firstName; set => Set(ref firstName, value); }

        private string middleName;

        public string MiddleName { get => middleName; set => Set(ref middleName, value); }

        private string lastName;

        public string LastName { get => lastName; set => Set(ref lastName, value); }

        private string address;

        public string Address { get => address; set => Set(ref address, value); }

        private string phone;

        public string Phone { get => phone; set => Set(ref phone, value); }

        private string department;

        public string Department { get => department; set => Set(ref department, value); }



        private Employee _NewEmplouee;

        public Employee NewEmplouee { get => _NewEmplouee; set => Set(ref _NewEmplouee, value); }

        public ICommand AddNewEmployeeCommand { get; }

        private bool CanAddNewEmployeeCommandExecute(object p)
        {
            return SelectCompany != null;
        }
        private void OnAddNewEmployeeCommandExecuted(object p)
        {
            var res = СheckingEmployeeData.Сheck(FirstName, MiddleName, LastName, Address, Phone, Department);
            if (res.Item1)
            {
                NewEmplouee = res.Item2;
                EmpluesHandler.Set(NewEmplouee, SelectCompany);
                ImmutableListOfEmployees = EmpluesHandler.Get(SelectCompany);
                Employees = new ObservableCollection<Employee>(ImmutableListOfEmployees);
            }
            else
            {
                Status = res.Item3;
            }
        }
        private Employee _SelectEmployee;
        public Employee SelectEmployee
        {
            get => _SelectEmployee; set
            {
                if (Set(ref _SelectEmployee, value))
                {
                    SaveUpdatetdEmployeeVisibility = System.Windows.Visibility.Hidden;
                    AddNewEmployeeCommandVisibility = System.Windows.Visibility.Visible;

                    FirstName = string.Empty;
                    LastName = string.Empty;
                    MiddleName = string.Empty;
                    Department = string.Empty;
                    Address = string.Empty;
                    Phone = string.Empty;
                }
            }
        }


        public ICommand RemoveEmployeeCommand { get; }

        private bool CanRemoveEmployeeCommandExecute(object p) => SelectEmployee != null;
        private void OnRemoveEmployeeCommandExecuted(object p)
        {
            int tempIndex = Employees.IndexOf(SelectEmployee);
            EmpluesHandler.Del(SelectEmployee);
            ImmutableListOfEmployees = EmpluesHandler.Get(SelectCompany);
            Employees = new ObservableCollection<Employee>(ImmutableListOfEmployees);

            if (tempIndex < Employees.Count)
            {
                SelectEmployee = Employees[tempIndex];
            }
            else if(Employees.Count > 0)
            {
                SelectEmployee = Employees[tempIndex-1];
            }

        }

        public ICommand EditEmployeeCommand { get; }

        private bool CanEditEmployeeCommandExecute(object p) => SelectEmployee != null;
        private void OnEditEmployeeCommandExecuted(object p)
        {
            SaveUpdatetdEmployeeVisibility = System.Windows.Visibility.Visible;
            AddNewEmployeeCommandVisibility = System.Windows.Visibility.Hidden;
            FirstName = SelectEmployee.FirstName;
            LastName = SelectEmployee.LastName;
            MiddleName = SelectEmployee.MiddleName;
            Department = SelectEmployee.Department;
            Address = SelectEmployee.Address;
            Phone = SelectEmployee.Phone;
        }

        public ICommand SaveEmployeeCommand { get; }

        private bool CanSaveEmployeeCommandExecute(object p) => SelectEmployee != null;
        private void OnSaveEmployeeCommandExecuted(object p)
        {
            var res = СheckingEmployeeData.Сheck(FirstName, MiddleName, LastName, Address, Phone, Department);
            if (res.Item1)
            {
                NewEmplouee = res.Item2;
                EmpluesHandler.Update(SelectEmployee, NewEmplouee);
                ImmutableListOfEmployees = EmpluesHandler.Get(SelectCompany);
                Employees = new ObservableCollection<Employee>(ImmutableListOfEmployees);
                SaveUpdatetdEmployeeVisibility = System.Windows.Visibility.Hidden;
                AddNewEmployeeCommandVisibility = System.Windows.Visibility.Visible;
            }
            else
            {
                Status = res.Item3;
            }
        }

        private System.Windows.Visibility saveUpdatetdEmployeeVisibility;

        public System.Windows.Visibility SaveUpdatetdEmployeeVisibility { get => saveUpdatetdEmployeeVisibility; set => Set(ref saveUpdatetdEmployeeVisibility, value); }

        private System.Windows.Visibility addNewEmployeeCommandVisibility;

        public System.Windows.Visibility AddNewEmployeeCommandVisibility { get => addNewEmployeeCommandVisibility; set => Set(ref addNewEmployeeCommandVisibility, value); }



        public ICommand EditCompanyCommand { get; }

        private bool CanEditCompanyCommandExecute(object p) => SelectCompany != null;
        private void OnEditCompanyCommandExecuted(object p)
        {
            if (string.IsNullOrEmpty(NewCompanyName) || NewCompanyName.Length < 3)
            {
                Status = "Название компании не заполнено (минимальная длина в 3 символа).";
            }
            else
            {
                RenameCompanyVisibility = EditCompanyVisibility;
                EditCompanyVisibility = System.Windows.Visibility.Hidden;
                CompanyHandler.Rename(NewCompanyName, SelectCompany);
                OnRefreshComponyCommandExecuted(p);
            }
        }

        public ICommand RenameCompanyCommand { get; }

        private bool CanRenameCompanyCommandExecute(object p) => SelectCompany != null;
        private void OnRenameCompanyCommandExecuted(object p)
        {
            NewCompanyName = SelectCompany.CompanyName;
            RenameCompanyVisibility = EditCompanyVisibility;
            EditCompanyVisibility = System.Windows.Visibility.Visible;
        }

        private System.Windows.Visibility renameCompanyVisibility = System.Windows.Visibility.Visible;

        public System.Windows.Visibility RenameCompanyVisibility { get => renameCompanyVisibility; set => Set(ref renameCompanyVisibility, value); }

        private System.Windows.Visibility editCompanyVisibility = System.Windows.Visibility.Hidden;

        public System.Windows.Visibility EditCompanyVisibility { get => editCompanyVisibility; set => Set(ref editCompanyVisibility, value); }


        public ICommand SeveCompaniesCommand { get; }

        private bool CanSeveCompaniesCommandExecute(object p) => Companies != null && Companies.Count > 0;
        private void OnSeveCompaniesCommandExecuted(object p)
        {
            new SaveInFile().Componies(Companies, "Компании");
        }


        public ICommand SeveCompanyEmployeesCommand { get; }

        private bool CanSeveCompanyEmployeesCommandExecute(object p) => SelectCompany != null;
        private void OnSeveCompanyEmployeesCommandExecuted(object p)
        {
            if (SelectCompany == null)
            {
                Status = "Не выбрана компания";
            }
            else if (Employees == null || Employees.Count == 0)
            {
                Status = "В выбранной компании отсутствуют сотрудники";
            }
            else
            {
                new SaveInFile().CompanyEmployees(Employees, SelectCompany.CompanyName);
            }

        }

        public ICommand SeveCompaniesAndEmployeesCommand { get; }

        private bool CanSeveCompaniesAndEmployeesCommandExecute(object p) => Companies != null;
        private void OnSeveCompaniesAndEmployeesCommandExecuted(object p)
        {
            if (Companies == null || Companies.Count == 0)
            {
                Status = "Отсутствуют компании отсутствуют сотрудники";
            }
            else
            {
                new SaveInFile().CompaniesAndEmployees(Companies);
            }
        }

        public ICommand SaveEmployeesUnderDeportationCommand { get; }

        private bool CanSaveEmployeesUnderDeportationCommandExecute(object p) => SelectCompany != null;
        private void OnSaveEmployeesUnderDeportationCommandExecuted(object p)
        {
            new SaveInFile().CompanyEmployeesSortForDepartament(Employees, SelectCompany.CompanyName);
        }

        private string findFirstName;

        public string FindFirstName
        {
            get => findFirstName; set
            {
                if (Set(ref findFirstName, value))
                {
                    Filter();
                }
            }
        }

        private void Filter()
        {
            Employees = FilterEmployees.Filter(FindFirstName, FindLastName, FindMiddleName, FindAddress, FindDepartment, FindPhone, ImmutableListOfEmployees);
        }

        private string findMiddleName;

        public string FindMiddleName
        {
            get => findMiddleName; set
            {
                if (Set(ref findMiddleName, value))
                {
                    Filter();
                }
            }
        }

        private string findLastName;

        public string FindLastName
        {
            get => findLastName; set
            {
                if (Set(ref findLastName, value))
                {
                    Filter();
                }
            }
        }

        private string findAddress;

        public string FindAddress
        {
            get => findAddress; set
            {
                if (Set(ref findAddress, value))
                {
                    Filter();
                }
            }
        }

        private string findPhone;

        public string FindPhone
        {
            get => findPhone; set
            {
                if (Set(ref findPhone, value))
                {
                    Filter();
                }
            }
        }

        private string findDepartment;

        public string FindDepartment
        {
            get => findDepartment; set
            {
                if (Set(ref findDepartment, value))
                {
                    Filter();
                }
            }
        }


        public ICommand ExportJsonCommand { get; }

        private bool CanExportJsonCommandExecute(object p) => Employees != null && Employees.Count > 0;
        private void OnExportJsonCommandExecuted(object p)
        {
            SaveInJson.Save(Employees, SelectCompany);
        }

        public ICommand ExportXMLCommand { get; }

        private bool CanExportXMLCommandExecute(object p) => Employees != null && Employees.Count > 0;
        private void OnExportXMLCommandExecuted(object p)
        {
            SaveInXml.Save(Employees, SelectCompany);
        }


        public ICommand OpenDataBaseCommand { get; }

        private bool CanOpenDataBaseCommandExecute(object p) => true;
        private void OnOpenDataBaseCommandExecuted(object p)
        {

            var res = OPenFileDialog.Open();
            if (res.Item1)
            {
                PathToDb.Path = res.Item2;
                Status = "OK!";
                OnRefreshComponyCommandExecuted(p);
            }
            else
            {
                Status = "Отмена";
            }
        }


    }
}