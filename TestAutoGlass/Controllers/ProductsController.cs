using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAutoGlass.Domain.Entities;
using TestAutoGlass.Domain.Interfaces.Services;
using TestAutoGlass.Domain.Requests;
using TestAutoGlass.Domain.Requests.Create;
using TestAutoGlass.Domain.Requests.Update;
using TestAutoGlass.Domain.Responses;

namespace TestAutoGlass.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService _productsService;
        private readonly IMapper _mapper;

        public ProductsController(IProductsService productsService, IMapper mapper)
        {
            _productsService = productsService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _productsService.GetByIdAsync(id);

            if (response == null)
                return NotFound("Product not found");

            var prodResponse = _mapper.Map<ProductsResponse>(response);

            return Ok(prodResponse);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFiltered([FromQuery] ProductsRequest productsParams, int pageNumber, int pageQuantity)
        {
            var response = await _productsService.GetAllAsync(productsParams, pageNumber, pageQuantity);

            if (response.Count() == 0)
                return NotFound("Products not found");

            var prodResponse = _mapper.Map<IEnumerable<ProductsResponse>>(response);

            return Ok(prodResponse);
        }

        [HttpPut]
        public async Task<IActionResult> Insert(CreateProductRequest product)
        {
            var newProd = _mapper.Map<Products>(product);

            var response = await _productsService.CreateAsync(newProd);

            var prodResponse = _mapper.Map<ProductsResponse>(response);

            return Ok(prodResponse);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateProductRequest product)
        {
            var updatedProd = _mapper.Map<Products>(product);

            var response = await _productsService.UpdateAsync(updatedProd);

            if (response == null)
                return NotFound("Product not found");

            var prodResponse = _mapper.Map<ProductsResponse>(response);

            return Ok(prodResponse);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _productsService.DeleteAsync(id);

            if (!response)
                return NotFound("Product not found");

            return Ok();
        }
    }
}
