using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelStation.Model
{
    public enum ItemTypeEnum
    {
        Fuel,
        Product,
        Service
    }
    public class Item
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public ItemTypeEnum ItemType { get; set; }
        public double Price { get; set; }
        public double Cost { get; set; }
        public TransactionLine TransactionLine { get; set; }

        public Item()
        {

        }

    }
}
