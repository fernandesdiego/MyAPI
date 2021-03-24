using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyAPI.Api.ViewModels;
using MyAPI.Business.Interfaces;
using MyAPI.Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyAPI.Api.Controllers
{
    [Route("api/[controller]")]
    public class SuppliersController : MainController
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly IMapper _mapper;

        public SuppliersController(ISupplierRepository supplierRepository, IMapper mapper)
        {
            _supplierRepository = supplierRepository;
            _mapper = mapper;
        }

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


        [HttpPost]
        public async Task<ActionResult<SupplierViewModel>> Add(SupplierViewModel supplierViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var supplier = _mapper.Map<Supplier>(supplierViewModel);

            await _supplierRepository.Add(supplier);

            return Ok(supplier);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<SupplierViewModel>> Update(Guid id, [FromBody] SupplierViewModel supplierViewModel)
        {
            if (!ModelState.IsValid || id != supplierViewModel.Id) return BadRequest();

            var supplier = _mapper.Map<Supplier>(supplierViewModel);

            await _supplierRepository.Update(supplier);

            return Ok(supplier);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<SupplierViewModel>> Delete(Guid id)
        {
            if (!ModelState.IsValid) return BadRequest();
            
            await _supplierRepository.Remove(id);
            return Ok(id);
        }
    }
}
