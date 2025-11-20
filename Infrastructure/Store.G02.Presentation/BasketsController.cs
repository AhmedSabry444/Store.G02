using Microsoft.AspNetCore.Mvc;
using Store.G02.Services.Abstractions;
using Store.G02.Shared.Dtos.Baskets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Presentation
{

    [ApiController]
    [Route("api/[controller]")]
    public class BasketsController(IServiceManger _serviceManger) : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> GetBasketById(string id)
    {
      var result = await  _serviceManger.BasketService.GetBasketAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrUpdateBasket(BasketDto dto)
        {
            var result = await  _serviceManger.BasketService.CreateBasketAsync(dto,TimeSpan.FromDays(1));
            return Ok(result);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteBasketById(string id)
        {
            var result = await  _serviceManger.BasketService.DeleteBasketAsync(id);
            return NoContent();
        }

    }
}
