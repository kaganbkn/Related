using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.Internal;
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
            return await _context.Products.Include(c => c.Tags).FirstOrDefaultAsync(c => c.ProductId == productId);
        }

        public async Task<Tag> GetTagAsync(Guid tagId)
        {
            return await _context.Tags.FirstOrDefaultAsync(c => c.TagId == tagId);
        }

        public IQueryable<Product> GetAllProducts()
        {
            return _context.Products.Include(c=>c.Tags).AsQueryable();
        }

        public void DeleteProduct(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            _context.Products.Remove(product);
        }

        public void DeleteTag(Tag tag)
        {
            if (tag == null)
            {
                throw new ArgumentNullException(nameof(tag));
            }

            _context.Tags.Remove(tag);
        }

        public async Task<List<Tag>> GetAllTagsByProductAsync(Guid productId)
        {
            var tags = await _context.Tags.Where(c => c.ProductId == productId).ToListAsync();
            return tags;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
