namespace Worker.Objects.Resources
{
    public interface IResource
    {
        double BasicProduction { get; }
        double Amount { get; set; }
    }
}