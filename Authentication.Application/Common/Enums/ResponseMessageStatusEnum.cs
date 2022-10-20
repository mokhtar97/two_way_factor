using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Application.Common.Enums
{
    public enum ResponseMessageStatusEnum
    {
        [Description("تمت العملية بنجاح")]
        Ok = 200,
        [Description("لم يتم التحقق من البيانات")]
        BadRequest = 400,
        [Description("حدث خطأ عام")]
        InternalServerError = 500
    }
}
