using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Worker.Objects.Resources
{
    public class Metal : BasicResource
    {
        public override double BasicProduction => 150;

        public Metal(string amount) : base(amount) { }

        public Metal(double amount) : base(amount) { }
    }
}
