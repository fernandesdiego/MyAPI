using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyAPI.Api.ViewModels;
using MyAPI.Business.Interfaces;
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
        public async Task<ActionResult<IEnumerable<SupplierViewModel>>> GetAll()
        {
            return Ok(_mapper.Map<IEnumerable<SupplierViewModel>>(await _supplierRepository.GetAll()));
        }
    }
}
