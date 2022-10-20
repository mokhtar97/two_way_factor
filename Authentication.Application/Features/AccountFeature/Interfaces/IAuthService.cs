using Authentication.Application.Features.AccountFeature.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Application.Features.AccountFeature.Interfaces
{
    public interface IAuthService
    {
        string GenerateToken(UserClaims userClaim, int expireMinutes = 1000000);
        bool IsValidToken(string token, string[] roles);

    }
}
