using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Worker.Objects.Resources
{
    public class Metal : IResource
    {
        public double BasicProduction => 150;
        public double Amount { get; set; }

        public Metal(string amount) : this(Convert.ToDouble(amount, new CultureInfo("en-US"))) { }

        public Metal(double amount)
        {
            Amount = amount;
        }
    }
}
