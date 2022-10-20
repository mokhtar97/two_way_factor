using Authenticaion.Domain.Entities;
using Authentication.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Application.Features.AccountFeature.Interfaces
{
    public interface ITokenService
    {
        Task<string> BuildTokenAsync(ApplicationUser user);
    }
}
