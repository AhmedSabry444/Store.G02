using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.G02.Presentation.Attributes;
using Store.G02.Services.Abstractions;
using Store.G02.Shared;
using Store.G02.Shared.Dtos.Products;
using Store.G02.Shared.ErrorModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Presentation
{

    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(IServiceManger _serviceManger) : ControllerBase
    {

        [HttpGet]
        [Cache(50)]
        
        [ProducesResponseType(StatusCodes.Status200OK,Type = typeof (PaginationResponse<ProductResponse>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError,Type = typeof (ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest,Type = typeof (ErrorDetails))]
        public async Task<ActionResult<PaginationResponse<ProductResponse>>> GetAllProducts([FromQuery] ProductQueryParameters parameters)
        {
            var result = await _serviceManger.ProductService.GetAllProductsAsync(parameters); 
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
        public async Task<ActionResult<ProductResponse>> GetProductById(int? id)
        {
            if(id is null) return BadRequest();

            var result = await _serviceManger.ProductService.GetProductByIdAsync(id.Value);
            return Ok(result);
        }

        [HttpGet("brands")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<BrandResponse>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
        public async Task<ActionResult<BrandResponse>> GetAllBrandsAsync(int id)
        {
            var result = await _serviceManger.ProductService.GetAllBrandsAsync(id);
            if (result is null) return BadRequest();
            return Ok(result);
        }

        [HttpGet("types")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TypeResponse>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
        public async Task<ActionResult<TypeResponse>> GetAllTypes(int id)
        {
            var result = await _serviceManger.ProductService.GetAllBrandsAsync(id);
            if (result is null) return BadRequest();
            return Ok(result);
        }
    }
}
