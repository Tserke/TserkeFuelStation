using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelStation.Model
{
    public class TransactionLine
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
        public Transaction Transaction { get; set; }
        public Item Item { get; set; }


        public TransactionLine()
        {
        }
    }
}
