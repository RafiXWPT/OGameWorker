namespace Worker.Objects.Resources
{
    public class Crystal : BasicResource
    {
        public Crystal(string amount) : base(amount)
        {
        }

        public Crystal(int amount) : base(amount)
        {
        }

        public override int BaseProduction => 75;
    }
}