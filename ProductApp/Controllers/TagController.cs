using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProductApp.DataTransferObjects;
using ProductApp.Entities;
using ProductApp.Repositories;

namespace ProductApp.Controllers
{
    [ApiController]
    [Route("api/product/{productId}/tag")]
    public class TagController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAppProductRepository _appProductRepository;

        public TagController(IMapper mapper, IAppProductRepository appProductRepository)
        {
            _mapper = mapper;
            _appProductRepository = appProductRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTag(Guid productId, TagCreationDto tag)
        {
            var product = await _appProductRepository.GetProductAsync(productId);

            if (product == null)
            {
                throw new ArgumentNullException(nameof(product)); // todo: add custom message
            }

            var tagToCreate = _mapper.Map<Tag>(tag);

            tagToCreate.Product = product;
            tagToCreate.ProductId = productId;

            _appProductRepository.AddTag(tagToCreate);
            
            await _appProductRepository.SaveChangesAsync();

            return Ok();
        }


        [HttpDelete("{tagId}")]
        public async Task<IActionResult> DeleteTag(Guid tagId)
        {
            var tag = await _appProductRepository.GetTagAsync(tagId);

            if (tag == null)
            {
                return NotFound();
            }

            tag.IsDeleted = true;

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
