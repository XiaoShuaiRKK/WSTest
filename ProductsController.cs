using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NuGet.Common;
using Session1WebApi.Data;
using Session1WebApi.Model;

namespace Session1WebApi.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext context;
        public ProductsController(AppDbContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public ActionResult Index()
        {
            var authHeader = Request.Headers["Authorization"].ToString();
            if(authHeader.IsNullOrEmpty() && !authHeader.StartsWith("Bearer "))
            {
                return Unauthorized(ErrorMessage.Unauthorized());
            }
            authHeader = authHeader.Substring("Bearer ".Length);
            if (!TokenTool.CheckToken(authHeader))
            {
                return Unauthorized(ErrorMessage.Unauthorized());
            }
            return Ok(context.Products.Include(p => p.Supplier).ToList());
        }

        [HttpPut]
        [Route("{id}/stock")]
        public IActionResult UpdateStock(int id, [FromQuery]int stock)
        {
            var authHeader = Request.Headers["Authorization"].ToString();
            if (String.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
            {
                return Unauthorized(ErrorMessage.Unauthorized());
            }
            authHeader = authHeader.Substring("Bearer ".Length);
            if (!TokenTool.CheckToken(authHeader))
            {
                return Unauthorized(ErrorMessage.Unauthorized());
            }


            var product = context.Products.Find(id);
            if(product == null)
            {
                return Unauthorized(ErrorMessage.Unauthorized());
            }
            product.Stock = stock;
            return Ok(context.Products.Update(product));
        }
    }
}
