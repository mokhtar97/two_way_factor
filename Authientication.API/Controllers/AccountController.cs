using Authenticaion.Domain.Entities;
using Authentication.Application.Common.Response;
using Authentication.Application.Features.AccountFeature.Interfaces;
using Authentication.Application.Features.AccountFeature.ViewModels;
using Authentication.Application.Features.AccountFeature.ViewModels.GoogleAuthenticatorModels;
using AutoMapper;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Web;

namespace Authenticaion.API.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IMailService _mailService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
      
        public AccountController(IAccountService accountService, UserManager<ApplicationUser> userManager, IMapper mapper, IMailService mailService)
        {
            _accountService = accountService;
            _userManager = userManager;
            _mapper = mapper;
            _mailService = mailService;
           
        }
       
        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login([FromHeader] UserLoginDto userLoginDto)
        {
            var tokenDto = _accountService.Login(userLoginDto).Result;
            if (tokenDto != null)
                return Ok(tokenDto);
            else
                return BadRequest(new ResponseVM { Error = "Username or Password Is Incorrect !!"});
        }

        [HttpPost]
        [Route("GetAll")]
        public async Task<IActionResult> GetAllUsers(PageVM pageVm)
        {
            var users = await _accountService.GetAllUsers(pageVm);
            return Ok(users);
        }

        [HttpPost]
        [Route("CreateUser")]

        public async Task<IActionResult> CreateUser(UserDto dto)
        {
            var result = await _accountService.CreateUserAsync(dto);
            if (result.Succeeded)
            {
                try
                {
                    var user = await _userManager.FindByEmailAsync(dto.email);
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var confirmationLink = Url.Action("ConfirmEmail", "Account", new { token = token, email = dto.email }, Request.Scheme);
                    bool emailResponse = await _mailService.SendConfirmationMailAsync(dto.email, confirmationLink);
                    
                    return Ok(new { result });
                }
                catch (Exception ex)
                {

                    throw;
                }
                
            }
            return BadRequest(new { result.Errors });

        }

        

      

        [HttpGet]
        [Route("GetUser/{id}")]
        public async Task<IActionResult> GetUserAsync(string id)
        {
            var user = await _accountService.GetUserById(id);
            return Ok(user);
        }

      

        [HttpPost("ChangePassword")]
        [AllowAnonymous]
        public IActionResult ChangePassword([FromBody] ChangePasswordDto changePasswordDto)
        {
            var result = _accountService.ChagePasswrd(changePasswordDto.UserId, changePasswordDto.NewPassword);
            if (!result.Result) return BadRequest(new { message = "حدث خطأ أثناء تغير كلمه المرور" });
            return Ok(new { message = "   تم تغير كلمه المرور بنجاح " });
        }


        [HttpPost("ConfirmEmail")]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail([FromBody]  ConfirmModel ConfirmModel)
        {
            var result= await _accountService.ConfirmEmail(ConfirmModel.Email, ConfirmModel.Code);
            return Ok(result);
        }

        [HttpPost("ConfirmPhone")]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmPhone([FromBody] ConfirmModel ConfirmModel)
        {
            var result = await _accountService.ConfirmPhone(ConfirmModel.Email, ConfirmModel.Code);
            return Ok(result);
        }

      

        [HttpPost("VerificationCode")]
        [AllowAnonymous]
        public IActionResult VerificationCode([FromBody] ForgetPassworDto forgetPassworDto)
        {
            var result = _accountService.SendVerificationCode(forgetPassworDto);
            if (!result.Result) return Ok(false);
            return Ok(true);
        }


        
        [HttpPost("ResetPassword")]
        [AllowAnonymous]
        public IActionResult ResetPassword([FromBody] ForgetPassworDto forgetPassworDto)
        {
            var result = _accountService.ResetPassword(forgetPassworDto);
            if (!result.Result) return Ok(false);
            return Ok(true);
        }

        ////google Authenticator
        ///EnableAuthenticator
        [HttpPost("EnableAuthenticator")]
        [AllowAnonymous]
        public async Task<EnableAuthenticatorViewModel> EnableAuthenticator([FromBody] EnableEmailModel email)
        {
            var result = _accountService.EnableAuthenticator(email.Email);
            return await result;
        }

        [HttpPost("IsValidAuthenticator")]
        [AllowAnonymous]
        public IActionResult IsValidAuthenticator([FromBody] EnableAuthenticatorViewModel enableAuthenticatorViewModel)
        {
            var result = _accountService.IsValidAuthenticator(enableAuthenticatorViewModel);
            if (!result.Result) return Ok(false);
            return Ok(true);
        }


       


    }
}
