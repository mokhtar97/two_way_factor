using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authenticaion.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string? ForgetEmailVerificationCode { get; set; }
    }
}