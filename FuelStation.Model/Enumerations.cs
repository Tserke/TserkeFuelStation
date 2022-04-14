using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelStation.Model
{
    public enum PaymentMethodEnum
    {
        Cash,
        CreditCard
    }

    public enum ItemTypeEnum
    {
        Fuel,
        Product,
        Service
    }

    public enum EmployeeTypeEnum
    {
        Manager,
        Staff,
        Cashier
    }

    public enum MonthEnum
    {
        None,
        January,
        February,
        March,
        April,
        May,
        June,
        July,
        August,
        September,
        October,
        November,
        December
    }
}
