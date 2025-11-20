using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Store.G02.Shared.Dtos.Auth;   

namespace Store.G02.Services.Abstractions.Auth
{
    public interface IAuthService
    {
      Task<UserResponse>  LoginAsync(LoginRequest requst);
      Task<UserResponse>  RegisterAsync(RegisterRequest requst);
      
       Task<bool>  CheckEmailExistAsync(string email);

        Task<UserResponse?> GetCurrentUserAsync(string email);
        Task<AddressDto?> GetCurrentUserAddressAsync(string email);
        Task<AddressDto?> UpdateCurrentUserAddressAsync(AddressDto request,string email);

    }
}
