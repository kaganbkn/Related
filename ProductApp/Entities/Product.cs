using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductApp.Entities;

namespace ProductApp
{
    public class Product
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public ICollection<Tag> Tags { get; set; }=new List<Tag>();
        public bool IsDeleted { get; set; }
    }
}
