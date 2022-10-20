using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Application.Features.AccountFeature.ViewModels
{
    public class JWTOptions
    {
        public string Secret { get; set; }
        public TimeSpan TokenLifeTime { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
