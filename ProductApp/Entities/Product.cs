using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ProductApp.Entities;

namespace ProductApp
{
    public class Product
    {
        public Guid ProductId { get; set; }
        [Required]
        [StringLength(64)]
        public string Name { get; set; }
        [Required]
        public double Price { get; set; }
        public ICollection<Tag> Tags { get; set; }=new List<Tag>();
        public bool IsDeleted { get; set; }
    }
}
