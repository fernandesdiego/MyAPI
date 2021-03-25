using MyAPI.Business.Interfaces;
using MyAPI.Business.Models;
using MyAPI.Business.Models.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAPI.Business.Services
{
    public class SupplierService : BaseService, ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly IAddressRepository _addressRepository;
        
        public SupplierService(ISupplierRepository supplierRepository, IAddressRepository addressRepository, INotifier notifier) : base (notifier)
        {
            _supplierRepository = supplierRepository;
            _addressRepository = addressRepository;
        }
        
        public async Task<bool> Add(Supplier supplier)
        {
            if (!ExecuteValidation(new SupplierValidation(), supplier)) return false;
            if (supplier.Address != null)
                if (!ExecuteValidation(new AddressValidation(), supplier.Address)) return false;

            if(_supplierRepository.Search(x => x.Document == supplier.Document).Result.Any())
            {
                Notify("There is already another supplier registered with this document number.");
                return false;
            }

            await _supplierRepository.Add(supplier);
            return true;
        }
        public async Task<bool> Update(Supplier supplier)
        {
            if (!ExecuteValidation(new SupplierValidation(), supplier)) 
                return false;

            if(_supplierRepository.Search(x => x.Document == supplier.Document && x.Id != supplier.Id).Result.Any())
            {
                Notify("There is already another supplier registered with this document number.");
                return false;
            }

            await _supplierRepository.Update(supplier);
            return true;
            
        }

        public async Task<bool> Remove(Guid id)
        {
            if (_supplierRepository.GetSupplierProductsAddress(id).Result.Products.Any())
            {
                Notify("You can't remove a supplier that have products registered");
                return false;
            }

            var address = await _addressRepository.GetAddressBySupplier(id);
            if(address != null)
            {
                await _addressRepository.Remove(address.Id);
            }

            await _supplierRepository.Remove(id);
            return true;

        }
        public async Task UpdateAddress(Address address)
        {
            if (!ExecuteValidation(new AddressValidation(), address))
                return;

            await _addressRepository.Update(address);
        }

        public void Dispose()
        {
            _supplierRepository?.Dispose();
            _addressRepository?.Dispose();
        }
    }
}
