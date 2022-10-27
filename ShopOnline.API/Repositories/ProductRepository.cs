using Microsoft.EntityFrameworkCore;
using ShopOnline.API.Data;
using ShopOnline.API.Entities;
using ShopOnline.API.Repositories.Contracts;

namespace ShopOnline.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ShopOnlineDbContext _context;
        public ProductRepository(ShopOnlineDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductCategory>> GetCategories()
        {
            return await _context.ProductCategories.ToListAsync();
        }

        public async Task<ProductCategory> GetCategory(int id)
        {
            return await _context.ProductCategories.SingleOrDefaultAsync(pc => pc.Id == id);
        }

        public async Task<Product> GetItem(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<IEnumerable<Product>> GetItems()
        {
            return await _context.Products.ToListAsync();
        }
    }
}
