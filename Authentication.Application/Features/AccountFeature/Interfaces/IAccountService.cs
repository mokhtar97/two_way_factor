using Authentication.Application.Common.Response;
using Authentication.Application.Features.AccountFeature.ViewModels;
using Authentication.Application.Features.AccountFeature.ViewModels.GoogleAuthenticatorModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Application.Features.AccountFeature.Interfaces
{
    public interface IAccountService
    {
        Task<TokenDto> Login(UserLoginDto model);
        Task<IdentityResult> CreateUserAsync(UserDto dto);
        Task<PageVMResponse> GetAllUsers(PageVM pageVm);
        Task<UserDto> GetUserById(string id);
        Task<bool> ChagePasswrd(string Id, string newpassword);
        Task<bool> ConfirmPhone(string phone, string code);
        Task<bool> ConfirmEmail(string email, string code);

        Task<bool> SendVerificationCode(ForgetPassworDto forgetPassworDto);

        Task<bool> ResetPassword(ForgetPassworDto forgetPassworDto);
        Task<EnableAuthenticatorViewModel> EnableAuthenticator(string email);
        Task<bool> IsValidAuthenticator(EnableAuthenticatorViewModel enableAuthenticatorViewModel);

    }
}
