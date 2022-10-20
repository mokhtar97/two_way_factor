using FluentValidation;
using Authentication.Application.Features.AccountFeature.ViewModels;
using Authentication.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authenticaion.Domain.Entities;

namespace Authentication.Application.Features.AccountFeature.Validators
{
    //public class UserDtoValidator : AbstractValidator<UserDto>
    //{
    //    private readonly IHttpContextAccessor _context;
    //    private readonly UserManager<ApplicationUser> _userManager;
    //    private readonly IStringLocalizer<UserDtoValidator> _stringLocalizer;

    //    public UserDtoValidator(UserManager<ApplicationUser> userManager, IHttpContextAccessor context, IStringLocalizer<UserDtoValidator> stringLocalizer)
    //    {
    //        _userManager = userManager;
    //        _context = context;
    //        _stringLocalizer = stringLocalizer;

    //        RuleFor(x => x.email).NotEmpty().WithMessage("إسم المستخدم فارغ!");
    //        RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage(_stringLocalizer["PhoneIsEmpty"]);
    //        RuleFor(x => x.password).NotEmpty().WithMessage("كلمة المرور فارغة!");
    //       // RuleFor(x => x.email).Must(IsUserExist).WithMessage("خطأ في كلمة المرور أو إسم المستخدم");

    //    }

    //    private bool IsUserExist(string username)
    //    {
    //        if (!string.IsNullOrEmpty(username))
    //        {
    //            var user = _userManager.FindByNameAsync(username)?.Result;
    //            return user != null;
    //        }

    //        return false;
    //    }

   // }
}
