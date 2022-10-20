using  Authentication.Application.Common.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

namespace  Authentication.API.Controllers
{
    public class ApiBaseController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    
        protected IActionResult Response(ResponseVM response) 
        {
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK: return Ok(response); 
                case HttpStatusCode.BadRequest: return BadRequest(response); 
                default:return Ok(response);
            }
        }
    }
}
