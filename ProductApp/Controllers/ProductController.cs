using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Configuration.Annotations;
using AutoMapper.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [HttpGet("{productId}", Name = "GetProduct")]
        public async Task<ProductOutputDto> GetProduct(Guid productId)
        {
            var product = await _appProductRepository.GetProductAsync(productId);

            if (product == null)
            {
                throw new ArgumentNullException(nameof(product)); // todo: add custom message
            }

            return _mapper.Map<ProductOutputDto>(product); //todo: remove isdeleted
        }

        [HttpGet]
        public List<ProductOutputDto> GetAllProduct([FromQuery] SearchParameters searchParameters)
        {

            var products = _appProductRepository.GetAllProducts();

            if (!products.Any())
            {
                throw new ArgumentNullException(nameof(products)); // todo: add custom message //todo:add notfound
            }

            if (!string.IsNullOrWhiteSpace(searchParameters.Name))
            {
                var name = searchParameters.Name.Trim();
                products = products.Where(c => c.Name.Contains(name));
            }

            if (!string.IsNullOrWhiteSpace(searchParameters.Tag))
            {
                var tag = searchParameters.Tag.Trim();
                products = products.Where(c => c.Tags.Any(t => t.Value.Contains(tag)));

            }

            return _mapper.Map<List<ProductOutputDto>>(products);
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

            return CreatedAtRoute("GetProduct", new { product.ProductId}, product);
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
            return CreatedAtRoute("GetProduct", new{ productId }, product);

        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteProduct(Guid productId)
        {
            var product = await _appProductRepository.GetProductAsync(productId);

            if (product == null)
            {
                return NotFound();
            }

            product.IsDeleted = true;

            await _appProductRepository.SaveChangesAsync();

            return NoContent();
        }
        
        [HttpOptions]
        public IActionResult GetAuthorsOptions()
        {
            Response.Headers.Add("Allow", "GET,DELETE,POST,PUT,OPTIONS");
            return Ok();
        }
    }
}
