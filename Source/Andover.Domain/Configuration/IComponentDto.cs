namespace Andover.Domain.Configuration
{
    public interface IComponentDto
    {
        string Type { get; set; }
        string Name { get; set; }
        string Description { get; set; }
    }
}