using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelStation.Blazor.Shared
{
    public class TransactionLineListViewModel
    {
        public int Id { get; set; }
        public int? TransactionId { get; set; }
        public int? ItemId { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? ItemPrice { get; set; }
        public decimal? NetValue { get; set; }
        public int? DiscountPercent { get; set; }
        public decimal? DiscountValue { get; set; }
        public decimal? TotalValue { get; set; }
        public TransactionListViewModel Transaction { get; set; }
        public ItemListViewModel Item { get; set; }
    }

    public class TransactionLineEditListViewModel
    {
        public int Id { get; set; }
        public int? TransactionId { get; set; }
        public int? ItemId { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? ItemPrice { get; set; }
        public decimal? NetValue { get; set; }
        public int? DiscountPercent { get; set; }
        public decimal? DiscountValue { get; set; }
        public decimal? TotalValue { get; set; }
        //public TransactionListViewModel Transaction { get; set; }
        //public ItemListViewModel Item { get; set; }
    }
}
