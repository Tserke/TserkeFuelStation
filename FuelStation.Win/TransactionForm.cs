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
        private decimal _quantity;
        private bool _fuelAdded = false;
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
            if (nudQuantity.Value <= 0 || grdItem.SelectedRows == null)  //|| spnPrice.Value == 0 || spnCost.Value == 0 || spnPrice.Value < spnCost.Value
                return;
            var transactionLine = new TransactionLineListViewModel();
            transactionLine.TransactionId = 11;
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
            RefreshData();

        }
        public async void RefreshData()
        {
            await RefreshTransactionLine();
            await RefreshItems();
        }

        private async Task RefreshItems()
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:7069/");
            var itemResponse = await httpClient.GetFromJsonAsync<List<ItemListViewModel>>("item");
            grdItem.DataSource = null;
            grdItem.DataSource = itemResponse;
            grdItem.Columns[index: 0].Visible = false;
            grdItem.Columns[index: 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grdItem.Columns[index: 2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grdItem.Columns[index: 3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grdItem.Columns[index: 4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            nudQuantity.Value = 0m;
        }

        public async Task RefreshTransactionLine()
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:7069/");
            var transactionLineResponse = await httpClient.GetFromJsonAsync<List<TransactionLineListViewModel>>("transactionLine");

            grdTransactionLine.DataSource = null;
            grdTransactionLine.DataSource = transactionLineResponse;
           
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
    }
}
