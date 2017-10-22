using System;

namespace Worker.Objects.Resources
{
    public class Deuterium : ResourceBase
    {
        public Deuterium(string amount) : base(amount)
        {
        }

        public Deuterium(int amount) : base(amount)
        {
        }

        public override int BaseProduction => 0;
    }
}