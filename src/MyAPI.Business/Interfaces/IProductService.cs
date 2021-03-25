using MyAPI.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyAPI.Business.Interfaces
{
    interface IProductService : IDisposable
    {
        Task Add(Product product);
        Task Update(Product product);
        Task Remove(Guid id);
    }
}
