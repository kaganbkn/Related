using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProductApp.Entities;

namespace ProductApp
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Jean",
                    Price = 10.3,
                    Tags = new List<Tag>
                    {
                        new Tag
                        {
                            TagId = Guid.NewGuid(),
                            Value = "blue"
                        },

                        new Tag
                        {
                            TagId = Guid.NewGuid(),
                            Value = "32"
                        }
                    }
                });
        }
    }
}
