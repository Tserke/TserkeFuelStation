using FuelStation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelStation.Blazor.Shared
{
    public class TransactionListViewModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public int EmployeeId { get; set; }
        public int CustomerId { get; set; }
        public PaymentMethodEnum PaymentMethod { get; set; }
        public decimal TotalValue { get; set; }
        public Employee Employee { get; set; }
        public Customer Customer { get; set; }
        public List<TransactionListViewModel> TransactionLineList { get; set; }
    }
}
