using System;

namespace Worker.Objects.Resources
{
    public class Metal : ResourceBase
    {
        public Metal(string amount) : base(amount)
        {
        }

        public Metal(int amount) : base(amount)
        {
        }

        public override int BaseProduction => 150;
    }
}