using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Application.Features.AccountFeature.ViewModels
{
    public class UserDto
    {
        public string Id { get; set; }
        public string username { set; get; }
        public string DisplayName { set; get; }
        public string password { set; get; }
        public string role { set; get; }
        public string email { set; get; }
        public string PhoneNumber { set; get; }
   
        public bool EmailConfirmed { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
   

    }


    public class EditUserDto
    {
        public string Id { get; set; }
        public string username { set; get; }        
        public string email { set; get; }
        public string PhoneNumber { set; get; }
      
    }
   
}
