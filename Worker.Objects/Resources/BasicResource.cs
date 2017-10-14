using System;
using System.Globalization;

namespace Worker.Objects.Resources
{
    public abstract class BasicResource
    {
        protected BasicResource(string amount)
            : this(amount.Contains("Mln")
                ? Convert.ToInt32(amount.Replace("Mln", string.Empty).Replace(",", string.Empty)) * 10000
                : Convert.ToInt32(amount, new CultureInfo("en-US")))
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