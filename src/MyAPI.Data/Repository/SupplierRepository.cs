using Microsoft.EntityFrameworkCore;
using MyAPI.Business.Interfaces;
using MyAPI.Business.Models;
using MyAPI.Data.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyAPI.Data.Repository
{
    public class SupplierRepository : Repository<Supplier>, ISupplierRepository
    {
        public SupplierRepository(ApplicationContext context) : base(context)
        {
        }

        public async Task<Supplier> GetSupplierAddress(Guid id)
        {
            return await _db.Suppliers.AsNoTracking().Include(x => x.Address)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Supplier> GetSupplierProductsAddress(Guid id)
        {
            return await _db.Suppliers.AsNoTracking()
                .Include(x => x.Products)
                .Include(x => x.Address)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
