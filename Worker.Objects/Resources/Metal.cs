using System;

namespace Worker.Objects.Resources
{
    public class Metal : BasicResource
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