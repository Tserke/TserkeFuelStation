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
    public partial class ItemsForm : Form
    {
        private int _selectedItemId;
        public ItemsForm()
        {
            InitializeComponent();
        }
        private void ItemsForm_Load(object sender, EventArgs e)
        {
            RefreshData();
            cmbType.SelectedItem = null;
            cmbType.Items.Add(ItemTypeEnum.Fuel);
            cmbType.Items.Add(ItemTypeEnum.Product);
            cmbType.Items.Add(ItemTypeEnum.Service);
        }
        private async void btnAdd_Click(object sender, EventArgs e)
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:7069/");
            if (string.IsNullOrEmpty(txtCode.Text) || string.IsNullOrEmpty(txtDescription.Text) 
                 || cmbType.SelectedIndex < 0)  //|| spnPrice.Value == 0 || spnCost.Value == 0 || spnPrice.Value < spnCost.Value
                return;
            var item = new ItemListViewModel();
            item.Code=txtCode.Text;
            item.Description=txtDescription.Text;
            item.Price = spnPrice.Value;
            item.Cost = spnCost.Value;
            item.ItemType = (ItemTypeEnum)cmbType.SelectedItem;
            var response = await httpClient.PostAsJsonAsync("item", item);
            RefreshData();
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshData();
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:7069/");
            var item = await httpClient.GetFromJsonAsync<ItemListViewModel>($"customer/{(_selectedItemId == null ? 0 : _selectedItemId)}");
            if (string.IsNullOrEmpty(txtCode.Text) || string.IsNullOrEmpty(txtDescription.Text)
                 || cmbType.SelectedIndex < 0)
                return;
            item.Code = txtCode.Text;
            item.Description = txtDescription.Text;
            item.Price = spnPrice.Value;
            item.Cost = spnCost.Value;
            item.ItemType = (ItemTypeEnum)cmbType.SelectedItem;
            var response = await httpClient.PostAsJsonAsync("item", item);
            RefreshData();
        }
        private async void btnDelete_Click(object sender, EventArgs e)
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:7069/");
            var item = await httpClient.GetFromJsonAsync<ItemListViewModel>($"customer/{(_selectedItemId == null ? 0 : _selectedItemId)}");
            var response = await httpClient.DeleteAsync($"item/{item.Id}");
            RefreshData();
        }

        public async void RefreshData()
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:7069/");
            var response = await httpClient.GetFromJsonAsync<List<ItemListViewModel>>("item");
            grdDisplayData.DataSource = null;
            grdDisplayData.DataSource = response;
            //grdDisplayData.Columns.GetColumnsWidth; //= DataGridViewAutoSizeColumnMode.Fill;
            grdDisplayData.Columns[index: 0].Visible = false;
            grdDisplayData.Columns[index: 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grdDisplayData.Columns[index: 2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grdDisplayData.Columns[index: 3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grdDisplayData.Columns[index: 4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grdDisplayData.Columns[index: 5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            txtCode.Text = null;
            txtDescription.Text = null;
            spnPrice.Value = 0m;
            spnCost.Value = 0m;
        }
        private void grdDisplayData_SelectionChanged(object sender, EventArgs e)
        {
            if (grdDisplayData.SelectedRows.Count != 1)
                return;

            _selectedItemId = (int)grdDisplayData.CurrentRow.Cells[0].Value;
            txtCode.Text = grdDisplayData.CurrentRow.Cells[1].Value.ToString();
            txtDescription.Text = grdDisplayData.CurrentRow.Cells[2].Value.ToString();
            spnPrice.Value = (decimal)grdDisplayData.CurrentRow.Cells[3].Value;
            spnCost.Value = (decimal)grdDisplayData.CurrentRow.Cells[4].Value;
            //cmbType.SelectedItem = (ItemTypeEnum)grdDisplayData.CurrentRow.Cells[5].Value;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
