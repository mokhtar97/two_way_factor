﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Application.Features.AccountFeature.ViewModels
{
    public class ChangePasswordDto
    {
        public string UserId { get; set; }
       // public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
