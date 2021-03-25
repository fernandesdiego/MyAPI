using MyAPI.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyAPI.Business.Interfaces
{
    public interface ISupplierService : IDisposable
    {
        Task<bool> Add(Supplier supplier);
        Task<bool> Update(Supplier supplier);
        Task<bool> Remove(Guid id);
        Task UpdateAddress(Address address);

    }
}
