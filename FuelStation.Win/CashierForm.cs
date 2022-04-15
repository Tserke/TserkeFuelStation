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
    public partial class CashierForm : Form
    {
        public Employee employee { get; set; }
        private CustomerForm _customerForm;
        public TransactionForm _transactionForm;
        public CashierForm()
        {
            InitializeComponent();
        }

        private void CashierForm_Load(object sender, EventArgs e)
        {

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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
