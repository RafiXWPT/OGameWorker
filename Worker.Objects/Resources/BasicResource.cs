using System;
using System.Globalization;

namespace Worker.Objects.Resources
{
    public abstract class BasicResource
    {
        protected BasicResource(string amount) : this(Convert.ToInt32(amount, new CultureInfo("en-US")))
        {
        }

        protected BasicResource(int amount)
        {
            Amount = amount;
        }

        public abstract int BaseProduction { get; }
        public int Amount { get; set; }
    }
}