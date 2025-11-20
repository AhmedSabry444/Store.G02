using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.G02.Services.Abstractions;
using Store.G02.Shared.Dtos.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController(IServiceManger _serviceManger) : ControllerBase
    {

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateOrder(OrderRequest request)
        {
            var userEmailClaim =  User.FindFirst(ClaimTypes.Email);
            var result = await _serviceManger.OrderService.CreateOrderAsync(request, userEmailClaim.Value);
            return Ok(result);
        }

        [HttpGet("deliveryMethods")]
        public async Task<IActionResult> GetAllDeliveryMethods() 
        {
            var result = await _serviceManger.OrderService.GetAllDeliveryMethodsAsync();
            return Ok(result);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetOrdersForSpecificUser() 
        {
            var userEmailClaim = User.FindFirst(ClaimTypes.Email);
            var result = await _serviceManger.OrderService.GetOrdersForSpecificUserAsync(userEmailClaim.Value);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetOrderByIdForSpecificUser(Guid id) 
        {
            var userEmailClaim = User.FindFirst(ClaimTypes.Email);
            var result = await _serviceManger.OrderService.GetOrderByIdForSpecificUserAsync(id,userEmailClaim.Value);
            return Ok(result);
        }

    }
}
