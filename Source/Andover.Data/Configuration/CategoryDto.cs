using System;
using System.Linq;
using System.Text;
using Andover.Domain.Configuration;

namespace Andover.Data.Configuration
{
    public class CategoryDto : ICategoryDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
