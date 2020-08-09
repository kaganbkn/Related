using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProductApp.Entities
{
    public class Tag
    {
        public Guid TagId { get; set; }
        public string Value { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        public Guid ProductId { get; set; }
    }
}
