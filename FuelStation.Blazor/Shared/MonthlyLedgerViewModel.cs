using FuelStation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelStation.Blazor.Shared
{
    public class MonthlyLedgerViewModel
    {
        public short Year { get; set; }
        public short Month { get; set; }
        public decimal Income { get; set; }
        public decimal Expenses { get; set; }
        public decimal Total { get; set; }

        public MonthEnum MonthEnum { get; set; }
    }
}
