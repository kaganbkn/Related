using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductApp.DataTransferObjects
{
    public class ProductCreateDto
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public ICollection<TagCreationDto> Tags { get; set; } = new List<TagCreationDto>();

    }
}
