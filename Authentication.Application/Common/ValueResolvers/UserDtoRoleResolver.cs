using Authenticaion.Domain.Entities;
using Authentication.Application.Features.AccountFeature.ViewModels;
using AutoMapper;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authenticaion.Application.Common.ValueResolvers
{
    public class UserDtoRoleResolver : IValueResolver<ApplicationUser, UserDto, string>
    {
        UserManager<ApplicationUser> _userManager;
        public UserDtoRoleResolver(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public string Resolve(ApplicationUser source, UserDto destination, string destMember, ResolutionContext context)
        {
            return _userManager.GetRolesAsync(source).Result[0];
        }
    }
}
