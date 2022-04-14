using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelStation.Blazor.Shared
{
    public class CustomerListViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        [RegularExpression(@"(^[A-a][0-9]+)$")] //, ErrorMessage ="Please start with 'A' and continue with numbers")]
        public string CardNumber { get; set; }
    }
    
    public class CustomerEditListViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        [RegularExpression(@"(^[A-a][0-9]+)$")]/*, ErrorMessage = "Please start with 'A' and continue with numbers")]*/
        public string CardNumber { get; set; }
    }
}
