using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aegis.Model.DTO.Auth;
using Aegis.Model.Vm.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Aegis.Services.Services.Interfaces
{
    public interface IAuthService
    {
        
         Task<RegisterResponse> RegisterAsync(RegisterDto model);

        //  Task<AuthResponse> LoginAsync(LoginDto model);
    }
}