﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Worker.Objects.Resources
{
    public class Energy : BasicResource
    {
        public override int BasicProduction => 0;

        public Energy(string amount) : base(amount) { }

        public Energy(int amount) : base(amount) { }
    }
}
