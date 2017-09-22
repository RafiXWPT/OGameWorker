using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Worker.Objects.Resources
{
    public class Crystal : IResource
    {
        public double BasicProduction => 75;
        public double Amount { get; set; }

        public Crystal(string amount) : this(Convert.ToDouble(amount, new CultureInfo("en-US"))) { }

        public Crystal(double amount)
        {
            Amount = amount;
        }
    }
}
