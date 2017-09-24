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
        public abstract int BasicProduction { get; }
        public double Amount { get; set; }

        protected BasicResource(string amount) : this(Convert.ToInt32(amount, new CultureInfo("en-US"))) { }

        protected BasicResource(int amount)
        {
            Amount = amount;
        }
    }
}
