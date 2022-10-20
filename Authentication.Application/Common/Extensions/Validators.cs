using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Application.Common.Extensions
{
    public static class Validators
    {
        //field type = text box 
        //field content = Area
        public static IRuleBuilderOptions<T, string> IsValidTextBoxArea<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.MinimumLength(1).MaximumLength(8).WithMessage("'{PropertyName}' قد تخطى عدد الخانات اقصى عدد  7 خانة");
        }


        public static IRuleBuilderOptions<T, int> IsValidId<T>(this IRuleBuilder<T, int> ruleBuilder)
        {
            return ruleBuilder.NotEmpty().WithMessage(" هذا الحقل مطلوب").GreaterThan(0).WithMessage("هذا الحقل يتطلب ارقام موجبة");
        }

        //like any address in system
        //field type = text area 
        //field content = text
        public static IRuleBuilderOptions<T, string> IsValidTextAreaText<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.MinimumLength(1).MaximumLength(501).WithMessage("قد تخطى عدد الخانات اقصى عدد  500 خانة ");
        }

        //field type = text box 
        //field content = text
        public static IRuleBuilderOptions<T, string> IsValidTextBoxText<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.MinimumLength(1).MaximumLength(101).WithMessage("قد تخطى عدد الخانات اقصى عدد  100 خانة ");
        }
    }
}
