using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelStation.Model
{
    public class Ledger
    {
        public string Year { get; set; }
        public string Month { get; set; }
        public double Income { get; set; }
        public double Expenses { get; set; }
        public double Total { get; set; }

        public Ledger()
        {

        }
    }
}
