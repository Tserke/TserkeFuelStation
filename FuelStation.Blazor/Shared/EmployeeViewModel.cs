using FuelStation.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelStation.Blazor.Shared
{
    public class EmployeeListViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime HireDateStart { get; set; }
        public DateTime? HireDateEnd { get; set; }
        public double SallaryPerMonth { get; set; }
        public EmployeeTypeEnum EmployeeType { get; set; }
    }

    public class EmployeeEditListViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        [Required]
        public DateTime? HireDateStart { get; set; }
        public DateTime? HireDateEnd { get; set; }
        public double SallaryPerMonth { get; set; }
        public EmployeeTypeEnum EmployeeType { get; set; }
    }


}
