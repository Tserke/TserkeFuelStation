using FuelStation.Blazor.Shared;
using FuelStation.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FuelStation.Win
{
    public partial class ManagerForm : Form
    {
        public Employee employee { get; set; }
        private ItemsForm _itemForm;
        private CustomerForm _customerForm;
        public TransactionForm _transactionForm;
        public ManagerForm()
        {
            InitializeComponent();
        }

        private void ManagerForm_Load(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var monthlyLedger = new MonthlyLedgerViewModel();
            monthlyLedger.RentingCost = nudRentingCost.Value;
        }

        private void menuItemCustomer_Click(object sender, EventArgs e)
        {
            _customerForm = new CustomerForm();
            _customerForm.ShowDialog();
        }

        private void menuItemTransaction_Click(object sender, EventArgs e)
        {
            _transactionForm = new TransactionForm();
            _transactionForm.employee = employee;
            _transactionForm.ShowDialog();
        }

        private void menuItemItem_Click(object sender, EventArgs e)
        {
            _itemForm = new ItemsForm();
            _itemForm.ShowDialog();
        }
    }
}
