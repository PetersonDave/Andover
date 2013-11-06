using Andover.Domain.Configuration;

namespace Andover.Data.Configuration
{
    public class ComponentDto : IComponentDto
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}