using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.G02.Services.Abstractions;
using Store.G02.Shared.Dtos.Auth;
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
    public class AuthController(IServiceManger _serviceManger) : ControllerBase
    {

        [HttpPost("login")]
        public async Task<IActionResult> login(LoginRequest request)
        {
            var result = await _serviceManger.AuthService.LoginAsync(request);
            return Ok(result);
        }



        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var result = await _serviceManger.AuthService.RegisterAsync(request);
            return Ok(result);
        }

        [HttpGet("EmailExists")]
        public async Task<IActionResult> CheckEmailExists(string email)
        {
            var result = await _serviceManger.AuthService.CheckEmailExistAsync(email);
            return Ok(result);
        }

        [Authorize]
        [HttpGet("currentUser")]
        public async Task<IActionResult> GetCurrentUser()
        {
           var email = User.FindFirst(ClaimTypes.Email);
            var result = await _serviceManger.AuthService.GetCurrentUserAsync(email.Value);
            return Ok(result);
        }

        [Authorize]
        [HttpGet("Address")]
        public async Task<IActionResult> GetCurrentUserAddress()
        {
            var email = User.FindFirst(ClaimTypes.Email);
            var result = await _serviceManger.AuthService.GetCurrentUserAddressAsync(email.Value);
            return Ok(result);
        }


        [Authorize]
        [HttpPut("Address")]
        public async Task<IActionResult> UpdateCurrentUserAddress(AddressDto request)
        {
            var email = User.FindFirst(ClaimTypes.Email);
            var result = await _serviceManger.AuthService.UpdateCurrentUserAddressAsync(request, email.Value);
            return Ok(result);
        }

    }
}