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
    public partial class TransactionForm : Form
    {
        public Employee employee { get; set; }
        private Customer _customer { get; set; } = new();
        private CustomerForm _customerForm { get; set; }
        private int _lastTransactionId { get; set; }
        private decimal _quantity;
        private bool _fuelAdded = false;
        private bool _customerFound = false;
        private Calculations _calculations = new();
        public TransactionForm()
        {
            InitializeComponent();
        }

        private void TransactionForm_Load(object sender, EventArgs e)
        { 
            RefreshData();
        }

        private async void btnAddItem_Click(object sender, EventArgs e)
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:7069/");
            var transaction = await httpClient.GetFromJsonAsync<TransactionListViewModel>($"transaction/{(_lastTransactionId == null ? 0 : _lastTransactionId)}");
            if (nudQuantity.Value <= 0 || grdItem.SelectedRows == null)  //|| spnPrice.Value == 0 || spnCost.Value == 0 || spnPrice.Value < spnCost.Value
                return;
            var transactionLine = new TransactionLineListViewModel();
            transactionLine.TransactionId = transaction.Id;
            transactionLine.Transaction = transaction;
            transactionLine.ItemId = (int)grdItem.CurrentRow.Cells[0].Value;
            transactionLine.Quantity = nudQuantity.Value;
            transactionLine.ItemPrice = (decimal)grdItem.CurrentRow.Cells[4].Value;
            transactionLine.NetValue = (decimal)_calculations.CalculateNetValue(transactionLine.ItemPrice, transactionLine.Quantity);
            if ((ItemTypeEnum)grdItem.CurrentRow.Cells[3].Value == ItemTypeEnum.Fuel && transactionLine.NetValue >= 20m)
            {
                transactionLine.DiscountPercent = 10;
            }
            else
            {
                transactionLine.DiscountPercent = null;
            }
            transactionLine.DiscountValue = _calculations.CalculateDiscountValue(transactionLine.NetValue, transactionLine.DiscountPercent);
            transactionLine.TotalValue = _calculations.CalculateTotalValue(transactionLine.DiscountValue, transactionLine.NetValue);

            var response = await httpClient.PostAsJsonAsync("transactionLine", transactionLine);
            // transaction.TransactionLineList.Add(transactionLine);
            RefreshData();

        }

        private async void StartTransaction()
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:7069/");
            //if (_customerFound is true)
            //{
                var transaction = new TransactionListViewModel();
                transaction.Customer = _customer;
                transaction.CustomerId = _customer.Id;
                transaction.Employee = employee;
                transaction.EmployeeId = employee.Id;
            //}
            var response = await httpClient.PostAsJsonAsync("transaction", transaction);
            var transactionResponse = await httpClient.GetFromJsonAsync<List<TransactionListViewModel>>("transaction");
            //TODO:Fix THIS
            //_lastTransactionId = transactionResponse.Last().Id;

        }

        public async void RefreshData()
        {
             RefreshTransactionLine();
             RefreshItems();
        }

        private async void RefreshItems()
        {
            if (_customerFound is false)
                return;

            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:7069/");
            var response = await httpClient.GetFromJsonAsync<List<ItemListViewModel>>("item");
            grdItem.DataSource = null;
            grdItem.DataSource = response;
            grdItem.Columns[index: 0].Visible = false;
            grdItem.Columns[index: 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grdItem.Columns[index: 2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grdItem.Columns[index: 3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grdItem.Columns[index: 4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            nudQuantity.Value = 0m;
        }

        public async void RefreshTransactionLine()
        {
            if (_customerFound is false)
                return;

            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:7069/");
            var response = await httpClient.GetFromJsonAsync<List<TransactionLineListViewModel>>("transactionLine");

            grdTransactionLine.DataSource = null;
            grdTransactionLine.DataSource = response;
           
            grdTransactionLine.Columns[index: 0].Visible = false;
            grdTransactionLine.Columns[index: 1].Visible = false;
            grdTransactionLine.Columns[index: 2].Visible = false;
            grdTransactionLine.Columns[index: 3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grdTransactionLine.Columns[index: 4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grdTransactionLine.Columns[index: 6].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grdTransactionLine.Columns[index: 7].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grdTransactionLine.Columns[index: 8].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

        }
        private void grdItem_SelectionChanged(object sender, EventArgs e)
        {

            if (grdItem.SelectedRows.Count != 1)
                return;
            if ((ItemTypeEnum)grdItem.CurrentRow.Cells[3].Value == ItemTypeEnum.Fuel)
            {
                _quantity=nudQuantity.Value;
            }
            if ((ItemTypeEnum)grdItem.CurrentRow.Cells[3].Value == ItemTypeEnum.Service || (ItemTypeEnum)grdItem.CurrentRow.Cells[3].Value == ItemTypeEnum.Product)
            {
                _quantity= decimal.Round(nudQuantity.Value);
            }

        }

        private async void FindCustomer()
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:7069/");
            var response = await httpClient.GetFromJsonAsync<List<CustomerListViewModel>>("customer");
            foreach (var item in response)
            {
                if(item.CardNumber == txtCardNumber.Text)
                {
                    txtName.Text=item.Name;
                    txtSurname.Text=item.Surname;
                    _customer.Id = item.Id;
                    _customer.Name = item.Name;
                    _customer.Surname = item.Surname;
                    _customer.CardNumber = item.CardNumber;
                    //_customer = item;
                    _customerFound =true;
                    StartTransaction();
                    RefreshData();
                    return;
                }
                txtName.Text = "not found";
                txtSurname.Text = "create new";
                btnNewCustomer.Enabled=true;
            }

        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            FindCustomer();
        }

        private void btnNewCustomer_Click(object sender, EventArgs e)
        {
            _customerForm=new CustomerForm();
            _customerForm.newCardNumber=txtCardNumber.Text;
            _customerForm.FromTransactionLoadCustomer = true;
            _customerForm.ShowDialog();
            FindCustomer();
        }
    }
}
