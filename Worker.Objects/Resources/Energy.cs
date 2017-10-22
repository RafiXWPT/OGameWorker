namespace Worker.Objects.Resources
{
    public class Energy : ResourceBase
    {
        public Energy(string amount) : base(amount)
        {
        }

        public Energy(int amount) : base(amount)
        {
        }

        public override int BaseProduction => 0;
    }
}