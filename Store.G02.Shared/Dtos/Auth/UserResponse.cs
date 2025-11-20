using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Shared.Dtos.Auth
{
    public class UserResponse
    {
        public string DisplayName { get; set; }
        public string Email { get; set; }   
        public string Token { get; set; }   
    }
}
