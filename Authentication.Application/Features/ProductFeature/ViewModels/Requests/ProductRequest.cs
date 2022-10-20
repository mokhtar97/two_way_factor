using Authentication.Application.Common.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Application.Features.ProductFeature.ViewModels.Requests
{
    public class ProductRequest:PagedRequest
    {
        public string Name { get; set; }
    }
}
