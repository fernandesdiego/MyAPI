using Microsoft.EntityFrameworkCore;
using MyAPI.Business.Intefaces;
using MyAPI.Business.Models;
using MyAPI.Data.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyAPI.Data.Repository
{
    public class AddressRepository : Repository<Address>, IAddressRepository
    {
        public AddressRepository(ApplicationContext context) : base(context)
        {

        }

        public async Task<Address> GetAddressBySupplier(Guid supplierId)
        {
            return await _db.Addresses.AsNoTracking().FirstOrDefaultAsync(x => x.SupplierId == supplierId);
        }
    }
}
