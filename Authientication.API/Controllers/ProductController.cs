using Authentication.API.Controllers;
using Authentication.Application.Features.ProductFeature.Queries;
using Authentication.Application.Features.ProductFeature.ViewModels.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Authientication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ApiBaseController
    {

        
        [HttpPost]
        [Route("get-page")]
        public async Task<IActionResult> GetPage([FromBody] ProductRequest _ProductRequest)
        {
            return Response(await mediator.Send(new GetProductPage() { ProductRequest = _ProductRequest }));
        }
    }
 }
