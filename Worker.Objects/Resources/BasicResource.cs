using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Worker.Objects.Resources
{
    public abstract class BasicResource
    {
        public abstract double BasicProduction { get; }
        public double Amount { get; set; }

        protected BasicResource(string amount) : this(Convert.ToDouble(amount, new CultureInfo("en-US"))) { }

        protected BasicResource(double amount)
        {
            Amount = amount;
        }
    }
}
