using Authentication.Application.Common.Enums;
using Authentication.Application.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Application.Common.Response
{
    public class ResponseVM
    {
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
        public string OperationMessage => ((ResponseMessageStatusEnum)this.StatusCode).GetEnumDescription();
        public object Data { get; set; }
        public object Error { get; set; }
    }
}
