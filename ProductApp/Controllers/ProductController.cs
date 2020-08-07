using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Internal;
using Microsoft.AspNetCore.Mvc;
using ProductApp.DataTransferObjects;
using ProductApp.Repositories;

namespace ProductApp.Controllers
{
    [ApiController]
    [Route("api/product")]
    public class ProductController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAppProductRepository _appProductRepository;

        public ProductController(IMapper mapper, IAppProductRepository appProductRepository)
        {
            _mapper = mapper;
            _appProductRepository = appProductRepository;
        }

        [HttpGet("productId", Name = "GetProduct")]
        public async Task<Product> GetProduct(Guid productId)
        {
            var product = await _appProductRepository.GetProductAsync(productId);

            if (product == null)
            {
                throw new ArgumentNullException(nameof(product)); // todo: add custom message
            }

            return product; //todo: add dto
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductCreateDto productToCreate)
        {
            var product = _mapper.Map<Product>(productToCreate);
            _appProductRepository.AddProduct(product);

            if (product.Tags.Any())
            {
                product.Tags.ForAll(c =>
                {
                    _appProductRepository.AddTag(c);
                });
            }

            await _appProductRepository.SaveChangesAsync();


            return Ok();
        }

        [HttpPut("{productId}")]
        public async Task<IActionResult> UpdateProduct(Guid productId, ProductUpdateDto productToUpdate)
        {
            var product = await _appProductRepository.GetProductAsync(productId);

            if (product == null)
            {
                return NotFound();
            }

            _mapper.Map(productToUpdate, product);

            await _appProductRepository.SaveChangesAsync();
            return CreatedAtRoute("GetProduct", productId, product);

        }
    }
}
