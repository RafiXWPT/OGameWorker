﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Worker.Objects.Resources
{
    public class Crystal : BasicResource
    {
        public override double BasicProduction => 75;

        public Crystal(string amount) : base(amount) { }

        public Crystal(double amount) : base(amount) { }

    }
}
