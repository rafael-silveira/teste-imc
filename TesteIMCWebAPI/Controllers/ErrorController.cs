using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace TesteIMCWebAPI.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [Route("/error")]
        public IActionResult Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();

            return BadRequest(context.Error.Message);
        }
    }
}
