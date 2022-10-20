using AutoMapper;
using Authentication.Application.Common.Interfaces;
using Authentication.Application.Features.AccountFeature.ViewModels;
using Authentication.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Authentication.Application.Features.AccountFeature.Interfaces;
using System.Linq.Expressions;
using Authentication.Application.Common.Enums;
using System.Security.Claims;

using Authentication.Application.Common.Helpers;
using Authenticaion.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Authentication.Infrastructure.NotificationServices.SMSService.Helpers;
using Authentication.Application.Features.AccountFeature.ViewModels.GoogleAuthenticatorModels;
using System.Text;
using System.Text.Encodings.Web;
using Authentication.Application.Common.Response;

namespace Authentication.Application.Features.AccountFeature
{


    public class AccountService : IAccountService
    {
        IAuthService _authService;
        ITokenService _tokenService;
       
        ISmsTwilio _SmsTwilio;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unit;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMailService _mailService;
        private readonly UrlEncoder _urlEncoder;

        private const string AuthenticatorUriFormat = "otpauth://totp/{0}:{1}?secret={2}&issuer={0}&digits=6";
        private const string RecoveryCodesKey = nameof(RecoveryCodesKey);

        public AccountService(IAuthService authService,
            ITokenService tokenService,
          
             ISmsTwilio SmsTwilio,
        UserManager<ApplicationUser> userManager,
            IConfiguration configuration,
            IUnitOfWork unit,
            IMapper mapper,
            IHttpContextAccessor httpContext,
            IMailService mailService,
            UrlEncoder urlEncoder,
            RoleManager<IdentityRole> roleManager)
        {
            _authService = authService;
            _tokenService = tokenService;
         
            _userManager = userManager;
            _configuration = configuration;
            _unit = unit;
            _mapper = mapper;
            _httpContext = httpContext;
            _roleManager = roleManager;
            _mailService = mailService;
            _SmsTwilio = SmsTwilio;
            _urlEncoder = urlEncoder;

        }
        public async Task<TokenDto> Login(UserLoginDto model)
        {
            // sign in using email if not then sign in by phone number ;property email in dto can hold both email and phone
            var user = await _userManager.FindByEmailAsync(model.Email) ?? _userManager.Users.Where(u => u.PhoneNumber == model.Email).FirstOrDefault();
            if (user != null)
            {
                if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
                {
                    // authentication successful so generate jwt and refresh tokens
                    return new TokenDto
                    {
                        // get jwt token for user from authservice
                        Token = await _tokenService.BuildTokenAsync(user)
                    };
                }
                return null;
            }
            else
            {
                return null;
            }
        }

        public async Task<IdentityResult> CreateUserAsync(UserDto dto)
        {
            var mapped = _mapper.Map<UserDto, ApplicationUser>(dto);
            mapped.Id = Guid.NewGuid().ToString();
            var Role = await _roleManager.FindByNameAsync(dto.role);
            if (Role == null)
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = dto.role });
                Role = await _roleManager.FindByNameAsync(dto.role);
            }
            var result = await _userManager.CreateAsync(mapped, dto.password);
            if (!result.Succeeded)
            {
                return result;
            }
            var result1 = await _userManager.AddToRoleAsync(mapped, Role.Name);
            return result1;
        }

      

        public async Task<PageVMResponse> GetAllUsers(PageVM pageVm)
        {

            PageVMResponse result = new PageVMResponse();
            List<UserDto> usersList = new List<UserDto>();

            var users = await _userManager.GetUsersInRoleAsync(pageVm.Role);
            foreach (var user in users)
            {
                var mapped = _mapper.Map<ApplicationUser, UserDto>(user);
                mapped.role = pageVm.Role;
                usersList.Add(mapped);
            }

            result.length = usersList.Count();
            if (!string.IsNullOrEmpty(pageVm.Name) && !pageVm.Name.Contains("empty"))
            {
                usersList = usersList.Where(u => u.username.Contains(pageVm.Name)).ToList();
            }
            usersList = usersList.Skip(pageVm.PageSize * (pageVm.PageNumber - 1)).Take(pageVm.PageSize).ToList();
            result.PageSize = pageVm.PageSize;
            result.PageNumber = pageVm.PageNumber;
            result.users = usersList;
            return result;
        }


     
        public async Task<bool> ChagePasswrd(string Id, string newpassword)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == Id);
            if (user != null)
            {

                var token = await this._userManager.GeneratePasswordResetTokenAsync(user);
                var result = await this._userManager.ResetPasswordAsync(user, token, newpassword);
                if (result.Succeeded)
                {
                    return true;
                }

            }
            return false;
        }

        public async Task<UserDto> GetUserById(string id)
        {
            try
            {
                var user = await _userManager.Users.Where(x => x.Id == id).FirstOrDefaultAsync();
                var roleName = await _userManager.GetRolesAsync(user);
                IdentityRole role = (IdentityRole)await _roleManager.FindByNameAsync(roleName.FirstOrDefault());
                var mapped = _mapper.Map<ApplicationUser, UserDto>(user);
                mapped.role = role.Name;
                return mapped;
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public async Task<bool> ConfirmEmail(string email, string code)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return false;
            if (user.ForgetEmailVerificationCode == code)
            {
                user.EmailConfirmed = true;
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded) return true;
                return false;
            }
            else
                return false;
        }

        public async Task<bool> ConfirmPhone(string phone, string code)
        {
            var user = await _userManager.Users.Where(x => x.PhoneNumber == phone).FirstOrDefaultAsync();
            if (user == null)
                return false;
            if (user.ForgetEmailVerificationCode == code)
            {
                user.PhoneNumberConfirmed = true;
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded) return true;
                return false;
            }
            else
                return false;
        }

     

        public async Task<bool> SendVerificationCode(ForgetPassworDto forgetPassworDto)
        {
            if (forgetPassworDto.isEmail)
            {
                var user = await _userManager.FindByEmailAsync(forgetPassworDto.Email);
                if (user == null)
                    return false;
                user.ForgetEmailVerificationCode = ForgetCodeValuesHelper.GenerateRandomNumber();
                await _userManager.UpdateAsync(user);
                return await _mailService.SendMailForgetPasswordAsync(user.Email, user.UserName, user.ForgetEmailVerificationCode);
            }
            else
            {
                var user = await _userManager.Users.Where(x => x.PhoneNumber == forgetPassworDto.Phone).FirstOrDefaultAsync();
                if (user == null)
                    return false;
                user.ForgetEmailVerificationCode = ForgetCodeValuesHelper.GenerateRandomNumber();
                await _userManager.UpdateAsync(user);
                _SmsTwilio.SendSMS(user.ForgetEmailVerificationCode, "+12182768159", "+2" + forgetPassworDto.Phone);
                return true;
            }
           
        }

        

        public async Task<bool> ResetPassword(ForgetPassworDto forgetPassworDto)
        {
            if (forgetPassworDto.isEmail)
            {
                var user = await _userManager.FindByEmailAsync(forgetPassworDto.Email);
                if (user == null)
                    return false;
                if (forgetPassworDto.Code == user.ForgetEmailVerificationCode)
                {
                    return await ChagePasswrd(user.Id, forgetPassworDto.NewPassword);
                }
                return false;
            }
            else
            {
                
                var user = await _userManager.Users.Where(x => x.PhoneNumber == forgetPassworDto.Phone).FirstOrDefaultAsync();
                if (user == null)
                    return false;
                if (forgetPassworDto.Code == user.ForgetEmailVerificationCode)
                {
                    return await ChagePasswrd(user.Id, forgetPassworDto.NewPassword);
                }
                return false;
            }
             
        }

      

      

        public async Task<EnableAuthenticatorViewModel> EnableAuthenticator(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            user.TwoFactorEnabled = true;
            await _userManager.UpdateAsync(user);
            var model = new EnableAuthenticatorViewModel();
            if (user == null) return model;
            await LoadSharedKeyAndQrCodeUriAsync(user, model);
            return model;
        }


        private string FormatKey(string unformattedKey)
        {
            var result = new StringBuilder();
            int currentPosition = 0;
            while (currentPosition + 4 < unformattedKey.Length)
            {
                result.Append(unformattedKey.Substring(currentPosition, 4)).Append(" ");
                currentPosition += 4;
            }
            if (currentPosition < unformattedKey.Length)
            {
                result.Append(unformattedKey.Substring(currentPosition));
            }

            return result.ToString().ToLowerInvariant();
        }

        private string GenerateQrCodeUri(string email, string unformattedKey)
        {
            return string.Format(
                AuthenticatorUriFormat,
                _urlEncoder.Encode("TwoFactAuth"),
                _urlEncoder.Encode(email),
                unformattedKey);
        }

        private async Task LoadSharedKeyAndQrCodeUriAsync(ApplicationUser user, EnableAuthenticatorViewModel model)
        {
            var unformattedKey = await _userManager.GetAuthenticatorKeyAsync(user);
            if (string.IsNullOrEmpty(unformattedKey))
            {
                await _userManager.ResetAuthenticatorKeyAsync(user);
                unformattedKey = await _userManager.GetAuthenticatorKeyAsync(user);
            }

            model.SharedKey = FormatKey(unformattedKey);
            model.AuthenticatorUri = GenerateQrCodeUri(user.Email, unformattedKey);
        }


        public async Task<bool> IsValidAuthenticator(EnableAuthenticatorViewModel enableAuthenticatorViewModel)
        {
            var user = await _userManager.FindByEmailAsync(enableAuthenticatorViewModel.Email);
            if (user == null) return false;
            await LoadSharedKeyAndQrCodeUriAsync(user, enableAuthenticatorViewModel);
            // Strip spaces and hypens
            var verificationCode = enableAuthenticatorViewModel.Code.Replace(" ", string.Empty).Replace("-", string.Empty);

            var is2faTokenValid = await _userManager.VerifyTwoFactorTokenAsync(
                user, _userManager.Options.Tokens.AuthenticatorTokenProvider, verificationCode);

            return is2faTokenValid;
        }


       

    }
}
