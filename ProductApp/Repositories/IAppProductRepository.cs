using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductApp.Entities;

namespace ProductApp.Repositories
{
    public interface IAppProductRepository
    {
        void AddProduct(Product product);
        void AddTag(Tag tag);
        Task<bool> SaveChangesAsync();
        Task<Product> GetProductAsync(Guid productId);
        Task<Tag> GetTagAsync(Guid tagId);
        IQueryable<Product> GetAllProducts();
        void DeleteProduct(Product product);
        void DeleteTag(Tag tag);
        Task<List<Tag>> GetAllTagsByProductAsync(Guid productId);
    }
}
