using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Application.Features.AccountFeature.ViewModels
{
    public class ConfirmModel
    {
        public string Code { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
