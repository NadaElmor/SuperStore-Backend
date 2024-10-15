using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperStore.Errors;
using SuperStore.Repositery;

namespace SuperStore.Controllers
{
  
    public class BuggyController : BaseController
    {
        private readonly SuperStoreDbContext _superStoreDbContext;

        public BuggyController(SuperStoreDbContext superStoreDbContext )
        {
            _superStoreDbContext = superStoreDbContext;
        }
        [HttpGet("NotFound")]
        public ActionResult GetNotFoundRequest()
        {
            var Product = _superStoreDbContext.Products.Find(1000);
            if (Product == null) { return NotFound(new ApiResponse(404)); }
            return Ok(Product);
        }
        [HttpGet("Server Error")]
        public ActionResult GetServerError()
        {
            var Product = _superStoreDbContext.Products.Find(100);
            var ProductToReturn = Product.ToString();
            //return null reference exception
            return Ok(ProductToReturn);
        }
        [HttpGet("BadRequest")]
        public ActionResult GetBadRequest()
        {
            return BadRequest();
        }
        [HttpGet("BadRequest/{id}")]
        public ActionResult GetBadRequest(int id)
        {
            return Ok();
        }
    }
}
