using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyAPI.Api.Extensions.Authorization;
using MyAPI.Api.ViewModels;
using MyAPI.Business.Interfaces;
using MyAPI.Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyAPI.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class SuppliersController : MainController
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly ISupplierService _supplierService;
        private readonly IMapper _mapper;

        public SuppliersController(ISupplierRepository  supplierRepository, 
                                   IAddressRepository   addressRepository,
                                   ISupplierService     supplierService,
                                   IMapper              mapper,
                                   INotifier            notifier) : base(notifier)
        {
            _supplierRepository = supplierRepository;
            _addressRepository = addressRepository;
            _supplierService = supplierService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SupplierViewModel>>> GetAll()
        {
            var supplier = _mapper.Map<IEnumerable<SupplierViewModel>>(await _supplierRepository.GetAll());
            if (supplier != null)
            {
                return Ok(supplier);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<IEnumerable<SupplierViewModel>>> GetById(Guid id)
        {
            var supplier = _mapper.Map<SupplierViewModel>(await _supplierRepository.GetById(id));
            if (supplier == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(supplier);
            }
        }

        [HttpGet("address/{id:guid}")]
        public async Task<ActionResult<AddressViewModel>> GetAddressById(Guid id)
        {
            return _mapper.Map<AddressViewModel>(await _addressRepository.GetById(id)); 
        }
        [ClaimsAuthorize("Supplier", "Update")]
        [HttpPut("address/{id:guid}")]
        public async Task<ActionResult<AddressViewModel>> UpdateAddress(Guid id, [FromBody] AddressViewModel addressViewModel)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            if (id != addressViewModel.Id)
            {
                NotifyError("The Id in the body is not the same the Id in the query.");
                return CustomResponse(addressViewModel);
            }

            await _supplierService.UpdateAddress(_mapper.Map<Address>(addressViewModel));

            return CustomResponse(addressViewModel);
        }

        [ClaimsAuthorize("Supplier","Add")]
        [HttpPost]
        public async Task<ActionResult<SupplierViewModel>> Add(SupplierViewModel supplierViewModel)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);
            
            await _supplierService.Add(_mapper.Map<Supplier>(supplierViewModel));

            return CustomResponse(supplierViewModel);
        }

        [ClaimsAuthorize("Supplier", "Update")]
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<SupplierViewModel>> Update(Guid id, [FromBody] SupplierViewModel supplierViewModel)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            if (id != supplierViewModel.Id)
            {
                NotifyError("The Id in the body is different from the Id in the query.");
                return CustomResponse(supplierViewModel);
            }

            if (await _supplierService.Update(_mapper.Map<Supplier>(supplierViewModel)) == false)
            {
                NotifyError("Error, check the fields and try again");
            }
            
            return CustomResponse(supplierViewModel);
        }

        [ClaimsAuthorize("Supplier", "Delete")]
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<SupplierViewModel>> Delete(Guid id)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var supplier = _supplierRepository.GetSupplierAddress(id);
            
            if (supplier == null) return NotFound();

            await _supplierService.Remove(id);

            return CustomResponse(); ;
        }
    }
}
