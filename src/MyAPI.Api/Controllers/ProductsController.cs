using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyAPI.Api.ViewModels;
using MyAPI.Business.Interfaces;
using MyAPI.Business.Models;
using MyAPI.Business.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MyAPI.Api.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : MainController
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        public ProductsController(INotifier notifier, IMapper mapper, IProductRepository productRepository, IProductService productService) : base(notifier)
        {
            _mapper = mapper;
            _productRepository = productRepository;
            _productService = productService;
        }

        [HttpGet]
        public async Task<IEnumerable<ProductViewModel>> GetAll()
        {
            return _mapper.Map<IEnumerable<ProductViewModel>>(await _productRepository.GetProductsAndSuppliers());
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ProductViewModel>> GetById(Guid id)
        {
            var product = _mapper.Map<ProductViewModel>(await _productRepository.GetById(id));

            if (product == null) return NotFound();

            return product;
        }

        [HttpPost]
        public async Task<ActionResult<ProductViewModel>> Add(ProductViewModel product)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var imgName = Guid.NewGuid() + "_" + product.Image;
            if (!UploadFile(product.ImageUpload, imgName))
            {
                return CustomResponse(product);
            }
            product.Image = imgName;
            await _productService.Add(_mapper.Map<Product>(product));

            return CustomResponse(product);
        }


        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<ProductViewModel>> Delete(Guid id)
        {
            var product = _mapper.Map<ProductViewModel>(_productRepository.GetById(id));
            if (product == null) return NotFound();

            await _productService.Remove(id);

            return CustomResponse(product);
        }

        private bool UploadFile(string file, string fileName)
        {
            byte[] imgBytes;

            if(string.IsNullOrEmpty(file))
            {
                NotifyError("Please upload a valid image for this product");
                return false;
            }

            try
            {
                imgBytes = Convert.FromBase64String(file);
            }
            catch(Exception e)
            {
                NotifyError(e.Message);
                return false;
            }


            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ProductImages", fileName);

            if (System.IO.File.Exists(filePath))
            {
                NotifyError("There's a file with the same name.");
                return false;
            }

            System.IO.File.WriteAllBytes(filePath, imgBytes);
            return true;
        }


    }
}
