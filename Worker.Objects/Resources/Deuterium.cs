using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Worker.Objects.Resources
{
    public class Deuterium : BasicResource
    {
        public override int BasicProduction => 0;

        public Deuterium(string amount) : base(amount) { }

        public Deuterium(int amount) : base(amount) { }
    }
}
