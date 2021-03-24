using MyAPI.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using MyAPI.Business.Interfaces;
using MyAPI.Data.Context;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace MyAPI.Data.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {

        public ProductRepository(ApplicationContext context) : base(context)
        {

        }
        public async Task<Product> GetProductAndSupplier(Guid id)
        {
            return await _db.Products.AsNoTracking().Include(f => f.Supplier)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Product>> GetProductsAndSuppliers()
        {
            return await _db.Products.AsNoTracking().Include(f => f.Supplier)
                .OrderBy(p => p.Name).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsBySupplier(Guid supplierId)
        {
            return await Search(p => p.SupplierId == supplierId);
        }
    }
}
