using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Worker.Objects.Resources
{
    public class Deuterium : IResource
    {
        public double BasicProduction => 0;
        public double Amount { get; set; }
        public Deuterium(string amount) : this(Convert.ToDouble(amount, new CultureInfo("en-US"))) { }

        public Deuterium(double amount)
        {
            Amount = amount;
        }
    }
}
