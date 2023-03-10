using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tekus.WebAPI.Errors;

namespace Tekus.WebAPI.Controllers
{
    [Route("errors")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : BaseApiController
    { 
       public IActionResult Error(int code) 
        {
            return new ObjectResult(new CodeErrorResponse(code));
        }       
    }
}
