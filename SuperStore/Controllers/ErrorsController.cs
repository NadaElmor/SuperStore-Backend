using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperStore.Errors;

namespace SuperStore.Controllers
{
    [Route("Errors/{code}")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi =true)]
    public class ErrorsController : ControllerBase
    {
        
        public ActionResult Error(int code)
        {
            return NotFound(new ApiResponse(404));
        }
    }
}
