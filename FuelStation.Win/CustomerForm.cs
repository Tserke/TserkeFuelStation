using FuelStation.Blazor.Shared;
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
    public partial class CustomerForm : Form
    {
        public string newCardNumber {get; set;}
        public bool FromTransactionLoadCustomer { get; set; }
        private int _selectedCustomerId;
        public CustomerForm()
        {
            InitializeComponent();
        }

        private async void CustomerForm_Load(object sender, EventArgs e)
        {
            //var httpClient= new HttpClient();
            //httpClient.BaseAddress = new Uri("https://localhost:7069/");
            //var response = await httpClient.GetFromJsonAsync<List<CustomerListViewModel>>("customer
            RefreshData();
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:7069/");
            var customerName = txtName.Text;
            var customerSurname = txtSurname.Text;
            var customerCardNumber = txtCardNumber.Text;
            if (string.IsNullOrEmpty(customerName) || string.IsNullOrEmpty(customerSurname) || string.IsNullOrEmpty(customerCardNumber))
                return;
            var customer = new CustomerListViewModel();
            customer.Name = customerName;
            customer.Surname = customerSurname;
            customer.CardNumber = customerCardNumber;
            var response = await httpClient.PostAsJsonAsync("customer", customer);
            RefreshData();
            if(FromTransactionLoadCustomer is true)
                this.Close();
        }
        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:7069/");
            var customer = await httpClient.GetFromJsonAsync<CustomerEditListViewModel>($"customer/{(_selectedCustomerId == null ? 0 : _selectedCustomerId)}");
            if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtSurname.Text) || string.IsNullOrEmpty(txtCardNumber.Text)) 
                return;
            customer.Name=txtName.Text;
            customer.Surname=txtSurname.Text;
            customer.CardNumber=txtCardNumber.Text;
            var response = await httpClient.PutAsJsonAsync("customer",customer);
            RefreshData();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshData();
        }


        public async void RefreshData()
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:7069/");
            var response = await httpClient.GetFromJsonAsync<List<CustomerListViewModel>>("customer");
            grdDisplayData.DataSource = null;
            grdDisplayData.DataSource = response;
            //grdDisplayData.Columns.GetColumnsWidth; //= DataGridViewAutoSizeColumnMode.Fill;
            grdDisplayData.Columns[index: 0].Visible = false;
            grdDisplayData.Columns[index: 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grdDisplayData.Columns[index: 2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grdDisplayData.Columns[index: 3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            txtName.Text = null;
            txtSurname.Text = null;
            txtCardNumber.Text = null;
            if (FromTransactionLoadCustomer is true)
            {
                txtCardNumber.Text = newCardNumber;
            }

        }

        private void grdDisplayData_SelectionChanged(object sender, EventArgs e)
        {
            if (grdDisplayData.SelectedRows.Count != 1)
                return;
            
            _selectedCustomerId = (int)grdDisplayData.CurrentRow.Cells[0].Value;
            txtName.Text = grdDisplayData.CurrentRow.Cells[1].Value.ToString();
            txtSurname.Text = grdDisplayData.CurrentRow.Cells[2].Value.ToString();
            txtCardNumber.Text = grdDisplayData.CurrentRow.Cells[3].Value.ToString();
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:7069/");
            var customer = await httpClient.GetFromJsonAsync<CustomerEditListViewModel>($"customer/{(_selectedCustomerId == null ? 0 : _selectedCustomerId)}");
            var response = await httpClient.DeleteAsync($"customer/{customer.Id}");
            RefreshData(); 
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
