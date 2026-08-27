using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aegis.Model.DTO.Auth
{
    public class RegisterDto
    {
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set;}  = string.Empty;

        public string Email { get; set; }  = string.Empty;

        public string Password { get; set; }  = string.Empty;
    }
}