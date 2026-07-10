using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aegis.Model.Vm.Auth
{
    public class RegisterResponse
    {
        public bool Success { get; set; }

        public bool Errors { get; set; }

        public string Message { get; set; } = string.Empty;

        public int StatusCode { get; set; }

        public List<string>? ValidationErrors { get; set; }

        public string? UserId { get; set; }
    }
}