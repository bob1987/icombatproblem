using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSale.Objects
{
    class Bill
    {
        public String Name { get; set; }
        public int Denomination { get; set; }
        public int Amount { get; set; }

        public Bill(String name, int denomination, int amount)
        {
            Name = name;
            Denomination = denomination;
            Amount = amount;
        }
    }
}
