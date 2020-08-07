using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProductApp.Entities;

namespace ProductApp.Repositories
{
    public class AppProductAppRepository : IAppProductRepository
    {
        private readonly AppDbContext _context;

        public AppProductAppRepository(AppDbContext context)
        {
            _context = context;
        }

        public void AddProduct(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            _context.Products.Add(product);
        }

        public void AddTag(Tag tag)
        {
            if (tag == null)
            {
                throw new ArgumentNullException(nameof(tag));
            }

            _context.Tags.Add(tag);
        }

        public async Task<Product> GetProductAsync(Guid productId)
        {
            return await _context.Products.FirstOrDefaultAsync(c => c.ProductId == productId);
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
