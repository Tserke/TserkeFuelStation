using FuelStation.Blazor.Shared;
using FuelStation.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FuelStation.Win
{
    public partial class LoginForm : Form
    {
        private TransactionForm _transactionForm;
        private ItemsForm _itemsForm;
        private CashierForm _cashierForm;
        private ManagerForm _managerForm;
        private bool _employeeLoaded = false;
        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            RefreshData();
        }

        public async void RefreshData()
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:7069/");
            var response = await httpClient.GetFromJsonAsync<List<EmployeeListViewModel>>("employee");


            if (_employeeLoaded is true)
                return;
            foreach (var item in response)
            {
                cmbAccount.Items.Add(item.Name);
            }
            _employeeLoaded = true;
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:7069/");
            var response = await httpClient.GetFromJsonAsync<List<EmployeeListViewModel>>("employee");

            foreach(var item in response)
            {
                if (cmbAccount.SelectedItem.ToString() == item.Name && txtPassword.Text == item.Name + item.Surname )
                {
                    Employee employee = new Employee
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Surname=item.Surname,
                        EmployeeType = item.EmployeeType,
                        HireDateStart = item.HireDateStart,
                        HireDateEnd = item.HireDateEnd,
                        SallaryPerMonth=item.SallaryPerMonth,
                    };
                    if (employee.EmployeeType == EmployeeTypeEnum.Staff)
                    {
                        _itemsForm = new ItemsForm();
                        _itemsForm.ShowDialog();
                    }
                    if (employee.EmployeeType == EmployeeTypeEnum.Cashier)
                    {
                        _cashierForm = new CashierForm();
                        _cashierForm.employee = employee;
                        _cashierForm.ShowDialog();
                    }
                    if(employee.EmployeeType == EmployeeTypeEnum.Manager)
                    {
                        _managerForm = new ManagerForm();
                        _managerForm.employee = employee;
                        _managerForm.ShowDialog();
                    }

                }
            }
        }
    }
}
