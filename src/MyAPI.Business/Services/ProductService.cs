using MyAPI.Business.Interfaces;
using MyAPI.Business.Models;
using MyAPI.Business.Models.Validations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyAPI.Business.Services
{
    class ProductService : BaseService, IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly INotifier _notifier;
        public ProductService(IProductRepository productRepository, INotifier notifier) : base(notifier)
        {
            _productRepository = productRepository;

        }
        public async Task Add(Product product)
        {
            if (!ExecuteValidation(new ProductValidation(), product)) return;

            await _productRepository.Add(product);
        }
        public async Task Update(Product product)
        {
            if (!ExecuteValidation(new ProductValidation(), product)) return;

            await _productRepository.Update(product);
        }
        public async Task Remove(Guid id)
        {
            await _productRepository.Remove(id);
        }

        public void Dispose()
        {
            _productRepository?.Dispose();
        }


    }
}
