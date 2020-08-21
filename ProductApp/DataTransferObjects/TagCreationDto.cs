using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductApp.DataTransferObjects
{
    public class TagCreationDto
    {
        public Guid TagId { get; set; }
        public string Value { get; set; }
    }
}
