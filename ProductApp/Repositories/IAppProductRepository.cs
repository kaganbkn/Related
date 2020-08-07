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
        Task<List<Product>> GetAllProductsAsync();
    }
}
