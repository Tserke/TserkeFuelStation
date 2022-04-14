using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelStation.Blazor.Shared
{
 
    public class Calculations
    {
        public decimal? CalculateNetValue(decimal? itemPrice, decimal? quantity)
        {
            var netValue = (itemPrice * quantity);
            return netValue;
        }
        public decimal? CalculateDiscountValue(decimal? netValue, int? discountPercent)
        {
            var discountValue = 0m;
            if (discountPercent.HasValue)
                discountValue = ((decimal)(netValue * (discountPercent / 100m)));
            return discountValue;
        }
        public decimal? CalculateTotalValue(decimal? discountValue, decimal? netValue)
        {
            var totalValue = (netValue - discountValue);
            if (discountValue == null)
                totalValue = netValue;
            return totalValue;
        }

    }

}
