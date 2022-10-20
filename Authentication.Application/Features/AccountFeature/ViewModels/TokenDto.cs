using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Application.Features.AccountFeature.ViewModels
{
    public class TokenDto
    {
        public string Token { get; set; }
        public string RoleName { get; set; }
        public string Email { get; set; }
        public string ImageUrl { get; set; }
        public string UserName { get; set; }
        public int UserId { get; set; }
        public bool requires2FA { get; set; }
    }
}
