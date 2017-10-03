namespace Worker.Objects.Resources
{
    public class Energy : BasicResource
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